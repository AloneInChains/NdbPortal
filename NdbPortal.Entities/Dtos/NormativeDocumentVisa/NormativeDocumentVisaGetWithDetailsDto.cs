using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NdbPortal.Entities.Dtos.Employee;
using NdbPortal.Entities.Dtos.NormativeDocument;

namespace NdbPortal.Entities.Dtos.NormativeDocumentVisa
{
    public class NormativeDocumentVisaGetWithDetailsDto
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("normativeDocument")]
        public NormativeDocumentGetDto NormativeDocument { get; set; } = default!;

        [JsonPropertyName("approver")]
        public EmployeeGetDto Approver { get; set; } = default!;

        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("isApproved")]
        public bool IsApproved { get; set; }
    }
}
