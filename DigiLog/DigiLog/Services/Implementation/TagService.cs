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
            var tag = _context.Tags.FirstOrDefault(t => t.TagNumber == assignTagDto.TagNumber && t.IsAvailable);

            if (tag == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Tag {assignTagDto.TagNumber} is not available.",
                };
            }

            // Check if the visitor has already been assigned a tag
            if (visitor.TagNumber != null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Visitor with ID {assignTagDto.VisitorId} has already been assigned a tag.",
                };
            }

            // Check if visitor has not checked out
            if (visitor.DepartureTime != DateTime.MinValue)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Visitor with ID {assignTagDto.VisitorId} has already checked out.",
                };
            }

            // Assign the tag to the visitor
            visitor.TagNumber = tag.TagNumber;
            tag.IsAvailable = false;
            visitor.TagAssignedDateTime = DateTime.Now;

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

            // Check if the visitor has a tag with the provided tag number
            if (visitor.TagNumber != checkOutTagDto.TagNumber)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Visitor with ID {checkOutTagDto.VisitorId} does not have tag {checkOutTagDto.TagNumber}.",
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
                tag.IsAvailable = true;
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
                    TagNumber = tag.TagNumber,
                    IsDisabled = tag.IsDisabled
                })
                .ToList();
        }

        // Disable a tag based on the provided tag number.
        public ServiceResponse<string> DisableTag(string tagNumber)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.TagNumber == tagNumber);

            if (tag == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Tag {tagNumber} not found.",
                };
            }

            //Check if the tag is already disabled
            if (tag.IsDisabled)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Tag {tagNumber} is already disabled.",
                };
            }
            
            // Disable the tag
            tag.IsAvailable = false;
            tag.IsDisabled = true;
            _context.SaveChanges();

            return new ServiceResponse<string>();
        }


    }
}
