using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class NormativeDocumentConfidentialityLevelRepository : RepositoryBase<NormativeDocumentConfidentialityLevel>, INormativeDocumentConfidentialityLevelRepository
    {
        public NormativeDocumentConfidentialityLevelRepository(NDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentConfidentialityLevel(NormativeDocumentConfidentialityLevel confLevel)
        {
            RepositoryContext.Add(confLevel);
        }

        public void DeleteNormativeDocumentConfidentialityLevel(NormativeDocumentConfidentialityLevel confLevel)
        {
            RepositoryContext.Remove(confLevel);
        }

        public async Task<IEnumerable<NormativeDocumentConfidentialityLevel>> GetAllNormativeDocumentConfidentialityLevelsAsync()
        {
            return await GetAll().OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<NormativeDocumentConfidentialityLevel?> GetNormativeDocumentConfidentialityLevelAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentConfidentialityLevel(NormativeDocumentConfidentialityLevel confLevel)
        {
            RepositoryContext.Update(confLevel);
        }
    }
}
