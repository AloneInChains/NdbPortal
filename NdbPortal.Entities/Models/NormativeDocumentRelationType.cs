namespace NdbPortal.Entities.Models
{
    public sealed class NormativeDocumentRelationType
    {
        public NormativeDocumentRelationType()
        {
            NormativeDocumentRelations = new HashSet<NormativeDocumentRelation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ReverseName { get; set; } = null!;

        public ICollection<NormativeDocumentRelation> NormativeDocumentRelations { get; set; }
    }
}
