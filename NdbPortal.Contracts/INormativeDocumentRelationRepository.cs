using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentRelationRepository
    {
        Task<IEnumerable<NormativeDocumentRelation>> GetAllNormativeDocumentRelationsAsync();
        Task<NormativeDocumentRelation> GetNormativeDocumentRelationAsync(Guid id);
        void CreateNormativeDocumentRelation(NormativeDocumentRelation normativeDocumentRelation);
        void UpdateNormativeDocumentRelation(NormativeDocumentRelation normativeDocumentRelation);
        void DeleteNormativeDocumentRelation(NormativeDocumentRelation normativeDocumentRelation);
    }
}
