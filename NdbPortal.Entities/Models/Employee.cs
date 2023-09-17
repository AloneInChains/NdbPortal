using System;
using System.Collections.Generic;

namespace NdbPortal.Entities.Models
{
    public partial class Employee
    {
        public Employee()
        {
            NormativeDocumentFiles = new HashSet<NormativeDocumentFile>();
            NormativeDocumentVisas = new HashSet<NormativeDocumentVisa>();
            NormativeDocuments = new HashSet<NormativeDocument>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public Guid CompanyId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid? ConfidentialityLevelId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual NormativeDocumentConfidentialityLevel? ConfidentialityLevel { get; set; }
        public virtual ICollection<NormativeDocumentFile> NormativeDocumentFiles { get; set; }
        public virtual ICollection<NormativeDocumentVisa> NormativeDocumentVisas { get; set; }
        public virtual ICollection<NormativeDocument> NormativeDocuments { get; set; }
    }
}
