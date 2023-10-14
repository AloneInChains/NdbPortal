using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentRelationRepository
    {
        Task<IEnumerable<NormativeDocumentRelation>> GetAllNormativeDocumentRelationsAsync();
        Task<NormativeDocumentRelation?> GetNormativeDocumentRelationAsync(Guid id);
        void CreateNormativeDocumentRelation(NormativeDocumentRelation documentRelation);
        void UpdateNormativeDocumentRelation(NormativeDocumentRelation documentRelation);
        void DeleteNormativeDocumentRelation(NormativeDocumentRelation documentRelation);
    }
}
