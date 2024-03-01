namespace DigiLog.DTOs
{
    public class SearchRequestDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string TagNumber { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
