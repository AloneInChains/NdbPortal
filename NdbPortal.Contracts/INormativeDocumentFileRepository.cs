using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentFileRepository
    {
        Task<IEnumerable<NormativeDocumentFile>> GetAllNormativeDocumentFilesAsync();
        Task<NormativeDocumentFile?> GetNormativeDocumentFileAsync(Guid id);
        void CreateNormativeDocumentFile(NormativeDocumentFile normativeDocumentFile);
        void UpdateNormativeDocumentFile(NormativeDocumentFile normativeDocumentFile);
        void DeleteNormativeDocumentFile(NormativeDocumentFile normativeDocumentFile);
    }
}
