using Microsoft.AspNetCore.Mvc;

namespace bus_reservation.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult index()
        {
           
            return View();
        }

        public IActionResult Addemployee()
        {

            return View();
        }
    }
}
