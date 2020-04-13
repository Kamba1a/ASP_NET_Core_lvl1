using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class EmployeeController : Controller
    {
        List<EmployeeViewModel> _employees;

        public EmployeeController()
        {
            _employees = new List<EmployeeViewModel> { 
            new EmployeeViewModel{Id=1, FirstName="Иван", LastName="Иванов", Patronymic="Иванович", Age=30, Position="Специалист"},
            new EmployeeViewModel{Id=2, FirstName="Петр", LastName="Петров", Patronymic="Петрович", Age=35, Position="Начальник отдела"}
            };
        }

        //GET: /<controller>/
        public IActionResult Index()
        {
            //возвращает представление из Views-Home-Index.cshtml
            return View();
        }

        //GET: /<controller>/employees
        public IActionResult Employees()
        {
            return View(_employees);
        }

        //GET: /<controller>/employees/details/{id}
        public IActionResult Details(int id)
        {
            return View(_employees.FirstOrDefault(empl => empl.Id == id));
        }
    }
}
