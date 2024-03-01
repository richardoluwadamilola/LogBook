using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;
using System.Net.NetworkInformation;

namespace DigiLog.Services.Implementation
{
    public class VisitorService : IVisitorService
    {
        private readonly IEmployeeService _employeeService;
        private readonly LogDbContext _context;
        private readonly ITagService _tagService;

        public VisitorService(LogDbContext context, IEmployeeService employeeService, ITagService tagService)
        {
            _context = context;
            _employeeService = employeeService;
            _tagService = tagService;
        }

        public ServiceResponse<string> CreateVisitor(VisitorDTO visitorDto)
        {
            var reasonForVisit = _context.ReasonForVisit.FirstOrDefault(r => r.Reason == visitorDto.ReasonForVisit);
            if (reasonForVisit == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = "Reason for visit not found",
                };
            }

            var photo = new Photo
            {
                PhotoData = GetByteFromImageString(visitorDto.Photo)
            };

            var employee = _context.Employees
                                  .Include(e => e.Department) // Eager loading of Department
                                  .FirstOrDefault(e => e.EmployeeNumber == visitorDto.EmployeeNumber);
            if (employee == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = "Employee not found",
                };
            }

            var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == employee.DepartmentId);
            if (department == null)
            {
                return new ServiceResponse<string>
                {
                    HasError = true,
                    Description = "Department not found",
                };
            }

            // Concatenate first name, middle name, and last name of the employee
            var employeeName = $"{employee.FirstName} {employee.MiddleName} {employee.LastName}";

            // Create a Visitor object
            var visitor = new Visitor
            {
                FullName = visitorDto.FullName,
                ContactAddress = visitorDto.ContactAddress,
                PhoneNumber = visitorDto.PhoneNumber,
                EmailAddress = visitorDto.EmailAddress,
                EmployeeNumber = visitorDto.EmployeeNumber,
                EmployeeName = employeeName,
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                ReasonForVisit = reasonForVisit,
                ReasonForVisitDescription = visitorDto.ReasonForVisitDescription,
                Photo = photo,
                ArrivalTime = DateTime.Now,
                DepartureTime = DateTime.MinValue,
                DateCreated = DateTime.Now,
            };

            // Add photo to the database
            _context.Photos.Add(photo);

            // Add visitor to the database
            _context.Visitors.Add(visitor);

            // Save changes to the database
            _context.SaveChanges();

            return new ServiceResponse<string>();
        }



        public List<VisitorDTO> GetVisitors()
        {
            return _context.Visitors
                .Include(v => v.Photo)
                .Include(v => v.Employee)
                .Include(v => v.ReasonForVisit)
                .Include(v => v.Department)
                .Include(v => v.Tag)
                .Select(visitor => new VisitorDTO
                {
                    Id = visitor.Id,
                    FullName = visitor.FullName,
                    ContactAddress = visitor.ContactAddress,
                    PhoneNumber = visitor.PhoneNumber,
                    EmailAddress = visitor.EmailAddress,
                    EmployeeNumber = visitor.EmployeeNumber,
                    EmployeeName = $"{visitor.Employee.FirstName} {visitor.Employee.LastName}",
                    DepartmentName = visitor.Department.DepartmentName,
                    TagNumber = visitor.Tag.TagNumber,
                    ReasonForVisit = visitor.ReasonForVisit.Reason,
                    ReasonForVisitDescription = visitor.ReasonForVisitDescription,
                    ArrivalTime = visitor.ArrivalTime,
                    TagAssignedDateTime = visitor.TagAssignedDateTime,
                    DepartureTime = visitor.DepartureTime,

                    // Convert PhotoData to a base64-encoded string
                    Photo = GetImageStringFromByte(visitor.Photo.PhotoData)

                })
                .ToList();
        }




        // Get Visitors by Tag Number.
        public List<VisitorDTO> GetVisitorsByTagNumber(string tagNumber)
        {
            //Get Visitors by Tag Number.
            var visitors = _context.Visitors
                .Include(v => v.Photo)
                .Include(v => v.Employee)
                .Include(v => v.ReasonForVisit)
                .Include(v => v.Department)
                .Include(v => v.Tag)
                .Where(v => v.TagNumber == tagNumber && v.DepartureTime == DateTime.MinValue)
                .Select(visitor => new VisitorDTO
                {
                    Id = visitor.Id,
                    FullName = visitor.FullName,
                    ContactAddress = visitor.ContactAddress,
                    PhoneNumber = visitor.PhoneNumber,
                    EmailAddress = visitor.EmailAddress,
                    EmployeeNumber = visitor.EmployeeNumber,
                    EmployeeName = $"{visitor.Employee.FirstName} {visitor.Employee.LastName}",
                    DepartmentName = visitor.Department.DepartmentName,
                    TagNumber = visitor.Tag.TagNumber,
                    ReasonForVisit = visitor.ReasonForVisit.Reason,
                    ReasonForVisitDescription = visitor.ReasonForVisitDescription,
                    ArrivalTime = visitor.ArrivalTime,
                    DepartureTime = visitor.DepartureTime,

                    // Convert PhotoData to a base64-encoded string
                    Photo = GetImageStringFromByte(visitor.Photo.PhotoData)

                })
            .ToList();

            return visitors;
        }

        public static byte[] GetByteFromImageString(string image)
        {
            // Remove the image format prefix ("data:image/png;base64,") to extract the base64-encoded string
            image = image.Replace("data:image/png;base64,", "");
            // Convert the base64-encoded string to a byte array
            byte[] imageByte = Convert.FromBase64String(image);
            // Return the byte array
            return imageByte;

        }

        public static string GetImageStringFromByte(byte[] image)
        {
            // Convert the byte array to a base64-encoded string
            string imageString = Convert.ToBase64String(image);
            // Return the base64-encoded string with the image format prefix
            return "data:image/png;base64," + imageString;
        }

        public List<VisitorDTO> SearchVisitors(SearchRequestDTO searchRequestDTO)
        {
            var query = _context.Visitors
                
                .AsQueryable();

           if (!string.IsNullOrEmpty(searchRequestDTO.FullName))
            {
                query = query.Where(v => v.FullName == searchRequestDTO.FullName);
            }

           if (!string.IsNullOrEmpty(searchRequestDTO.EmployeeNumber))
            {
                query = query.Where(v => v.EmployeeNumber == searchRequestDTO.EmployeeNumber);
            }

           if (!string.IsNullOrEmpty(searchRequestDTO.TagNumber))
            {
                 query = query.Where(v => v.TagNumber == searchRequestDTO.TagNumber);
            }

           if (searchRequestDTO.StartDate != null && searchRequestDTO.EndDate != null)
            {
                query = query.Where(v => v.ArrivalTime >= searchRequestDTO.StartDate && v.ArrivalTime <= searchRequestDTO.EndDate);
            }

           query = query
                .Include(v => v.Photo)
                .Include(v => v.Employee)
                .Include(v => v.ReasonForVisit)
                .Include(v => v.Department)
                .Include(v => v.Tag);

            var visitors = query.Select(visitor => new VisitorDTO
            {
                Id = visitor.Id,
                FullName = visitor.FullName,
                ContactAddress = visitor.ContactAddress,
                PhoneNumber = visitor.PhoneNumber,
                EmailAddress = visitor.EmailAddress,
                EmployeeNumber = visitor.EmployeeNumber,
                EmployeeName = $"{visitor.Employee.FirstName} {visitor.Employee.LastName}",
                DepartmentName = visitor.Department.DepartmentName,
                TagNumber = visitor.Tag.TagNumber,
                ReasonForVisit = visitor.ReasonForVisit.Reason,
                ReasonForVisitDescription = visitor.ReasonForVisitDescription,
                ArrivalTime = visitor.ArrivalTime,
                DepartureTime = visitor.DepartureTime,

                // Convert PhotoData to a base64-encoded string
                Photo = GetImageStringFromByte(visitor.Photo.PhotoData)

            })
                .ToList();
            return visitors;
        }   
    }
}
