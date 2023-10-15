using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentConfidentialityLevelRepository
    {
        Task<IEnumerable<NormativeDocumentConfidentialityLevel>> GetAllNormativeDocumentConfidentialityLevelsAsync();
        Task<NormativeDocumentConfidentialityLevel?> GetNormativeDocumentConfidentialityLevelAsync(Guid id);
        void CreateNormativeDocumentConfidentialityLevel(NormativeDocumentConfidentialityLevel confLevel);
        void UpdateNormativeDocumentConfidentialityLevel(NormativeDocumentConfidentialityLevel confLevel);
        void DeleteNormativeDocumentConfidentialityLevel(NormativeDocumentConfidentialityLevel confLevel);
    }
}
