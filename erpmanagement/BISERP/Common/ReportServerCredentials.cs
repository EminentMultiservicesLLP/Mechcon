using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;

namespace BISERP.Common
{
    public class ReportServerCredentials : IReportServerConnection
    {
        private string _username;
        private string _password;
        private string _domain;
       
        #region Public Properties
        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_username, _password, _domain); }
        }
        #endregion Public Properties

        #region Constructor
        public ReportServerCredentials(string userName, string password, string domain)
        {
            _username = userName;
            _password = password;
            //_domain = domain;
        }
        public ReportServerCredentials()
        {
            var appSetting = WebConfigurationManager.AppSettings;
            _username = appSetting["ReportServerUser"];
            _password = appSetting["ReportServerPassword"];
            //_domain = appSetting["ReportServerDomain"];
        }
        #endregion Constructor

        #region Public Method
        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;
            return false;
        }
        #endregion Public Method        
        public Uri ReportServerUrl { get { return new Uri(WebConfigurationManager.AppSettings["ReportServerUrl"]); } }
        public int Timeout { get { return 60000; } }
    }
}