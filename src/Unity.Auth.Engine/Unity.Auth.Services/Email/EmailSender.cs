
namespace Unity.Auth.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="optionsAccessor">The options accessor.</param>
        public EmailSender(IOptions<EmailSettings> optionsAccessor)
        {
            this.Options = optionsAccessor.Value;
        }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <value>
        /// The options.
        /// </value>
        public EmailSettings Options { get; }  // set only via Secret Manager

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns>
        /// Asynchronous operation.
        /// </returns>
        public async Task SendEmailAsync(EmailModel mail)
        {
            var client = new SendGridClient(this.Options.ApiKey);
            var from = new EmailAddress(this.Options.FromAddress, this.Options.FromName);
            var to = new EmailAddress(mail.ToAddeess, mail.ToName);
            var plainTextContent = mail.Body;
            var htmlContent = $"<strong>{mail.Body}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, mail.Subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
