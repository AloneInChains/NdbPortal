using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class NormativeDocumentRepository : RepositoryBase<NormativeDocument>, INormativeDocumentRepository
    {
        public NormativeDocumentRepository(NDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocument(NormativeDocument document)
        {
            RepositoryContext.Add(document);
        }

        public void DeleteNormativeDocument(NormativeDocument document)
        {
            RepositoryContext.Remove(document);
        }

        public async Task<IEnumerable<NormativeDocument>> GetAllNormativeDocumentsAsync()
        {
            return await GetAll().OrderBy(x => x.CreatedOn).ToListAsync();
        }

        public async Task<NormativeDocument?> GetNormativeDocumentAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocument(NormativeDocument document)
        {
            RepositoryContext.Update(document);
        }
    }
}
