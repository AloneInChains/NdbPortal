using System.ComponentModel.DataAnnotations;

namespace NdbPortal.Entities.Dtos.NormativeDocumentType
{
    public class NormativeDocumentTypeUpdateDto
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
