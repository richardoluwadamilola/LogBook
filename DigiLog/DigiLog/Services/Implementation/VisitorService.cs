using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;
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
                Photo = GetByteFromImageString(visitorDto.Photo),
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
                    ReasonForVisit = (int)visitor.ReasonForVisit,
                    ReasonForVisitDescription = visitor.ReasonForVisitDescription,
                    Photo = GetImageStringFromByte(visitor.Photo)

                })
            .ToList();

            return visitors;
        }

        public static byte[] GetByteFromImageString(string image)
        {
            image = image.Replace("data:image/png;base64,", "");
            byte[] imageByte = Convert.FromBase64String(image);
            return imageByte;
            
        }

        public static string GetImageStringFromByte(byte[] image)
        {
            string imageString = Convert.ToBase64String(image);
            return "data:image/png;base64," + imageString;
        }
    }
}
