namespace Restaurant.Core.Application.Dto.Account
{
    public class ServiceResult
    {
        public bool HasError { get; set; }   
        public string? Error { get; set; }
        public dynamic? Data { get; set; }
    }
}
