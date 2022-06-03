using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        IDataResult<List<Company>> GetAll();
        IDataResult<Company> Get(Company company); // bunu değiştircem id ye göre getirme aşagıda var zaten


       

        IResult Delete(Company entity);




        
        IResult AddUserCompany(int userId, int companyId);
        IResult Add(Company entity); // aynı anda hem user hem companu ekleme için kullanılıyor
        IResult AddCompanyUser(CompanyDto dto); // mevcut usera company ekler
        IDataResult<Company> GetById(int id); // id ye göre company getirir

        IResult Update(Company entity); // company güncelleme
    }
}
