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

        #region Вход пользователя в систему
        //ПШ L6 2:08 Метод Get информацию ReturnUrl мы сохраняем в модели
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl});

        //ПШ L6 2:08 ответный асинхронный метод будет работать с LoginView моделью
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            var login_result = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
                false //Не хотим блокировать пользователя если он накосячил со вводом пароля 10 раз
                );


            if (login_result.Succeeded)
            {
                //ПШ L6 проверяем, что сайт локальный, т.к. бывают хакерские атаки с подсовыванием хитрого имени URL, иначе, перенаправляем на нашу страницу
                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return RedirectToAction(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            //ПШ L6 2:12 При ошибке выводим сообщения
            ModelState.AddModelError(string.Empty, "Неверное имя пользователя, или пароль");

            //ПШ L6 возвращаем модель
            return View(Model);

        }
        #endregion
        public async Task<IActionResult> Logout() {

            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
