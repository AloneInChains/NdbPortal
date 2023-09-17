using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.Employee
{
    public class EmployeeGetDto
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("surname")]
        public string Surname { get; set; } = null!;

        [JsonPropertyName("companyId")]
        public Guid CompanyId { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;

        [JsonPropertyName("confidentialityLevelId")]
        public Guid? ConfidentialityLevelId { get; set; }
    }
}
