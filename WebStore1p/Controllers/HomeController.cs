using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebStore1p.Controllers
{
    public class HomeController : Controller
    {
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            //return View("OtherViewName");
            return View();
        }
        public IActionResult SomeAction()
        {
            return View();
        }

    }
}
