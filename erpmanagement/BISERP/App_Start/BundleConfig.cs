using System.Web;
using System.Web.Optimization;

namespace BISERP
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Scripts/jquery-{version}.min.js",
                      "~/Scripts/jquery-ui-{version}.min.js",
                      "~/Scripts/jquery.dataTables.min.js",
                      "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Bootstrap").Include(
                      "~/Scripts/moment.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/bootstrap-timepicker.min.js",
                      "~/Scripts/dataTables.bootstrap.min.js",
                      "~/Scripts/respond.min.js",
                      "~/Scripts/metisMenu.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/CommonJs").Include(
                    "~/Scripts/sb-admin-2.js",
                    "~/Scripts/MessageBox/lobibox.min.js",
                    "~/Scripts/jquery.PrintArea.js",
                    "~/Scripts/jLinq-2.2.1.js",
                    "~/Scripts/itemModelScript.js",
                    "~/Scripts/jspdf.umd.min.js",
                    "~/Scripts/apexChart/apexchart.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/jquerymodels").Include(
                       "~/Scripts/SupplierModel.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/Grid_Chart").Include(
                      "~/Scripts/pqgrid/jquery.resize.js",
                      "~/Scripts/chartjs/Chart.min.js",
                      "~/Scripts/pqgrid/jsZip-2.5.0/jszip.min.js",
                      "~/Scripts/pqgrid/jsZip-2.5.0/filesaver.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/Bootstrap").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Content/bootstrap-timepicker.min.css",
                      "~/Content/font.css",
                      "~/Content/PIXEDEN.css"));

            bundles.Add(new StyleBundle("~/Content/Common").Include(
                      "~/Content/metisMenu.min.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/Site.css",
                      "~/Content/NewCSS.css",
                      "~/Content/scrolltabs.css",
                      "~/Content/defaultGridModal.css",
                      "~/Content/defaultDashboard.css",
                      "~/Content/MessageBox/lobibox.min.css"));

            bundles.Add(new StyleBundle("~/Content/GridChart").Include(
                       "~/Content/jquery-ui.css",
                       "~/Content/chartjs/Chart.min.css",
                       "~/Content/pqgrid/pqgrid.minv5.1.css",
                       "~/Content/pqgrid/pqgrid.ui.minv5.1.css",
                       "~/Content/pqgrid/themes/steelblue/pqgrid.css",
                       "~/Content/jsgrid/jsgrid.min.css",
                       "~/Content/jsgrid/jsgrid-theme.min.css"
                      ));

            bundles.IgnoreList.Clear();
        }
    }
}