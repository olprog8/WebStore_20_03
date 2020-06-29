using System;
using System.Collections.Generic;
using WebStore1p.Models;
using WebStore1p.Data;
using WebStore1p.Infrastructure.Interfaces;
using System.Linq;

namespace WebStore1p.Infrastructure.Services
{
    public class InMemoryEmployeesData: IEmployeesData
    {
        private readonly List<Employee> _Employees = TestData.Employees;
        public IEnumerable<Employee> GetAll() => _Employees;;

        public Employee GetById(int id) => _Employees.FirstOrDefault(e => e.Id == id);

        public void Add(Employee Employee) {
            
            if (Employee is null)
                throw new ArgumentNullException(nameof(Employee));
            
            if (_Employees.Contains(Employee)) return;
            
            Employee.Id = _Employees.Count == 0 ? 1 : _Employees.Max(e => e.Id) + 1;
            
            _Employees.Add(Employee);
        }

        //ПШ У нас приложение живет небольшими сессиями, каждое обращение для нашего приложения как что-то новое
        //ПШ т.е. если обычное десктопное приложение запускается, то у него есть интерфейс который постоянно существует и набор сервисов, модель
        //ПШ все это постоянно есть, в данном случае мы не можем говорить, что у нас постоянно есть какие-то сервисы, что у нас есть какие-то данные
        //ПШ хостинг может быть настроен так, что будет отключать ваш сайт, когда 5 минут не было обращений к нему
        //ПШ поэтому принцип построен так, что для приложения каждое обращение это новые данные новые объекты, новые модели, новые объекты сервисов
        public void Edit(int id, Employee Employee) 
        {
            if (Employee is null)
                throw new ArgumentOutOfRangeException(nameof(Employee));

            if (_Employees.Contains(Employee)) return;

            var db_employee = GetById(id);
            if (db_employee is null)
                return; //ПШ здесь бы надо выбросить исключение, что объект не найден

            //ПШ Идентификатор не заносим, т.к. им управляет БД
            db_employee.SurName = Employee.SurName;
            db_employee.FirstName = Employee.FirstName;
            db_employee.Patronymic = Employee.Patronymic;
            db_employee.Age = Employee.Age;
        };

        public bool Delete(int id) 
        {
            var db_employee = GetById(id);
            if (db_employee is null)
                return false;

            return _Employees.Remove(db_employee);

        }

        public void SaveChages() { }


    }
}
