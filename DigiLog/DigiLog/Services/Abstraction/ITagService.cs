using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface ITagService
    {
        //int GenerateTagNumber();
        List<TagDTO> GetTags();
        ServiceResponse<string> AssignTagToVisitor(long tagId, long visitorId);
        ServiceResponse<string> CheckOutVisitor(long tagId);

       
    }
}
