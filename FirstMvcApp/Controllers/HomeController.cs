using FirstMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstMvcApp.Controllers
{

    public class HomeController : Controller
    {

        [Authorize]
        public IActionResult Index()
        {
            Console.WriteLine("Hello from action method.");
            return View();
        }


        [HttpPost]
        [Authorize]
        public IActionResult Index(Person person)
        {

            if (ModelState.IsValid)
            {
                Attendance.AddAttendant(person);
                TempData["FirstName"] = person.FirstName + " " + person.LastName;
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }


        public IActionResult AttendantDetails(string firstName, string lastName)
        {
            PeopleContext db = new PeopleContext();
            Person person = db.People.Where(p => p.FirstName.ToLower().Equals(firstName.ToLower()) && p.LastName.ToLower().Equals(lastName.ToLower())).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }
            return View("Attendant", person);
        }

    }
}
