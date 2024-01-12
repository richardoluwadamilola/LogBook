﻿using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;

namespace DigiLog.Services.Implementation
{
    public class TagService : ITagService
    {
        private readonly LogDbContext _context;


        public TagService(LogDbContext context)
        {
            _context = context;

        }

        public ServiceResponse<string> CreateTag(TagDTO tagDto)
        {
            //Create Tag.
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

        public ServiceResponse<string> AssignTagToVisitor(string tagNumber, long visitorId)
        {
            var response = new ServiceResponse<string>();

            // Find an available tag
            var tag = _context.Tags.FirstOrDefault(t => t.TagNumber == tagNumber);

            if (tag == null)
            {
                response.HasError = true;
                response.Description = $"Tag with ID {tagNumber} not found";
                return response;
            }


            if (tag.IsAvailable)
            {
                var visitor = _context.Visitors.Find(visitorId);
                if (visitor == null)
                {
                    response.HasError = true;
                    response.Description = $"Visitor with ID {visitorId} not found.";
                    return response;

                }
                visitor.TagNumber = tag.TagNumber;
                tag.IsAvailable = false;
                _context.SaveChanges();

                response.HasError = false;
                response.Description = $"Tag with ID {tagNumber} assigned to Visitor with ID {visitorId} successfully.";

                return response;
            }
            else
            {
                response.HasError = true;
                response.Description = $"Tag with ID {tagNumber} is not available.";
                return response;
            }

        }

        public ServiceResponse<string> CheckOutVisitor(string tagNumber)
        {
            var response = new ServiceResponse<string>();

            // Find the visitor using the tagId
            var visitor = _context.Visitors.FirstOrDefault(v => v.TagNumber == tagNumber);

            if (visitor == null)
            {
                response.HasError = true;
                response.Description = $"No visitor found with Tag ID {tagNumber}.";
                return response;
            }

            // Check if the visitor is already checked out
            if (visitor.DepartureTime > DateTime.MinValue)
            {
                response.HasError = true;
                response.Description = $"Visitor with Tag ID {tagNumber} is already checked out.";
                return response;
            }

            // Set departure time to the current time
            visitor.DepartureTime = DateTime.Now;

            // Reassign tag 
            var tag = _context.Tags.FirstOrDefault(t => t.TagNumber == tagNumber);

            if (tag != null)
            {
                tag.IsAvailable = true;
            }

            _context.SaveChanges();

            response.HasError = false;
            response.Description = $"Visitor with Tag ID {tagNumber} checked out successfully.";

            return response;
        }


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
