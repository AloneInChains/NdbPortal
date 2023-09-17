using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentRepository
    {
        Task<IEnumerable<NormativeDocument>> GetAllNormativeDocumentsAsync();
        Task<NormativeDocument> GetNormativeDocumentAsync(Guid id);
        void CreateNormativeDocument(NormativeDocument normativeDocument);
        void UpdateNormativeDocument(NormativeDocument normativeDocument);
        void DeleteNormativeDocument(NormativeDocument normativeDocument);
    }
}
