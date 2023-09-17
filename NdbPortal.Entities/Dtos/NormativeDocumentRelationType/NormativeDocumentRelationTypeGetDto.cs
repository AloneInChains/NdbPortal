using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.NormativeDocumentRelationType
{
    [Serializable]
    public class NormativeDocumentRelationTypeGetDto
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("reverseName")]
        public string ReverseName { get; set; } = null!;
    }
}
