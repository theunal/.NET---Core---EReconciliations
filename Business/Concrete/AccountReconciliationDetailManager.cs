using Business.Abstract;
using Business.Const;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using ExcelDataReader;
using System.Text;

namespace Business.Concrete
{
    public class AccountReconciliationDetailManager : IAccountReconciliationDetailService
    {
        private readonly IAccountReconciliationDetailDal accountReconciliationDetailDal;
        public AccountReconciliationDetailManager(IAccountReconciliationDetailDal accountReconciliationDetailDal)
        {
            this.accountReconciliationDetailDal = accountReconciliationDetailDal;
        }


        public IDataResult<List<AccountReconciliationDetail>> GetAll(int accountReconciliationId)
        {
            return new SuccessDataResult<List<AccountReconciliationDetail>>
                (accountReconciliationDetailDal.GetAll(x => x.AccountReconciliationId == accountReconciliationId),
                Messages.AccountReconciliationDetailsHasBeenBrought);
        }

        public IDataResult<AccountReconciliationDetail> GetById(int id)
        {
            return new SuccessDataResult<AccountReconciliationDetail>
                    (accountReconciliationDetailDal.Get(x => x.Id == id), Messages.AccountReconciliationDetailHasBeenBrought);
        }






        public IResult Add(AccountReconciliationDetail entity)
        {
            accountReconciliationDetailDal.Add(entity);
            return new SuccessResult(Messages.AccountReconciliationDetailAdded);
        }

        public IResult Delete(AccountReconciliationDetail entity)
        {
            accountReconciliationDetailDal.Delete(entity);
            return new SuccessResult(Messages.AccountReconciliationDetailDeleted);
        }

        public IResult Update(AccountReconciliationDetail entity)
        {
            accountReconciliationDetailDal.Update(entity);
            return new SuccessResult(Messages.AccountReconciliationDetailUpdated);
        }





        public IResult AddByExcel(AccountReconciliationDetailDto dto)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(dto.filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {

                        String code = reader.GetValue(0) != null ? reader.GetValue(0).ToString() : null;

                        if (code is null) break;
                        if (code == "Cari Kodu") continue;

                        DateTime startingDate = Convert.ToDateTime(reader.GetValue(1));
                        DateTime endingDate = Convert.ToDateTime(reader.GetValue(2));
                        int currencyId = Convert.ToInt32(reader.GetValue(3));
                        decimal debit = Convert.ToDecimal(reader.GetValue(4));
                        decimal credit = Convert.ToDecimal(reader.GetValue(5));


                        if (code != "Cari Kodu") // ilk satırı okumaması için böyle yaptım
                        {
                            var currencyAccountId = currencyAccountService.GetByCompanyIdAndCode(code, dto.CompanyId).Data.Id;
                            var x = currencyAccountId;
                            AccountReconciliation accountReconciliation = new AccountReconciliation
                            {
                                CompanyId = dto.CompanyId,
                                CurrencyAccountId = currencyAccountId,
                                CurrencyCredit = credit,
                                CurrencyDebit = debit,
                                CurrencyId = currencyId, // currency id TL USD
                                StartingDate = startingDate,
                                EndingDate = endingDate,
                                IsSendEmail = true
                            };

                            accountReconciliationDetailDal.Add(accountReconciliation);
                        }
                    }
                }
            }
            return new SuccessResult(Messages.AccountReconciliationsAdded);
        }

    }
}
