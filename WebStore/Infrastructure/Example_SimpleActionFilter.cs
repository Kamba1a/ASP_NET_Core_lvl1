﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Infrastructure
{
    public class Example_SimpleActionFilter: Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //постобработка 
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //предобработка 
            //throw new NotImplementedException();
        }
    }
}
