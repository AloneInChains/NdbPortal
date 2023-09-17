using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentTypeRepository
    {
        Task<IEnumerable<NormativeDocumentType>> GetAllNormativeDocumentTypesAsync();
        Task<NormativeDocumentType> GetNormativeDocumentTypeAsync(Guid id);
        void CreateNormativeDocumentType(NormativeDocumentType normativeDocumentType);
        void UpdateNormativeDocumentType(NormativeDocumentType normativeDocumentType);
        void DeleteNormativeDocumentType(NormativeDocumentType normativeDocumentType);
    }
}
