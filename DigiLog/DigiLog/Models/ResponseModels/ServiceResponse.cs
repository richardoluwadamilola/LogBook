using DigiLog.DTOs;

namespace DigiLog.Models.ResponseModels
{
    public class ServiceResponse<T>
    {
        public bool HasError { get; set; }
        public string? Description { get; set; }
        public T? Data { get; internal set; }
    }
}