namespace NdbPortal.Entities.Models
{
    public class NormativeDocumentFile
    {
        public Guid Id { get; set; }
        public Guid NormativeDocumentId { get; set; }
        public string? FileName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedById { get; set; }
        public byte[] Data { get; set; } = null!;

        public virtual Employee CreatedBy { get; set; } = null!;
    }
}
