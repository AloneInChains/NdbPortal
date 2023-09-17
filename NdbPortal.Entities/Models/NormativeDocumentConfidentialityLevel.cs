using System;
using System.Collections.Generic;

namespace NdbPortal.Entities.Models
{
    public partial class NormativeDocumentConfidentialityLevel
    {
        public NormativeDocumentConfidentialityLevel()
        {
            Employees = new HashSet<Employee>();
            NormativeDocuments = new HashSet<NormativeDocument>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<NormativeDocument> NormativeDocuments { get; set; }
    }
}
