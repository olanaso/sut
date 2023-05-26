using System.Web.Optimization;

namespace Sut.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap2.css",

                      "~/dist/css/skins/skin-red.min.css",
                      "~/dist/css/AdminLTE2.css",
                      "~/plugins/select2/select2.min.css",
                      "~/plugins/iCheck/square/red.css",
                      "~/plugins/sweetalert2/sweetalert2.min.css",
                      "~/plugins/datepicker/datepicker3.css",
                      "~/plugins/timepicker/bootstrap-timepicker.min.css",
                      "~/Content/common.css"


                      ));

            bundles.Add(new StyleBundle("~/bundles/metro-ui/css").Include(
                        "~/Content/metro-ui/css/iconFont.css",
                        "~/Content/metro-ui/css/metro-icons.css",
                        "~/Content/metro-ui/css/metro.css",
                        "~/Content/login.css"

                      ));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.additional.methods.min.js",
                        "~/Scripts/jquery-sortable.js",


                        "~/Scripts/bootstrap.js",
                        "~/Scripts/notify.min.js",
                        "~/Scripts/notify.js",

                        "~/Scripts/modernizr-*",

                        "~/dist/js/app.min.js",
                        "~/dist/js/demo.js",

                        "~/plugins/slimScroll/jquery.slimscroll.min.js",
                        "~/plugins/select2/select2.min.js",
                        "~/plugins/iCheck/icheck.min.js",
                        "~/plugins/sweetalert2/sweetalert2.min.js",
                        "~/plugins/datepicker/bootstrap-datepicker.js",
                        "~/plugins/datepicker/locales/bootstrap-datepicker.es.js",
                        "~/plugins/knob/jquery.knob.js",
                        "~/plugins/timepicker/bootstrap-timepicker.min.js",

                        "~/Scripts/sut.js",
                        "~/Scripts/editar.js",
                        "~/Scripts/common.js",
                        "~/Scripts/jcd.js",
                        "~/Scripts/js-cookie.js"
                      ));
        }
    }
}
