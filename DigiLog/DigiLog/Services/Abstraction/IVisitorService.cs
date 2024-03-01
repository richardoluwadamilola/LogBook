using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface IVisitorService
    {
        ServiceResponse<string> CreateVisitor(VisitorDTO visitorDto);
        List<VisitorDTO> GetVisitors();
        List<VisitorDTO> GetVisitorsByTagNumber(string tagNumber);
        List<VisitorDTO> SearchVisitors(SearchRequestDTO searchRequetDTO);


    }
}
