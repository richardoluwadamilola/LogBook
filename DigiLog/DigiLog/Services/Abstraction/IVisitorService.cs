using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface IVisitorService
    {
        ServiceResponse<string> CreateVisitor(VisitorDTO visitorDto);
        List<VisitorDTO> GetVisitorsByCheckInDate(DateTime date);
        List<EmployeeDTO> GetEmployees();
    }
}
