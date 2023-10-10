#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NdbPortal.Entities.Dtos.Company;
using NdbPortal.Contracts;
using NdbPortal.Entities;
using NdbPortal.Entities.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace NdbPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompaniesController : ControllerBase
    {
        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<CompaniesController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyGetDto>>> GetCompanies()
        {

            var companies = await _repository.Company.GetAllCompaniesAsync();
            var companiesResult = _mapper.Map<IEnumerable<CompanyGetDto>>(companies);

            return Ok(companiesResult);

        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyGetDto>> GetCompany(Guid id)
        {
            var company = await _repository.Company.GetCompanyAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            var companyResult = _mapper.Map<CompanyGetDto>(company);

            return Ok(companyResult);
            
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, Company company)
        {

            if (id != company.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Company.UpdateCompany(company);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CompanyExistsAsync(id))
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

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCompany([FromBody]CompanyAddDto company)
        {
            try
            {
                if (company is null)
                {
                    _logger.LogError("Company object sent from client is null.");
                    return BadRequest("Company object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid company object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var companyEntity = _mapper.Map<Company>(company);
                _repository.Company.CreateCompany(companyEntity);
                await _repository.SaveAsync();

                var createdCompany = _mapper.Map<CompanyAddDto>(companyEntity);
                return CreatedAtAction("GetCompany", new { id = companyEntity.Id }, createdCompany);
            }
            catch (DbUpdateException ex)
            {
                if (company != null && await CompanyExistsAsync(company.Id))
                {
                    _logger.LogError("Error in PostCompany, company already exists");
                    return Conflict();
                }
                else
                {
                    _logger.LogError("Error in PostCompany", ex.Message);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in PostCompany. {0}", ex.Message);
                return StatusCode(500, "Internal server error");
            }

        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {

            if (!await CompanyExistsAsync(id))
            {
                _logger.LogError($"Error in DeleteCompany. Company does not exist (id = {id})");
                return NotFound();
            }

            var company = await _repository.Company.GetCompanyAsync(id);              

            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();

            return NoContent();

        }

        private async Task<bool> CompanyExistsAsync(Guid id)
        {
            return (await _repository.Company.GetAllCompaniesAsync()).Any(e => e.Id == id);
        }
    }
}
