using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
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
        public ActionResult<ServiceResponse<string>> CreateVisitor([FromBody] VisitorDTO visitorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ServiceResponse<string> { HasError = true, Description = "Invalid model state" });

            var createdVisitor = _visitorService.CreateVisitor(visitorDto);
            return Ok(new ServiceResponse<string> { HasError = false, Description = "Successful" });
        }

        // GET: api/Visitor/GetEmployees
        [HttpGet("GetEmployees")]
        public ActionResult<List<EmployeeDTO>> GetEmployees()
        {
            var employees = _visitorService.GetEmployees();
            return Ok(new ServiceResponse<List<EmployeeDTO>> { Data = employees, HasError = false, Description = "Successful" });
        }

        // GET: api/Visitor/GetVisitorsByCheckInDate?date=2023-01-01
        [HttpGet("GetVisitorsByCheckInDate")]
        public ActionResult<List<VisitorDTO>> GetVisitorsByCheckInDate([FromQuery] DateTime date)
        {
            var visitors = _visitorService.GetVisitorsByCheckInDate(date);
            return Ok(new ServiceResponse<List<VisitorDTO>> { Data = visitors, HasError = false, Description = "Successful" });
        }



    }
}
