using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewComponents
{
    public class LoginLogout: ViewComponent
    {
        public IViewComponentResult Invoke() //обязательный метод для ViewComponent (либо AsyncInvoke())
        {
            return View();
        }
    }
}
