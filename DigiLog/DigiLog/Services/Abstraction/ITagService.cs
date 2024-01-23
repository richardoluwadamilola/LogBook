﻿using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface ITagService
    {
        ServiceResponse<string> CreateTag(TagDTO tagDto);
        List<TagDTO> GetTags();
        ServiceResponse<bool>IsTagAvailable(string tagNumber);
        ServiceResponse<string> AssignTagToVisitor(AssignTagDto assignTagDto);
        ServiceResponse<string> CheckOutVisitor(CheckOutTagDto checkOutTagDto);
        ServiceResponse<string> DeleteTag(string tagNumber);

       
    }
}
