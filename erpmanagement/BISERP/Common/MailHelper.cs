using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BISERPCommon;

namespace BISERP.Common
{
    public class MailHelper : IDisposable
    {
        private readonly ILogger _logger = Logger.Register(typeof(MailHelper));
        public async Task SendEmail(IEnumerable<MailHelperModel> mailMessages)
        {
            using (var mailClient = new SmtpClient
            {
                Host = Constants.MailHostAddress,
                Port = Constants.MainPortAddress,
                Credentials = new System.Net.NetworkCredential(Constants.MailUserID, Constants.MailPWD),
                EnableSsl = false
            })
            {
                foreach (var msg in mailMessages)
                {
                    using (var mail = new MailMessage())
                    {
                        {
                            var logBody = String.Join(";", msg.RecipientTo) + " CC:" + String.Join(";", msg.RecipientCc)
                                          + " BCC:" + String.Join(";", msg.RecipientBcc) + " Subject:" + msg.Subject
                                          + " MessageBody:" + msg.Body;

                            _logger.LogInfo(" ****** Starting email notification for " + logBody);

                            try
                            {
                                mail.Sender = new MailAddress(msg.Sender);

                                msg.RecipientTo.ToList().ForEach(t => mail.To.Add(t));
                                msg.RecipientCc.ToList().ForEach(t => mail.CC.Add(t));
                                msg.RecipientBcc.ToList().ForEach(t => mail.Bcc.Add(t));

                                mail.Subject = msg.Subject;
                                mail.Body = msg.Body;
                                mail.IsBodyHtml = msg.IsHtmlBody;

                                foreach (var attachmentFile in msg.Attachments)
                                {
                                    if (File.Exists(attachmentFile))
                                    {
                                        var att = new Attachment(attachmentFile);
                                        mail.Attachments.Add(att);
                                    }
                                }
                                await mailClient.SendMailAsync(mail);

                                _logger.LogInfo(" ****** Successfully send email to:" + logBody);
                            }
                            catch (Exception)
                            {
                                _logger.LogError("*****Error while sending email to:" + logBody);
                            }
                        }
                    }
                }
            }
        }

        private bool _disposed;
        public void SendEmail(string[] toAddress, string[] ccAddress, string subject, string messageBody, string sender = "", bool isHtmlBody = true)
        {
            Task.Run(async () =>
            {
                using (var mailClient = new SmtpClient
                {
                    Host = Constants.MailHostAddress,
                    Credentials = new System.Net.NetworkCredential(Constants.MailUserID, Constants.MailPWD),
                    EnableSsl = false
                })
                {
                    var mailMessages = new List<MailHelperModel>
                    {
                        new MailHelperModel()
                        {
                            Sender = (string.IsNullOrWhiteSpace(sender) ? Constants.MailCommonSender: sender),
                            RecipientTo = toAddress,
                            RecipientCc = ccAddress,
                            Subject = subject,
                            Body = messageBody,
                            IsHtmlBody = isHtmlBody
                        }
                    };

                    foreach (var msg in mailMessages)
                    {
                        using (var mail = new MailMessage())
                        {
                            {
                                var logBody = "TO:" + (msg.RecipientTo != null && msg.RecipientTo.Any() ? String.Join(";", msg.RecipientTo) : "") +
                                              " CC:" + (msg.RecipientCc != null && msg.RecipientCc.Any() ? String.Join(";", msg.RecipientCc) : "") +
                                              " BCC:" + (msg.RecipientBcc != null && msg.RecipientBcc.Any() ? String.Join(";", msg.RecipientBcc) : "") +
                                              " Subject:" + msg.Subject
                                              + " MessageBody:" + msg.Body;

                                _logger.LogInfo(" ****** Starting email notification ******");

                                try
                                {
                                    mail.Sender = new MailAddress(msg.Sender);
                                    mail.From = new MailAddress(msg.Sender);

                                    if (msg.RecipientTo != null && msg.RecipientTo.Any())
                                        msg.RecipientTo.ToList().ForEach(t => mail.To.Add(t));
                                    if (msg.RecipientCc != null && msg.RecipientCc.Any())
                                        msg.RecipientCc.ToList().ForEach(t => mail.CC.Add(t));
                                    if (msg.RecipientBcc != null && msg.RecipientBcc.Any())
                                        msg.RecipientBcc.ToList().ForEach(t => mail.Bcc.Add(t));

                                    mail.Subject = msg.Subject;
                                    mail.Body = msg.Body;
                                    mail.IsBodyHtml = msg.IsHtmlBody;

                                    if (msg.Attachments != null)
                                        foreach (var attachmentFile in msg.Attachments)
                                        {
                                            if (File.Exists(attachmentFile))
                                            {
                                                var att = new Attachment(attachmentFile);
                                                mail.Attachments.Add(att);
                                            }
                                        }
                                    await mailClient.SendMailAsync(mail);
                                    _logger.LogInfo(" ****** Successfully send email to:" + logBody);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError("*****Error while sending email to:" + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + logBody);
                                }
                            }
                        }
                    }
                }
            });
        }

        ~MailHelper()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //Dispose();
                }
            }
            _disposed = true;
        }
    }


    public class MailHelperModel
    {
        public string Sender { get; set; }
        public string[] RecipientTo { get; set; }
        public string[] RecipientCc { get; set; }
        public string[] RecipientBcc { get; set; }
        public string Subject { get; set; }
        public bool IsReply { get; set; }
        public bool IsHtmlBody { get; set; }
        public string Body { get; set; }
        public string[] Attachments { get; set; }
    }

}