using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DigiLog.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly LogDbContext _context;
        public EmployeeService(LogDbContext context)
        {
            _context = context;
        }

        public ServiceResponse<string> CreateEmployee(EmployeeDTO employeeDto)
        {
            //Create Employee.
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                MiddleName = employeeDto.MiddleName,
                LastName = employeeDto.LastName,
                Department = employeeDto.Department,
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();
            return new ServiceResponse<string> { HasError = false, Description = "Successful" };
        }
        public ServiceResponse<EmployeeDTO> GetEmployeeById(long employeeId)
        {
            //Get Employee by EmployeeId.
            var employee = _context.Employees.Find(employeeId);

            if (employee == null)
                return new ServiceResponse<EmployeeDTO> { HasError = true, Description = "Employee not found" };

            return new ServiceResponse<EmployeeDTO>
            {
                HasError = false,
                Description = "Successful",
                Data = new EmployeeDTO
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    Department = employee.Department,

                }
            };
        }

        public List<EmployeeDTO> GetEmployees()
        {
            //Returns list of Employees.
            return _context.Employees
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Department = e.Department,
                })
                .ToList();
        }

        public List<EmployeeDTO> SearchEmployees(string keyword)
        {
            // Allows keywords for searching employees regardless of the case used(lower/uppercase).
            var query = _context.Employees;

            // Convert the keyword to lowercase for case-insensitive comparison
            keyword = keyword.ToLower();

            return query
                .Where(e =>
                    EF.Functions.Like(e.FirstName.ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.MiddleName.ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.LastName.ToLower(), $"%{keyword}%") ||
                    EF.Functions.Like(e.Department.ToLower(), $"%{keyword}%"))
                .Select(e => new EmployeeDTO
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Department = e.Department,
                })
                .ToList();
        }


    }
}
