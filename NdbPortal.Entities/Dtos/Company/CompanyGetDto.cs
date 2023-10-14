using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.Company
{
    public class CompanyGetDto
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
        [JsonPropertyName("address")]
        public string Address { get; set; } = null!;
    }
}
