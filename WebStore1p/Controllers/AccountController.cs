using Microsoft.AspNetCore.Mvc;

using WebStore1p.ViewModels.Identity;

namespace WebStore1p.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register() => View(new RegisterUserViewModel());

        public IActionResult Login() => View(new LoginViewModel());
    }
}
