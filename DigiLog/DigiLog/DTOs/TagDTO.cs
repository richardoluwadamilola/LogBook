namespace DigiLog.DTOs
{
    public class TagDTO
    {
        internal int TagID { get; set; }
        public string? TagNumber { get; set; }
    }
    public class AssignTagDto
    {
        //Represents the data needed to assign tag to a visitor.
        public int TagID { get; set; }
        public int VisitorId { get; set; }
    }

    public class CheckOutTagDto
    {
        //Represents the data needed to check out a visitor.
        public int TagID { get; set; }
        public int VisitorId { get; set; }
    }
}
