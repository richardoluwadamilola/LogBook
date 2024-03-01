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


        //// GET: api/Visitor/GetVisitorsByTagNumber?tagNumber=123456
        [HttpGet("GetVisitorsByTagNumber")]
        public ActionResult<List<VisitorDTO>> GetVisitorsByTagNumber([FromQuery] string tagNumber)
        {
            // Get visitors by tag number.
            var visitors = _visitorService.GetVisitorsByTagNumber(tagNumber);
            return Ok(visitors);
        }

        // GET: api/Visitor/SearchVisitors?tagNumber=123456&employeeNumber=123456&fullName=John Doe&startDate=2023-01-01&endDate=2023-01-02
        [HttpGet("SearchVisitors")]
        public ActionResult<List<VisitorDTO>> SearchVisitors([FromQuery] SearchRequestDTO searchRequestDTO)
        {
            // Search visitors.
            var visitors = _visitorService.SearchVisitors(searchRequestDTO);
            return Ok(visitors);
        }



    }
}
