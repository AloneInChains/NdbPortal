using Microsoft.AspNetCore.Mvc.Rendering;
using NdbPortal.Entities.Dtos.NormativeDocument;
using NdbPortal.Entities.Dtos.NormativeDocumentFile;
using NdbPortal.Entities.Dtos.NormativeDocumentRelation;
using NdbPortal.Entities.Dtos.NormativeDocumentVisa;

namespace NdbPortal.Web.Models
{
    public class NormativeDocumentViewModel
    {
        public NormativeDocumentGetWithDetailsDto? NormativeDocument { get; set; } = new NormativeDocumentGetWithDetailsDto();
        public SelectList? NormativeDocumentTypeList { get; set; } = null;
        public SelectList? NormativeDocumentConfLevelList { get; set; } = null;
        public IEnumerable<NormativeDocumentRelationGetWithDetailsDto> RelatedDocuments { get; set; } = new List<NormativeDocumentRelationGetWithDetailsDto>();
        public IEnumerable<NormativeDocumentVisaGetWithDetailsDto> Approvals { get; set; } = new List<NormativeDocumentVisaGetWithDetailsDto>();
        public IEnumerable<NormativeDocumentFileGetWithDetailsDto> Files { get; set; } = new List<NormativeDocumentFileGetWithDetailsDto>();
        public IEnumerable<NormativeDocumentGetWithDetailsDto> Versions { get; set; } = new List<NormativeDocumentGetWithDetailsDto>();
        public SelectList? AvailableRelatedDocumentList { get; set; } = null;
        public SelectList? AvailableRelationTypeList { get; set; } = null;

    }
}
