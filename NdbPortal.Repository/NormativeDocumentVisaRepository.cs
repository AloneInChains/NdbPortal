using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class NormativeDocumentVisaRepository : RepositoryBase<NormativeDocumentVisa>, INormativeDocumentVisaRepository
    {
        public NormativeDocumentVisaRepository(NDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentVisa(NormativeDocumentVisa documentVisa)
        {
            RepositoryContext.Add(documentVisa);
        }

        public void DeleteNormativeDocumentVisa(NormativeDocumentVisa documentVisa)
        {
            RepositoryContext.Remove(documentVisa);
        }

        public async Task<IEnumerable<NormativeDocumentVisa>> GetAllNormativeDocumentVisasAsync()
        {
            return await GetAll().OrderBy(x => x.CreatedOn).ToListAsync();
        }

        public async Task<NormativeDocumentVisa?> GetNormativeDocumentVisaAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentVisa(NormativeDocumentVisa documentVisa)
        {
            RepositoryContext.Update(documentVisa);
        }
    }
}
