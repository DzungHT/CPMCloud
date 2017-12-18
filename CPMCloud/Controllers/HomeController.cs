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
            return View();
        }
    }
}