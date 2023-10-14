#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NdbPortal.Contracts;
using NdbPortal.Entities;
using NdbPortal.Entities.Dtos.Employee;
using NdbPortal.Entities.Dtos.NormativeDocument;
using NdbPortal.Entities.Dtos.NormativeDocumentVisa;
using NdbPortal.Entities.Models;

namespace NdbPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NormativeDocumentVisasController : ControllerBase
    {
        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NormativeDocumentVisasController> _logger;

        public NormativeDocumentVisasController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<NormativeDocumentVisasController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/NormativeDocumentVisas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NormativeDocumentVisaGetDto>>> GetNormativeDocumentVisas()
        {
            var normativeDocumentVisas = await _repository.NormativeDocumentVisa.GetAllNormativeDocumentVisasAsync();
            var normativeDocumentVisasResult = _mapper.Map<IEnumerable<NormativeDocumentVisaGetDto>>(normativeDocumentVisas);

            return Ok(normativeDocumentVisasResult);
        }

        // GET: api/NormativeDocumentVisas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NormativeDocumentVisaGetDto>> GetNormativeDocumentVisa(Guid id)
        {
            if (!await NormativeDocumentVisaExistsAsync(id))
            {
                return NotFound();
            }

            var normativeDocumentVisa = await _repository.NormativeDocumentVisa.GetNormativeDocumentVisaAsync(id);
            var normativeDocumentVisaResult = _mapper.Map<IEnumerable<NormativeDocumentVisaGetDto>>(normativeDocumentVisa);

            return Ok(normativeDocumentVisaResult);
        }

        [HttpGet("WithDetails/{id}")]
        public async Task<ActionResult<IEnumerable<NormativeDocumentVisaGetWithDetailsDto>>> GetNormativeDocumentVisaByDocId(Guid id)
        {
            if (!await NormativeDocumentExistsAsync(id))
            {
                return NotFound();
            }

            var normativeVisas = await _context.Set<NormativeDocumentVisa>()
                .Include(x => x.Approver)
                .Include(x => x.NormativeDocument).AsNoTracking().ToListAsync();

            var normativeVisasEntity = normativeVisas.Select(x => new NormativeDocumentVisaGetWithDetailsDto
            {
                Id = x.Id,
                Approver = _mapper.Map<EmployeeGetDto>(x.Approver),
                Comment = x.Comment,
                CreatedOn = x.CreatedOn,
                IsApproved = x.IsApproved,
                NormativeDocument = _mapper.Map<NormativeDocumentGetDto>(x.NormativeDocument)
            }).Where(x => x.NormativeDocument.Id == id);

            return Ok(normativeVisasEntity);
        }

        // PUT: api/NormativeDocumentVisas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNormativeDocumentVisa(Guid id, NormativeDocumentVisaUpdateDto documentVisa)
        {
            if (id != documentVisa.Id)
            {
                return BadRequest();
            }

            try
            {
                var normativeDocumentVisaEntity = _mapper.Map<NormativeDocumentVisa>(documentVisa);
                _repository.NormativeDocumentVisa.UpdateNormativeDocumentVisa(normativeDocumentVisaEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await NormativeDocumentVisaExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/NormativeDocumentVisas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NormativeDocumentVisa>> PostNormativeDocumentVisa(NormativeDocumentVisaAddDto documentVisa)
        {
            try
            {
                if (documentVisa is null)
                {
                    _logger.LogError("normativeDocumentVisa object sent from client is null.");
                    return BadRequest("normativeDocumentVisa object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid normativeDocumentVisa object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var normativeDocumentVisaEntity = _mapper.Map<NormativeDocumentVisa>(documentVisa);
                _repository.NormativeDocumentVisa.CreateNormativeDocumentVisa(normativeDocumentVisaEntity);
                await _repository.SaveAsync();

                var createdNormativeDocumentVisaEntity =
                    _mapper.Map<NormativeDocumentVisaAddDto>(documentVisa);
                return CreatedAtAction("GetNormativeDocumentVisa",
                    new { id = documentVisa.Id }, createdNormativeDocumentVisaEntity);
            }
            catch (DbUpdateException ex)
            {
                if (documentVisa != null && await NormativeDocumentVisaExistsAsync(documentVisa.Id))
                {
                    _logger.LogError($"Normative document visa already exists. {ex.Message}");
                    return Conflict();
                }

                throw;
            }
        }

        // DELETE: api/NormativeDocumentVisas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNormativeDocumentVisa(Guid id)
        {

            try
            {
                if (!await NormativeDocumentVisaExistsAsync(id))
                {
                    _logger.LogError("Normative document visa does not exist (id = {0})", id);
                    return NotFound();
                }

                var normativeDocumentVisa = await _repository.NormativeDocumentVisa.GetNormativeDocumentVisaAsync(id);

                _repository.NormativeDocumentVisa.DeleteNormativeDocumentVisa(normativeDocumentVisa);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch(DbUpdateException ex)
            {
                return Conflict(ex);
            }
        }

        private async Task<bool> NormativeDocumentVisaExistsAsync(Guid id)
        {
            return (await _repository.NormativeDocumentVisa.GetAllNormativeDocumentVisasAsync()).Any(e => e.Id == id);
        }
        private async Task<bool> NormativeDocumentExistsAsync(Guid id)
        {
            return (await _repository.NormativeDocument.GetAllNormativeDocumentsAsync()).Any(e => e.Id == id);
        }
    }
}
