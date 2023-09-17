using System;
using System.Collections.Generic;

namespace NdbPortal.Entities.Models
{
    public partial class NormativeDocumentRelationType
    {
        public NormativeDocumentRelationType()
        {
            NormativeDocumentRelations = new HashSet<NormativeDocumentRelation>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ReverseName { get; set; } = null!;

        public virtual ICollection<NormativeDocumentRelation> NormativeDocumentRelations { get; set; }
    }
}
