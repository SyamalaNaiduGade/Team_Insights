using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using TeamInsights.Models;
using TeamInsights.DAL;
namespace TeamInsights.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TeamInsightsContext _context; public HomeController(ILogger<HomeController> logger, TeamInsightsContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var managerCount = await _context.People
                .Where(p => _context.People.Any(e => e.ManagerID == p.PersonID))
                .CountAsync(); var employeeCount = await _context.People
                .Where(p => p.ManagerID != null)
                .CountAsync(); ViewBag.ManagerCount = managerCount;
            ViewBag.EmployeeCount = employeeCount; return View();
        }
        public async Task<IActionResult> Managers()
        {
            var managers = await _context.People
                .Include(p => p.Manager)
                .Where(p => _context.People.Any(e => e.ManagerID == p.PersonID))
                .ToListAsync(); return View(managers);
        }
        public async Task<IActionResult> Employees()
        {
            var employees = await _context.People
                .Include(p => p.Manager)
                .Where(p => p.ManagerID != null)
                .ToListAsync(); return View(employees);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}