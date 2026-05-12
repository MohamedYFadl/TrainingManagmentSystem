using Microsoft.AspNetCore.Mvc;

namespace MVC02.Controllers
{
    public class StateController : Controller
    {

        public IActionResult SetSession(string name,int age)
        {
            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetInt32("Age", age);
            return Content("Session Saved");
        }

        public IActionResult GetSession()
        {
            var name = HttpContext.Session.GetString("Name");
            var age = HttpContext.Session.GetInt32("Age");
            return Content($"Name: {name}, Age: {age}");
        }
    }
}
