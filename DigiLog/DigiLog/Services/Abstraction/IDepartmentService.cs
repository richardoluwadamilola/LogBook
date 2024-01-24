using DigiLog.DTOs;
using DigiLog.Models.ResponseModels;

namespace DigiLog.Services.Abstraction
{
    public interface IDepartmentService
    {
        ServiceResponse<string> CreateDepartment(CreateDepartmentDTO createDepartmentDto);
        List<DepartmentDTO> GetDepartments();
        ServiceResponse<DepartmentDTO> GetDepartmentById(long departmentId);
        ServiceResponse<string> UpdateDepartment(DepartmentDTO departmentDto);
        ServiceResponse<string> DeleteDepartment(long departmentId);
    }
}
