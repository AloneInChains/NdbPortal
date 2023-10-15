namespace NdbPortal.Entities.Models
{
    public sealed class NormativeDocument
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

        public Company Company { get; set; } = null!;
        public NormativeDocumentConfidentialityLevel? ConfidentialityLevel { get; set; }
        public Employee CreatedBy { get; set; } = null!;
        public NormativeDocumentType? DocumentType { get; set; }
        public NormativeDocument? MainDocument { get; set; }
        public ICollection<NormativeDocument> InverseMainDocument { get; set; }
        public ICollection<NormativeDocumentRelation> NormativeDocumentRelationRelatedDocuments { get; set; }
        public ICollection<NormativeDocumentRelation> NormativeDocumentRelationRelationDocuments { get; set; }
        public ICollection<NormativeDocumentVisa> NormativeDocumentVisas { get; set; }
    }
}
