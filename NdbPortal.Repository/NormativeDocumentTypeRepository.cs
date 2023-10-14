using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class NormativeDocumentTypeRepository : RepositoryBase<NormativeDocumentType>, INormativeDocumentTypeRepository
    {
        public NormativeDocumentTypeRepository(NDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentType(NormativeDocumentType documentType)
        {
            RepositoryContext.Add(documentType);
        }

        public void DeleteNormativeDocumentType(NormativeDocumentType documentType)
        {
            RepositoryContext.Remove(documentType);
        }

        public async Task<IEnumerable<NormativeDocumentType>> GetAllNormativeDocumentTypesAsync()
        {
            return await GetAll().OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<NormativeDocumentType?> GetNormativeDocumentTypeAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentType(NormativeDocumentType documentType)
        {
            RepositoryContext.Update(documentType);
        }
    }
}
