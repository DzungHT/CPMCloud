using CPMCloud.Models;
using CPMCloud.Models.Entities;
using CybertronFramework;
using CybertronFramework.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CPMCloud.Controllers
{
    public class MasterDataController : Controller
    {
        CommonBusiness commonBu = new CommonBusiness();
        public JsonResult GetListMenu()
        {
            List<Menu> lstResult = commonBu.GetAll<Menu>();
            return Json(lstResult, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListApplication()
        {
            List<Application> lstResult = commonBu.GetAll<Application>();
            return Json(lstResult, JsonRequestBehavior.AllowGet);
        }
    }
}