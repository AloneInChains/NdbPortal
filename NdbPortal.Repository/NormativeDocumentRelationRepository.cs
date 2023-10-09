using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdbPortal.Repository
{
    public class NormativeDocumentRelationRepository : RepositoryBase<NormativeDocumentRelation>, INormativeDocumentRelationRepository
    {
        public NormativeDocumentRelationRepository(NDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentRelation(NormativeDocumentRelation normativeDocumentRelation)
        {
            RepositoryContext.Add(normativeDocumentRelation);
        }

        public void DeleteNormativeDocumentRelation(NormativeDocumentRelation normativeDocumentRelation)
        {
            RepositoryContext.Remove(normativeDocumentRelation);
        }

        public async Task<IEnumerable<NormativeDocumentRelation>> GetAllNormativeDocumentRelationsAsync()
        {
            return await GetAll().OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<NormativeDocumentRelation> GetNormativeDocumentRelationAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentRelation(NormativeDocumentRelation normativeDocumentRelation)
        {
            RepositoryContext.Update(normativeDocumentRelation);
        }
    }
}
