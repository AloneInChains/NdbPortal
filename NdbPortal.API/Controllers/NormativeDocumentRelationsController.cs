#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NdbPortal.Contracts;
using NdbPortal.Entities;
using NdbPortal.Entities.Dtos.NormativeDocument;
using NdbPortal.Entities.Dtos.NormativeDocumentRelation;
using NdbPortal.Entities.Models;

namespace NdbPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NormativeDocumentRelationsController : ControllerBase
    {
        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NormativeDocumentRelationsController> _logger;

        public NormativeDocumentRelationsController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<NormativeDocumentRelationsController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/NormativeDocumentRelations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NormativeDocumentRelationGetDto>>> GetNormativeDocumentRelations()
        {
            var normativeDocumentRelations = await _repository.NormativeDocumentRelation.GetAllNormativeDocumentRelationsAsync();
            var normativeDocumentRelationsResult = _mapper.Map<IEnumerable<NormativeDocumentRelationAddDto>>(normativeDocumentRelations);

            return Ok(normativeDocumentRelationsResult);
        }

        // GET: api/NormativeDocumentRelations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NormativeDocumentRelationGetDto>> GetNormativeDocumentRelation(Guid id)
        {
            if (!await NormativeDocumentRelationExistsAsync(id))
            {
                return NotFound();
            }

            var normativeDocumentRelation = await _repository.NormativeDocumentRelation.GetNormativeDocumentRelationAsync(id);
            var normativeDocumentRelationResult = _mapper.Map<IEnumerable<NormativeDocumentRelationGetDto>>(normativeDocumentRelation);

            return Ok(normativeDocumentRelationResult);
        }

        [HttpGet("WithDetails/{id}")]
        public async Task<ActionResult<IEnumerable<NormativeDocumentRelationGetWithDetailsDto>>> GetNormativeDocumentRelationByDocId(Guid id)
        {
            if (!await NormativeDocumentExistsAsync(id))
            {
                return NotFound();
            }

            var relations = await _context.Set<VwNormativeDocumentRelation>()
                .Join(_context.NormativeDocuments, relation => relation.DocumentA, documentA =>documentA.Id, (relation, documentA) =>
                new 
                {
                    relation, documentA
                })
                .Join(_context.NormativeDocuments, r => r.relation.DocumentB, db => db.Id, (relationCombined, documentB) =>
                new
                {
                    relationCombined.relation.RelationId,
                    relationCombined.relation.RelationName,
                    RelationDocument = relationCombined.documentA,
                    RelatedDocument = documentB
                })
            .AsNoTracking()
            .ToListAsync();

            var relationsDto = relations
                .Select(x => new NormativeDocumentRelationGetWithDetailsDto
                {
                    RelationId = x.RelationId,
                    RelationName = x.RelationName,
                    RelatedDocument = _mapper.Map<NormativeDocumentGetWithDetailsDto>(x.RelatedDocument),
                    RelationDocument = _mapper.Map<NormativeDocumentGetWithDetailsDto>(x.RelationDocument)
                }).Where(x => x.RelationDocument.Id == id);

            return Ok(relationsDto);
        }

        // PUT: api/NormativeDocumentRelations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNormativeDocumentRelation(Guid id, NormativeDocumentRelationUpdateDto documentRelation)
        {
            if (id != documentRelation.Id)
            {
                return BadRequest();
            }

            try
            {
                var normativeDocumentRelationEntity = _mapper.Map<NormativeDocumentRelation>(documentRelation);
                _repository.NormativeDocumentRelation.UpdateNormativeDocumentRelation(normativeDocumentRelationEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await NormativeDocumentRelationExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/NormativeDocumentRelations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NormativeDocumentRelation>> PostNormativeDocumentRelation(NormativeDocumentRelationAddDto documentRelation)
        {
            try
            {
                if (documentRelation is null)
                {
                    _logger.LogError("normativeDocumentFile object sent from client is null.");
                    return BadRequest("normativeDocumentFile object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid normativeDocumentFile object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var normativeDocumentRelationEntity = _mapper.Map<NormativeDocumentRelation>(documentRelation);
                _repository.NormativeDocumentRelation.CreateNormativeDocumentRelation(normativeDocumentRelationEntity);
                await _repository.SaveAsync();

                var createdNormativeDocumentRelationEntity =
                    _mapper.Map<NormativeDocumentRelationAddDto>(documentRelation);
                return CreatedAtAction("GetNormativeDocumentRelation",
                    new { id = documentRelation.Id }, createdNormativeDocumentRelationEntity);
            }
            catch (DbUpdateException ex)
            {
                if (documentRelation != null
                    && await NormativeDocumentRelationExistsAsync(documentRelation.Id))
                {
                    _logger.LogError($"Normative document file already exists. {ex.Message}");
                    return Conflict();
                }

                throw;
            }
        }

        // DELETE: api/NormativeDocumentRelations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNormativeDocumentRelation(Guid id)
        {
            if (!await NormativeDocumentRelationExistsAsync(id))
            {
                _logger.LogError("Normative document relation does not exist (id = {0})", id);
                return NotFound();
            }

            var normativeDocumentRelation = await _repository.NormativeDocumentRelation.GetNormativeDocumentRelationAsync(id);

            if (normativeDocumentRelation != null)
                _repository.NormativeDocumentRelation.DeleteNormativeDocumentRelation(normativeDocumentRelation);
            await _repository.SaveAsync();

            return NoContent();
        }

        private async Task<bool> NormativeDocumentRelationExistsAsync(Guid id)
        {
            return (await _repository.NormativeDocumentRelation.GetAllNormativeDocumentRelationsAsync()).Any(e => e.Id == id);
        }
        private async Task<bool> NormativeDocumentExistsAsync(Guid id)
        {
            return (await _repository.NormativeDocument.GetAllNormativeDocumentsAsync()).Any(e => e.Id == id);
        }
    }
}
