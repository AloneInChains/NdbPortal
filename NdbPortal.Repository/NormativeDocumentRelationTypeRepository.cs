using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdbPortal.Repository
{
    internal class NormativeDocumentRelationTypeRepository : RepositoryBase<NormativeDocumentRelationType>, INormativeDocumentRelationTypeRepository
    {
        public NormativeDocumentRelationTypeRepository(NDBContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentRelationType(NormativeDocumentRelationType relationType)
        {
            RepositoryContext.Add(relationType);
        }

        public void DeleteNormativeDocumentRelationType(NormativeDocumentRelationType relationType)
        {
            RepositoryContext.Remove(relationType);
        }

        public async Task<IEnumerable<NormativeDocumentRelationType>> GetAllNormativeDocumentRelationTypesAsync()
        {
            return await GetAll().OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<NormativeDocumentRelationType> GetNormativeDocumentRelationTypeAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentRelationType(NormativeDocumentRelationType relationType)
        {
            RepositoryContext.Update(relationType);
        }
    }
}
