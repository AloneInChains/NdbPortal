using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(NDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateEmployee(Employee employee)
        {
            RepositoryContext.Add(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            RepositoryContext.Remove(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await GetAll().OrderBy(employee => employee.Name).ToListAsync();
        }

        public async Task<Employee?> GetEmployeeAsync(Guid id)
        {
            return await GetWithWhere(employee => employee.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public void UpdateEmployee(Employee employee)
        {
            RepositoryContext.Update(employee);
        }
    }
}
