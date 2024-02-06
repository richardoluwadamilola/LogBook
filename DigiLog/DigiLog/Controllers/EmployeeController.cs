using DigiLog.DTOs;
using DigiLog.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetEmployeeByEmployeeNumber(string employeeNumber)
        {
            // Retrieves an employee by employeeId from the employee service.
            var employee = _employeeService.GetEmployeeByEmployeeNumber(employeeNumber);

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

        // PUT api/<EmployeeController>/5
        [HttpPut("{employeeNumber}")]
        public IActionResult UpdateEmployee(string employeeNumber, [FromBody] EmployeeDTO employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(employeeDto);

            //Calls the employee service to update an employee.
            var response = _employeeService.UpdateEmployee(employeeDto);

            //Returns the update employee.
            return Ok(response);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{employeeNumber}")]
        public IActionResult DeleteEmployee(string employeeNumber)
        {
            //Calls the employee service to delete an employee.
            var response = _employeeService.DeleteEmployee(employeeNumber);

            //Returns the delete employee.
            return Ok(response);
        }

        //[HttpGet("search")]
        //public IActionResult SearchEmployees([FromQuery] string keyword)
        //{
        //    // Retrieves a list of employees by keyword from the employee service.
        //    var employees = _employeeService.SearchEmployees(keyword);

        //    if (employees == null || employees.Count == 0)
        //    {
        //        return NotFound("No employees found");
        //    }

        //    // Return the list of employees.
        //    return Ok(employees);
        //}

    }
}
