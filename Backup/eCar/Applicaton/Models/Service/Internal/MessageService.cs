using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using eCar.Applicaton.Models.Service.Interfaces;

namespace eCar.Applicaton.Models.Service.Internal
{
    public class MessageService:IMessageService
    {
        public void SendEmail(MailMessage mailMessage)
        {
            new SmtpClient().Send(mailMessage);
        }
    }
}