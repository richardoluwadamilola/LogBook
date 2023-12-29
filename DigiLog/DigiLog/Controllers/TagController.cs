using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigiLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly LogDbContext _context;
        private readonly ITagService _tagService;

        public TagController(LogDbContext context, ITagService tagService)
        {
            _context = context;
            _tagService = tagService;
        }

        // GET: api/<TagController>
        [HttpGet]
        public IActionResult GetTags()
        {
            //Retrieves the list of tags from the tag service.
            var tags = _tagService.GetTags();
            //Returns the list of tags.
            return Ok(tags);
        }

        // POST api/<TagController>
        [HttpPost("assign")]
        public IActionResult AssignTagToVisitor([FromBody] AssignTagDto assignTagDto)
        {
            try
            {
                // Calls the tag service to assign a tag to a visitor.
                _tagService.AssignTagToVisitor(assignTagDto.TagID, assignTagDto.VisitorId);

                // Returns a success message or additional details.
                return Ok(new { Message = "Tag assigned successfully.", TagId = assignTagDto.TagID, VisitorId = assignTagDto.VisitorId });
            }
            catch (ErrorLog.AppException ex)
            {
                // Returns an error message.
                return BadRequest(new { ErrorCode = ex.ErrorCode, Message = ex.Message });
            }
        }


        // DELETE api/<TagController>/5
        [HttpDelete("checkout")]
        public IActionResult CheckOutVisitor([FromBody] CheckOutTagDto checkOutTagDto)
        {
            try
            {
                //Calls the tag service to check out a visitor.
                _tagService.CheckOutVisitor(checkOutTagDto.TagID);
                //Returns a success message.
                return Ok("Visitor checked out successfully.");
            }
            catch (ErrorLog.AppException ex)
            {
                //Returns an error message.
                return BadRequest(new { ErrorCode = ex.ErrorCode, Message = ex.Message });
            }
        }

    }
}
