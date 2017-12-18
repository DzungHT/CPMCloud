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
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPMCloud.Controllers
{
    public class RolesController : Controller
    {
        CommonBusiness commonBu = new CommonBusiness();
        /// GET: Resources
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> SearchProcess(RoleViewModel formData)
        {
            DataTableResponse<Role> dataTableResponse = new DataTableResponse<Role>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                string sql = "SELECT * FROM Role a WHERE 1 = 1 ";
                sql += commonBu.MakeFilterString("a.RoleID", formData.RoleID, ref parameters);
                sql += commonBu.MakeFilterString("a.Code", formData.Code, ref parameters);
                sql += commonBu.MakeFilterString("a.Name", formData.Name, ref parameters);
                sql += commonBu.MakeFilterString("a.Description", formData.Description, ref parameters);

                dataTableResponse = commonBu.Search<Role>(formData.DataTable.start, formData.DataTable.length, sql, "Code,RoleID", parameters.ToArray());
            }
            catch (Exception ex)
            {

            }
            return Json(dataTableResponse, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Save(Role obj)
        {
            try
            {
                Role entities = new Role();
                if (obj.RoleID != 0)
                {
                    entities = commonBu.Get<Role>(obj.RoleID);
                    if (entities != null)
                    {
                        entities.GetTransferData(obj);
                        commonBu.Update(entities);
                        ViewBag.Status = "1";
                    }
                    else
                    {
                        ViewBag.Status = "-1";
                    }
                }
                else
                {
                    commonBu.Save(obj);
                    ViewBag.Status = "1";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Status = "-1";
            }
            return PartialView(Constants.VIEW.SAVE_RESULT);
        }

        [HttpPost]
        public async Task<JsonResult> PrepareUpdate(int id)
        {
            try
            {
                Role obj = commonBu.Get<Role>(id);
                return Json(obj);
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
            try
            {
                commonBu.Delete<Role>(id);
                ViewBag.Status = "1";
            }
            catch (Exception ex)
            {
                ViewBag.Status = "-1";
            }
            return PartialView(Constants.VIEW.SAVE_RESULT);
        }


        #region RoleMenu
        [HttpPost]
        public async Task<JsonResult> SearchMenuByRole(MenuViewModel formData)
        {
            DataTableResponse<MenuViewModel> dataTableResponse = new DataTableResponse<MenuViewModel>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                string sql = @"SELECT m.*, a.Name as ApplicationName FROM Menu m
                                INNER JOIN Application a ON a.ApplicationID = m.ApplicationID
                                INNER JOIN RoleMenu rm ON rm.MenuID = m.MenuID
                                WHERE 1 = 1 ";
                sql += commonBu.MakeFilterString("m.Code", formData.Code, ref parameters);
                sql += commonBu.MakeFilterString("m.Name", formData.Name, ref parameters);
                sql += commonBu.MakeFilterString("m.ApplicationID", formData.ApplicationID, ref parameters);
                sql += commonBu.MakeFilterString("rm.RoleID", formData.RoleID, ref parameters);

                var data = commonBu.Search<MenuViewModel>(formData.DataTable.start, formData.DataTable.length, sql, "ApplicationID,Sort_Order", parameters.ToArray());
            }
            catch (Exception ex)
            {
            }
            return Json(dataTableResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SearchMenuForRole(MenuViewModel formData)
        {
            DataTableResponse<MenuViewModel> dataTableResponse = new DataTableResponse<MenuViewModel>();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                string sql = @"SELECT m.*, a.Name as ApplicationName FROM Menu m
INNER JOIN Application a ON a.ApplicationID = m.ApplicationID
WHERE 1 = 1  AND NOT EXISTS(SELECT 1 FROM RoleMenu rm WHERE rm.MenuID = m.MenuID AND rm.RoleID = @RoleID)";
                parameters.Add(new SqlParameter("@RoleID", formData.RoleID));
                sql += commonBu.MakeFilterString("m.Code", formData.Code, ref parameters);
                sql += commonBu.MakeFilterString("m.Name", formData.Name, ref parameters);
                sql += commonBu.MakeFilterString("m.ApplicationID", formData.ApplicationID, ref parameters);

                dataTableResponse = commonBu.Search<MenuViewModel>(formData.DataTable.start, formData.DataTable.length, sql, "ApplicationID,Sort_Order", parameters.ToArray());
            }
            catch (Exception ex)
            {
            }
            return Json(dataTableResponse, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> DeleteMenu(MenuViewModel obj)
        {
            var trans = commonBu.getDbContext().Database.BeginTransaction();
            try
            {
                string sql = " DELETE RoleMenu WHERE MenuID = @MenuID AND RoleID = @RoleID ";
                int n = commonBu.getDbContext().Database.ExecuteSqlCommand(sql, new SqlParameter("@MenuID", obj.MenuID), new SqlParameter("@RoleID", obj.RoleID));
                trans.Commit();
                ViewBag.Status = "1";
            }
            catch (Exception ex)
            {
                trans.Rollback();
                ViewBag.Status = "-1";
            }
            return PartialView(Constants.VIEW.SAVE_RESULT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMenus(MenuViewModel obj)
        {
            var trans = commonBu.getDbContext().Database.BeginTransaction();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                string sql = "";
                foreach (var MenuID in obj.Selection)
                {
                    string param = "@MenuID" + parameters.Count;
                    sql += @" INSERT INTO RoleMenu (RoleID, MenuID) Values(@RoleID, " + param + ") ";
                    parameters.Add(new SqlParameter(param, MenuID));
                }
                parameters.Add(new SqlParameter("@RoleID", obj.RoleID));
                int n = commonBu.getDbContext().Database.ExecuteSqlCommand(sql, parameters.ToArray());
                trans.Commit();
                ViewBag.Status = "1";
            }
            catch (Exception ex)
            {
                ViewBag.Status = "-1";
                trans.Rollback();
            }
            return PartialView(Constants.VIEW.SAVE_RESULT);
        }
        #endregion
    }
}