using DigiLog.Data;
using DigiLog.DTOs;
using DigiLog.Models;
using DigiLog.Models.ResponseModels;
using DigiLog.Services.Abstraction;

namespace DigiLog.Services.Implementation
{
    public class ReasonForVisitService : IReasonForVisitService
    {
        private readonly LogDbContext _context;

        public ReasonForVisitService(LogDbContext context)
        {
            _context = context;
        }

        //Create a new reason  for visit.
        public ServiceResponse<string> CreateReasonForVisit(ReasonForVisitDTO reasonForVisitDto)
        {
            var reasonForVisit = new ReasonForVisit
            {
                Reason = reasonForVisitDto.Reason
            };

            _context.ReasonForVisit.Add(reasonForVisit);
            _context.SaveChanges();
            return new ServiceResponse<string>();
        }

        //Get all reasons for visit.
        public List<ReasonForVisitDTO> GetReasonsForVisit()
        {
           return _context.ReasonForVisit.Select(r => new ReasonForVisitDTO
           {
               Id = r.Id,
               Reason = r.Reason
            }).ToList();
        }

    }
}
