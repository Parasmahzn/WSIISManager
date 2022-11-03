namespace WSIISManager.Services;

public interface IEmailService
{
    Task SendEmail(Email email);
}
