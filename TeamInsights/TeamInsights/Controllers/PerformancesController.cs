using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamInsights.DAL;
using TeamInsights.Models;

namespace TeamInsights.Controllers
{
    public class PerformancesController : Controller
    {
        private readonly TeamInsightsContext _context;

        public PerformancesController(TeamInsightsContext context)
        {
            _context = context;
        }

        // GET: Performances
        public async Task<IActionResult> Index()
        {
            var teamInsightsContext = _context.Performances.Include(p => p.Contribution).Include(p => p.EmployeeRole).Include(p => p.Evaluation).Include(p => p.Manager).Include(p => p.Project);
            return View(await teamInsightsContext.ToListAsync());
        }

        // GET: Performances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performances
                .Include(p => p.Contribution)
                .Include(p => p.EmployeeRole)
                .Include(p => p.Evaluation)
                .Include(p => p.Manager)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.PerformanceID == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // GET: Performances/Create
        public IActionResult Create()
        {
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "Description");
            ViewData["EmployeeRoleID"] = new SelectList(_context.EmployeeRoles, "EmployeeRoleID", "EmployeeRoleID");
            ViewData["EvaluationID"] = new SelectList(_context.Evaluations, "EvaluationID", "Comments");
            ViewData["ManagerID"] = new SelectList(_context.People, "PersonID", "FirstName");
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName");
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerformanceID,ManagerID,EmployeeRoleID,ProjectID,ContributionID,EvaluationID,HoursWorked,Year")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "Description", performance.ContributionID);
            ViewData["EmployeeRoleID"] = new SelectList(_context.EmployeeRoles, "EmployeeRoleID", "EmployeeRoleID", performance.EmployeeRoleID);
            ViewData["EvaluationID"] = new SelectList(_context.Evaluations, "EvaluationID", "Comments", performance.EvaluationID);
            ViewData["ManagerID"] = new SelectList(_context.People, "PersonID", "FirstName", performance.ManagerID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", performance.ProjectID);
            return View(performance);
        }

        // GET: Performances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performances.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "Description", performance.ContributionID);
            ViewData["EmployeeRoleID"] = new SelectList(_context.EmployeeRoles, "EmployeeRoleID", "EmployeeRoleID", performance.EmployeeRoleID);
            ViewData["EvaluationID"] = new SelectList(_context.Evaluations, "EvaluationID", "Comments", performance.EvaluationID);
            ViewData["ManagerID"] = new SelectList(_context.People, "PersonID", "FirstName", performance.ManagerID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", performance.ProjectID);
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerformanceID,ManagerID,EmployeeRoleID,ProjectID,ContributionID,EvaluationID,HoursWorked,Year")] Performance performance)
        {
            if (id != performance.PerformanceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformanceExists(performance.PerformanceID))
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
            ViewData["ContributionID"] = new SelectList(_context.Contributions, "ContributionID", "ContributionID", performance.ContributionID);
            ViewData["EmployeeRoleID"] = new SelectList(_context.EmployeeRoles, "EmployeeRoleID", "EmployeeRoleID", performance.EmployeeRoleID);
            ViewData["EvaluationID"] = new SelectList(_context.Evaluations, "EvaluationID", "EvaluationID", performance.EvaluationID);
            ViewData["ManagerID"] = new SelectList(_context.People, "PersonID", "FirstName", performance.ManagerID);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", performance.ProjectID);
            return View(performance);
        }

        // GET: Performances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performances
                .Include(p => p.Contribution)
                .Include(p => p.EmployeeRole)
                .Include(p => p.Evaluation)
                .Include(p => p.Manager)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.PerformanceID == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performance = await _context.Performances.FindAsync(id);
            if (performance != null)
            {
                _context.Performances.Remove(performance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformanceExists(int id)
        {
            return _context.Performances.Any(e => e.PerformanceID == id);
        }
    }
}
