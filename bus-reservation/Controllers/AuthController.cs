using bus_reservation.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bus_reservation.Controllers
{
   

    public class AuthController : Controller
	{
        private readonly OnlineBusTicketReservationContext db;

        public AuthController(OnlineBusTicketReservationContext _db)
        {
            db = _db;
        }

        //[HttpGet]

        //public IActionResult Signup()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Signup(Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.Employees.Add(employee);
        //            await db.SaveChangesAsync();
        //            return RedirectToAction("Index", "Admin");
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log the exception (not shown here)
        //            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(employee); // Pass the employee back to the view to show validation errors
        //}

        public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(string Email , string Password)
		{
			
			var checkUser = db.Employees.FirstOrDefault(a => a.EmployeeEmail == Email);
            //var checkadmin = db.Admin.FirstOrDefault(a => a.Email == Email);

            if (checkUser != null)
			{
				var hasher = new PasswordHasher<string>();
				var verifyPass = hasher.VerifyHashedPassword(Email, checkUser.Password, Password);

				if (verifyPass == PasswordVerificationResult.Success)
				{
					var identity = new ClaimsIdentity(new[]
					{
				new Claim(ClaimTypes.Name, checkUser.Username),
				new Claim(ClaimTypes.Role, "Employee") // Replace "User" with dynamic role if you have roles
            },
                    CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

					 HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

					HttpContext.Session.SetInt32("UserID", checkUser.EmployeeId);
					HttpContext.Session.SetString("UserEmail", checkUser.EmployeeEmail);

					return RedirectToAction("Index", "Admin"); // Redirect to the appropriate controller/action
				}
				else
				{
					ViewBag.msg = "Invalid Credentials";
					return View();
				}
			}
            //else if (checkadmin != null)
            //{
            //    var hasher = new PasswordHasher<string>();
            //    var verifyPass = hasher.VerifyHashedPassword(Email, checkadmin.Password, Password);

            //    if (verifyPass == PasswordVerificationResult.Success)
            //    {
            //        var identity = new ClaimsIdentity(new[]
            //        {
            //    new Claim(ClaimTypes.Name, checkadmin.Username),
            //    new Claim(ClaimTypes.Role, "Admin") // Replace "User" with dynamic role if you have roles
            //}, CookieAuthenticationDefaults.AuthenticationScheme);

            //        var principal = new ClaimsPrincipal(identity);

            //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            //        HttpContext.Session.SetInt32("UserID", checkadmin.Id);
            //        HttpContext.Session.SetString("UserEmail", checkadmin.Email);

            //        return RedirectToAction("Index", "Admin"); // Redirect to the appropriate controller/action
            //    }
            //    else
            //    {
            //        ViewBag.msg = "Invalid Credentials";
            //        return View();
            //    }
            //}
            else
			{
				ViewBag.msg = "Invalid User";
				return View();
			}
		}

	}
}
