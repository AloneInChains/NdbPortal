#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NdbPortal.Contracts;
using NdbPortal.Entities;
using NdbPortal.Entities.Dtos.NormativeDocument;
using NdbPortal.Entities.Models;

namespace NdbPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NormativeDocumentsController : ControllerBase
    {
        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NormativeDocumentsController> _logger;
        public NormativeDocumentsController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<NormativeDocumentsController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/NormativeDocuments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NormativeDocumentGetDto>>> GetNormativeDocuments()
        {
            var normativeDocuments = await _repository.NormativeDocument.GetAllNormativeDocumentsAsync();
            var normativeDocumentsResult = _mapper.Map<IEnumerable<NormativeDocumentGetDto>>(normativeDocuments);

            return Ok(normativeDocumentsResult);
        }

        // GET: api/NormativeDocuments/WithDetails
        [HttpGet("WithDetails")]
        public async Task<ActionResult<IEnumerable<NormativeDocumentGetWithDetailsDto>>> GetNormativeDocumentsWithDetails()
        {
            var normativeDocuments = await _context.Set<NormativeDocument>()
                .Include(x => x.CreatedBy)
                .Include(x => x.Company)
                .Include(x => x.MainDocument)
                .Include(x => x.DocumentType)
                .Include(x => x.ConfidentialityLevel).AsNoTracking().ToListAsync();
              
            var normativeDocumentsResult = _mapper.Map<IEnumerable<NormativeDocumentGetWithDetailsDto>>(normativeDocuments);

            return Ok(normativeDocumentsResult);
        }

        [HttpGet("MaxVersion/{mainDocumentId}")]
        public async Task<ActionResult<int>> GetMaxVersion(Guid mainDocumentId)
        {
            var maxVersion = await _context.Set<NormativeDocument>()
                .AsNoTracking()
                .Where(x => x.MainDocumentId == mainDocumentId)
                .Select(x => x.VersionNumber).MaxAsync();

            return Ok(maxVersion ?? 1);
        }

        [HttpGet("Versions/{mainDocumentId}")]
        public async Task<ActionResult<IEnumerable<NormativeDocumentGetWithDetailsDto>>> GetVersions(Guid mainDocumentId)
        {
            var normativeDocuments = await _context.Set<NormativeDocument>()
                .Include(n => n.DocumentType)
                .Include(n => n.ConfidentialityLevel)
                .Include(n => n.CreatedBy)
                .Include(n => n.Company)
                .Where(x => x.MainDocumentId == mainDocumentId).AsNoTracking().ToListAsync();
            var normativeDocumentsResult = _mapper.Map<IEnumerable<NormativeDocumentGetWithDetailsDto>>(normativeDocuments);

            return Ok(normativeDocumentsResult);
        }

        // GET: api/NormativeDocuments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NormativeDocument>> GetNormativeDocument(Guid id)
        {
            var normativeDocument = await _repository.NormativeDocument.GetNormativeDocumentAsync(id);

            if (normativeDocument == null)
            {
                return NotFound();
            }

            var normativeDocumentResult = _mapper.Map<NormativeDocumentGetDto>(normativeDocument);

            return Ok(normativeDocumentResult);
        }

        // PUT: api/NormativeDocuments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNormativeDocument(Guid id, NormativeDocumentUpdateDto document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            try
            {
                var normativeDocumentEntity = _mapper.Map<NormativeDocument>(document);
                _repository.NormativeDocument.UpdateNormativeDocument(normativeDocumentEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await NormativeDocumentExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/NormativeDocuments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NormativeDocument>> PostNormativeDocument(NormativeDocumentAddDto document)
        {
            try
            {
                if (document is null)
                {
                    _logger.LogError("Normative document type object sent from client is null.");
                    return BadRequest("Normative document type is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid normative document type object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var normativeDocumentEntity = _mapper.Map<NormativeDocument>(document);
                _repository.NormativeDocument.CreateNormativeDocument(normativeDocumentEntity);
                await _repository.SaveAsync();

                var createdNormativeDocument = _mapper.Map<NormativeDocumentAddDto>(normativeDocumentEntity);
                return CreatedAtAction("GetNormativeDocument", new { id = normativeDocumentEntity.Id }, createdNormativeDocument);
            }
            catch (DbUpdateException)
            {
                if (document != null
                    && await NormativeDocumentExistsAsync(document.Id))
                {
                    _logger.LogError("Normative document already exists");
                    return Conflict();
                }

                throw;
            }
        }

        // DELETE: api/NormativeDocuments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNormativeDocument(Guid id)
        {
            if (!await NormativeDocumentExistsAsync(id))
            {
                _logger.LogError("Normative document does not exist (id = {0})", id);
                return NotFound();
            }

            var normativeDocument = await _repository.NormativeDocument.GetNormativeDocumentAsync(id);
            try
            {
                _repository.NormativeDocument.DeleteNormativeDocument(normativeDocument!);
                await _repository.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting document");
                return Conflict(ex.Message);
            }

            return NoContent();
        }

        private async Task<bool> NormativeDocumentExistsAsync(Guid id)
        {
            return (await _repository.NormativeDocument.GetAllNormativeDocumentsAsync()).Any(e => e.Id == id);
        }
    }
}
