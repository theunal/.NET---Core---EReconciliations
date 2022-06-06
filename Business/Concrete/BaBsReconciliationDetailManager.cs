using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Excel;
using ExcelDataReader;
using System.Text;

namespace Business.Concrete
{
    public class BaBsReconciliationDetailManager : IBaBsReconciliationDetailService
    {
        private readonly IBaBsReconciliationDetailDal baBsReconciliationDetailDal;
        private readonly IBaBsReconciliationService baBsReconciliationService;
        public BaBsReconciliationDetailManager(IBaBsReconciliationDetailDal baBsReconciliationDetailDal,
            IBaBsReconciliationService baBsReconciliationService)
        {
            this.baBsReconciliationDetailDal = baBsReconciliationDetailDal;
            this.baBsReconciliationService = baBsReconciliationService;
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheAspect(30)]
        public IDataResult<List<BaBsReconciliationDetail>> GetAll(int babsReconciliationId)
        {
            return new SuccessDataResult<List<BaBsReconciliationDetail>>
                (baBsReconciliationDetailDal.GetAll(x => x.BaBsReconciliationId == babsReconciliationId),
                Messages.BaBsReconciliationDetailsHasBeenBrought);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheAspect(30)]
        public IDataResult<BaBsReconciliationDetail> GetById(int id)
        {
            var result = baBsReconciliationDetailDal.Get(x => x.Id == id);
            if (result is not null)
            {
                return new SuccessDataResult<BaBsReconciliationDetail>
                  (baBsReconciliationDetailDal.Get(x => x.Id == id),
                  Messages.BaBsReconciliationDetailHasBeenBrought);
            }
            return new ErrorDataResult<BaBsReconciliationDetail>(Messages.BaBsReconciliationDetailNotFound);
        }





        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]
        public IResult Add(BaBsReconciliationDetail entity) { 

            baBsReconciliationDetailDal.Add(entity);
            return new SuccessResult(Messages.BaBsReconciliationDetailAdded);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]
        public IResult Delete(BaBsReconciliationDetail entity)
        {
            baBsReconciliationDetailDal.Delete(entity);
            return new SuccessResult(Messages.BaBsReconciliationDetailDeleted);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]
        public IResult Update(BaBsReconciliationDetail entity)
        {
            baBsReconciliationDetailDal.Update(entity);
            return new SuccessResult(Messages.BaBsReconciliationDetailUpdated);
        }



        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBaBsReconciliationDetailService.Get")]
        [TransactionScopeAspect]
        public IResult AddByExcel(BaBsReconciliationDetailExcelDto dto)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(dto.FilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {

                        var date = reader.GetValue(0) != null ? reader.GetValue(0).ToString() : null;

                        if (date is null) break;
                        if (date == "Tarih") continue;

                        string description = reader.GetString(1);
                        decimal amount = Convert.ToDecimal(reader.GetValue(2));


                        if (date != "Tarih") // ilk satırı okumaması için böyle yaptım
                        {

                            BaBsReconciliationDetail baBsReconciliationDetail = new BaBsReconciliationDetail
                            {
                                BaBsReconciliationId = dto.BabsReconciliationId,
                                Date = Convert.ToDateTime(date),
                                Description = description,
                                Amount = amount
                            };

                            baBsReconciliationDetailDal.Add(baBsReconciliationDetail);
                        }
                    }
                }
            }
            File.Delete(dto.FilePath);
            return new SuccessResult(Messages.BaBsReconciliationDetailsAdded);
        }
    }
}
