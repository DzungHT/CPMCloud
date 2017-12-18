using CPMCloud.CybertronFramework.Common;
using CPMCloud.Models;
using CPMCloud.Models.Entities;
using CPMCloud.Models.ViewModels;
using CybertronFramework;
using CybertronFramework.Libraries;
using CybertronFramework.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace CPMCloud.Controllers
{
    public class AccountsController : Controller
    {
        CommonBusiness commonBu = new CommonBusiness();

        #region HttpGet

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> SearchProcess(UserViewModel formData)
        {
            DataTableResponse<UserViewModel> dataTableResponse = new DataTableResponse<UserViewModel>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            string sql = "SELECT * FROM [User] a WHERE 1 = 1 ";
            sql += commonBu.MakeFilterString("a.LoginName", formData.LoginName, ref parameters);
            sql += commonBu.MakeFilterString("a.FullName", formData.FullName, ref parameters);
            sql += commonBu.MakeFilterString("a.Email", formData.Email, ref parameters);
            sql += commonBu.MakeFilterString("a.PhoneNumber", formData.PhoneNumber, ref parameters);
            sql += commonBu.MakeFilterString("a.Status", formData.Status, ref parameters);
            dataTableResponse = commonBu.Search<UserViewModel>(formData.DataTable.start, formData.DataTable.length, sql, "UserID", parameters.ToArray());
            return Json(dataTableResponse, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> searchRoleByUser(RoleViewModel formData)
        {
            DataTableResponse<Role> dataTableResponse = new DataTableResponse<Role>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            string sql = "select r.* from Role r INNER JOIN UserRole ur on ur.RoleID = r.RoleID WHERE 1 = 1 ";
            sql += commonBu.MakeFilterString("ur.UserID", formData.UserID, ref parameters);
            sql += commonBu.MakeFilterString("r.Code", formData.Code, ref parameters);
            sql += commonBu.MakeFilterString("r.Name", formData.Name, ref parameters);

            dataTableResponse = commonBu.Search<Role>(formData.DataTable.start, formData.DataTable.length, sql, "RoleID", parameters.ToArray());
            return Json(dataTableResponse, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Save(User obj)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.USERS.SEARCH))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<User>, User>(URLResources.SAVE_USER, obj);
                    if(apiResult.Status == "05")
                    {
                        ViewBag.Status = "0";
                    } else
                    {
                        ViewBag.Status = "1";
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
            User entities;
            if (userDTO.UserID != 0)
            {
                //if (!owinContext.Authentication.User.IsInRole(RoleCodes.Users.UPDATE))
                //{
                //    return new OutPutDTO(false, Constants.STATUS_CODE.NOT_PERMISSION, Constants.STATUS_MESSAGE.NOT_PERMISSION, "");
                //}
                entities = userBusiness.Get<User>(userDTO.UserID);
                if (entities != null)
                {
                    entities.GetTransferData(userDTO);
                    userBusiness.Update(entities);
                    return new OutPutDTO(true, Constants.STATUS_CODE.SUCCESS, Constants.STATUS_MESSAGE.SUCCESS, entities);
                }
                else
                {
                    return new OutPutDTO(false, Constants.STATUS_CODE.FAILURE, Constants.STATUS_MESSAGE.FAILURE, entities);
                }
            }
            else
            {
                //if (!owinContext.Authentication.User.IsInRole(RoleCodes.Users.INSERT))
                //{
                //    return new OutPutDTO(false, Constants.STATUS_CODE.NOT_PERMISSION, Constants.STATUS_MESSAGE.NOT_PERMISSION, "");
                //}
                entities.GetTransferData(userDTO);
                entities.Password = entities.Password.Encrypt(Constants.ENCRYPT_KEY);
                entities.Status = 1;
                userBusiness.Save(entities);
                return new OutPutDTO(true, Constants.STATUS_CODE.SUCCESS, Constants.STATUS_MESSAGE.SUCCESS, entities);
            }
            return PartialView(Constants.VIEW.SAVE_RESULT);
        }

        [HttpPost]
        public async Task<JsonResult> PrepareUpdate(int id)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                var apiResult = await client.GetApiAsync<JsonResultObject<User>>(URLResources.GET_USER + id);
                return Json(apiResult.Data);
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> LockUnlock(int id)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.USERS.SEARCH))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<User>, object>(URLResources.LOCK_UNLOCK_USER + id,
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> RestartPassword(int id)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.USERS.SEARCH))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<User>, object>(URLResources.RESTART_USER + id,null);
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
        public async Task<ActionResult> AddRoleUser(RoleViewModel app)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.USERS.UPDATE))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<RoleViewModel>, RoleViewModel>("api/v1/Users/addRole", app);
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
        public async Task<ActionResult> DeleteRoleUser(RoleViewModel app)
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                if (Permission.HasPermission(RoleCodes.Roles.UPDATE))
                {
                    var apiResult = await client.PostApiAsync<JsonResultObject<String>, object>("api/v1/Users/deleteRole", app);
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

        // GET: Account
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            Session["ReturnUrl"] = ReturnUrl ?? "/";
            FormsAuthentication.SignOut();
            return PartialView();
        }

        [HttpGet]
        [CybertronAuthorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Accounts");
        }
        #endregion
        
        #region HttpPost
        [HttpPost]
        public async Task<ActionResult> GoogleLogin(GoogleAccountModel instance)
        {
           
            ApiClient client = ApiClient.Instance;
            try
            {
                if (instance != null)
                {
                    Session["USER"] = instance;
                    return Redirect("/Home/Index");
                }
                ModelState.AddModelError(string.Empty, "Đăng nhập không thành công!");
                return PartialView("login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Đăng nhập không thành công!");
                return PartialView("login");
            }
        }
        #endregion
    }
}