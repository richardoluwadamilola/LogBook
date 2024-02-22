using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface IReasonForVisitService
    {
        ServiceResponse<string> CreateReasonForVisit(ReasonForVisitDTO reasonForVisitDto);
        List<ReasonForVisitDTO> GetReasonsForVisit();
    }
}
