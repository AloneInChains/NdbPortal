using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentVisaRepository
    {
        Task<IEnumerable<NormativeDocumentVisa>> GetAllNormativeDocumentVisasAsync();
        Task<NormativeDocumentVisa?> GetNormativeDocumentVisaAsync(Guid id);
        void CreateNormativeDocumentVisa(NormativeDocumentVisa documentVisa);
        void UpdateNormativeDocumentVisa(NormativeDocumentVisa documentVisa);
        void DeleteNormativeDocumentVisa(NormativeDocumentVisa documentVisa);
    }
}
