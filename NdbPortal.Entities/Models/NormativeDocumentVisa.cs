using System;
using System.Collections.Generic;

namespace NdbPortal.Entities.Models
{
    public partial class NormativeDocumentVisa
    {
        public Guid Id { get; set; }
        public Guid NormativeDocumentId { get; set; }
        public Guid ApproverId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? Comment { get; set; }
        public bool IsApproved { get; set; }

        public virtual Employee Approver { get; set; } = null!;
        public virtual NormativeDocument NormativeDocument { get; set; } = null!;
    }
}
