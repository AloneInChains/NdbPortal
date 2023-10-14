using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentRepository
    {
        Task<IEnumerable<NormativeDocument>> GetAllNormativeDocumentsAsync();
        Task<NormativeDocument?> GetNormativeDocumentAsync(Guid id);
        void CreateNormativeDocument(NormativeDocument document);
        void UpdateNormativeDocument(NormativeDocument document);
        void DeleteNormativeDocument(NormativeDocument document);
    }
}
