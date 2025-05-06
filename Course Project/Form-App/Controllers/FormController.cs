using Microsoft.AspNetCore.Mvc;

namespace Form_App.Controllers
{
    public class FormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
