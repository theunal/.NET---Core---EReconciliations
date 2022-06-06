﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAccountReconciliationService
    {
        IDataResult<List<AccountReconciliation>> GetAll(int companyId);
        IDataResult<List<AccountReconciliationDto>> GetAllDto(int companyId);
        IDataResult<AccountReconciliation> GetById(int id);
        IDataResult<AccountReconciliation> GetByCode(string code);

        IResult Add(AccountReconciliation entity);
        IResult Update(AccountReconciliation entity);
        IResult Delete(AccountReconciliation entity);


        
        IResult AddByExcel(AccountReconciliationExcelDto dto);

        IResult SendReconciliationMail(AccountReconciliationDto entity);
    }
}
