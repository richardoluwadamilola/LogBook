using DigiLog.DTOs;

namespace DigiLog.Services.Abstraction
{
    public interface IEmployeeService
    {
        void CreateEmployee(EmployeeDTO employeeDto);
        List<EmployeeDTO> GetEmployees();
        List<EmployeeDTO> SearchEmployees(string keyword);
        EmployeeDTO GetEmployeeById(int employeeId);
    }
}
