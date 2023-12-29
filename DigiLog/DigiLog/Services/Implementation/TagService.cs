using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Services.Abstraction;
using System.Data.SqlTypes;
using static DigiLog.ErrorLog;

namespace DigiLog.Services.Implementation
{
    public class TagService : ITagService
    {
        private readonly LogDbContext _context;
        private readonly ILogger<TagService> _logger;

        public TagService(LogDbContext context, ILogger<TagService> logger)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void AssignTagToVisitor(int tagId, int visitorId)
        {
            try
            {
                // Logging: Output the tagId being searched
                _logger.LogInformation($"Assigning tag to visitor. TagId: {tagId}, VisitorId: {visitorId}");

                // Find an available tag
                var tag = _context.Tags.FirstOrDefault(t => t.TagID == tagId && !_context.Visitors.Any(v => v.TagID == t.TagID));
                if (tag == null)
                {
                    // Logging: Output when a tag is not found
                    _logger.LogWarning($"Tag with ID {tagId} not found or already in use.");
                    throw new TagNotFoundException(tagId.ToString());
                }

                // Assign tag to visitor
                var visitor = _context.Visitors.Find(visitorId);
                if (visitor == null)
                {
                    // Logging: Output when a visitor is not found
                    _logger.LogWarning($"Visitor with ID {visitorId} not found.");
                    throw new VisitorNotFoundException(visitorId);
                }

                visitor.TagID = tag.TagID;

                _context.SaveChanges();

                // Logging: Output when the tag is successfully assigned
                _logger.LogInformation($"Tag with ID {tagId} assigned to Visitor with ID {visitorId}");
            }
            catch (Exception ex)
            {
                // Logging: Output any unexpected exceptions
                _logger.LogError($"Error assigning tag to visitor: {ex.Message}");
                throw;
            }
        }




        public void CheckOutVisitor(int tagId)
        {
            var visitor = _context.Visitors.FirstOrDefault(v => v.TagID == tagId);
            if (visitor == null)
            {
                throw new Exception($"Visitor with assigned tagId not found");
            }

            // Check if the found visitor's TagID matches the provided tagId
            if (visitor.TagID != tagId)
            {
                throw new Exception($"Provided Id doesn't match with visitor's assigned TagId");
            }

            // Set Departure Time
            visitor.DepartureTime = DateTime.Now;

            // Reset the tag to enable it to be used again.
            visitor.TagID = 0;

            _context.SaveChanges();
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
