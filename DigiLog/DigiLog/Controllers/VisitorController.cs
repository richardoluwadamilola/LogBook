using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigiLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly LogDbContext _context;
        private readonly IVisitorService _visitorService;
        private readonly ITagService _tagService;

        public VisitorController(LogDbContext context, IVisitorService visitorService, ITagService tagService)
        {
            _context = context;
            _visitorService = visitorService;
            _tagService = tagService;
        }

        // POST: api/<VisitorController>
        [HttpPost]
        public ActionResult<VisitorDTO> CreateVisitor([FromBody] VisitorDTO visitorDto)
        {
            try
            {
                var createdVisitor = _visitorService.CreateVisitor(visitorDto);
                return Ok(createdVisitor);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating visitor: {ex.Message}");
            }
        }

        // GET: api/Visitor/GetEmployees
        [HttpGet("GetEmployees")]
        public ActionResult<List<EmployeeDTO>> GetEmployees()
        {
            try
            {
                var employees = _visitorService.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting employees: {ex.Message}");
            }
        }

        // GET: api/Visitor/GetVisitorsByCheckInDate?date=2023-01-01
        [HttpGet("GetVisitorsByCheckInDate")]
        public ActionResult<List<VisitorDTO>> GetVisitorsByCheckInDate([FromQuery] DateTime date)
        {
            try
            {
                var visitors = _visitorService.GetVisitorsByCheckInDate(date);
                return Ok(visitors);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting visitors: {ex.Message}");
            }
        }



    }
}
