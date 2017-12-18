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
    [Authorize]
    public class AccountsController : Controller
    {
        CommonBusiness commonBu = new CommonBusiness();

        #region HttpGet

        // GET: Account
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            Session["ReturnUrl"] = ReturnUrl ?? "/";
            FormsAuthentication.SignOut();
            return PartialView();
        }

        [HttpGet]
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