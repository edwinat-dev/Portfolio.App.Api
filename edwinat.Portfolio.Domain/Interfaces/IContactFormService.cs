
using edwinat.Portfolio.App.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edwinat.Portfolio.Domain.Interfaces
{
    public interface IContactFormService
    {
        public Task<ContactFormAlertResponse> ContactFormAlertAsync(ContactFormAlertRequest request);
    }
}
