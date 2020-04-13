using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class BookController : Controller
    {
        List<BookViewModel> _books;

        public BookController()
        {
            _books = new List<BookViewModel>{
                new BookViewModel{Id=1, Title="Все о нашей вселенной", Author="Иванов", PagesNumber=300, Year=2017},
                new BookViewModel{Id=2, Title="Жизнь на планете Земля", Author="Петров", PagesNumber=150, Year=2015}
            };
        }

        // GET: /<controller>/
        public IActionResult Books()
        {
            return View(_books);
        }

        public IActionResult BookDetails(int id)
        {
            return View(_books.FirstOrDefault(book => book.Id == id));
        }
    }
}
