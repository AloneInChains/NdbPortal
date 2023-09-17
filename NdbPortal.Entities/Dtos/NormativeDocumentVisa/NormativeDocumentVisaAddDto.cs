using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.NormativeDocumentVisa
{
    public class NormativeDocumentVisaAddDto
    {
        [Key]
        [JsonPropertyName("id")]
        [Required (ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [JsonPropertyName("normativeDocumentId")]
        [Required (ErrorMessage = "Normative document is required")]
        public Guid NormativeDocumentId { get; set; }

        [JsonPropertyName("approverId")]
        [Required (ErrorMessage = "Approver is required")]
        public Guid ApproverId { get; set; }

        [JsonPropertyName("createdOn")]
        [Required (ErrorMessage = "Created on is required")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("isApproved")]
        public bool IsApproved { get; set; }
    }
}
