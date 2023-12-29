using DigiLog.DTOs;

namespace DigiLog.Services.Abstraction
{
    public interface IVisitorService
    {
        VisitorDTO CreateVisitor(VisitorDTO visitorDto);
        List<VisitorDTO> GetVisitorsByCheckInDate(DateTime date);
        List<EmployeeDTO> GetEmployees();
    }
}
