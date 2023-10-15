#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NdbPortal.Contracts;
using NdbPortal.Entities;
using NdbPortal.Entities.Dtos.NormativeDocumentType;
using NdbPortal.Entities.Models;

namespace NdbPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NormativeDocumentTypesController : ControllerBase
    {
        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NormativeDocumentTypesController> _logger;

        public NormativeDocumentTypesController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<NormativeDocumentTypesController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/NormativeDocumentTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NormativeDocumentTypeGetDto>>> GetNormativeDocumentTypes()
        {
            var normativeDocumentTypes = await _repository.NormativeDocumentType.GetAllNormativeDocumentTypesAsync();
            var normativeDocumentTypesResult = _mapper.Map<IEnumerable<NormativeDocumentTypeGetDto>>(normativeDocumentTypes);

            return Ok(normativeDocumentTypesResult);
        }

        // GET: api/NormativeDocumentTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NormativeDocumentTypeGetDto>> GetNormativeDocumentType(Guid id)
        {
            var normativeDocumentType = await _repository.NormativeDocumentType.GetNormativeDocumentTypeAsync(id);

            if (normativeDocumentType == null)
            {
                return NotFound();
            }

            var normativeDocumentTypeResult = _mapper.Map<NormativeDocumentTypeGetDto>(normativeDocumentType);

            return Ok(normativeDocumentTypeResult);
        }

        // PUT: api/NormativeDocumentTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNormativeDocumentType(Guid id, NormativeDocumentTypeUpdateDto documentType)
        {
            if (id != documentType.Id)
            {
                return BadRequest();
            }

            try
            {
                var normativeDocumentTypeDto = _mapper.Map<NormativeDocumentType>(documentType);
                _repository.NormativeDocumentType.UpdateNormativeDocumentType(normativeDocumentTypeDto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await NormativeDocumentTypeExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/NormativeDocumentTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NormativeDocumentType>> PostNormativeDocumentType(NormativeDocumentTypeAddDto documentType)
        {
            try
            {
                if (documentType is null)
                {
                    _logger.LogError("Normative document type object sent from client is null.");
                    return BadRequest("Normative document type is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid normative document type object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var normativeDocumentTypeEntity = _mapper.Map<NormativeDocumentType>(documentType);
                _repository.NormativeDocumentType.CreateNormativeDocumentType(normativeDocumentTypeEntity);
                await _repository.SaveAsync();

                var createdNormativeDocumentType = _mapper.Map<NormativeDocumentTypeAddDto>(normativeDocumentTypeEntity);
                return CreatedAtAction("GetNormativeDocumentType", new { id = normativeDocumentTypeEntity.Id }, createdNormativeDocumentType);
            }
            catch (DbUpdateException)
            {
                if (documentType != null
                    && await NormativeDocumentTypeExistsAsync(documentType.Id))
                {
                    _logger.LogError("Normative document type already exists");
                    return Conflict();
                }

                throw;
            }
        }

        // DELETE: api/NormativeDocumentTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNormativeDocumentType(Guid id)
        {
            if (!await NormativeDocumentTypeExistsAsync(id))
            {
                _logger.LogError("Normative document type does not exist (id = {0})", id);
                return NotFound();
            }

            var normativeDocumentType = await _repository.NormativeDocumentType.GetNormativeDocumentTypeAsync(id);

            _repository.NormativeDocumentType.DeleteNormativeDocumentType(normativeDocumentType!);
            await _repository.SaveAsync();

            return NoContent();
        }

        private async Task<bool> NormativeDocumentTypeExistsAsync(Guid id)
        {
            return (await _repository.NormativeDocumentType.GetAllNormativeDocumentTypesAsync()).Any(e => e.Id == id);
        }
    }
}
