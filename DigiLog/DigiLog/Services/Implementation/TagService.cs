using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;

namespace DigiLog.Services.Implementation
{
    // Service responsible for handling operations related to tags.
    public class TagService : ITagService
    {
        private readonly LogDbContext _context;

        // Constructor to initialize the service with the database context.
        public TagService(LogDbContext context)
        {
            _context = context;
        }

        // Create a new tag based on the provided TagDTO.
        public ServiceResponse<string> CreateTag(TagDTO tagDto)
        {
            // Create Tag.
            var tag = new Tag
            {
                TagNumber = tagDto.TagNumber,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Deleted = false
            };

            _context.Tags.Add(tag);
            _context.SaveChanges();
            return new ServiceResponse<string>();
        }

        // Assign a tag to a visitor based on the provided AssignTagDto.
        public ServiceResponse<string> AssignTagToVisitor(AssignTagDto assignTagDto)
        {
            var response = new ServiceResponse<string>();

            // Find an available tag
            var tag = _context.Tags.FirstOrDefault(t => t.TagNumber == assignTagDto.TagNumber);

            if (tag == null)
            {
                response.HasError = true;
                response.Description = $"Tag with ID {assignTagDto.VisitorId} not found";
                return response;
            }

            if (tag.IsAvailable)
            {
                // Find the visitor
                var visitor = _context.Visitors.Find(assignTagDto.VisitorId);

                if (visitor == null)
                {
                    response.HasError = true;
                    response.Description = $"Visitor with ID {assignTagDto.VisitorId} not found.";
                    return response;
                }

                // Assign tag to the visitor
                visitor.TagNumber = tag.TagNumber;
                tag.IsAvailable = false;
                _context.SaveChanges();

                response.HasError = false;
                response.Description = $"Tag with ID {assignTagDto.TagNumber} assigned to Visitor with ID {assignTagDto.VisitorId} successfully.";

                return response;
            }
            else
            {
                response.HasError = true;
                response.Description = $"Tag with ID {assignTagDto.TagNumber} is not available.";
                return response;
            }
        }

        // Check out a visitor based on the provided CheckOutTagDto.
        public ServiceResponse<string> CheckOutVisitor(CheckOutTagDto checkOutTagDto)
        {
            var response = new ServiceResponse<string>();

            // Find the visitor using the visitorId
            var visitor = _context.Visitors.FirstOrDefault(v => v.Id == checkOutTagDto.VisitorId);

            if (visitor == null)
            {
                response.HasError = true;
                response.Description = $"No visitor found with ID {checkOutTagDto.VisitorId}.";
                return response;
            }

            // Check if the visitor is already checked out
            if (visitor.DepartureTime > DateTime.MinValue)
            {
                response.HasError = true;
                response.Description = $"Visitor with ID {checkOutTagDto.VisitorId} is already checked out.";
                return response;
            }

            // Set departure time to the current time
            visitor.DepartureTime = DateTime.Now;

            // Reassign tag if found
            var tag = _context.Tags.FirstOrDefault(t => t.TagNumber == checkOutTagDto.TagNumber);

            if (tag != null)
            {
                tag.IsAvailable = true;
            }

            _context.SaveChanges();

            response.HasError = false;
            response.Description = $"Visitor with ID {checkOutTagDto.VisitorId} checked out successfully.";

            return response;
        }

        // Get a list of TagDTOs representing the available tags.
        public List<TagDTO> GetTags()
        {
            return _context.Tags
                .Select(tag => new TagDTO
                {
                    TagNumber = tag.TagNumber
                })
                .ToList();
        }
    }
}
