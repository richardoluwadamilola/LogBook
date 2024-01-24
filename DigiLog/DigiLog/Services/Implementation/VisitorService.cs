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

            var photo = new Photo
            {
                PhotoData = GetByteFromImageString(visitorDto.Photo)
            };


            var visitor = new Visitor
            {
                //Create Visitor.
                FullName = visitorDto.FullName,
                ContactAddress = visitorDto.ContactAddress,
                PhoneNumber = visitorDto.PhoneNumber,
                EmployeeNumber = visitorDto.EmployeeNumber,
                ReasonForVisit = (ReasonForVisit)visitorDto.ReasonForVisit,
                ReasonForVisitDescription = visitorDto.ReasonForVisitDescription,
                Photo = photo,
                ArrivalTime = DateTime.Now,
                DateCreated = DateTime.Now,

            };

            //Add visitor to the database.
            _context.Visitors.Add(visitor);
            //Save changes to the database.
            _context.SaveChanges();

            return new ServiceResponse<string>();
        }

        public List<VisitorDTO> GetVisitorsByCheckInDate(DateTime date)
        {
            //Get Visitors by check in date.
           var visitors = _context.Visitors
                .Include(v => v.Photo)
                .Where(v => v.ArrivalTime.Date ==  date.Date)
                .Select(visitor => new VisitorDTO
                {
                    Id = visitor.Id,
                    FullName = visitor.FullName,
                    ContactAddress = visitor.ContactAddress,
                    PhoneNumber = visitor.PhoneNumber,
                    EmployeeNumber = visitor.EmployeeNumber,
                    ReasonForVisit = (int)visitor.ReasonForVisit,
                    //ReasonForVisitEnum = visitor.ReasonForVisit,
                    ReasonForVisitDescription = visitor.ReasonForVisitDescription,
                    ArrivalTime = visitor.ArrivalTime,
                    DepartureTime = visitor.DepartureTime,

                    // Convert PhotoData to a base64-encoded string
                    Photo = GetImageStringFromByte(visitor.Photo.PhotoData)

                })
            .ToList();

            return visitors;
        }


        // Get Visitors by Employee Number.
        public List<VisitorDTO> GetVisitorsByEmployeeNumber(string employeeNumber)
        {
            //Get Visitors by Employee Number.
            var visitors = _context.Visitors
                .Include(v => v.Photo)
                .Where(v => v.EmployeeNumber == employeeNumber)
                .Select(visitor => new VisitorDTO
                {
                    Id = visitor.Id,
                    FullName = visitor.FullName,
                    ContactAddress = visitor.ContactAddress,
                    PhoneNumber = visitor.PhoneNumber,
                    EmployeeNumber = visitor.EmployeeNumber,
                    ReasonForVisit = (int)visitor.ReasonForVisit,
                    //ReasonForVisitEnum = visitor.ReasonForVisit,
                    ReasonForVisitDescription = visitor.ReasonForVisitDescription,
                    ArrivalTime = visitor.ArrivalTime,
                    DepartureTime = visitor.DepartureTime,

                    // Convert PhotoData to a base64-encoded string
                    Photo = GetImageStringFromByte(visitor.Photo.PhotoData)

                })
            .ToList();

            return visitors;
        }

        // Get Visitors by Tag Number.
        public List<VisitorDTO> GetVisitorsByTagNumber(string tagNumber)
        {
            //Get Visitors by Tag Number.
            var visitors = _context.Visitors
                .Include(v => v.Photo)
                .Where(v => v.TagNumber == tagNumber)
                .Select(visitor => new VisitorDTO
                {
                    Id = visitor.Id,
                    FullName = visitor.FullName,
                    ContactAddress = visitor.ContactAddress,
                    PhoneNumber = visitor.PhoneNumber,
                    EmployeeNumber = visitor.EmployeeNumber,
                    ReasonForVisit = (int)visitor.ReasonForVisit,
                    //ReasonForVisitEnum = visitor.ReasonForVisit,
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
    }
}
