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
using NdbPortal.Entities.Dtos.NormativeDocumentConfidentialityLevel;
using NdbPortal.Entities.Models;

namespace NdbPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NormativeDocumentConfidentialityLevelsController : ControllerBase
    {
        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NormativeDocumentConfidentialityLevelsController> _logger;

        public NormativeDocumentConfidentialityLevelsController(NDbContext context, IRepositoryWrapper repository, 
            IMapper mapper, ILogger<NormativeDocumentConfidentialityLevelsController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/NormativeDocumentConfidentialityLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NormativeDocumentConfidentialityLevelGetDto>>> GetNormativeDocumentConfidentialityLevels()
        {
            var levels = await _repository.NormativeDocumentConfidentialityLevel.GetAllNormativeDocumentConfidentialityLevelsAsync();
            var levelsResult = _mapper.Map<IEnumerable<NormativeDocumentConfidentialityLevelAddDto>>(levels);

            return Ok(levelsResult);
        }

        // GET: api/NormativeDocumentConfidentialityLevels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NormativeDocumentConfidentialityLevelGetDto>> GetNormativeDocumentConfidentialityLevel(Guid id)
        {

            if (!await NormativeDocumentConfidentialityLevelExists(id))
            {
                return NotFound();
            }

            var levels = await _repository.NormativeDocumentConfidentialityLevel.GetNormativeDocumentConfidentialityLevelAsync(id);
            var levelsResult = _mapper.Map<IEnumerable<NormativeDocumentConfidentialityLevelAddDto>>(levels);

            return Ok(levelsResult);
        }

        // PUT: api/NormativeDocumentConfidentialityLevels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNormativeDocumentConfidentialityLevel(Guid id, NormativeDocumentConfidentialityLevel normativeDocumentConfidentialityLevel)
        {
            if (id != normativeDocumentConfidentialityLevel.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.NormativeDocumentConfidentialityLevel.UpdateNormativeDocumentConfidentialityLevel(normativeDocumentConfidentialityLevel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await NormativeDocumentConfidentialityLevelExists(id))
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

        // POST: api/NormativeDocumentConfidentialityLevels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NormativeDocumentConfidentialityLevel>> PostNormativeDocumentConfidentialityLevel(NormativeDocumentConfidentialityLevel normativeDocumentConfidentialityLevel)
        {
            try
            {
                if (normativeDocumentConfidentialityLevel is null)
                {
                    _logger.LogError("NormativeDocumentConfidentialityLevel object sent from client is null.");
                    return BadRequest("NormativeDocumentConfidentialityLevel object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid NormativeDocumentConfidentialityLevel object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var normativeDocumentConfidentialityLevelEntity = _mapper.Map<NormativeDocumentConfidentialityLevel>(normativeDocumentConfidentialityLevel);
                _repository.NormativeDocumentConfidentialityLevel.CreateNormativeDocumentConfidentialityLevel(normativeDocumentConfidentialityLevelEntity);
                await _repository.SaveAsync();

                var createdNormativeDocumentConfidentialityLevelEntity = 
                    _mapper.Map<NormativeDocumentConfidentialityLevelAddDto>(normativeDocumentConfidentialityLevelEntity);
                return CreatedAtAction("GetNormativeDocumentConfidentialityLevel",
                    new { id = normativeDocumentConfidentialityLevelEntity.Id }, createdNormativeDocumentConfidentialityLevelEntity);
            }
            catch (DbUpdateException ex)
            {
                if (normativeDocumentConfidentialityLevel != null 
                    && await NormativeDocumentConfidentialityLevelExists(normativeDocumentConfidentialityLevel.Id))
                {
                    _logger.LogError($"Normative document confidentiality level already exists. {ex.Message}");
                    return Conflict();
                }

                throw;
            }
        }

        // DELETE: api/NormativeDocumentConfidentialityLevels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNormativeDocumentConfidentialityLevel(Guid id)
        {
            if (!await NormativeDocumentConfidentialityLevelExists(id))
            {
                _logger.LogError("Normative document confidentiality level does not exist (id = {0})", id);
                return NotFound();
            }

            var normativeDocumentConfidentialityLevel = await _repository.NormativeDocumentConfidentialityLevel.GetNormativeDocumentConfidentialityLevelAsync(id);

            _repository.NormativeDocumentConfidentialityLevel.DeleteNormativeDocumentConfidentialityLevel(normativeDocumentConfidentialityLevel);
            await _repository.SaveAsync();

            return NoContent();
        }

        private async Task<bool> NormativeDocumentConfidentialityLevelExists(Guid id)
        {
            return (await _repository.NormativeDocumentConfidentialityLevel.GetAllNormativeDocumentConfidentialityLevelsAsync()).Any(e => e.Id == id);
        }
    }
}
