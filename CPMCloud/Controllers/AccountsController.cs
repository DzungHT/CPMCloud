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
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace CPMCloud.Controllers
{
    public class AccountsController : Controller
    {
        CommonBusiness commonBu = new CommonBusiness();

        #region HttpGet

        // GET: Account
        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            Session["ReturnUrl"] = ReturnUrl ?? "/";

            return PartialView();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["USER"] = null;
            return RedirectToAction("Login", "Accounts");
        }
        #endregion
        
        #region HttpPost
        [HttpPost]
        public JsonResult GoogleLogin(GoogleAccountModel instance)
        {
            try
            {
                if (instance != null)
                {
                    Session["USER"] = instance;
                    MailHelper.SendEmail(new MailAddress(instance.Email,instance.FullName),"Cảnh báo đăng nhập!","Tài khoản của bạn vừa đăng nhập vào hệ thống CPM");
                    return Json("0");
                }
                return Json("1"); ;
            }
            catch (Exception ex)
            {
                return Json("2"); ;
            }
        }
        #endregion
    }
}