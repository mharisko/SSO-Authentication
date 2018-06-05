
namespace Unity.Auth.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns>
        /// Asynchronous operation.
        /// </returns>
        Task SendEmailAsync(EmailModel mail);        
    }
}
