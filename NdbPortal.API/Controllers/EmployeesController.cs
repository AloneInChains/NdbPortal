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
using NdbPortal.Entities.Models;

namespace NdbPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly NDbContext _context;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(NDbContext context, IRepositoryWrapper repository, IMapper mapper, ILogger<EmployeesController> logger)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeGetDto>>> GetEmployees()
        {
            var employees = await _repository.Employee.GetAllEmployeesAsync();
            var employeesResult = _mapper.Map<IEnumerable<EmployeeGetDto>>(employees);

            return Ok(employeesResult);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeGetDto>> GetEmployee(Guid id)
        {
            if (!await EmployeeExistsAsync(id))
            {
                return NotFound();
            }

            var employee = await _repository.Employee.GetEmployeeAsync(id);
            var employeeResult = _mapper.Map<EmployeeGetDto>(employee);

            return Ok(employeeResult);
        }

        [HttpGet("WithDetails")]
        public async Task<ActionResult<EmployeeGetWithDetailsDto>> GetEmployeeWithDetailsAsync(Guid id)
        {
            if (!await EmployeeExistsAsync(id))
            {
                return NotFound();
            }

            var employee = await _repository.Employee.GetEmployeeAsync(id);
            var employeeResult = _mapper.Map<EmployeeGetWithDetailsDto>(employee);
            return Ok(employeeResult);
        }


        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Employee.UpdateEmployee(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmployeeExistsAsync(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            try
            {
                if (employee is null)
                {
                    _logger.LogError("Employee object sent from client is null.");
                    return BadRequest("Employee object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid employee object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var employeeEntity = _mapper.Map<Employee>(employee);
                _repository.Employee.CreateEmployee(employeeEntity);
                await _repository.SaveAsync();

                var createdEmployee = _mapper.Map<EmployeeAddDto>(employeeEntity);
                return CreatedAtAction("GetEmployee", new { id = employeeEntity.Id }, createdEmployee);
            }
            catch (DbUpdateException)
            {
                if (await EmployeeExistsAsync(employee.Id))
                {
                    _logger.LogError("Employee already exists");
                    return Conflict();
                }

                throw;
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            if (!await EmployeeExistsAsync(id))
            {
                _logger.LogError("Error in DeleteEmployee. Employee does not exist (id = {0})", id);
                return NotFound();
            }

            var employee = await _repository.Employee.GetEmployeeAsync(id);

            if (employee == null)
            {
                _logger.LogError("Error in DeleteEmployee. Employee does not exist (id = {0})", id);
                return NotFound();
            }

            _repository.Employee.DeleteEmployee(employee);
            await _repository.SaveAsync();

            return NoContent();
        }

        private async Task<bool> EmployeeExistsAsync(Guid id)
        {
            return (await _repository.Employee.GetAllEmployeesAsync()).Any(e => e.Id == id);
        }
    }
}
