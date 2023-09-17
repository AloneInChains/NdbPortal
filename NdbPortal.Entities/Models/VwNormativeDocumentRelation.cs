using System;
using System.Collections.Generic;

namespace NdbPortal.Entities.Models
{
    public partial class VwNormativeDocumentRelation
    {
        public Guid RelationId { get; set; }
        public Guid DocumentA { get; set; }
        public Guid DocumentB { get; set; }
        public string RelationName { get; set; } = null!;
    }
}
