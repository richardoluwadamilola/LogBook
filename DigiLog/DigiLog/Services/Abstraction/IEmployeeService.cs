using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface IEmployeeService
    {
        ServiceResponse<string> CreateEmployee(EmployeeDTO employeeDto);
        List<EmployeeDTO> GetEmployees();
        List<EmployeeDTO> SearchEmployees(string keyword);
        ServiceResponse<EmployeeDTO> GetEmployeeById(long employeeId);
    }
}
