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
    public class MiscController : ControllerBase
    {

        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public MiscController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<MiscController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Misc/GetNormativeDocumentsByEmployee
        [HttpGet("GetNormativeDocumentsByEmployeeId")]
        public async Task<ActionResult<IEnumerable<NormativeDocumentGetWithDetailsDto>>> GetNormativeDocumentsByEmployeeId (Guid employeeId)
        {

            var employee = await _repository.Employee.GetEmployeeAsync(employeeId);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            if (!employee.ConfidentialityLevelId.HasValue)
            {
                return UnprocessableEntity("Employee without confidentiality level");
            }

            var employeeConfidentialityLevel = await _repository.NormativeDocumentConfidentialityLevel.GetNormativeDocumentConfidentialityLevelAsync(employee.ConfidentialityLevelId.Value);
            
            var normativeDocuments = await _context.Set<NormativeDocument>()
                .Include(n => n.DocumentType)
                .Include(n => n.ConfidentialityLevel)
                .Include(n => n.CreatedBy)
                .Include(n => n.Company)
                .Where(x => x.ConfidentialityLevel != null && (x.ConfidentialityLevel.OrderNumber <= employeeConfidentialityLevel!.OrderNumber && x.CompanyId == employee.CompanyId || x.CreatedById == employeeId)).AsNoTracking().ToListAsync();
            var normativeDocumentsResult = _mapper.Map<IEnumerable<NormativeDocumentGetWithDetailsDto>>(normativeDocuments);

            return Ok(normativeDocumentsResult);

        }
    }
}
