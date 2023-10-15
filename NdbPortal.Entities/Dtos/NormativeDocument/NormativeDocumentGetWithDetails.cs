using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NdbPortal.Entities.Dtos.Company;
using NdbPortal.Entities.Dtos.Employee;
using NdbPortal.Entities.Dtos.NormativeDocumentConfidentialityLevel;
using NdbPortal.Entities.Dtos.NormativeDocumentType;

namespace NdbPortal.Entities.Dtos.NormativeDocument
{
    public class NormativeDocumentGetWithDetailsDto
    {
        [Key]
        [Display(Name = "Id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Display(Name = "Document number")]
        [JsonPropertyName("documentNumber")]
        public string DocumentNumber { get; set; } = null!;

        [Display(Name = "Document type id")]
        [JsonPropertyName("documentTypeId")]
        public Guid DocumentTypeId { get; set; }

        [Display(Name = "Document type")]
        [JsonPropertyName("documentType")]
        public NormativeDocumentTypeGetDto? DocumentType { get; set; }

        [Display(Name = "Created on")]
        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Created by id")]
        [JsonPropertyName("createdById")]
        public Guid CreatedById { get; set; }

        [Display(Name = "Created by")]
        [JsonPropertyName("createdBy")]
        public EmployeeGetDto? CreatedBy { get; set; }

        [Display(Name = "Description")]
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [Display(Name = "Company")]
        [JsonPropertyName("companyId")]
        public Guid CompanyId { get; set; }

        [Display(Name = "Company")]
        [JsonPropertyName("company")]
        public CompanyGetDto? Company { get; set; }

        [Display(Name = "Main document id")]
        [JsonPropertyName("mainDocumentId")]
        public Guid? MainDocumentId { get; set; }

        [Display(Name = "Main document")]
        [JsonPropertyName("mainDocument")]
        public NormativeDocumentGetWithDetailsDto? MainDocument { get; set; }

        [Display(Name = "Version number")]
        [JsonPropertyName("versionNumber")]
        public int? VersionNumber { get; set; }

        [Display(Name = "Confidentiality level")]
        [JsonPropertyName("confidentialityLevelId")]
        public Guid? ConfidentialityLevelId { get; set; }

        [Display(Name = "Confidentiality level")]
        [JsonPropertyName("confidentialityLevel")]
        public NormativeDocumentConfidentialityLevelGetDto? ConfidentialityLevel { get; set; }
    }
}
