namespace NdbPortal.Entities.Models
{
    public sealed class NormativeDocumentType
    {
        public NormativeDocumentType()
        {
            NormativeDocuments = new HashSet<NormativeDocument>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<NormativeDocument> NormativeDocuments { get; set; }
    }
}
