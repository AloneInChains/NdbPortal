using System.ComponentModel.DataAnnotations;

namespace NdbPortal.Entities.Dtos.NormativeDocumentRelation
{
    public class NormativeDocumentRelationUpdateDto
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Relation document is required")]
        public Guid RelationDocumentId { get; set; }
        [Required(ErrorMessage = "Related document is required")]
        public Guid RelatedDocumentId { get; set; }
        [Required(ErrorMessage = "Relation type is required")]
        public Guid RelationTypeId { get; set; }
    }
}
