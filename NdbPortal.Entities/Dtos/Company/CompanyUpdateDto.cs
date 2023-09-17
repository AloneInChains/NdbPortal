using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.Company
{
    public class CompanyUpdateDto
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name length cannot be more than 50 characters")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(500, ErrorMessage = "Address length cannot be more than 50 characters")]
        [JsonPropertyName("address")]
        public string Address { get; set; } = null!;
    }
}
