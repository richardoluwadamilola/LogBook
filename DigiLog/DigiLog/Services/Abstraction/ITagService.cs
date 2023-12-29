using DigiLog.DTOs;

namespace DigiLog.Services.Abstraction
{
    public interface ITagService
    {
        //int GenerateTagNumber();
        List<TagDTO> GetTags();
        void AssignTagToVisitor(int tagId, int visitorId);
        void CheckOutVisitor(int tagId);
    }
}
