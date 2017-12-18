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
using System.Web.Mvc;

namespace CPMCloud.Controllers
{
    public class MenusController : Controller
    {
        CommonBusiness commonBu = new CommonBusiness();
        // GET: Menus
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SearchProcess(MenuViewModel formData)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            string sql = "SELECT * FROM Menu a WHERE 1 = 1 ";
            sql += commonBu.MakeFilterString("a.ApplicationID", formData.ApplicationID, ref parameters);
            sql += commonBu.MakeFilterString("a.Code", formData.Code, ref parameters);
            sql += commonBu.MakeFilterString("a.Name", formData.Name, ref parameters);

            var data = commonBu.Search<Menu>(formData.DataTable.start, formData.DataTable.length, sql, "MenuID", parameters.ToArray());
            DataTableResponse<Menu> dataTableResponse = new DataTableResponse<Menu>();
            dataTableResponse = data;
            return Json(dataTableResponse, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Menu obj)
        {
            try
            {
                Menu entities = new Menu();
                if (obj.MenuID != 0)
                {
                    entities = commonBu.Get<Menu>(obj.MenuID);
                    if (entities != null)
                    {
                        entities.GetTransferData(obj);
                        commonBu.Update(entities);
                    }
                }
                else
                {
                    entities.GetTransferData(obj);
                    commonBu.Save(entities);
                }

                ViewBag.Status = "1";

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
        public JsonResult PrepareUpdate(int id)
        {
            Menu obj = commonBu.Get<Menu>(id);
            MenuDTO result = new MenuDTO();
            result.GetTransferData(obj);
            if (result.MenuPID != null)
            {
                Menu objP = commonBu.Get<Menu>(result.MenuPID);
                result.MenuPName = objP.Name;
            }
            return Json(result);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                commonBu.Delete<Menu>(id);
                ViewBag.Status = "1";

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