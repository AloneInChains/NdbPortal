using System.Text.Json.Serialization;
using NdbPortal.Entities.Dtos.NormativeDocument;

namespace NdbPortal.Entities.Dtos.NormativeDocumentRelation
{
    public class NormativeDocumentRelationGetWithDetailsDto
    {
        [JsonPropertyName("relationId")]
        public Guid RelationId { get; set; }
        [JsonPropertyName("relationDocument")]
        public NormativeDocumentGetWithDetailsDto RelationDocument { get; set; } = new NormativeDocumentGetWithDetailsDto();
        [JsonPropertyName("relatedDocument")]
        public NormativeDocumentGetWithDetailsDto RelatedDocument { get; set; } = new NormativeDocumentGetWithDetailsDto();
        [JsonPropertyName("relationName")]
        public string RelationName { get; set; } = default!;
    }
}
