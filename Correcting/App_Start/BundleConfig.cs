using System.Web;
using System.Web.Optimization;

namespace Correcting
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //angular
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-ui-router.min.js",
                "~/Scripts/angular-animate.min.js"));
            //weui
            bundles.Add(new StyleBundle("~/Content/weui").Include(
             "~/Content/weui.css"));
            //main
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                "~/Scripts/js/services.js",
                "~/Scripts/js/directive.js",
                "~/Scripts/js/controllers.js",
                "~/Scripts/js/app.js" 
            ));            
            //admin
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/adminjs/services.js",
                "~/Scripts/adminjs/controllers.js",
                "~/Scripts/adminjs/app.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/mobile").Include(
                      "~/Content/mobile.css"));
            
        }
    }
}
