using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface IEmployeeService
    {
        ServiceResponse<string> CreateEmployee(EmployeeDTO employeeDto);
        List<EmployeeDTO> GetEmployees();
        //List<EmployeeDTO> SearchEmployees(string keyword);
        ServiceResponse<EmployeeDTO> GetEmployeeById(string employeeNumber);
        ServiceResponse<string> UpdateEmployee(EmployeeDTO employeeDto);
        ServiceResponse<string> DeleteEmployee(string employeeNumber);
    }
}
