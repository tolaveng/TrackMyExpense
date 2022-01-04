using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.IServices
{
    public interface IEmailService
    {
        bool SendEmailConfirmation(string receiverEmail, string receiverName, string confirmEmailLink);
        bool SendPasswordReset(string receiverEmail, string receiverName, string confirmEmailLink);
    }
}
