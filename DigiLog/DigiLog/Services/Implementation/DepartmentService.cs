using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;

namespace DigiLog.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly LogDbContext _context;

        // Constructor to initialize the service with the database context.
        public DepartmentService(LogDbContext context)
        {
            _context = context;
        }

        // Create a department.
        public ServiceResponse<string> CreateDepartment(DepartmentDTO departmentDto)
        {
            // Create Department.
            var department = new Department
            {
                DepartmentId = departmentDto.DepartmentId,
                DepartmentName = departmentDto.DepartmentName,
                DateCreated = DateTime.Now,
            };

            _context.Departments.Add(department);
            _context.SaveChanges();
            return new ServiceResponse<string>();
        }

        // Get all departments.
        public List<DepartmentDTO> GetDepartments()
        {
            return _context.Departments
                .Select(department => new DepartmentDTO
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName,
                })
                .ToList();
        }

        //Get a department by its ID.
        public ServiceResponse<DepartmentDTO> GetDepartmentById(long departmentId)
        {
            // Get the department.
            var department = _context.Departments.Find(departmentId);

            // If the department does not exist, return an error response.
            if (department == null)
            {
                return new ServiceResponse<DepartmentDTO>
                {
                    HasError = true,
                    Description = $"Department with ID {departmentId} not found.",
                };
            }

            // Return a success response.
            return new ServiceResponse<DepartmentDTO>
            {
                Data = new DepartmentDTO
                {
                    DepartmentId = department.DepartmentId,
                    DepartmentName = department.DepartmentName,
                },
            };
        }

        //Update a department.
        public ServiceResponse<string> UpdateDepartment(DepartmentDTO departmentDto)
        {
            // Get the department.
            var department = _context.Departments.Find(departmentDto.DepartmentId);

            // If the department does not exist, return an error response.
            if (department == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Department with ID {departmentDto.DepartmentId} not found.",
                };
            }

            // Update the department.
            department.DepartmentName = departmentDto.DepartmentName;
            department.DateModified = DateTime.Now;

            // Save changes to the database.
            _context.SaveChanges();

            // Return a success response.
            return new ServiceResponse<string>();
        }

        // Delete a department.
        public ServiceResponse<string> DeleteDepartment(long departmentId)
        {
            // Get the department.
            var department = _context.Departments.Find(departmentId);

            // If the department does not exist, return an error response.
            if (department == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = $"Department with ID {departmentId} not found.",
                };
            }

            // Delete the department.
            _context.Departments.Remove(department);
            _context.SaveChanges();

            // Return a success response.
            return new ServiceResponse<string>();
        }
    }
}
