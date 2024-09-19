using Microsoft.AspNetCore.Mvc;
using bus_reservation.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace bus_reservation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly BusReservationContext _context;

        public EmployeeController(BusReservationContext context)
        {
            _context = context;
        }

		// GET: Employee/Edit/5
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var employee = await _context.Employees.FindAsync(id);
			if (employee == null)
			{
				return NotFound();
			}

			return View(employee);
		}


		[HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Employeedetail));
            }
            return View(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        // GET: Employee/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Employeedetail");
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var hasher = new PasswordHasher<string>();
                string hashPassword = hasher.HashPassword(employee.EmployeeEmail, employee.Password);
                employee.Password = hashPassword;
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Employeedetail()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }


    }
}
