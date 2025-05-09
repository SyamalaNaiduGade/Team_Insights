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
    public class EmployeeCertificationsController : Controller
    {
        private readonly TeamInsightsContext _context;

        public EmployeeCertificationsController(TeamInsightsContext context)
        {
            _context = context;
        }

        // GET: EmployeeCertifications
        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name; // This gets the username of the logged-in user
            ViewData["UserName"] = userName;
            var teamInsightsContext = _context.EmployeeCertifications.Include(e => e.Certification).Include(e => e.Employee);
            return View(await teamInsightsContext.ToListAsync());
        }

        // GET: EmployeeCertifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCertification = await _context.EmployeeCertifications
                .Include(e => e.Certification)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeCertificationID == id);
            if (employeeCertification == null)
            {
                return NotFound();
            }

            return View(employeeCertification);
        }

        // GET: EmployeeCertifications/Create
        public IActionResult Create()
        {
            ViewData["CertificationID"] = new SelectList(_context.Certifications, "CertificationID", "CertificationName");
            ViewData["EmployeeID"] = new SelectList(_context.People, "PersonID", "FirstName");
            return View();
        }

        // POST: EmployeeCertifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeCertificationID,EmployeeID,CertificationID,IssuedDate")] EmployeeCertification employeeCertification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeCertification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CertificationID"] = new SelectList(_context.Certifications, "CertificationID", "CertificationName", employeeCertification.CertificationID);
            ViewData["EmployeeID"] = new SelectList(_context.People, "PersonID", "FirstName", employeeCertification.EmployeeID);
            return View(employeeCertification);
        }

        // GET: EmployeeCertifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCertification = await _context.EmployeeCertifications.FindAsync(id);
            if (employeeCertification == null)
            {
                return NotFound();
            }
            ViewData["CertificationID"] = new SelectList(_context.Certifications, "CertificationID", "CertificationName", employeeCertification.CertificationID);
            ViewData["EmployeeID"] = new SelectList(_context.People, "PersonID", "FirstName", employeeCertification.EmployeeID);
            return View(employeeCertification);
        }

        // POST: EmployeeCertifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeCertificationID,EmployeeID,CertificationID,IssuedDate")] EmployeeCertification employeeCertification)
        {
            if (id != employeeCertification.EmployeeCertificationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeCertification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeCertificationExists(employeeCertification.EmployeeCertificationID))
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
            ViewData["CertificationID"] = new SelectList(_context.Certifications, "CertificationID", "CertificationName", employeeCertification.CertificationID);
            ViewData["EmployeeID"] = new SelectList(_context.People, "PersonID", "FirstName", employeeCertification.EmployeeID);
            return View(employeeCertification);
        }

        // GET: EmployeeCertifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeCertification = await _context.EmployeeCertifications
                .Include(e => e.Certification)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeCertificationID == id);
            if (employeeCertification == null)
            {
                return NotFound();
            }

            return View(employeeCertification);
        }

        // POST: EmployeeCertifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeCertification = await _context.EmployeeCertifications.FindAsync(id);
            if (employeeCertification != null)
            {
                _context.EmployeeCertifications.Remove(employeeCertification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeCertificationExists(int id)
        {
            return _context.EmployeeCertifications.Any(e => e.EmployeeCertificationID == id);
        }
    }
}
