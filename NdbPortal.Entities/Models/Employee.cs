namespace NdbPortal.Entities.Models
{
    public sealed class Employee
    {
        public Employee()
        {
            NormativeDocumentFiles = new HashSet<NormativeDocumentFile>();
            NormativeDocumentVisas = new HashSet<NormativeDocumentVisa>();
            NormativeDocuments = new HashSet<NormativeDocument>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public Guid CompanyId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid? ConfidentialityLevelId { get; set; }

        public Company Company { get; set; } = null!;
        public NormativeDocumentConfidentialityLevel? ConfidentialityLevel { get; set; }
        public ICollection<NormativeDocumentFile> NormativeDocumentFiles { get; set; }
        public ICollection<NormativeDocumentVisa> NormativeDocumentVisas { get; set; }
        public ICollection<NormativeDocument> NormativeDocuments { get; set; }
    }
}
