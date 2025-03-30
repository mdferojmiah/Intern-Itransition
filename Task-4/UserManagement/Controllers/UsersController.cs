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
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly DatabaseService _databaseService;
        public UsersController(DatabaseService databaseService){
            _databaseService = databaseService; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            
            if(userEmail == null){
                return RedirectToAction("Login", "Account");
            }

            if(!_databaseService.IsUserValid(userEmail)){
                TempData["ErrorMessage"] = "Your account has been blocked or deleted!";
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new UserViewModel{
                Users = _databaseService.GetAllUsers(),
                CurrentUserEmail = userEmail
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Block(List<string> users){
            var userEmail = HttpContext.Session.GetString("UserEmail");
            
            if(userEmail == null){
                return RedirectToAction("Login", "Account");
            }

            if(!_databaseService.IsUserValid(userEmail)){
                HttpContext.Session.Clear();
                TempData["ErrorMessage"] = "Your account has been blocked or deleted!";
                return RedirectToAction("Login", "Account");
            }

            if(users == null || users.Count == 0){
                TempData["ErrorMessage"] = "No user selected!";
                return RedirectToAction("Index");
            }

            _databaseService.BlockUsers(users);
            
            // if user block himself he will be logged out
            if(users.Contains(userEmail)){
                HttpContext.Session.Clear();
                TempData["ErrorMessage"] = "Your account has been blocked!";
                return RedirectToAction("Login", "Account");
            }

            TempData["SuccessMessage"] = "Users blocked successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Unblock(List<string> users){
            var userEmail = HttpContext.Session.GetString("UserEmail");
            
            if(userEmail == null){
                return RedirectToAction("Login", "Account");
            }

            if(!_databaseService.IsUserValid(userEmail)){
                HttpContext.Session.Clear();
                TempData["ErrorMessage"] = "Your account has been blocked or deleted!";
                return RedirectToAction("Login", "Account");
            }

            if(users == null || users.Count == 0){
                TempData["ErrorMessage"] = "No user selected!";
                return RedirectToAction("Index");
            }

            _databaseService.UnblockUsers(users);

            TempData["SuccessMessage"] = "Users unblocked successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(List<string> users){
            var userEmail = HttpContext.Session.GetString("UserEmail");
            
            if(userEmail == null){
                return RedirectToAction("Login", "Account");
            }

            if(!_databaseService.IsUserValid(userEmail)){
                TempData["ErrorMessage"] = "Your account has been blocked or deleted!";
                return RedirectToAction("Login", "Account");
            }

            if(users == null || users.Count == 0){
                TempData["ErrorMessage"] = "No user selected!";
                return RedirectToAction("Index");
            }

            _databaseService.DeleteUsers(users);
            
            // if user block himself he will be logged out
            if(users.Contains(userEmail)){
                HttpContext.Session.Clear();
                TempData["ErrorMessage"] = "Your account has been deleted!";
                return RedirectToAction("Register", "Account");
            }

            TempData["SuccessMessage"] = "Users deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}