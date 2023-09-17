using System.ComponentModel.DataAnnotations;

namespace NdbPortal.Entities.Dtos.NormativeDocumentRelation
{
    public class NormativeDocumentRelationGetDto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid RelationDocumentId { get; set; }
        public Guid RelatedDocumentId { get; set; }
        public Guid RelationTypeId { get; set; }
    }
}
