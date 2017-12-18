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
    [Authorize]
    public class RolesController : Controller
    {
        /// GET: Resources
        [CybertronAuthorize(Roles = RoleCodes.Roles.INDEX)]
        public ActionResult Index()
        {
            return View();
        }

        [CybertronAuthorize(Roles = RoleCodes.Roles.SEARCH)]
        [HttpPost]
        public async Task<JsonResult> SearchProcess(RoleViewModel formData)
        {
            ApiClient client = ApiClient.Instance;
            DataTableResponse<Role> dataTableResponse = new DataTableResponse<Role>();
            try
            {
                var apiResult = await client.PostApiAsync<JsonResultObject<DataTableResponse<Role>>, object>(Resources.URLResources.SEARCH_ROLE + "?offset=" + formData.DataTable.start.ToString() + "&recordPerPage=" + formData.DataTable.length.ToString(),
                    new { Code = formData.Code, Name = formData.Name }
                    );
                if (apiResult != null && apiResult.IsSuccess)
                {
                    dataTableResponse = apiResult.Data;
                    dataTableResponse.draw = formData.DataTable.draw;
                }

            }
            catch (Exception ex)
            {

            }
            return Json(dataTableResponse, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Save(Role app)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.Roles.INSERT))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<Role>, Role>(Resources.URLResources.SAVE_ROLE, app);
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
                var apiResult = await client.GetApiAsync<JsonResultObject<Role>>(Resources.URLResources.GET_ROLE + id);
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
                if (Permission.HasPermission(RoleCodes.Roles.DELETE))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<String>, object>(Resources.URLResources.DELETE_ROLE + id,
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

        #region permission
        [CybertronAuthorize(Roles = RoleCodes.Roles.SEARCH)]
        [HttpPost]
        public async Task<JsonResult> SearchPermissionByRole(PermissionViewModel formData)
        {
            ApiClient client = ApiClient.Instance;
            DataTableResponse<PermissionViewModel> dataTableResponse = new DataTableResponse<PermissionViewModel>();
            try
            {
                var apiResult = await client.PostApiAsync<JsonResultObject<DataTableResponse<PermissionViewModel>>, object>("api/v1/Roles/searchByRole?offset=" + formData.DataTable.start.ToString() + "&recordPerPage=" + formData.DataTable.length.ToString(),
                    new { RoleID = formData.RoleID, Code = formData.Code, Name = formData.Name }
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

        [CybertronAuthorize(Roles = RoleCodes.Roles.SEARCH)]
        [HttpPost]
        public async Task<JsonResult> SearchPermissionForRole(PermissionViewModel formData)
        {
            ApiClient client = ApiClient.Instance;
            DataTableResponse<PermissionViewModel> dataTableResponse = new DataTableResponse<PermissionViewModel>();
            try
            {
                var apiResult = await client.PostApiAsync<JsonResultObject<DataTableResponse<PermissionViewModel>>, object>("api/v1/Roles/searchPermissionForRole?offset=" + formData.DataTable.start.ToString() + "&recordPerPage=" + formData.DataTable.length.ToString(),
                    new { RoleID = formData.RoleID, Code = formData.Code, Name = formData.Name, ApplicationID = formData.ApplicationID }
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
        public async Task<ActionResult> deletePermission(PermissionViewModel formData)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.Roles.DELETE))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<String>, object>("api/v1/Roles/deletePermission",
                    new { PermissionID = formData.PermissionID, RoleID = formData.RoleID });
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> AddPermissions(PermissionViewModel app)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.Roles.UPDATE))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<PermissionViewModel>, PermissionViewModel>("api/v1/Roles/addPermissions", app);
                    if (apiResult.IsSuccess)
                    {
                        ViewBag.Status = "1";
                    }
                    else
                    {
                        ViewBag.Status = "-1";
                    }
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

        #endregion



        [CybertronAuthorize(Roles = RoleCodes.Roles.SEARCH)]
        [HttpPost]
        public async Task<JsonResult> SearchMenuByRole(MenuViewModel formData)
        {
            ApiClient client = ApiClient.Instance;
            DataTableResponse<MenuViewModel> dataTableResponse = new DataTableResponse<MenuViewModel>();
            try
            {
                var apiResult = await client.PostApiAsync<JsonResultObject<DataTableResponse<MenuViewModel>>, object>("api/v1/Roles/searchMenuByRole?offset=" + formData.DataTable.start.ToString() + "&recordPerPage=" + formData.DataTable.length.ToString(),
                    new { RoleID = formData.RoleID, Code = formData.Code, Name = formData.Name }
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

        [CybertronAuthorize(Roles = RoleCodes.Roles.SEARCH)]
        [HttpPost]
        public async Task<JsonResult> SearchMenuForRole(MenuViewModel formData)
        {
            ApiClient client = ApiClient.Instance;
            DataTableResponse<MenuViewModel> dataTableResponse = new DataTableResponse<MenuViewModel>();
            try
            {
                var apiResult = await client.PostApiAsync<JsonResultObject<DataTableResponse<MenuViewModel>>, object>("api/v1/Roles/searchMenuForRole?offset=" + formData.DataTable.start.ToString() + "&recordPerPage=" + formData.DataTable.length.ToString(),
                    new { RoleID = formData.RoleID, Code = formData.Code, Name = formData.Name, ApplicationID = formData.ApplicationID }
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
        public async Task<ActionResult> DeleteMenu(MenuViewModel formData)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.Roles.DELETE))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<String>, object>("api/v1/Roles/deleteMenu",
                    new { MenuID = formData.MenuID, RoleID = formData.RoleID });
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> AddMenus(MenuViewModel app)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.Roles.UPDATE))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<MenuViewModel>, MenuViewModel>("api/v1/Roles/addMenus", app);
                    if (apiResult.IsSuccess)
                    {
                        ViewBag.Status = "1";
                    }
                    else
                    {
                        ViewBag.Status = "-1";
                    }
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