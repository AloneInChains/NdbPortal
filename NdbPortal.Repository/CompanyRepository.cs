using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(NDBContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCompany(Company company)
        {
            RepositoryContext.Add(company);
        }

        public void DeleteCompany(Company company)
        {
            RepositoryContext.Remove(company);
        }

        public void UpdateCompany(Company company)
        {
            RepositoryContext.Entry(company).State = EntityState.Modified;
            RepositoryContext.Update(company);
        }

        public async Task <Company> GetCompanyAsync(Guid id)
        {
            return await GetWithWhere(company => company.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await GetAll().OrderBy(company => company.Name).ToListAsync();
                
        }
    }
}
