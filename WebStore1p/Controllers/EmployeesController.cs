using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebStore1p.Data;
using WebStore1p.Infrastructure.Interfaces;
using WebStore1p.Models;

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
        public IActionResult Index() => View(_EmployeesData.GetAll());
        
        //[Route("Employee/{Id}")]
        public IActionResult Details(int Id)
        {
            var employee = _EmployeesData.GetById(Id);
            if (employee is null)
                return NotFound();
            return View(employee);

        }

        //ПШ редактирование реализуется в 2 этапа

        public IActionResult Edit(int? Id)
        {
            if (Id is null) return View(new Employee());

            if (Id < 0)
                return BadRequest();

            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        public IActionResult Create()
        {
            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Create(Employee Employee)
        {
            if (Employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (!ModelState.IsValid)
                return View(Employee);

            _EmployeesData.Add(Employee);
            _EmployeesData.SaveChages();

            return RedirectToAction("Index");
        }


        //ПШ ответная часть тоже называется также (обычно) в данном случае Edit, но в качестве параметра указывается модель (или ViewModel)
        //И указываем что данный метод будет реагировать исключительно на Post запросы

        [HttpPost]
        public IActionResult Edit(Employee Employee)
        {
            if (Employee is null)
                throw new System.ArgumentNullException(nameof(Employee));

            //ПШ Делаем проверку модели, если не соответствует отправляем пользователю обратно
            if (!ModelState.IsValid)
                return View(Employee);

            var id = Employee.Id;
            //ПШ если Id нулевой, то это новый сотрудник и мы его добавляем, иначе редактируем
            if (id == 0)
                _EmployeesData.Add(Employee);
            else
                _EmployeesData.Edit(id, Employee);

            _EmployeesData.SaveChages();

            return RedirectToAction("Index");

        }
    }
}
