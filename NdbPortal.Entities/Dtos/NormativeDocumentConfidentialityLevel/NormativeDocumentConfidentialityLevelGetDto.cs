using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.NormativeDocumentConfidentialityLevel
{
    [Serializable]
    public class NormativeDocumentConfidentialityLevelGetDto
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("orderNumber")]
        public int OrderNumber { get; set; }

    }
}
