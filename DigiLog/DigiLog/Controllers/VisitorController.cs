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
        //private readonly LogDbContext _context;
        private readonly IVisitorService _visitorService;
        private readonly ITagService _tagService;

        public VisitorController(IVisitorService visitorService, ITagService tagService)
        {
            //_context = context;
            _visitorService = visitorService;
            _tagService = tagService;
        }

        // POST: api/<VisitorController>
        [HttpPost]
        public ActionResult<ServiceResponse<string>> CreateVisitor([FromBody] VisitorDTO visitorDto)
        {
            // Check if model state is valid.
            if (!ModelState.IsValid)
                // Return error indicating that the model state is invalid.
                return BadRequest(new ServiceResponse<string> { HasError = true, Description = "Invalid model state" });

            // Create visitor.
            var createdVisitor = _visitorService.CreateVisitor(visitorDto);
            return Ok(new ServiceResponse<string> { Data = createdVisitor.Data, Description = "Visitor created successfully" });
        }

        //GET api/<VisitorController>/GetVisitors
        [HttpGet("GetVisitors")]
        public ActionResult<List<VisitorDTO>> GetVisitors()
        {
            // Get all visitors.
            var visitors = _visitorService.GetVisitors();
            return Ok(visitors);
        }


        // GET: api/Visitor/GetVisitorsByCheckInDate?date=2023-01-01
        [HttpGet("GetVisitorsByCheckInDate")]
        public ActionResult<List<VisitorDTO>> GetVisitorsByCheckInDate([FromQuery] DateTime date)
        {
            // Get visitors by check in date.
            var visitors = _visitorService.GetVisitorsByCheckInDate(date);
            return Ok(visitors);
        }

        // GET: api/Visitor/GetVisitorsByEmployeeNumber?employeeNumber=123456
        [HttpGet("GetVisitorsByEmployeeNumber")]
        public ActionResult<List<VisitorDTO>> GetVisitorsByEmployeeNumber([FromQuery] string employeeNumber)
        {
            // Get visitors by employee number.
            var visitors = _visitorService.GetVisitorsByEmployeeNumber(employeeNumber);
            return Ok(visitors);
        }

        // GET: api/Visitor/GetVisitorsByTagNumber?tagNumber=123456
        [HttpGet("GetVisitorsByTagNumber")]
        public ActionResult<List<VisitorDTO>> GetVisitorsByTagNumber([FromQuery] string tagNumber)
        {
            // Get visitors by tag number.
            var visitors = _visitorService.GetVisitorsByTagNumber(tagNumber);
            return Ok(visitors);
        }



    }
}
