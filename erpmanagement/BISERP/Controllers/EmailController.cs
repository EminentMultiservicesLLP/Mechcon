using BISERPCommon.EncryptDecrypt;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace BISERP.Controllers
{
    public class EmailController : Controller
    {
        public int SmtpSettings(string toAddress, string body, string subject, string MailAttachment = "")
        {
            int Result = 0;
            try
            {
                string fromPassword = EncryptDecryptDES.DecryptString(ConfigurationManager.AppSettings["mailPassword"]);
                string fromAddress = ConfigurationManager.AppSettings["mailID"];

                var smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["smtpHost"];
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPortNo"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["sslSecurityStatus"]);
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 120000;

                MailAddress from = new MailAddress(fromAddress);
                MailAddress to = new MailAddress(toAddress);
                MailMessage message = new MailMessage(from, to);

                if (!string.IsNullOrEmpty(MailAttachment))
                {
                    string AttachmentPath = Path.Combine(HttpRuntime.AppDomainAppPath, MailAttachment);
                    message.Attachments.Add(new Attachment(@AttachmentPath));
                }

                message.IsBodyHtml = true;
                message.Body = body;
                message.Subject = subject;

                smtp.Send(message);
                Result = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

            return Result;
        }

    }
}
