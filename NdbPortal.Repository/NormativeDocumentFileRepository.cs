using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdbPortal.Repository
{
    public class NormativeDocumentFileRepository : RepositoryBase<NormativeDocumentFile>, INormativeDocumentFileRepository
    {
        public NormativeDocumentFileRepository(NDBContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNormativeDocumentFile(NormativeDocumentFile normativeDocumentFile)
        {
            RepositoryContext.NormativeDocumentFiles.Add(normativeDocumentFile);
        }

        public void DeleteNormativeDocumentFile(NormativeDocumentFile normativeDocumentFile)
        {
            RepositoryContext.NormativeDocumentFiles.Remove(normativeDocumentFile);
        }

        public async Task<IEnumerable<NormativeDocumentFile>> GetAllNormativeDocumentFilesAsync()
        {
            return await GetAll().OrderBy(x => x.CreatedOn).ToListAsync();
        }

        public async Task<NormativeDocumentFile> GetNormativeDocumentFileAsync(Guid id)
        {
            return await GetWithWhere(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateNormativeDocumentFile(NormativeDocumentFile normativeDocumentFile)
        {
            RepositoryContext.Update(normativeDocumentFile);
        }
    }
}
