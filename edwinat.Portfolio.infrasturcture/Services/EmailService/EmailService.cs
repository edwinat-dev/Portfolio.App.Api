using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using edwinat.Portfolio.Domain.Entities.Model;
using edwinat.Portfolio.infrasturcture.Interfaces;

namespace edwinat.Portfolio.infrasturcture.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly AmazonSimpleEmailServiceClient _emailClient;
        public EmailService(AmazonSimpleEmailServiceClient emailClient)
        {
            _emailClient = emailClient;
        }

        public async Task<bool> SendMailAsync(ContactForm contactForm, string SourceEmail, List<string> DestinationEmails, string Subject, string Body)
        {
            var destination = new Destination()
            {
                ToAddresses = DestinationEmails
            };
            var subject = new Content(Subject);
            var body = new Body()
            {
                Html = new Content(Body)
            };
            var message = new Message(subject, body);
            var request = new SendEmailRequest(SourceEmail, destination, message);

            try
            {
                await _emailClient.SendEmailAsync(request);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
