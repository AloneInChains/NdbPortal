using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.NormativeDocument
{
    public class NormativeDocumentGetDto
    {
        [Key]
        [JsonPropertyName("id")]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "Document number")]
        [JsonPropertyName("documentNumber")]
        public string DocumentNumber { get; set; } = null!;

        [Display(Name = "Document type")]
        [JsonPropertyName("documentTypeId")]
        public Guid? DocumentTypeId { get; set; }

        [Display(Name = "Created on")]
        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Created by")]
        [JsonPropertyName("createdById")]
        public Guid CreatedById { get; set; }

        [Display(Name = "Description")]
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [Display(Name = "Company")]
        [JsonPropertyName("companyId")]
        public Guid CompanyId { get; set; }

        [Display(Name = "Main document")]
        [JsonPropertyName("mainDocumentId")]
        public Guid? MainDocumentId { get; set; }

        [Display(Name = "Version number")]
        [JsonPropertyName("versionNumber")]
        public int? VersionNumber { get; set; }

        [Display(Name = "Confidentiality level")]
        [JsonPropertyName("confidentialityLevelId")]
        public Guid? ConfidentialityLevelId { get; set; }
    }
}
