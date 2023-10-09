﻿#nullable disable
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
using NdbPortal.Entities.Dtos.NormativeDocumentRelationType;
using NdbPortal.Entities.Models;

namespace NdbPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NormativeDocumentRelationTypesController : ControllerBase
    {
        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NormativeDocumentRelationsController> _logger;

        public NormativeDocumentRelationTypesController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<NormativeDocumentRelationsController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/NormativeDocumentRelationTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NormativeDocumentRelationTypeGetDto>>> GetNormativeDocumentRelationTypes()
        {
            var normativeDocumentRelationsTypes = await _repository.NormativeDocumentRelationType.GetAllNormativeDocumentRelationTypesAsync();
            var normativeDocumentRelationsTypesResult = _mapper.Map<IEnumerable<NormativeDocumentRelationTypeGetDto>>(normativeDocumentRelationsTypes);

            return Ok(normativeDocumentRelationsTypesResult);
        }

        // GET: api/NormativeDocumentRelationTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NormativeDocumentRelationTypeGetDto>> GetNormativeDocumentRelationType(Guid id)
        {
            if (!await NormativeDocumentRelationTypeExistsAsync(id))
            {
                return NotFound();
            }

            var normativeDocumentRelationType = await _repository.NormativeDocumentRelationType.GetNormativeDocumentRelationTypeAsync(id);
            var normativeDocumentRelationTypeResult = _mapper.Map<IEnumerable<NormativeDocumentRelationTypeGetDto>>(normativeDocumentRelationType);

            return Ok(normativeDocumentRelationTypeResult);
        }

        // PUT: api/NormativeDocumentRelationTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNormativeDocumentRelationType(Guid id, NormativeDocumentRelationTypeUpdateDto normativeDocumentRelationType)
        {
            if (id != normativeDocumentRelationType.Id)
            {
                return BadRequest();
            }

            try
            {
                var normativeDocumentRelationTypeResult = _mapper.Map<NormativeDocumentRelationType>(normativeDocumentRelationType);

                _repository.NormativeDocumentRelationType.UpdateNormativeDocumentRelationType(normativeDocumentRelationTypeResult);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await NormativeDocumentRelationTypeExistsAsync(id))
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

        // POST: api/NormativeDocumentRelationTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NormativeDocumentRelationType>> PostNormativeDocumentRelationType([FromBody] NormativeDocumentRelationTypeAddDto normativeDocumentRelationType)
        {
            try
            {
                if (normativeDocumentRelationType is null)
                {
                    _logger.LogError("Normative document relation type object sent from client is null.");
                    return BadRequest("Normative document relation type object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid company object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var normativeDocumentRelationTypeEntity = _mapper.Map<NormativeDocumentRelationType>(normativeDocumentRelationType);
                _repository.NormativeDocumentRelationType.CreateNormativeDocumentRelationType(normativeDocumentRelationTypeEntity);
                await _repository.SaveAsync();

                var createdNormativeDocumentRelationType = _mapper.Map<NormativeDocumentRelationTypeAddDto>(normativeDocumentRelationTypeEntity);
                return CreatedAtAction("GetNormativeDocumentRelationType", new { id = createdNormativeDocumentRelationType.Id }, createdNormativeDocumentRelationType);
            }
            catch (DbUpdateException ex)
            {
                if (normativeDocumentRelationType != null
                    && !await NormativeDocumentRelationTypeExistsAsync(normativeDocumentRelationType.Id))
                {
                    _logger.LogError("Error in PostNormativeDocumentRelationType, normative document relation type already exists");
                    return Conflict();
                }
                else
                {
                    _logger.LogError("Error in PostNormativeDocumentRelationType", ex.Message);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PostNormativeDocumentRelationType. {0}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/NormativeDocumentRelationTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNormativeDocumentRelationType(Guid id)
        {

            if (!await NormativeDocumentRelationTypeExistsAsync(id))
            {
                _logger.LogError("Error in DeleteNormativeDocumentRelationType. Normative document relation type does not exist (id = {0})", id);
                return NotFound();
            }

            var company = await _repository.Company.GetCompanyAsync(id);

            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();

            return NoContent();

        }

        private async Task<bool> NormativeDocumentRelationTypeExistsAsync(Guid id)
        {
            return (await _repository.NormativeDocumentRelationType.GetAllNormativeDocumentRelationTypesAsync()).Any(e => e.Id == id);
        }
    }
}
