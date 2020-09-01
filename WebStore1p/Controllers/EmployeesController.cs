using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebStore1p.Data;
using WebStore1p.Infrastructure.Interfaces;
using WebStore1p.Models;
using WebStore1p.ViewModels;

namespace WebStore1p.Controllers
{

    //[Route("users")]
    public class EmployeesController : Controller
    {
        //ПШ Стандартный сценарий(3й урок на 1.32мин)
        //ПШ Создать интерфейс, того чего вы хотите сделать с описанием действий, которые вы хотите сделать
        //ПШ Создать реализацию этого интерфейса в виде класса, со всеми методами
        //ПШ Идем в Startup и добавляем этот интерфейс в контейнер сервисов
        //ПШ services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
        //ПШ Описание контроллера, который будет взаимодействовать
        //ПШ Здесь смысл интерфейсов очень ярко проявляет себя

        //ПШ добавляем поле
        private readonly IEmployeesData _EmployeesData;

        //ПШ добавляем конструктор
        public EmployeesController(IEmployeesData EmployeesData) { _EmployeesData = EmployeesData; }

        //[Route("Employees")]
        public IActionResult Index() => View(_EmployeesData.GetAll().Select(e => new EmployeeViewModel
        {
            Id = e.Id,
            Name = e.FirstName,
            SecondName = e.SurName,
            Patronymic = e.Patronymic,
            Age = e.Age
        }));

        //[Route("Employee/{Id}")]
        public IActionResult Details(int Id)
        {
            var employee = _EmployeesData.GetById(Id);
            if (employee is null)
                return NotFound();
            return View(new EmployeeViewModel 
            {
                Id = employee.Id,
                Name = employee.FirstName,
                SecondName = employee.SurName,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });

        }

        //ПШ редактирование реализуется в 2 этапа

        public IActionResult Edit(int? Id)
        {
            if (Id is null) return View(new EmployeeViewModel());

            if (Id < 0)
                return BadRequest();

            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.FirstName,
                SecondName = employee.SurName,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });
        }

        public IActionResult Create()
        {
            return View(new EmployeeViewModel());
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel Employee)
        {
            if (Employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (Employee.Name == "123" && Employee.SecondName == "QWE")
                ModelState.AddModelError(string.Empty, "Странное имя и фамилия ...");

            if (!ModelState.IsValid)
                return View(Employee);

            _EmployeesData.Add(new Employee
            {
                Id = Employee.Id,
                FirstName = Employee.Name,
                SurName = Employee.SecondName,
                Patronymic = Employee.Patronymic,
                Age = Employee.Age
            });
            _EmployeesData.SaveChages();

            return RedirectToAction("Index");
        }


        //ПШ ответная часть тоже называется также (обычно) в данном случае Edit, но в качестве параметра указывается модель (или ViewModel)
        //И указываем что данный метод будет реагировать исключительно на Post запросы

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel Employee)
        {
            if (Employee is null)
                throw new System.ArgumentNullException(nameof(Employee));

            //ПШ Делаем проверку модели, если не соответствует отправляем пользователю обратно
            if (!ModelState.IsValid)
                return View(Employee);

            var id = Employee.Id;
            //ПШ если Id нулевой, то это новый сотрудник и мы его добавляем, иначе редактируем
            if (id == 0)
                _EmployeesData.Add(new Employee
                {
                Id = Employee.Id,
                    FirstName = Employee.Name,
                    SurName = Employee.SecondName,
                    Patronymic = Employee.Patronymic,
                    Age = Employee.Age
                });
            else
                _EmployeesData.Edit(id, new Employee
                {
                    Id = Employee.Id,
                    FirstName = Employee.Name,
                    SurName = Employee.SecondName,
                    Patronymic = Employee.Patronymic,
                    Age = Employee.Age
                });

            _EmployeesData.SaveChages();

            return RedirectToAction("Index");

        }


        public IActionResult Delete(int Id)
        {
            if (Id <= 0) return BadRequest();

            var employee = _EmployeesData.GetById(Id);

            if (employee is null) return NotFound();

            //_EmployeesData.Delete(Id);
            //return RedirectToAction("Index");

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.FirstName,
                SecondName = employee.SurName,
                Patronymic = employee.Patronymic,
                Age = employee.Age
            });
        }

        public IActionResult DeleteConfirmed(int Id)
        {
            _EmployeesData.Delete(Id);
            _EmployeesData.SaveChages();

            return RedirectToAction("Index");
        }
    }
}
