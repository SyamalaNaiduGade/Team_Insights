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
    public class EmployeeSkillsController : Controller
    {
        private readonly TeamInsightsContext _context;

        public EmployeeSkillsController(TeamInsightsContext context)
        {
            _context = context;
        }

        // GET: EmployeeSkills
        public async Task<IActionResult> Index()
        {
            var teamInsightsContext = _context.EmployeeSkills.Include(e => e.Employee).Include(e => e.Skill);
            return View(await teamInsightsContext.ToListAsync());
        }

        // GET: EmployeeSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSkill = await _context.EmployeeSkills
                .Include(e => e.Employee)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.EmployeeSkillID == id);
            if (employeeSkill == null)
            {
                return NotFound();
            }

            return View(employeeSkill);
        }

        // GET: EmployeeSkills/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.People, "PersonID", "FirstName");
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillName");
            return View();
        }

        // POST: EmployeeSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeSkillID,EmployeeID,SkillID,AcquiredDate")] EmployeeSkill employeeSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.People, "PersonID", "FirstName", employeeSkill.EmployeeID);
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillName", employeeSkill.SkillID);
            return View(employeeSkill);
        }

        // GET: EmployeeSkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSkill = await _context.EmployeeSkills.FindAsync(id);
            if (employeeSkill == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.People, "PersonID", "FirstName", employeeSkill.EmployeeID);
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillName", employeeSkill.SkillID);
            return View(employeeSkill);
        }

        // POST: EmployeeSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeSkillID,EmployeeID,SkillID,AcquiredDate")] EmployeeSkill employeeSkill)
        {
            if (id != employeeSkill.EmployeeSkillID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeSkillExists(employeeSkill.EmployeeSkillID))
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
            ViewData["EmployeeID"] = new SelectList(_context.People, "PersonID", "FirstName", employeeSkill.EmployeeID);
            ViewData["SkillID"] = new SelectList(_context.Skills, "SkillID", "SkillName", employeeSkill.SkillID);
            return View(employeeSkill);
        }

        // GET: EmployeeSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSkill = await _context.EmployeeSkills
                .Include(e => e.Employee)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.EmployeeSkillID == id);
            if (employeeSkill == null)
            {
                return NotFound();
            }

            return View(employeeSkill);
        }

        // POST: EmployeeSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeSkill = await _context.EmployeeSkills.FindAsync(id);
            if (employeeSkill != null)
            {
                _context.EmployeeSkills.Remove(employeeSkill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeSkillExists(int id)
        {
            return _context.EmployeeSkills.Any(e => e.EmployeeSkillID == id);
        }
    }
}
