using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace DigiLog.Services.Implementation
{
    // Service responsible for handling operations related to employees.
    public class EmployeeService : IEmployeeService
    {
        private readonly LogDbContext _context;

        // Constructor to initialize the service with the database context.
        public EmployeeService(LogDbContext context)
        {
            _context = context;
        }

        // Create a new employee based on the provided EmployeeDTO.
        public ServiceResponse<string> CreateEmployee(EmployeeDTO employeeDto)
        {

            // Check if the department exists.
            var department = _context.Departments
                .FirstOrDefault(d => d.DepartmentName.ToLower() == employeeDto.Department.ToLower());

            // If the department does not exist, return an error response.
            if (department == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = "Department does not exist. Please select a valid department.",
                };
            }

            // Create Employee.
            var employee = new Employee
            {
                EmployeeNumber = employeeDto.EmployeeNumber,
                FirstName = employeeDto.FirstName,
                MiddleName = employeeDto.MiddleName,
                LastName = employeeDto.LastName,
                DepartmentId = department.DepartmentId,
                DateCreated = DateTime.Now,
                //DateModified = DateTime.Now,
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            // Return a success response.
            return new ServiceResponse<string>();
        }

        // Get an employee by their employee number.
        public ServiceResponse<EmployeeDTO> GetEmployeeById(string employeeNumber)
        {
            // Get Employee by EmployeeNumber.
            var employee = _context.Employees
                .Include(e => e.Department) // Include Department for the relationship
                .FirstOrDefault(e => e.EmployeeNumber == employeeNumber);


            // Return an empty response if employee is not found.
            if (employee == null)
                return new ServiceResponse<EmployeeDTO>();

            // Return a response with the employee data.
            return new ServiceResponse<EmployeeDTO>
            {
                HasError = false,
                Description = "Successful",
                Data = new EmployeeDTO
                {
                    EmployeeNumber = employee.EmployeeNumber,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    Department = employee.Department.DepartmentName,
                }
            };
        }

        // Get a list of EmployeeDTOs representing all employees.
        public List<EmployeeDTO> GetEmployees()
        {
            // Returns a list of Employees.
            return _context.Employees
                .Select(e => new EmployeeDTO
                {
                    EmployeeNumber = e.EmployeeNumber,
                    FirstName = e.FirstName,
                    MiddleName = e.MiddleName,
                    LastName = e.LastName,
                    Department = e.Department.DepartmentName,
                })
                .ToList();
        }

        // Update the details of an employee
        public ServiceResponse<string> UpdateEmployee(EmployeeDTO employeeDto)
        {
            // Check if the department exists.
            var department = _context.Departments
                .FirstOrDefault(d => d.DepartmentName.ToLower() == employeeDto.Department.ToLower());

            // If the department does not exist, return an error response.
            if (department == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = "Department does not exist. Please select a valid department.",
                };
            }

            // Get the employee to be updated.
            var employee = _context.Employees
                .FirstOrDefault(e => e.EmployeeNumber == employeeDto.EmployeeNumber);

            // If the employee does not exist, return an error response.
            if (employee == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = "Employee does not exist. Please select a valid employee.",
                };
            }

            // Update the employee details.
            employee.FirstName = employeeDto.FirstName;
            employee.MiddleName = employeeDto.MiddleName;
            employee.LastName = employeeDto.LastName;
            employee.DepartmentId = department.DepartmentId;
            employee.DateModified = DateTime.Now;

            // Save the changes to the database.
            _context.SaveChanges();

            // Return a success response.
            return new ServiceResponse<string>();
        }

        // Delete an employee from the database.
        public ServiceResponse<string> DeleteEmployee(string employeeNumber)
        {
            // Find the employee to be deleted.
            var employee = _context.Employees
                .FirstOrDefault(e => e.EmployeeNumber == employeeNumber);

            // If the employee does not exist, return an error response.
            if (employee == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = "Employee does not exist. Please select a valid employee.",
                };
            }

            // Delete the employee.
            _context.Employees.Remove(employee);

            // Save the changes to the database.
            _context.SaveChanges();

            // Return a success response.
            return new ServiceResponse<string>();
        }

        // Search for employees based on a keyword.
        //public List<EmployeeDTO> SearchEmployees(string keyword)
        //{
        //    // Allows keywords for searching employees regardless of the case used (lower/uppercase).
        //    var query = _context.Employees;

        //    // Convert the keyword to lowercase for case-insensitive comparison.
        //    keyword = keyword.ToLower();

        //    // Perform a search based on the provided keyword.
        //    return query
        //        .Where(e =>
        //            EF.Functions.Like(e.FirstName.ToLower(), $"%{keyword}%") ||
        //            EF.Functions.Like(e.MiddleName.ToLower(), $"%{keyword}%") ||
        //            EF.Functions.Like(e.LastName.ToLower(), $"%{keyword}%") ||
        //            EF.Functions.Like(e.Department.ToLower(), $"%{keyword}%"))
        //        .Select(e => new EmployeeDTO
        //        {
        //            EmployeeNumber = e.EmployeeNumber,
        //            FirstName = e.FirstName,
        //            MiddleName = e.MiddleName,
        //            LastName = e.LastName,
        //            Department = e.Department,
        //        })
        //        .ToList();
        //}
    }
}
