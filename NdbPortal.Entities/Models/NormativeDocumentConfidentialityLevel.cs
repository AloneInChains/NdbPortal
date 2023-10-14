namespace NdbPortal.Entities.Models
{
    public sealed class NormativeDocumentConfidentialityLevel
    {
        public NormativeDocumentConfidentialityLevel()
        {
            Employees = new HashSet<Employee>();
            NormativeDocuments = new HashSet<NormativeDocument>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int OrderNumber { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<NormativeDocument> NormativeDocuments { get; set; }
    }
}
