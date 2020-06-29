using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using WebStore1p.Models;

namespace WebStore1p.Controllers
{

    [Route("users")]
    public class EmployeesController : Controller
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


        [Route("Employees")]
        public IActionResult Index() => View(__Employees);
        
        [Route("Employee/{Id}")]
        public IActionResult Details(int Id)
        {
            var employee = __Employees.FirstOrDefault(e => e.Id == Id);
            if (employee is null)
                return NotFound();
            return View(employee);

        }
    }
}
