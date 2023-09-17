using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class NormativeDocumentTypeRepository : RepositoryBase<NormativeDocumentType>, INormativeDocumentTypeRepository
    {
        public NormativeDocumentTypeRepository(NDBContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentType(NormativeDocumentType normativeDocumentType)
        {
            RepositoryContext.Add(normativeDocumentType);
        }

        public void DeleteNormativeDocumentType(NormativeDocumentType normativeDocumentType)
        {
            RepositoryContext.Remove(normativeDocumentType);
        }

        public async Task<IEnumerable<NormativeDocumentType>> GetAllNormativeDocumentTypesAsync()
        {
            return await GetAll().OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<NormativeDocumentType> GetNormativeDocumentTypeAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentType(NormativeDocumentType normativeDocumentType)
        {
            RepositoryContext.Update(normativeDocumentType);
        }
    }
}
