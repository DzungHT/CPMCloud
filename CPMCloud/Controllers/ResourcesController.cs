using CPMCloud.CybertronFramework.Common;
using CPMCloud.Models;
using CPMCloud.Models.Common;
using CPMCloud.Models.ViewModels;
using CybertronFramework;
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
    //[Authorize]
    public class ResourcesController : Controller
    {
        /// GET: Resources
        //[CybertronAuthorize(Roles = RoleCodes.Resources.INDEX)]
        public ActionResult Index()
        {
            return View();
        }

        //[CybertronAuthorize(Roles = RoleCodes.Resources.SEARCH)]
        public async Task<JsonResult> SearchProcess(ResourceViewModel formData)
        {
            ApiClient client = ApiClient.Instance;
            DataTableResponse<Resource> dataTableResponse = new DataTableResponse<Resource>();
            try
            {
                var apiResult = await client.PostApiAsync<JsonResultObject<DataTableResponse<Resource>>, object>(Resources.URLResources.SEARCH_RESOURCE + "?offset="+formData.DataTable.start.ToString()+ "&recordPerPage="+formData.DataTable.length.ToString(),
                    new { ApplicationID = formData.ApplicationID, Code = formData.Code, Description = formData.Description, Name = formData.Name }
                    );
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
        public async Task<ActionResult> Save(Resource app)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.Resources.INSERT))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<Resource>, Resource>(Resources.URLResources.SAVE_RESOURCE, app);
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
                var apiResult = await client.GetApiAsync<JsonResultObject<Resource>>(Resources.URLResources.GET_RESOURCE + id);
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
                if (Permission.HasPermission(RoleCodes.Resources.DELETE))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<String>, object>(Resources.URLResources.DELETE_RESOURCE + id,
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

        [CybertronAuthorize(Roles = RoleCodes.Resources.SEARCH)]
        public async Task<JsonResult> GetApplications()
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                var apiResult = await client.GetApiAsync<JsonResultObject<List<Application>>>(Resources.URLResources.GET_ALL_APPLICATION);
                if (apiResult.IsSuccess)
                {
                    return Json(apiResult.Data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}