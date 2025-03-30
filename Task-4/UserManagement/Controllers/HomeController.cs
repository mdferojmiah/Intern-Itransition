using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UserManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("UserEmail") == null){
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Index", "Users");
        }
    }
}