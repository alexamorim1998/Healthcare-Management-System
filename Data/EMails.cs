using RINTE_Care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace RINTE_Care.Data
{
    public class EMails
    {
        public const string EmailClinic = "rintecare2021@gmail.com";
        const string PasswordClinic = "!Alexandre1998";
        const string ClinicName = "RINTECare Porto";

        public void SendEmail(Email email)
        {
            SmtpClient Client = new SmtpClient()
            {
                //Using GMail SMTP
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = EmailClinic,
                    Password = PasswordClinic
                }
            };

            MailAddress FromeMail = new MailAddress(EmailClinic, ClinicName);
            MailMessage Message = new MailMessage()
            {
                From = FromeMail,
                Subject = email.Subject,
                Body = email.Body,
                
            };

            if (email.FileAttachment != default)
            {
                System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType
                {
                    MediaType = System.Net.Mime.MediaTypeNames.Application.Octet,
                    Name = email.FileAttachment
                };
                Message.Attachments.Add(new Attachment(email.FileAttachment, contentType));
            }

            if (email.DestinationEmail1 != null)
            {
                MailAddress ToeMail = new MailAddress(email.DestinationEmail1, email.DestinationName1);
                Message.To.Add(ToeMail);
            }
            if (email.DestinationEmail2 != null)
            {
                MailAddress ToeMail = new MailAddress(email.DestinationEmail2, email.DestinationName2);
                Message.To.Add(ToeMail);
            }
            Client.Send(Message);
        }
    }
}
