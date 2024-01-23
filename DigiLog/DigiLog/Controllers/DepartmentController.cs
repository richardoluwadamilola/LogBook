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
    }
}
