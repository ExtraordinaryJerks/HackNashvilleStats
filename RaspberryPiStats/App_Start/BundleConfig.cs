using System.Web;
using System.Web.Optimization;

namespace RaspberryPiStats
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/js/jquery-{version}.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/js/bootstrap.js",
                      "~/Content/js/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/style.min.css",
                      "~/Content/site.css"));

<<<<<<< HEAD
            bundles.Add(new ScriptBundle("~/bundles/flot").Include("~/Content/js/jquery.flot.min.js"));
=======
            bundles.Add(new ScriptBundle("~/bundles/flot").Include("~/Scripts/flot/jquery.flot.min.js", "~/Scripts/flot/jquery.flot.time.min.js"));
>>>>>>> f999e4cc57deb2f37759127d2cf03a312e5d4a8f
        }
    }
}
