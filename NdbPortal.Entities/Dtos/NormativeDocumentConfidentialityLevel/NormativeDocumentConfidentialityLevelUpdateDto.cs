using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdbPortal.Entities.Dtos.NormativeDocumentConfidentialityLevel
{
    public class NormativeDocumentConfidentialityLevelUpdateDto
    {
        [Key]
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int OrderNumber { get; set; }

    }
}
