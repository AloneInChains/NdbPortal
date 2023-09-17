using System;
using System.Collections.Generic;

namespace NdbPortal.Entities.Models
{
    public partial class NormativeDocumentRelation
    {
        public Guid Id { get; set; }
        public Guid RelationDocumentId { get; set; }
        public Guid RelatedDocumentId { get; set; }
        public Guid RelationTypeId { get; set; }

        public virtual NormativeDocument RelatedDocument { get; set; } = null!;
        public virtual NormativeDocument RelationDocument { get; set; } = null!;
        public virtual NormativeDocumentRelationType RelationType { get; set; } = null!;
    }
}
