using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

using WebStore1p.ViewModels.Identity;

using WebStore1p.Domain.Identity;

using System.Threading.Tasks;

namespace WebStore1p.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }

        #region Регистрация пользователя в системе
        public IActionResult Register() => View(new RegisterUserViewModel());


        //ПШ L6  1:43 ValidateAntiForgeryToken - система защиты сервера от несанкционированного внедрения и подлога
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid) //ПШ L6 1:43 Если модель не валидна возвратим ту же модель, с выводом ошибок валидации
                return View(Model);

            //ПШ L6 Если всё ок,создаем пользвателя
            var user = new User
            {
                UserName = Model.UserName

            };

            //ПШ L6 Обращаемся к менеджеру пользователей, но регистрация может пройти успешно, а может неуспешно
            var register_result = await _UserManager.CreateAsync(user, Model.Password);
            if (register_result.Succeeded)
            {
                await _SignInManager.SignInAsync(user, false);

                //ПШ L6 перебрасываем на главный контроллер
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in register_result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(Model);
        }
        #endregion

        public IActionResult Login() => View(new LoginViewModel());
    }
}
