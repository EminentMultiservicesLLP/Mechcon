using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Linq.Expressions;
using System.Reflection;
using System.Net;
using System.Net.NetworkInformation;

namespace BISERP.Common
{
    public class Constants
    {
        public const string JsonTypeResult = "?type=Json";
        public const string XMLTypeResult = "?type=XML";

        public static readonly string WebAPIAddress = Convert.ToString(ConfigurationManager.AppSettings["WebAPIAddress"]);
        public static readonly string ScandocUrl = Convert.ToString(ConfigurationManager.AppSettings["ScandocUrl"]);
        public static readonly bool EnableEmailNotification = Convert.ToBoolean(ConfigurationManager.AppSettings["enableEmailNotification"]);
        public const int AppUserId = 1;
        public static readonly string MacName = HttpContext.Current.Request.UserHostAddress;
        public static readonly string MacId = GetMACAddress();
        public static readonly string IpAddress = GetIPAddress();

        public static readonly string MailHostAddress = Convert.ToString(ConfigurationManager.AppSettings["MailHostAddress"]);
        public static readonly int MainPortAddress = Convert.ToInt16(ConfigurationManager.AppSettings["MainPortAddress"]);
        public static readonly string MailUserID = Convert.ToString(ConfigurationManager.AppSettings["MailUserID"]);
        public static readonly string MailPWD = Convert.ToString(ConfigurationManager.AppSettings["MailPWD"]);

        public static readonly string MailCommonSender =
            Convert.ToString(ConfigurationManager.AppSettings["MailSenderID"]);


        /******** Email Ids ******/
        public static readonly string TestEmailAddress = Convert.ToString(ConfigurationManager.AppSettings["TestEmail"]);
        public static readonly string PurchaseEmailAddress = Convert.ToString(ConfigurationManager.AppSettings["PurchaseEmail"]);
        public static readonly string StoreEmailAddress = Convert.ToString(ConfigurationManager.AppSettings["StoreEmail"]);
        public static readonly string AssetEmailAddress = Convert.ToString(ConfigurationManager.AppSettings["AssetEmail"]);
        public static readonly string EDPEmailAddress = Convert.ToString(ConfigurationManager.AppSettings["EDPEmail"]);
        

        /******** Template File names ********/
        public static readonly string MaterialIndentCreatedTemplate = "~/Views/Templates/MaterialIndentrequest.cshtml";
        public static readonly string MaterialIndentAuthorizedTemplate = "~/Views/Templates/MaterialAuthorized.cshtml";
        public static readonly string MaterialIssueCreatedTemplate = "~/Views/Templates/MaterialIssueRequest.cshtml";
        public static readonly string MaterialIssueAcceptanceTemplate = "~/Views/Templates/MaterialIssueAcceptance.cshtml";
        public static readonly string MaterialReturnCreatedTemplate = "~/Views/Templates/MaterialReturnRequest.cshtml";

        public static readonly string PIndentCreatedTemplate = "~/Views/Templates/PurchaseIndentRequest.cshtml";
        public static readonly string GrnCreatedTemplate = "~/Views/Templates/GRNRequest.cshtml";
        public static readonly string GrnAuthorizationTemplate = "~/Views/Templates/GRNAuthorized.cshtml";
        public static readonly string MaterialTransferTemplate = "~/Views/Templates/MaterialTransferTemplate.cshtml";
        public static readonly string PurchaseReturnTemplate = "~/Views/Templates/PurchaseReturnTemplate.cshtml";

        public static readonly string NewIndentCreatedSubject = "New Indent Created";
        public static readonly string AuthorizedIndentSubject = "Material Indent Authorized";
        public static readonly string NewIssueCreatedSubject = "New Material Issue Created";
        public static readonly string AcceptanceIssueSubject = "Material Issue Accepted";
        public static readonly string NewMaterialReturnCreatedSubject = "New Material Return Created";
        public static readonly string NewPICreatedSubject = "New Purchase Indent Created";

        public static readonly string GrnCreatedSubject = "New GRN";
        public static readonly string GrnAuthorizedSubject = "GRN Authorization";
        public static readonly string MaterialTransferSubject = "Material Transfer";
        public static readonly string PurchaseReturnSubject = "Material Transfer";
      

        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return sMacAddress;
        }

        public static string GetIPAddress()
        {
            HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}