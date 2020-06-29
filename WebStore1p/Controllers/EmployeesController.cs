using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore1p.Data;

namespace WebStore1p.Controllers
{

    //[Route("users")]
    public class EmployeesController : Controller
    {

        //[Route("Employees")]
        public IActionResult Index() => View(TestData.Employees);
        
        //[Route("Employee/{Id}")]
        public IActionResult Details(int Id)
        {
            var employee = TestData.Employees.FirstOrDefault(e => e.Id == Id);
            if (employee is null)
                return NotFound();
            return View(employee);

        }
    }
}
