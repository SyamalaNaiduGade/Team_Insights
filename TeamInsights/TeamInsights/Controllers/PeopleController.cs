using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamInsights.DAL;
using TeamInsights.Models;
using TeamInsights.ViewModels;

namespace TeamInsights.Controllers
{
    public class PeopleController : Controller
    {
        private readonly TeamInsightsContext _context;

        public PeopleController(TeamInsightsContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name; // This gets the username of the logged-in user
            ViewData["UserName"] = userName;
            var teamInsightsContext = _context.People.Include(p => p.Manager);
            return View(await teamInsightsContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["ManagerID"] = new SelectList(_context.People, "PersonID", "FirstName");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FirstName,LastName,Email,PhoneNumber,Street,City,State,ZipCode,HireDate,Experience,Salary,ManagerID")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerID"] = new SelectList(_context.People, "PersonID", "FirstName", person.ManagerID);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["ManagerID"] = new SelectList(_context.People, "PersonID", "FirstName", person.ManagerID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonID,FirstName,LastName,Email,PhoneNumber,Street,City,State,ZipCode,HireDate,Experience,Salary,ManagerID")] Person person)
        {
            if (id != person.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerID"] = new SelectList(_context.People, "PersonID", "FirstName", person.ManagerID);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Performance(int personId, int? year)
        {
            // Get the employee details
            var employee = await _context.People.FindAsync(personId);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            // Fetch all performances for the employee
            IQueryable<Performance> performancesQuery = _context.Performances
                .Include(p => p.Project)
                .Include(p => p.Contribution)
                .Include(p => p.Evaluation)
                .Where(p => p.EmployeeRole.EmployeeID == personId);

            // Filter by year if provided
            if (year.HasValue)
            {
                performancesQuery = performancesQuery.Where(p => p.Year.Value.Year == year.Value);
            }

            var performances = await performancesQuery.ToListAsync();

            // Get distinct years for filtering
            var years = await _context.Performances
                .Where(p => p.EmployeeRole.EmployeeID == personId)
                .Select(p => p.Year.Value.Year)
                .Distinct()
                .OrderBy(y => y)
                .ToListAsync();

            // Pass the report data to the View
            var viewModel = new PerformanceReportViewModel
            {
                Employee = employee,
                Performances = performances,
                Year = year ?? DateTime.Now.Year // Use current year if year is null
            };

            ViewBag.Years = years;
            ViewBag.SelectedYear = year;

            return View(viewModel);
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.PersonID == id);
        }
    }
}