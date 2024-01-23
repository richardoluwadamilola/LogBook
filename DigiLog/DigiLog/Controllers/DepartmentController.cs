using DigiLog.DTOs;
using DigiLog.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigiLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var departments = _departmentService.GetDepartments();

            if (departments == null || departments.Count == 0)
            {
                return NotFound("No departments found");
            }

            return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        public IActionResult GetDepartmentById(int departmentId)
        {
            var department = _departmentService.GetDepartmentById(departmentId);

            if (department == null)
            {
                return NotFound("Department not found");
            }

            return Ok(department);
        }

        [HttpPost]
        public IActionResult CreateDepartment([FromBody] DepartmentDTO departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(departmentDto);

            var createdDepartment = _departmentService.CreateDepartment(departmentDto);
            return Ok(createdDepartment);
        }

        [HttpPut("{departmentId}")]
        public IActionResult UpdateDepartment(int departmentId, [FromBody] DepartmentDTO departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(departmentDto);

            var updatedDepartment = _departmentService.UpdateDepartment(departmentDto);
            return Ok(updatedDepartment);
        }

        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartment(int departmentId)
        {
            var deletedDepartment = _departmentService.DeleteDepartment(departmentId);
            return Ok(deletedDepartment);
        }
    }
}
