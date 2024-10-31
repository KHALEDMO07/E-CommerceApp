namespace E_Commerce.Email_Seneding
{
    public interface IEmailSender
    {
        Task SendEmailAsync( string subject,string toEmail  ,  string message);
    }
}
