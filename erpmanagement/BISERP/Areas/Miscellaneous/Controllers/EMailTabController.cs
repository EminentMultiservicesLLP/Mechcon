using System.Web.Mvc;
using BISERP.Areas.Store.Models.Master;
using BISERPCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BISERP.Areas.DashBoards.Models;
using BISERP.Areas.Miscellaneous.Models;
using System.Net.Mail;

namespace BISERP.Areas.Miscellaneous.Controllers
{
    public class EMailTabController : Controller
    {
        HttpClient client;
        string url = BISERP.Common.Constants.WebAPIAddress;
        static BISERPCommon.ILogger _logger = Logger.Register(typeof(EMailTabController));
        public EMailTabController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
        }
        [HttpGet]
        public async Task<JsonResult> SendMailStatusOfDashBoard(string tomailId)
        {
            DateTime fromdate = System.DateTime.Now;
            DateTime todate = System.DateTime.Now;
            JsonResult jResult;
            try
            {
                var _url = url + "/storeDashboard/SendMailStatusOfDashBoard/" + fromdate.ToString("MM-dd-yy") + "/" + todate.ToString("MM-dd-yy") + Common.Constants.JsonTypeResult;

                var result = await Common.AsyncWebCalls.GetAsync<List<DashboardRequestCounts>>(client, _url, CancellationToken.None);
                if (result != null)
                {
                    SendInquiryEmail(result, tomailId);
                    jResult = Json(new { result }, JsonRequestBehavior.AllowGet);
                }
                else
                    jResult = Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in GetGrnCount :" + ex.Message + Environment.NewLine + ex.StackTrace.ToString());
                jResult = Json("Error");
            }
            return jResult;
        }

        public void SendInquiryEmail(List<DashboardRequestCounts> mails, string tomailId)
        {
            string strMsg = "";
            strMsg = " <table style=\"border-collapse:collapse; text-align:center;border-color:red;\" border=\"1\"><tr><td style='background-color:#71A942'> Transaction Type </td><td style='background-color:#71A942'>Count</td></tr>";    
            int n = 1;
            EMailTabModel abc = new EMailTabModel();
            foreach (var item in mails)
            {
                if (n % 2 == 0)
                {
                    strMsg = strMsg + " <tr style='background-color:#E3EEDA'><td> " + item.RequestType + "</td><td>" + item.RequestCount + "</td></tr>";
                }
                else
                {
                    strMsg = strMsg + " <tr style='background-color:#C7DEB3'><td > " + item.RequestType + "</td><td>" + item.RequestCount + "</td></tr>";
                }
                n++;
            }

             strMsg = strMsg + "</table>";
             string date = DateTime.Now.ToString();
             var fromAddress = "mechconmails@gmail.com";
             var toAddress = "mechconmails@gmail.com";
             const string fromPassword = "mechcon@admin";
             string subject = "Transaction Data in ERP As On Date: " + date;

             MailMessage mail = new MailMessage();
             System.Net.Mail.SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
             mail.From = new MailAddress(fromAddress);
             mail.To.Add(tomailId);
             mail.Subject = subject;
             mail.Body = strMsg;
             mail.IsBodyHtml = true;
             SmtpServer.Port = 587;
             SmtpServer.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
             SmtpServer.EnableSsl = true;
             SmtpServer.Send(mail);  
        }
    }
}
