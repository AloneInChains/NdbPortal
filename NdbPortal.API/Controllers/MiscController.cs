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

        private readonly NDBContext _context;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly ILogger<MiscController> _logger;

        public MiscController(NDBContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<MiscController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
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

            var normativeDocuments = await _context.Set<NormativeDocument>()
                .Include(n => n.DocumentType)
                .Include(n => n.ConfidentialityLevel)
                .Include(n => n.CreatedBy)
                .Include(n => n.Company)
                .Where(x => x.ConfidentialityLevelId == employee.ConfidentialityLevelId && x.CompanyId == employee.CompanyId).AsNoTracking().ToListAsync();
            var normativeDocumentsResult = _mapper.Map<IEnumerable<NormativeDocumentGetWithDetailsDto>>(normativeDocuments);

            return Ok(normativeDocumentsResult);

        }
    }
}
