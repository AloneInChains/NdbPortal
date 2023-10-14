using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;

namespace NdbPortal.Repository
{
    public class NormativeDocumentRelationRepository : RepositoryBase<NormativeDocumentRelation>, INormativeDocumentRelationRepository
    {
        public NormativeDocumentRelationRepository(NDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentRelation(NormativeDocumentRelation documentRelation)
        {
            RepositoryContext.Add(documentRelation);
        }

        public void DeleteNormativeDocumentRelation(NormativeDocumentRelation documentRelation)
        {
            RepositoryContext.Remove(documentRelation);
        }

        public async Task<IEnumerable<NormativeDocumentRelation>> GetAllNormativeDocumentRelationsAsync()
        {
            return await GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<NormativeDocumentRelation?> GetNormativeDocumentRelationAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentRelation(NormativeDocumentRelation documentRelation)
        {
            RepositoryContext.Update(documentRelation);
        }
    }
}
