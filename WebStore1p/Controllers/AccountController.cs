﻿using Microsoft.AspNetCore.Mvc;

namespace WebStore1p.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register() => View();

        public IActionResult Login() => View();
    }
}
