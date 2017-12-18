using System.Web.Mvc;
using CybertronFramework;
using CPMCloud.Models;

namespace CPMCloud.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        //[Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}