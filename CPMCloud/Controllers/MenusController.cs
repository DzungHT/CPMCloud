using CPMCloud.CybertronFramework.Common;
using CPMCloud.Models;
using CPMCloud.Models.Common;
using CPMCloud.Models.Entities;
using CybertronFramework.Libraries;
using CybertronFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPMCloud.Controllers
{
    public class MenusController : Controller
    {
        // GET: Menus
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> SearchProcess(MenuViewModel formData)
        {
            ApiClient client = ApiClient.Instance;
            DataTableResponse<Menu> dataTableResponse = new DataTableResponse<Menu>();
            try
            {
                var apiResult = await client.PostApiAsync<JsonResultObject<DataTableResponse<Menu>>, object>(URLResources.SEARCH_MENU + "?offset=" + formData.DataTable.start.ToString() + "&recordPerPage=" + formData.DataTable.length.ToString(),
                    new { ApplicationId = formData.ApplicationID, Code = StringUtil.NVL(formData.Code), Name = StringUtil.NVL(formData.Name) });
                if (apiResult != null && apiResult.IsSuccess)
                {
                    dataTableResponse = apiResult.Data;
                }

            }
            catch (Exception ex)
            {

            }
            return Json(dataTableResponse, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Save(Menu obj)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.Applications.SEARCH))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<Menu>, Menu>(Resources.URLResources.SAVE_MENU, obj);
                    ViewBag.Status = "1";
                }
                else
                {
                    ViewBag.Status = "0";
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401"))
                {
                    ViewBag.Status = "-2";
                }
            }
            return PartialView(Constants.VIEW.SAVE_RESULT);
        }

        [HttpPost]
        public async Task<JsonResult> PrepareUpdate(int id)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                var apiResult = await client.GetApiAsync<JsonResultObject<Menu>>(URLResources.GET_MENU_BY_ID + id);
                return Json(apiResult.Data);
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.Applications.SEARCH))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<String>, object>(Resources.URLResources.DELETE_MENU + id,
                    new { });
                    ViewBag.Status = "1";
                }
                else
                {
                    ViewBag.Status = "0";
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("401"))
                {
                    ViewBag.Status = "-2";
                }
            }
            return PartialView(Constants.VIEW.SAVE_RESULT);
        }
    }
}