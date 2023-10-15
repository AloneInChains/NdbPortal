using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentFileRepository
    {
        Task<IEnumerable<NormativeDocumentFile>> GetAllNormativeDocumentFilesAsync();
        Task<NormativeDocumentFile?> GetNormativeDocumentFileAsync(Guid id);
        void CreateNormativeDocumentFile(NormativeDocumentFile documentFile);
        void UpdateNormativeDocumentFile(NormativeDocumentFile documentFile);
        void DeleteNormativeDocumentFile(NormativeDocumentFile documentFile);
    }
}
