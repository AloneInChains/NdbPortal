using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.NormativeDocumentVisa
{
    public class NormativeDocumentVisaGetDto
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("normativeDocumentId")]
        public Guid NormativeDocumentId { get; set; }

        [JsonPropertyName("approverId")]
        public Guid ApproverId { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("isApproved")]
        public bool IsApproved { get; set; }
    }
}
