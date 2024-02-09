using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
using System.Transactions;

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
            };

            _context.Tags.Add(tag);
            _context.SaveChanges();
            return new ServiceResponse<string>();
        }



        // Assign a tag to a visitor based on the provided AssignTagDto.
        public ServiceResponse<string> AssignTagToVisitor(AssignTagDto assignTagDto)
        {
            var visitor = _context.Visitors.FirstOrDefault(v => v.Id == assignTagDto.VisitorId);

            if (visitor == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Visitor with ID {assignTagDto.VisitorId} not found.",
                };
            }

            // Check if the tag exists and is available
            var tag = _context.Tags.FirstOrDefault(t => t.TagNumber == assignTagDto.TagNumber && t.IsAvaliable);

            if (tag == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Tag {assignTagDto.TagNumber} is not available.",
                };
            }

            // Assign the tag to the visitor
            visitor.TagNumber = tag.TagNumber;
            tag.IsAvaliable = false;

            _context.SaveChanges();

            return new ServiceResponse<string>
            {
                Data = tag.TagNumber
            };
        }





        // Check out a visitor based on the provided CheckOutTagDto.
        public ServiceResponse<string> CheckOutVisitor(CheckOutTagDto checkOutTagDto)
        {
            var visitor = _context.Visitors.FirstOrDefault(v => v.Id == checkOutTagDto.VisitorId);

            if (visitor == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Visitor with ID {checkOutTagDto.VisitorId} not found.",
                };
            }

            // Check if the visitor has a tag
            if (string.IsNullOrEmpty(visitor.TagNumber))
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Visitor with ID {checkOutTagDto.VisitorId} does not have a tag.",
                };
            }

            // Check if the visitor has already checked out
            if (visitor.DepartureTime != DateTime.MinValue)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Visitor with ID {checkOutTagDto.VisitorId} has already checked out.",
                };
            }

            // Check out the visitor
            visitor.DepartureTime = DateTime.Now;

            // Make the tag available
            var tag = _context.Tags.FirstOrDefault(t => t.TagNumber == visitor.TagNumber);
            if (tag != null)
            {
                tag.IsAvaliable = true;
            }

            _context.SaveChanges();

            return new ServiceResponse<string>();
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
