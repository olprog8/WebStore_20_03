using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebStore1p.Controllers
{
    public class HomeController : Controller
    {
        //public async Task<IActionResult> Index()
        public IActionResult Index()
        {
            return Conflict("Home controller - action Index");
        }
        public IActionResult SomeAction()
        {
            return Conflict("Home controller - action SomeAction");
        }

    }
}
