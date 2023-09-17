using NdbPortal.Entities.Models;

namespace NdbPortal.Contracts
{
    public interface INormativeDocumentVisaRepository
    {
        Task<IEnumerable<NormativeDocumentVisa>> GetAllNormativeDocumentVisasAsync();
        Task<NormativeDocumentVisa> GetNormativeDocumentVisaAsync(Guid id);
        void CreateNormativeDocumentVisa(NormativeDocumentVisa normativeDocumentVisa);
        void UpdateNormativeDocumentVisa(NormativeDocumentVisa normativeDocumentVisa);
        void DeleteNormativeDocumentVisa(NormativeDocumentVisa normativeDocumentVisa);
    }
}
