using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BISERP.Areas.Masters.Models;
using BISERPCommon;
using BISERPCommon.Extensions;
using DotNetOpenAuth.Messaging;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BISERP.Common
{

    public enum EmailProcessFor
    {
        MaterialIndent= 0,
        MaterialAuthorizationCancellation =1,
        MaterialIssue =2,
        MatrialAcceptance =3,
        MaterialReturn =4,
        GrnRequest =5,
        GrnAuthorization =6,
        PurchaseIndentRequest = 7,
        MaterialIssueAcceptance=8,
        MaterialTransfer=9,
        PurchaseReturn=10
    }


    public class UtilitiesCls
    {
        private static readonly ILogger Logger = BISERPCommon.Logger.Register(typeof(UtilitiesCls));

        public static async Task SendEmailTask(EmailProcessFor emailfor, Controller controller, object model = null,
            bool partial = false, string[] toEmails = null, string[] ccEmails = null)
         {
            var toAddress = new List<string>() ;
            var ccAddress = new List<string>();
            var subject = string.Empty;
            string viewName = string.Empty;

            switch (emailfor)
            {
                //case EmailProcessFor.MaterialIndent:
                //    toAddress = new string[] {Constants.StoreEmailAddress, Constants.PurchaseEmailAddress};
                //    toAddress = new string[] { Constants.TestEmailAddress };
                //    subject = Constants.NewIndentCreatedSubject;
                //    viewName = Constants.MaterialIndentCreatedTemplate;
                //    break;
                case EmailProcessFor.MaterialAuthorizationCancellation:
                    toAddress.Add(Constants.StoreEmailAddress.ToString());
                    subject = Constants.AuthorizedIndentSubject;
                    viewName = Constants.MaterialIndentAuthorizedTemplate;
                    break;

                case EmailProcessFor.MaterialIssue:
                    toAddress.Add(Constants.StoreEmailAddress.ToString());
                    ccAddress.Add(Constants.StoreEmailAddress.ToString());
                    subject = Constants.NewIssueCreatedSubject;
                    viewName = Constants.MaterialIssueCreatedTemplate;
                    break;

                case EmailProcessFor.MaterialIssueAcceptance:
                    toAddress.Add(Constants.StoreEmailAddress.ToString());
                    subject = Constants.AcceptanceIssueSubject;
                    viewName = Constants.MaterialIssueAcceptanceTemplate;
                    break;

                case EmailProcessFor.MaterialReturn:
                    toAddress.Add(Constants.StoreEmailAddress.ToString());
                    subject = Constants.NewMaterialReturnCreatedSubject;
                    viewName = Constants.MaterialReturnCreatedTemplate;
                    break;

                case EmailProcessFor.MaterialTransfer:
                    subject = Constants.MaterialTransferSubject;
                    viewName = Constants.MaterialTransferTemplate;
                    break;

                case EmailProcessFor.PurchaseIndentRequest:
                    toAddress.Add(Constants.PurchaseEmailAddress.ToString());
                    ccAddress.Add(Constants.StoreEmailAddress.ToString());
                    subject = Constants.NewPICreatedSubject;
                    viewName = Constants.PIndentCreatedTemplate;
                    break;
               
               
                case EmailProcessFor.PurchaseReturn:
                    toAddress.Add(Constants.StoreEmailAddress.ToString());
                    toAddress.Add(Constants.PurchaseEmailAddress.ToString());
                    subject = Constants.PurchaseReturnSubject;
                    viewName = Constants.PurchaseReturnTemplate;
                    break;
                case EmailProcessFor.GrnRequest:
                    toAddress.Add(Constants.StoreEmailAddress.ToString());
                    toAddress.Add(Constants.PurchaseEmailAddress.ToString());
                    subject = Constants.GrnCreatedSubject;
                    viewName = Constants.GrnCreatedTemplate;
                    break;
                case EmailProcessFor.GrnAuthorization:
                    toAddress.Add(Constants.StoreEmailAddress.ToString());
                    toAddress.Add(Constants.PurchaseEmailAddress.ToString());
                    subject = Constants.GrnAuthorizedSubject;
                    viewName = Constants.GrnAuthorizationTemplate;
                    break;
            }

            var htmlView = RenderRazorViewToString(controller, viewName, model, partial);
            
            using (var mailhelper = new MailHelper())
            {
                try
                {

                    if (toEmails != null && toEmails.Any())
                        toAddress.AddRange(toEmails.ToList());

                    toAddress.Add(Constants.EDPEmailAddress);

                    if (ccEmails != null && ccEmails.Any())
                        ccAddress.AddRange(ccEmails);


                    toAddress = toAddress.Where(w => w.Trim().Length > 0).ToList();
                    ccAddress = ccAddress.Where(w => w.Trim().Length > 0).ToList();

                    if(toAddress.Count > 0)
                        mailhelper.SendEmail(
                            toAddress.ToArray(), 
                            (ccAddress.Count > 0 ? ccAddress.ToArray(): new string[]{}), subject, htmlView, Constants.MailCommonSender, true);
                }
                catch (Exception ex)
                {

                    Logger.LogException(ex);
                }

            }

            Byte[] bytes;
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();

                        using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
                        {
                            using (var sr = new StringReader(htmlView))
                            {
                                htmlWorker.Parse(sr);
                            }
                        }

                        doc.Close();
                    }
                }

                bytes = ms.ToArray();
            }
            var pdftFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");
            File.WriteAllBytes(pdftFile, bytes);
           
            
           }

        private static string RenderRazorViewToString(Controller controller, string viewName, object model = null, bool partial = false)
        {
            var result = string.Empty;
            TryCatch.Run(() =>
            {
                controller.ViewData.Model = model;
                using (var sw = new StringWriter())
                {
                    var viewResult = partial
                        ? ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName)
                        : ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null);

                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData,
                        controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                    result = sw.GetStringBuilder().ToString();
                }
            }).IfNotNull(ex =>
            {
                Logger.LogException(ex);
                result = string.Empty;
            });

            return result;
        }

        public static async Task<BranchModel> GetBranchDetailbyStoreId(int storeId)
        {
            var result = await Common.AsyncWebCalls.GetAsync<BranchModel>("/stores/BranchbyStore/" + storeId, CancellationToken.None);
            return result;
        }
    }

    public enum FileLocationAreaWise
    {
        Store = 0,Transport = 1,Branch = 2,AssetMangement = 3
    }
}

