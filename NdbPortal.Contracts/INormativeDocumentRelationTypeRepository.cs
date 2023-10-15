using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentRelationTypeRepository
    {
        Task<IEnumerable<NormativeDocumentRelationType>> GetAllNormativeDocumentRelationTypesAsync();
        Task<NormativeDocumentRelationType?> GetNormativeDocumentRelationTypeAsync(Guid id);
        void CreateNormativeDocumentRelationType(NormativeDocumentRelationType relationType);
        void UpdateNormativeDocumentRelationType(NormativeDocumentRelationType relationType);
        void DeleteNormativeDocumentRelationType(NormativeDocumentRelationType relationType);
    }
}
