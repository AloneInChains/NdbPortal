using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company> GetCompanyAsync(Guid id);
        void CreateCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
    }
}
