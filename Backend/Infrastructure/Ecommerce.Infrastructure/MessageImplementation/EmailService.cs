using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Email;
using FluentEmail.Core;
using Microsoft.Extensions.Options;

namespace Ecommerce.Infrastructure.MessageImplementation;

public class EmailService(IFluentEmail fluentEmail, IOptions<EmailFluentSettings> emailFluentSettings)
    : IEmailService
{
    private readonly IFluentEmail _fluentEmail = fluentEmail;
    private readonly EmailFluentSettings _emailFluentSettings = emailFluentSettings.Value;

    public async Task<bool> SendEmailAsync(EmailMessage email, string token)
    {
        var htmlContent = $"{email.Body} {_emailFluentSettings.BaseUrlClient}/password/reset/{token}";
        var result = await _fluentEmail
            .To(email.To)
            .Subject(email.Subject)
            .Body(htmlContent)
            .SendAsync();
        
        return result.Successful;
    }
}