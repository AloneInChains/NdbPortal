using System;
using System.Collections.Generic;

namespace NdbPortal.Entities.Models
{
    public partial class NormativeDocument
    {
        public NormativeDocument()
        {
            InverseMainDocument = new HashSet<NormativeDocument>();
            NormativeDocumentRelationRelatedDocuments = new HashSet<NormativeDocumentRelation>();
            NormativeDocumentRelationRelationDocuments = new HashSet<NormativeDocumentRelation>();
            NormativeDocumentVisas = new HashSet<NormativeDocumentVisa>();
        }

        public Guid Id { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public Guid? DocumentTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedById { get; set; }
        public string Description { get; set; } = null!;
        public Guid CompanyId { get; set; }
        public Guid? MainDocumentId { get; set; }
        public int? VersionNumber { get; set; }
        public Guid? ConfidentialityLevelId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual NormativeDocumentConfidentialityLevel? ConfidentialityLevel { get; set; }
        public virtual Employee CreatedBy { get; set; } = null!;
        public virtual NormativeDocumentType? DocumentType { get; set; }
        public virtual NormativeDocument? MainDocument { get; set; }
        public virtual ICollection<NormativeDocument> InverseMainDocument { get; set; }
        public virtual ICollection<NormativeDocumentRelation> NormativeDocumentRelationRelatedDocuments { get; set; }
        public virtual ICollection<NormativeDocumentRelation> NormativeDocumentRelationRelationDocuments { get; set; }
        public virtual ICollection<NormativeDocumentVisa> NormativeDocumentVisas { get; set; }
    }
}
