using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigiLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly LogDbContext _context;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(LogDbContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                //Retrieves all employees from the employee service.
                var employee = _employeeService.GetEmployees();
                return Ok(employee);
            }
            catch (ErrorLog.AppException ex)
            {
                //Returns an error message.
                return BadRequest(new { ErrorCode = ex.ErrorCode, Message = ex.Message });
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById(int employeeId)
        {
            try
            {
                //Retrieves an employee by employeeId from the employee service.
                var employee = _employeeService.GetEmployeeById(employeeId);
                return Ok(employee);
            }
            catch (ErrorLog.AppException ex)
            {
                //Returns an error message.
                return BadRequest(new { ErrorCode = ex.ErrorCode, Message = ex.Message });
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                //Calls the employee service to create an employee.
                _employeeService.CreateEmployee(employeeDto);
                //Returns the create employee.
                return CreatedAtAction(nameof(GetEmployeeById), new { employeeId = employeeDto.Id }, employeeDto);
            }
            catch (ErrorLog.AppException ex)
            {
                //Returns an error message.
                return BadRequest(new { ErrorCode = ex.ErrorCode, Message = ex.Message });
            }
        }
        [HttpGet("search")]
        public IActionResult SearchEmployees([FromQuery] string keyword)
        {
            try
            {
                //Retrieves a list of employees by keyword from the employee service.
                var employee = _employeeService.SearchEmployees(keyword);
                return Ok(employee);
            }
            catch (ErrorLog.AppException ex)
            {
                //Returns an error message.
                return BadRequest(new { ErrorCode = ex.ErrorCode, Message = ex.Message });
            }
        }
    }
}
