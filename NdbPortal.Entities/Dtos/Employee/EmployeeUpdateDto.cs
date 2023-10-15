using System.ComponentModel.DataAnnotations;

namespace NdbPortal.Entities.Dtos.Employee
{
    public  class EmployeeUpdateDto
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name length cannot be more than 50 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Surname length cannot be more than 50 characters")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Company is required")]
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Email length cannot be more than 50 characters")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Password length cannot be more than 50 characters")]
        public string Password { get; set; } = null!;
        public Guid? ConfidentialityLevelId { get; set; }
    }
}
