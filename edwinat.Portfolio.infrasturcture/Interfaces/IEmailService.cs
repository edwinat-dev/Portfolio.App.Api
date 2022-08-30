using edwinat.Portfolio.Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edwinat.Portfolio.infrasturcture.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendMailAsync(ContactForm contactForm, string SourceEmail, List<string> DestinationEmails, string Subject, string Body);
    }
}
