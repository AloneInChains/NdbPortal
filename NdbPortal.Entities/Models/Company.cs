using System;
using System.Collections.Generic;

namespace NdbPortal.Entities.Models
{
    public partial class Company
    {
        public Company()
        {
            Employees = new HashSet<Employee>();
            NormativeDocuments = new HashSet<NormativeDocument>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<NormativeDocument> NormativeDocuments { get; set; }
    }
}
