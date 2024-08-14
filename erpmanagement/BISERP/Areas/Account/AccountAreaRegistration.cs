using System.Web.Mvc;

namespace BISERP.Areas.Account
{
    public class Account2AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Account2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Account_default",
                "Account2/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
