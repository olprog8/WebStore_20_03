﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebStore1p.ViewModels;
using WebStore1p.Models;

namespace WebStore1p.Infrastructure.Mapping
{
    public static class EmployeeMapping
    {
        public static EmployeeViewModel ToView(this Employee e) => new EmployeeViewModel
        {
            Id = e.Id,
            Name = e.FirstName,
            SecondName = e.SurName,
            Patronymic = e.Patronymic,
            Age = e.Age
        };

        public static Employee FromView(this EmployeeViewModel e) => new Employee
        {
            FirstName = e.Name,
            SurName = e.SecondName,
            Patronymic = e.Patronymic,
            Age = e.Age
        };
    }
}