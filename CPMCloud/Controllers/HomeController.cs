using System.Web.Mvc;
using CybertronFramework;
using CPMCloud.Models;

namespace CPMCloud.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["USER"] == null)
            {
                return RedirectToAction("Login", "Accounts");
            }
            return View();
        }
    }
}