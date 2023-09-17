using System;
using System.Collections.Generic;

namespace NdbPortal.Entities.Models
{
    public partial class NormativeDocumentType
    {
        public NormativeDocumentType()
        {
            NormativeDocuments = new HashSet<NormativeDocument>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<NormativeDocument> NormativeDocuments { get; set; }
    }
}
