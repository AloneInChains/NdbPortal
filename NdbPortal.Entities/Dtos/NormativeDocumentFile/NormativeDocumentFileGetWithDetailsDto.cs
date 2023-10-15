using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NdbPortal.Entities.Dtos.Employee;

namespace NdbPortal.Entities.Dtos.NormativeDocumentFile
{
    public class NormativeDocumentFileGetWithDetailsDto
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
        public EmployeeGetDto CreatedBy { get; set; } = new EmployeeGetDto();
        [JsonPropertyName("data")]
        public byte[] Data { get; set; } = null!;
    }
}
