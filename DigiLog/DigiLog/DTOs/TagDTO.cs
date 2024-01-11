namespace DigiLog.DTOs
{
    public class TagDTO
    {
        public long TagID { get; set; }
        public string? TagNumber { get; set; }
    }
    public class AssignTagDto
    {
        //Represents the data needed to assign tag to a visitor.
        public long TagID { get; set; }
        public long VisitorId { get; set; }
    }

    public class CheckOutTagDto
    {
        //Represents the data needed to check out a visitor.
        public long TagID { get; set; }
        public long VisitorId { get; set; }
    }
}
