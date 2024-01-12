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
        //private readonly LogDbContext _context;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            //_context = context;
            _employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            // Retrieves all employees from the employee service.
            var employees = _employeeService.GetEmployees();

            if (employees == null || employees.Count == 0)
            {
                // Return error indicating that no employees were found.
                return NotFound("No employees found");
            }

            // Return the list of employees.
            return Ok(employees);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{employeeNumber}")]
        public IActionResult GetEmployeeById(string employeeNumber)
        {
            // Retrieves an employee by employeeId from the employee service.
            var employee = _employeeService.GetEmployeeById(employeeNumber);

            if (employee == null)
            {
                // Return error indicating that the employee was not found.
                return NotFound("Employee not found");
            }

            // Return the employee.
            return Ok(employee);
        }


        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDTO employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(employeeDto);
            //Calls the employee service to create an employee.
            var response = _employeeService.CreateEmployee(employeeDto);
            //Returns the create employee.
            return Ok(response);

        }
        [HttpGet("search")]
        public IActionResult SearchEmployees([FromQuery] string keyword)
        {
            // Retrieves a list of employees by keyword from the employee service.
            var employees = _employeeService.SearchEmployees(keyword);

            if (employees == null || employees.Count == 0)
            {
                return NotFound("No employees found");
            }

            // Return the list of employees.
            return Ok(employees);
        }

    }
}
