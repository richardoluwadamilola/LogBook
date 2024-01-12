using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;

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
            var visitor = new Visitor
            {
                //Create Visitor.
                FirstName = visitorDto.FirstName,
                MiddleName = visitorDto.MiddleName,
                LastName = visitorDto.LastName,
                ContactAddress = visitorDto.ContactAddress,
                PhoneNumber = visitorDto.PhoneNumber,
                EmployeeNumber = visitorDto.EmployeeNumber,
                ReasonForVisit = (ReasonForVisit)visitorDto.ReasonForVisit,
                ReasonForVisitDescription = visitorDto.ReasonForVisitDescription,
                //Photo = visitorDto.Photo
                ArrivalTime = DateTime.Now,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Deleted = false
            };

            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            return new ServiceResponse<string>();
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
                    EmployeeNumber = visitor.EmployeeNumber,
                    ReasonForVisit = visitor.ReasonForVisit,
                    ReasonForVisitDescription = visitor.ReasonForVisitDescription,
                    //Photo = visitor.Photo

                })
            .ToList();

            return visitors;
        }
    }
}
