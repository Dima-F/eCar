using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace eCar.Applicaton.Models.Service.Interfaces
{
    public interface IMessageService
    {
        void SendEmail(MailMessage mailMessage);
    }
}
