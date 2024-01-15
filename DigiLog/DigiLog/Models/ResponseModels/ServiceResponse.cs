using DigiLog.DTOs;

namespace DigiLog.Models.ResponseModels
{
    public class ServiceResponse<T>
    {
        public bool HasError { get; set; } = false;
        public string? Description { get; set; } = "Successfull";
        public T? Data { get; set; }
    }
}