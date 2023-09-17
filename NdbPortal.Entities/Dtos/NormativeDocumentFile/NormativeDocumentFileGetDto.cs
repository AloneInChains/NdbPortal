using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.NormativeDocumentFile
{
    public class NormativeDocumentFileGetDto
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("normativeDocumentId")]
        public Guid NormativeDocumentId { get; set; }
        [JsonPropertyName("fileName")]
        public string? FileName { get; set; }
        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }
        [JsonPropertyName("createdBy")]
        public Guid CreatedById { get; set; }
        [JsonPropertyName("data")]
        public byte[] Data { get; set; } = null!;
    }
}
