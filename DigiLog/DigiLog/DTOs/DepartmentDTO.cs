﻿namespace DigiLog.DTOs
{
    public class DepartmentDTO
    {
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }

    public class CreateDepartmentDTO
    {
        public string DepartmentName { get; set; } = string.Empty;
    }
}
