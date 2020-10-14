using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;


namespace WebStore1p.Components
{
    public class UserInfoViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke() => User.Identity.IsAuthenticated
            ? View("UserInfo")
            : View();
    }
}
