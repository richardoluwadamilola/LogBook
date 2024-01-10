using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
using Microsoft.Extensions.Logging;

namespace DigiLog.Services.Implementation
{
    public class VisitorService : IVisitorService
    {
        private readonly IEmployeeService _employeeService;
        private readonly LogDbContext _context;
        private readonly ILogger<VisitorService> _logger;
        private readonly ITagService _tagService;

        public VisitorService(LogDbContext context, IEmployeeService employeeService, ITagService tagService)
        {
            _context = context;
            _employeeService = employeeService;
            _tagService = tagService;
        }

        public ServiceResponse<string> CreateVisitor(VisitorDTO visitorDto)
        {
            var visitor = new Visitor
            {
                FirstName = visitorDto.FirstName,
                MiddleName = visitorDto.MiddleName,
                LastName = visitorDto.LastName,
                ContactAddress = visitorDto.ContactAddress,
                PhoneNumber = visitorDto.PhoneNumber,
                EmployeeId = visitorDto.EmployeeId,
                ReasonForVisit = visitorDto.ReasonForVisit,
                ReasonForVisitDescription = visitorDto.ReasonForVisitDescription,
                Photo = visitorDto.Photo
            };

            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            return new ServiceResponse<string> { HasError = false, Description = "Successful" };
        }
        public List<EmployeeDTO> GetEmployees()
        {
            return _employeeService.GetEmployees();
        }
        public List<VisitorDTO> GetVisitorsByCheckInDate(DateTime date)
        {
           var visitors = _context.Visitors
                .Where(v => v.ArrivalTime.Date ==  date.Date)
                .Select(visitor => new VisitorDTO
                {
                    Id = visitor.Id,
                    FirstName = visitor.FirstName,
                    MiddleName= visitor.MiddleName,
                    LastName = visitor.LastName,
                    ContactAddress = visitor.ContactAddress,
                    PhoneNumber = visitor.PhoneNumber,
                    EmployeeId = visitor.EmployeeId,
                    ReasonForVisit = visitor.ReasonForVisit,
                    ReasonForVisitDescription = visitor.ReasonForVisitDescription,
                    Photo = visitor.Photo

                })
            .ToList();

            return visitors;
        }
    }
}
