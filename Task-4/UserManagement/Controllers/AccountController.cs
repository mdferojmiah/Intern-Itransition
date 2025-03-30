using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseService _databaseService;
        public AccountController(DatabaseService databaseService){
            _databaseService = databaseService; 
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserEmail") != null){
                return RedirectToAction("Index", "Users");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model){
            if(ModelState.IsValid){
                var user = _databaseService.AuthenticateUser(model.Email, model.Password);
                if(user != null){
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    TempData["SuccessMessage"] = "Login successfull!";
                    return RedirectToAction("Index", "Users");
                }

                ModelState.AddModelError("", "Invalid Login attempt or account is blocked!");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(){
            if(HttpContext.Session.GetString("UserEmail") != null){
                return RedirectToAction("Index", "Users");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model){
            if(ModelState.IsValid){
                if(_databaseService.RegisterUser(model)){
                    TempData["SuccessMessage"] = "Registration successfull!";
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("Email", "Email already exists!");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}