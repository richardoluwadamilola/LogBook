﻿namespace DigiLog.DTOs
{
    public class TagDTO
    {
        public string TagNumber { get; set; } = string.Empty;
        public bool IsDisabled { get; set; }
    }
    public class AssignTagDto
    {
        //Represents the data needed to assign tag to a visitor.
        public long VisitorId { get; set; }
        public string TagNumber { get; set; } = string.Empty;
    }

    public class CheckOutTagDto
    {
        //Represents the data needed to check out a visitor.
        public long VisitorId { get; set; }
        public string TagNumber { get; set; } = string.Empty;
    }
}
