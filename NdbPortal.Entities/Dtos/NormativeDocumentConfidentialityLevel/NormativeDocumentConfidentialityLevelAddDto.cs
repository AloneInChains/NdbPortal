using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NdbPortal.Entities.Dtos.NormativeDocumentConfidentialityLevel
{
    public class NormativeDocumentConfidentialityLevelAddDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int OrderNumber { get; set; }
    }
}
