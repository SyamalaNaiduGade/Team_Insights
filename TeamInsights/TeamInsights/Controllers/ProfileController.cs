using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamInsights.DAL;

namespace TeamInsights.Controllers
{
    public class ProfileController : Controller
    {

        private readonly TeamInsightsContext _context;
        public ProfileController(TeamInsightsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var email = User.Identity.Name;
            var person = await _context.People
                .Include(p => p.EmployeeRoles).ThenInclude(er => er.Role)
                .Include(p => p.EmployeeSkills).ThenInclude(es => es.Skill)
                .Include(p => p.EmployeeCertifications).ThenInclude(ec => ec.Certification)
                .FirstOrDefaultAsync(p => p.Email == email);

            if (person == null)
                return NotFound();

            return View(person);
        }
    }
}
