﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore1p.Models;

namespace WebStore1p.Controllers
{
    public class HomeController : Controller
    {

        private static readonly List<Employee> __Employees = new List<Employee>()
        {
            new Employee
            {
                Id =1,
                SurName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                Age = 39
            }
            ,
            new Employee
            {
                Id =2,
                SurName = "Петров",
                FirstName = "Петр",
                Patronymic = "Петрович",
                Age = 18
            },
            new Employee
            {
                Id =3,
                SurName = "Сидоров",
                FirstName = "Сидор",
                Patronymic = "Сидорович",
                Age = 27
            }
        };

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

        public IActionResult Employees()
        {
            return View(__Employees);
        }

        public IActionResult Employee(int Id)
        {
            var employee = __Employees.FirstOrDefault(e => e.Id == Id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        public IActionResult Error404() => View();

        public IActionResult Blog() => View();

        public IActionResult BlogSingle() => View();

        public IActionResult Cart() => View();

        public IActionResult CheckOut() => View();

        public IActionResult ContactUs() => View();

        public IActionResult Login() => View();

        public IActionResult Shop() => View();

        public IActionResult ProductDetails() => View();
    }
}
