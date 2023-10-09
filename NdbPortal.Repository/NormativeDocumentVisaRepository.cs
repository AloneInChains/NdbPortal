using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class NormativeDocumentVisaRepository : RepositoryBase<NormativeDocumentVisa>, INormativeDocumentVisaRepository
    {
        public NormativeDocumentVisaRepository(NDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentVisa(NormativeDocumentVisa normativeDocumentVisa)
        {
            RepositoryContext.Add(normativeDocumentVisa);
        }

        public void DeleteNormativeDocumentVisa(NormativeDocumentVisa normativeDocumentVisa)
        {
            RepositoryContext.Remove(normativeDocumentVisa);
        }

        public async Task<IEnumerable<NormativeDocumentVisa>> GetAllNormativeDocumentVisasAsync()
        {
            return await GetAll().OrderBy(x => x.CreatedOn).ToListAsync();
        }

        public async Task<NormativeDocumentVisa> GetNormativeDocumentVisaAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentVisa(NormativeDocumentVisa normativeDocumentVisa)
        {
            RepositoryContext.Update(normativeDocumentVisa);
        }
    }
}
