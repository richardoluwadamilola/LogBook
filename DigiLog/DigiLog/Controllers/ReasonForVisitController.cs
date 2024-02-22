using DigiLog.DTOs;
using DigiLog.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigiLog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReasonForVisitController : ControllerBase
    {
        private readonly IReasonForVisitService _reasonForVisitService;

        public ReasonForVisitController(IReasonForVisitService reasonForVisitService)
        {
            _reasonForVisitService = reasonForVisitService;
        }

        // GET: api/<ReasonForVisitController>
        [HttpGet]
        public IActionResult GetReasonsForVisit()
        {
            var reasons = _reasonForVisitService.GetReasonsForVisit();

            if (reasons == null || reasons.Count == 0)
            {
                return NotFound("No reasons found");
            }

            return Ok(reasons);
        }

        // POST: api/<ReasonForVisitController>
        [HttpPost]
        public IActionResult CreateReasonForVisit([FromBody] ReasonForVisitDTO reasonForVisitDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createdReason = _reasonForVisitService.CreateReasonForVisit(reasonForVisitDTO);
            return Ok(createdReason);
        }

       
    }
}
