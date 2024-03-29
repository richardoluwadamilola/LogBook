﻿using DigiLog.Data;
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
        //private readonly LogDbContext _context;
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            //_context = context;
            _tagService = tagService;
        }

        // GET: api/<TagController>
        [HttpGet]
        public IActionResult GetTags()
        {
            // Retrieves all tags from the tag service.
            var tags = _tagService.GetTags();

            if (tags == null || tags.Count == 0)
            {
                // Return error indicating that no tags were found.
                return NotFound("No tags found");
            }

            // Return the list of tags.
            return Ok(tags);
        }

        // POST api/<TagController>
        [HttpPost("create")]
        public IActionResult CreateTag([FromBody] TagDTO tagDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createdTag = _tagService.CreateTag(tagDto);
            return Ok(createdTag);
        }

        // POST api/<TagController>
        [HttpPost("assign")]
        public IActionResult AssignTagToVisitor([FromBody] AssignTagDto assignTagDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Calls the tag service to assign a tag to a visitor.
            var response = _tagService.AssignTagToVisitor(assignTagDto);

            // Returns the service response.
            return Ok(response);
        }

        //PUT api/<TagController>/5
        [HttpPut("disable")]
        public IActionResult DisableTag([FromBody] TagDTO tagDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(tagDto);

            // Calls the tag service to disable a tag.
            var response = _tagService.DisableTag(tagDto.TagNumber);

            if (response.HasError)
            {
                // Returns the service response.
                return NotFound(response.Description);
            }

            // Returns the service response.
            return Ok(response);
        }

        // PUT api/<TagController>/5
        [HttpPut("checkout")]
        public IActionResult CheckOutVisitor([FromBody] CheckOutTagDto checkOutTagDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(checkOutTagDto);

            // Calls the tag service to check out a visitor.
            var response = _tagService.CheckOutVisitor(checkOutTagDto);

            // Returns the service response.
            return Ok(response);
        }

    }
}
