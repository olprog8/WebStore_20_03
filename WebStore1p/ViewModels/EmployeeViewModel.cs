using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore1p.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]

        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage ="Имя является обязательным")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательным")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }
    }

}
