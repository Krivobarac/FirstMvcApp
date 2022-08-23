using FirstMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FirstMvcApp.Controllers
{

    public class HomeController : Controller
    {
        IStringLocalizer<SharedResource> _localizer;

        public HomeController(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        [Authorize]
        public IActionResult Index()
        {
            Console.WriteLine(_localizer["Hello from action method."].Value);
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

        public IActionResult SetCulture(string culture, string sourceUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return Redirect(sourceUrl);
        }


    }
}
