using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentTypeRepository
    {
        Task<IEnumerable<NormativeDocumentType>> GetAllNormativeDocumentTypesAsync();
        Task<NormativeDocumentType> GetNormativeDocumentTypeAsync(Guid id);
        void CreateNormativeDocumentType(NormativeDocumentType documentType);
        void UpdateNormativeDocumentType(NormativeDocumentType documentType);
        void DeleteNormativeDocumentType(NormativeDocumentType documentType);
    }
}
