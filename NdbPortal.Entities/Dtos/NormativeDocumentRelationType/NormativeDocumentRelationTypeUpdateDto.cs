using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.NormativeDocumentRelationType
{
    public class NormativeDocumentRelationTypeUpdateDto
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Reverse name is required")]
        [JsonPropertyName("reverseName")]
        public string ReverseName { get; set; } = null!;
    }
}
