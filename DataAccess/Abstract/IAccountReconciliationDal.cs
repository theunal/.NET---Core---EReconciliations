using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IAccountReconciliationDal : IEntityRepository<AccountReconciliation>
    {
        List<AccountReconciliationDto> GetAllDto(int companyId);
    }
}
