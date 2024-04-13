using Restaurant.Core.Application.Dto.Email;
using Restaurant.Core.Domain.Settings;

namespace Restaurant.Core.Application.Interfaces.IServices
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync (EmailRequest request);
    }
}
