using System.Web.Optimization;

namespace CPMCloud
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region CSS
            bundles.Add(new StyleBundle("~/css/common")
                            .Include("~/Content/Common/bootstrap.min.css")
                            .Include("~/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css")
                            .Include("~/vendors/bootstrap-daterangepicker/daterangepicker.css")
                            .Include("~/Content/Common/font-glyphicons.css", new CssRewriteUrlTransform())
                            .Include("~/Content/Common/font-awesome.css", new CssRewriteUrlTransform())
                            .Include("~/vendors/nprogress/nprogress.css")
                            .Include("~/vendors/iCheck/skins/flat/green.css", new CssRewriteUrlTransform())
                            .Include("~/Content/Common/custom.min.css")
                            .Include("~/Scripts/Common/ngselect2/select2.css")
                            .Include("~/Content/Common/mycss.css")
                        );

            bundles.Add(new StyleBundle("~/css/pnotify")
                            .Include("~/vendors/pnotify/dist/pnotify.css")
                            .Include("~/vendors/pnotify/dist/pnotify.brighttheme.css")
                            .Include("~/vendors/pnotify/dist/pnotify.buttons.css")
                            .Include("~/vendors/pnotify/dist/pnotify.nonblock.css")
                        );
            bundles.Add(new StyleBundle("~/css/dataTable")
                            .Include("~/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css", new CssRewriteUrlTransform())
                        );
            bundles.Add(new StyleBundle("~/css/inlineValidation")
                            .Include("~/Scripts/Common/inlineValidation/validationEngine.jquery.css", new CssRewriteUrlTransform())
                        );
            bundles.Add(new StyleBundle("~/css/confirm")
                            .Include("~/Scripts/Common/jquery-confirm/jquery-confirm.min.css", new CssRewriteUrlTransform())
                        );
            #endregion

            #region JS
            bundles.Add(new ScriptBundle("~/js/common")
                            .Include("~/Scripts/Common/jquery/jquery-1.12.4.min.js")
                            .Include("~/Scripts/Common/bootstrap/bootstrap.min.js")
                            .Include("~/vendors/nprogress/nprogress.js")
                            .Include("~/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js")
                            .Include("~/vendors/iCheck/icheck.min.js")
                            .Include("~/vendors/DateJS/build/date.js")
                            .Include("~/vendors/moment/min/moment.min.js")
                            .Include("~/vendors/bootstrap-daterangepicker/daterangepicker.js")
                            .Include("~/Scripts/Common/custom.js")
                            .Include("~/Scripts/Common/ngselect2/select2.js")
                        );

            bundles.Add(new ScriptBundle("~/js/fastClick")
                            .Include("~/vendors/fastclick/lib/fastclick.js")
                        );

            bundles.Add(new ScriptBundle("~/js/chart")
                            .Include("~/vendors/Chart.js/dist/Chart.min.js")
                            .Include("~/vendors/Flot/jquery.flot.js")
                            .Include("~/vendors/Flot/jquery.flot.pie.js")
                            .Include("~/vendors/Flot/jquery.flot.time.js")
                            .Include("~/vendors/Flot/jquery.flot.stack.js")
                            .Include("~/vendors/Flot/jquery.flot.resize.js")
                            .Include("~/vendors/flot.orderbars/js/jquery.flot.orderBars.js")
                            .Include("~/vendors/flot-spline/js/jquery.flot.spline.min.js")
                            .Include("~/vendors/flot.curvedlines/curvedLines.js")
                            .Include("~/vendors/gauge.js/dist/gauge.min.js")
                        );

            bundles.Add(new ScriptBundle("~/js/Skycons")
                            .Include("~/vendors/skycons/skycons.js")
                        );

            bundles.Add(new ScriptBundle("~/js/pnotify")
                            .Include("~/vendors/pnotify/dist/pnotify.js")
                            .Include("~/vendors/pnotify/dist/pnotify.buttons.js")
                            .Include("~/vendors/pnotify/dist/pnotify.nonblock.js")
                        );
            bundles.Add(new ScriptBundle("~/js/dataTable")
                            .Include("~/vendors/datatables.net/js/jquery.dataTables.min.js")
                            .Include("~/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js")
                            .Include("~/vendors/datatables.net/js/dataTables.defaults.js")
                        );

            bundles.Add(new ScriptBundle("~/js/angular")
                            .Include("~/node_modules/core-js/client/shim.min.js")
                            .Include("~/node_modules/zone.js/dist/zone.js")
                            .Include("~/node_modules/systemjs/dist/system.src.js")
                            .Include("~/systemjs.config.js")
                        );
            bundles.Add(new ScriptBundle("~/js/angularJS")
                            .Include("~/Scripts/Common/angularJS/angular.js")
                            .Include("~/Scripts/Common/ngselect2/angular-ui-select2/select2.js")
                            .Include("~/Scripts/Common/angularJS/app.js")
                        );
            bundles.Add(new ScriptBundle("~/js/inlineValidation")
                            .Include("~/Scripts/Common/inlineValidation/jquery.validationEngine_vi_VN.js")
                            .Include("~/Scripts/Common/inlineValidation/jquery.validationEngine.js")
                        );
            bundles.Add(new ScriptBundle("~/js/confirm")
                           .Include("~/Scripts/Common/jquery-confirm/jquery-confirm.min.js")
                       );
            #endregion
        }
    }
}