using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
using System.Data.SqlTypes;

namespace DigiLog.Services.Implementation
{
    public class TagService : ITagService
    {
        private readonly LogDbContext _context;
        

        public TagService(LogDbContext context)
        {
            _context = context;
            
        }

        public ServiceResponse<string> AssignTagToVisitor(long tagId, long visitorId)
        {
            var response = new ServiceResponse<string>();

            // Find an available tag
            var tag = _context.Tags.FirstOrDefault(t => t.TagID == tagId && !_context.Visitors.Any(v => v.TagID == t.TagID));

            if (tag == null)
            {
                response.HasError = true;
                response.Description = $"Tag with ID {tagId} not found or already in use.";
                return response;
            }

            // Assign tag to visitor
            var visitor = _context.Visitors.Find(visitorId);
            if (visitor == null)
            {
                response.HasError = true;
                response.Description = $"Visitor with ID {visitorId} not found.";
                return response;
            }

            visitor.TagID = tag.TagID;
            _context.SaveChanges();

            response.HasError = false;
            response.Description = $"Tag with ID {tagId} assigned to Visitor with ID {visitorId} successfully.";

            return response;
        }

        public ServiceResponse<string> CheckOutVisitor(long tagId)
        {
            var response = new ServiceResponse<string>();

            // Find the visitor using the tagId
            var visitor = _context.Visitors.FirstOrDefault(v => v.TagID == tagId);

            if (visitor == null)
            {
                response.HasError = true;
                response.Description = $"No visitor found with Tag ID {tagId}.";
                return response;
            }

            // Set departure time to the current time
            visitor.DepartureTime = DateTime.Now;
            //Remove tag from visitor
            visitor.TagID = 0;

            _context.SaveChanges();

            response.HasError = false;
            response.Description = $"Visitor with Tag ID {tagId} checked out successfully.";

            return response;
        }

        public List<TagDTO> GetTags()
        {
            return _context.Tags
                .Select(tag => new TagDTO
                {
                    TagID = tag.TagID,
                    TagNumber = tag.TagNumber,
                })
                .ToList();
        }

        
    }
}
