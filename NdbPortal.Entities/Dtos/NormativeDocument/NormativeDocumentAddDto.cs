using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.NormativeDocument
{
    public class NormativeDocumentAddDto
    {
        [Key]
        [Required (ErrorMessage = "Id is required")]
        [Display(Name = "Id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Display(Name = "Document number")]
        [Required (ErrorMessage = "Document number is required")]
        [StringLength(50, ErrorMessage = "Document neuber length cannot be more than 50 characters")]
        [JsonPropertyName("documentNumber")]
        public string DocumentNumber { get; set; } = null!;

        [Display(Name = "Type")]
        [JsonPropertyName("documentTypeId")]
        public Guid? DocumentTypeId { get; set; }

        [Display(Name = "Created on")]
        [Required (ErrorMessage = "Created on is required")]
        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Created by")]
        [Required(ErrorMessage = "Created by is required")]
        [JsonPropertyName("createdById")]
        public Guid CreatedById { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description on is required")]
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [Display(Name = "Company")]
        [Required(ErrorMessage = "Company is required")]
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
