using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    //[Example_SimpleActionFilter] //через атрибут можно подключить фильтр к целому контроллеру или отдельным экшн-методам
    //[Route("users")] //пример маршрутизации атрибутами (путь будет не employee, а users)
    public class EmployeeController : Controller
    {
        IitemService<EmployeeViewModel> _employees;

        public EmployeeController(IitemService<EmployeeViewModel> employees)
        {
            _employees = employees;
        }

        //[Route("all")] //пример маршрутизации атрибутами (// GET: users/all)
        //GET: /<controller>/employees
        public IActionResult Employees()
        {
            return View(_employees.GetAll());
        }

        //[Route("{id}")] //пример маршрутизации атрибутами (// GET: /users/{id})
        //GET: /<controller>/employees/details/{id}
        public IActionResult Details(int id)
        {
            return View(_employees.GetById(id));
        }

        //GET: /<controller>/employees/edit/{id?}
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return View(new EmployeeViewModel());
            EmployeeViewModel employee = _employees.GetById(id.Value);
            if (employee==null) return NotFound();
            return View(employee);
        }

        //POST: /<controller>/employees/edit/
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel _employee)
        {
            EmployeeViewModel employee = _employees.GetById(_employee.Id);
            if (employee == null) _employees.AddNew(_employee);
            else
            {
                employee.FirstName = _employee.FirstName;
                employee.LastName = _employee.LastName;
                employee.Patronymic = _employee.Patronymic;
                employee.Age = _employee.Age;
                employee.Position = _employee.Position;
            }
            _employees.Commit();
            return RedirectToAction("Employees");
        }

        public IActionResult Delete(int id)
        {
            _employees.Delete(id);
            return RedirectToAction("Employees");
        }
    }
}
