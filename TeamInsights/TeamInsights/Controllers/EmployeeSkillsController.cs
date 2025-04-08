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
            var userName = User.Identity.Name; // This gets the username of the logged-in user
            ViewData["UserName"] = userName;
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

        public IActionResult SkillCertificationMatrix(string searchTerm, string sortBy)
        {
            var employees = _context.People
                .Include(p => p.EmployeeSkills)
                    .ThenInclude(es => es.Skill)
                .Include(p => p.EmployeeCertifications)
                    .ThenInclude(ec => ec.Certification)
                .Include(p => p.EmployeeRoles)
                    .ThenInclude(er => er.Role)
                .Include(p => p.EmployeeRoles)
                    .ThenInclude(er => er.Performances)
                        .ThenInclude(pf => pf.Contribution)
                .Include(p => p.EmployeeRoles)
                    .ThenInclude(er => er.Performances)
                        .ThenInclude(pf => pf.Evaluation)
                .Include(p => p.EmployeeRoles) // Include Performances for projects
                    .ThenInclude(er => er.Performances)
                        .ThenInclude(pf => pf.Project)
                .Select(emp => new SkillCertificationMatrixViewModel
                {
                    EmployeeID = emp.PersonID,
                    EmployeeName = emp.FirstName,
                    Skills = emp.EmployeeSkills.Select(es => new SkillEntry
                    {
                        SkillName = es.Skill.SkillName,
                        SkillLevel = es.Skill.SkillLevel,
                        AcquiredDate = es.AcquiredDate
                    }).ToList(),
                    Certifications = emp.EmployeeCertifications.Select(ec => new CertificationEntry
                    {
                        CertificationName = ec.Certification.CertificationName,
                        IssuedDate = ec.IssuedDate
                    }).ToList(),
                    Roles = emp.EmployeeRoles.Select(er => new RoleEntry
                    {
                        RoleName = er.Role.RoleName,
                        AssignedDate = null, // Adjust if you add this field to EmployeeRole
                        RoleContributionsCount = er.Performances
                            .Count(pf => pf.ContributionID != null),
                        RoleContributionsDescriptions = er.Performances
                            .Where(pf => pf.ContributionID != null)
                            .Select(pf => pf.Contribution.Description)
                            .Where(desc => !string.IsNullOrEmpty(desc))
                            .ToList(),
                        AverageEvaluationScore = er.Performances
                            .Where(pf => pf.EvaluationID != null)
                            .Select(pf => pf.Evaluation.Score)
                            .DefaultIfEmpty()
                            .Average()
                    }).ToList(),
                    ProjectNames = emp.EmployeeRoles
                        .SelectMany(er => er.Performances)
                        .Where(pf => pf.ProjectID != null)
                        .Select(pf => pf.Project.ProjectName)
                        .Distinct() // Avoid duplicate project names
                        .ToList(),
                    ContributionsCount = emp.EmployeeRoles
                        .SelectMany(er => er.Performances)
                        .Count(pf => pf.ContributionID != null),
                    ContributionsDescriptions = emp.EmployeeRoles
                        .SelectMany(er => er.Performances)
                        .Where(pf => pf.ContributionID != null)
                        .Select(pf => pf.Contribution.Description)
                        .Where(desc => !string.IsNullOrEmpty(desc))
                        .ToList(),
                    AverageEvaluationScore = emp.EmployeeRoles
                        .SelectMany(er => er.Performances)
                        .Where(pf => pf.EvaluationID != null)
                        .Select(pf => pf.Evaluation.Score)
                        .DefaultIfEmpty()
                        .Average()
                });
            // Filtering
            if (!string.IsNullOrEmpty(searchTerm))
            {
                employees = employees.Where(e => e.EmployeeName.Contains(searchTerm) ||
                                                 e.Skills.Any(s => s.SkillName.Contains(searchTerm)) ||
                                                 e.Certifications.Any(c => c.CertificationName.Contains(searchTerm)) ||
                                                 e.Roles.Any(r => r.RoleName.Contains(searchTerm)) ||
                                                 e.ProjectNames.Any(pn => pn.Contains(searchTerm))); // Include project names in search
            }
            // Sorting
            employees = sortBy switch
            {
                "name" => employees.OrderBy(e => e.EmployeeName),
                "skill" => employees.OrderBy(e => e.Skills.FirstOrDefault().SkillName ?? ""),
                "certification" => employees.OrderBy(e => e.Certifications.FirstOrDefault().CertificationName ?? ""),
                "role" => employees.OrderBy(e => e.Roles.FirstOrDefault().RoleName ?? ""),
                "avgEvaluationScore" => employees
                    .OrderByDescending(e => e.AverageEvaluationScore.HasValue ? e.AverageEvaluationScore.Value : double.MinValue),
                _ => employees
            };
            return View(employees.ToList());
        }

    }
}
