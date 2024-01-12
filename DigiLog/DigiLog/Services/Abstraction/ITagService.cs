using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface ITagService
    {
        ServiceResponse<string> CreateTag(TagDTO tagDto);
        List<TagDTO> GetTags();
        ServiceResponse<string> AssignTagToVisitor(string tagNumber, long visitorId);
        ServiceResponse<string> CheckOutVisitor(string tagNumber);

       
    }
}
