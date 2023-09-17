using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.NormativeDocumentFile
{
    public class NormativeDocumentFileAddDto
    {
        [Key]
        [Required (ErrorMessage = "Id is required")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [Required (ErrorMessage = "Normative document is required")]
        [JsonPropertyName("normativeDocumentId")]
        public Guid NormativeDocumentId { get; set; }
        [JsonPropertyName("fileName")]
        public string? FileName { get; set; }
        [JsonPropertyName("createdOn")]
        [Required (ErrorMessage = "Created on is required")]
        public DateTime CreatedOn { get; set; }
        [JsonPropertyName("createdById")]
        [Required (ErrorMessage = "Created by is required")]
        public Guid CreatedById { get; set; }
        [JsonPropertyName("data")]
        [Required (ErrorMessage = "Data is required")]
        public byte[] Data { get; set; } = null!;
    }
}
