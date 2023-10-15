using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class NormativeDocumentFileRepository : RepositoryBase<NormativeDocumentFile>, INormativeDocumentFileRepository
    {
        public NormativeDocumentFileRepository(NDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentFile(NormativeDocumentFile documentFile)
        {
            RepositoryContext.NormativeDocumentFiles.Add(documentFile);
        }

        public void DeleteNormativeDocumentFile(NormativeDocumentFile documentFile)
        {
            RepositoryContext.NormativeDocumentFiles.Remove(documentFile);
        }

        public async Task<IEnumerable<NormativeDocumentFile>> GetAllNormativeDocumentFilesAsync()
        {
            return await GetAll().OrderBy(x => x.CreatedOn).ToListAsync();
        }

        public async Task<NormativeDocumentFile?> GetNormativeDocumentFileAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentFile(NormativeDocumentFile documentFile)
        {
            RepositoryContext.Update(documentFile);
        }
    }
}
