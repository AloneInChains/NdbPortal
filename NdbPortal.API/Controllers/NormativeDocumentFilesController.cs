#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NdbPortal.Contracts;
using NdbPortal.Entities;
using NdbPortal.Entities.Dtos.Employee;
using NdbPortal.Entities.Dtos.NormativeDocumentFile;
using NdbPortal.Entities.Models;

namespace NdbPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NormativeDocumentFilesController : ControllerBase
    {
        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NormativeDocumentFilesController> _logger;

        public NormativeDocumentFilesController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<NormativeDocumentFilesController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/NormativeDocumentFiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NormativeDocumentFileGetDto>>> GetNormativeDocumentFiles()
        {
            var normativeDocumentFiles = await _repository.NormativeDocumentFile.GetAllNormativeDocumentFilesAsync();
            var normativeDocumentFilesResult = _mapper.Map<IEnumerable<NormativeDocumentFileGetDto>>(normativeDocumentFiles);

            return Ok(normativeDocumentFilesResult);
        }

        // GET: api/NormativeDocumentFiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NormativeDocumentFileGetDto>> GetNormativeDocumentFile(Guid id)
        {
            if (!await NormativeDocumentFileExists(id))
            {
                return NotFound();
            }

            var normativeDocumentFile = await _repository.NormativeDocumentFile.GetNormativeDocumentFileAsync(id);
            var normativeDocumentFileResult = _mapper.Map<IEnumerable<NormativeDocumentFileGetDto>>(normativeDocumentFile);

            return Ok(normativeDocumentFileResult);
        }

        [HttpGet("WithDetails/{id}")]
        public async Task<ActionResult<IEnumerable<NormativeDocumentFileGetDto>>> GetNormativeDocumentFilesByDocId(Guid id)
        {
            if (!await NormativeDocumentExistsAsync(id))
            {
                return NotFound();
            }

            var normativeFiles = (await _context.Set<NormativeDocumentFile>()
                .Include(x => x.CreatedBy).AsNoTracking().ToListAsync())
                .Where(x => x.NormativeDocumentId == id)
                .Select(x => new NormativeDocumentFileGetWithDetailsDto
            {
                Id = x.Id,
                CreatedBy = _mapper.Map<EmployeeGetDto>(x.CreatedBy),
                CreatedOn = x.CreatedOn,
                FileName = x.FileName,
                Data = x.Data,
                NormativeDocumentId = x.NormativeDocumentId
            });

            return Ok(normativeFiles);
        }

        // PUT: api/NormativeDocumentFiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNormativeDocumentFile(Guid id, NormativeDocumentFileUpdateDto normativeDocumentFile)
        {
            if (id != normativeDocumentFile.Id)
            {
                return BadRequest();
            }

            try
            {
                var normativeDocumentFileEntity = _mapper.Map<NormativeDocumentFile>(normativeDocumentFile);
                _repository.NormativeDocumentFile.UpdateNormativeDocumentFile(normativeDocumentFileEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await NormativeDocumentFileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NormativeDocumentFiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NormativeDocumentFile>> PostNormativeDocumentFile(NormativeDocumentFileAddDto normativeDocumentFile)
        {
            try
            {
                if (normativeDocumentFile is null)
                {
                    _logger.LogError("NormativeDocumentFile object sent from client is null.");
                    return BadRequest("NormativeDocumentFile object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid normativeDocumentFile object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var normativeDocumentFileEntity = _mapper.Map<NormativeDocumentFile>(normativeDocumentFile);
                _repository.NormativeDocumentFile.CreateNormativeDocumentFile(normativeDocumentFileEntity);
                await _repository.SaveAsync();

                var createdNormativeDocumentFileEntity =
                    _mapper.Map<NormativeDocumentFileAddDto>(normativeDocumentFileEntity);
                return CreatedAtAction("GetNormativeDocumentFile",
                    new { id = normativeDocumentFileEntity.Id }, createdNormativeDocumentFileEntity);
            }
            catch (DbUpdateException ex)
            {
                if (normativeDocumentFile != null && await NormativeDocumentFileExists(normativeDocumentFile.Id))
                {
                    _logger.LogError($"Normative document file already exists. {ex.Message}");
                    return Conflict();
                }

                throw;
            }
        }

        // DELETE: api/NormativeDocumentFiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNormativeDocumentFile(Guid id)
        {
            if (!await NormativeDocumentFileExists(id))
            {
                _logger.LogError("Normative document file does not exist (id = {0})", id);
                return NotFound();
            }

            var normativeDocumentFile = await _repository.NormativeDocumentFile.GetNormativeDocumentFileAsync(id);

            _repository.NormativeDocumentFile.DeleteNormativeDocumentFile(normativeDocumentFile);
            await _repository.SaveAsync();

            return NoContent();
        }

        private async Task<bool> NormativeDocumentFileExists(Guid id)
        {
            return (await _repository.NormativeDocumentFile.GetAllNormativeDocumentFilesAsync()).Any(e => e.Id == id);
        }
        private async Task<bool> NormativeDocumentExistsAsync(Guid id)
        {
            return (await _repository.NormativeDocument.GetAllNormativeDocumentsAsync()).Any(e => e.Id == id);
        }
    }
}
