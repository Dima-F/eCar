using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using eCar.Applicaton.Models;

namespace eCar.Controllers
{
    public partial class ContactController
    {
        /// <summary>
        /// Модель представления. Содержит в себе имя, емейл и сообщения отправителя
        /// </summary>
        public class IndexModel : LayoutModel
        {
            [DisplayName("Your name")]
            [Required(ErrorMessage = "Please enter your name.")]
            public string SenderName { get; set; }

            [DisplayName("Your email address")]
            [Required(ErrorMessage = "Please enter your email address.")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "Wrong email format")]
            public string SenderEmail { get; set; }

            [Required(ErrorMessage = "Please enter your message.")]
            public string Message { get; set; }
        }
    }
}