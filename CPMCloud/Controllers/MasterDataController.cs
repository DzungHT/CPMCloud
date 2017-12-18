using CPMCloud.Models;
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
        public async Task<JsonResult> getListApplication()
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                var apiResult = await client.GetApiAsync<JsonResultObject<List<Application>>>(Resources.URLResources.GET_ALL_APPLICATION);
                if (apiResult != null && apiResult.IsSuccess)
                {
                    return Json(apiResult.Data, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetListMenu()
        {
            ApiClient client = ApiClient.Instance;
            try
            {
                var apiResult = await client.GetApiAsync<JsonResultObject<List<Menu>>>(Resources.URLResources.GET_ALL_MENU);
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