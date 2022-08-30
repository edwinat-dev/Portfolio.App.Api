using Amazon.SimpleEmail;
using edwinat.Portfolio.App.Api.Model;
using edwinat.Portfolio.Domain.Interfaces;
using edwinat.Portfolio.infrasturcture;
using edwinat.Portfolio.infrasturcture.Interfaces;
using edwinat.Portfolio.infrasturcture.Services.EmailService;

namespace edwinat.Portfolio.Domain.Services
{
    public class ContactFormService : IContactFormService
    {
        private readonly IEmailService emailService = new EmailService(new AmazonSimpleEmailServiceClient(Amazon.RegionEndpoint.APSouth1));
        public async Task<ContactFormAlertResponse> ContactFormAlertAsync(ContactFormAlertRequest request)
        {
            var entity = new Entities.Model.ContactForm
            {
                FirstName = request.ContactForm.FirstName,
                LastName = request.ContactForm.LastName,
                Email = request.ContactForm.Email,
                Phone = request.ContactForm.Phone,
                Comments = request.ContactForm.Comments,
            };

            await emailService.SendMailAsync(entity,
                "edwinabrhmt@gmail.com",
                new List<string> { "edwinabrahamthomas@gmail.com" },
                "Contact Form Alert From Portfolio Website", "<H3> <b>" + entity.FirstName + " " + entity.LastName + " " +
                "</b>is trying to reach you through portfolio website</H3><Br>" +
                "<H3>Contact Information</H3><Br>" +
                "Name: " + entity.FirstName + " " + entity.LastName + "<Br>" +
                "Email: " + entity.Email + "<Br>" +
                "Phone: " + entity.Phone + "<Br>" +
                "Comments: " + entity.Comments + "<Br>");

            var response = new ContactFormAlertResponse
            {
                ContactForm = new ContactForm
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Phone = entity.Phone,
                    Email = entity.Email,
                    Comments = entity.Comments,
                },
                Success = true
            };

            return response;
        }
    }
}
