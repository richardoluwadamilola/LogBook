using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Services.Abstraction;
using Microsoft.Extensions.Logging;
using static DigiLog.ErrorLog;

namespace DigiLog.Services.Implementation
{
    public class VisitorService : IVisitorService
    {
        private readonly IEmployeeService _employeeService;
        private readonly LogDbContext _context;
        private readonly ILogger<VisitorService> _logger;
        private readonly ITagService _tagService;

        public VisitorService(LogDbContext context, ILogger<VisitorService> logger, IEmployeeService employeeService, ITagService tagService)
        {
            _context = context;
            _logger = logger;
            _employeeService = employeeService;
            _tagService = tagService;
        }

        public VisitorDTO CreateVisitor(VisitorDTO visitorDto)
        {
            var visitor = new Visitor
            {

                //Create a new Visitor entity
                FirstName = visitorDto.FirstName,
                MiddleName = visitorDto.MiddleName,
                LastName = visitorDto.LastName,
                ContactAddress = visitorDto.ContactAddress,
                PhoneNumber = visitorDto.PhoneNumber,
                ReasonForVisit = visitorDto.ReasonForVisit,
                ReasonForVisitDescription = visitorDto.ReasonForVisitDescription,
               // Photo = visitorDto.Photo,
                ArrivalTime = DateTime.Now,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Deleted = false
            };

            //Adds the visitor to the database.
            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            //Update DTO with the saved ID.
            visitorDto.Id = visitor.Id;
            return visitorDto;
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
                    ReasonForVisit = visitor.ReasonForVisit,
                    ReasonForVisitDescription = visitor.ReasonForVisitDescription,
                    Photo = visitor.Photo

                })
            .ToList();

            return visitors;
        }
    }
}
