using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore1p.Models;

namespace WebStore1p.Infrastructure.Interfaces
{
    //Создаем сервис (в виде интерфейса и перечисляем его члены)
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        void Add(Employee Employee);

        void Edit(int id, Employee Employee);

        bool Delete(int id);

        void SaveChages();
    }
}
