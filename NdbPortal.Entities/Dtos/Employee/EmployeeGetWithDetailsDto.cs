using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NdbPortal.Entities.Dtos.Company;
using NdbPortal.Entities.Dtos.NormativeDocumentConfidentialityLevel;

namespace NdbPortal.Entities.Dtos.Employee
{
    public class EmployeeGetWithDetailsDto
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("surname")]
        public string Surname { get; set; } = null!;

        [JsonPropertyName("company")]
        public CompanyGetDto? Company { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;

        [JsonPropertyName("confidentialityLevel")]
        public NormativeDocumentConfidentialityLevelGetDto? ConfidentialityLevel { get; set; }
    }
}
