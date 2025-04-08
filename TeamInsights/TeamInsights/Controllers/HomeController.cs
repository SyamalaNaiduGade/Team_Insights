using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using TeamInsights.Models;
using TeamInsights.DAL;
using TeamInsights.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index(string? roleFilter)
        {
            var userName = User.Identity.Name; // This gets the username of the logged-in user
            ViewData["UserName"] = userName;
            //for cards 
            var managerCount = await _context.People
                .Where(p => _context.People.Any(e => e.ManagerID == p.PersonID))
                .CountAsync(); 
            var employeeCount = await _context.People
                .Where(p => p.ManagerID != null)
                .CountAsync(); 
            ViewBag.ManagerCount = managerCount;
            ViewBag.EmployeeCount = employeeCount;


            //const double MaxPossibleAvgEvaluationScore = 100.0;
            //const int MaxPossibleProjects = 20;
            //const int MaxPossibleSkills = 50;
            //const double MaxPossibleHours = 2080.0;

            //var employees = await _context.People
            //    .Include(p => p.Performances)!.ThenInclude(perf => perf.Project)
            //    .Include(p => p.Performances)!.ThenInclude(perf => perf.Evaluation)
            //    .Include(p => p.EmployeeSkills)
            //    .Include(p => p.EmployeeRoles)!.ThenInclude(er => er.Role)
            //    .Where(p => p.Performances != null && p.Performances.Any())
            //    .ToListAsync();

            //var employeeScores = new List<TopPerformerViewModel>();

            //foreach (var employee in employees)
            //{
            //    var roleNames = employee.EmployeeRoles?
            //                        .Select(r => r.Role?.RoleName)
            //                        .Where(r => !string.IsNullOrEmpty(r))
            //                        .ToList() ?? new List<string>();

            //    // Skip if no roles or does not match filter
            //    if (!roleNames.Any()) continue;
            //    if (!string.IsNullOrEmpty(roleFilter) && !roleNames.Contains(roleFilter)) continue;

            //    var evals = employee.Performances!.Select(p => p.Evaluation?.Score).Where(s => s.HasValue).ToList();
            //    double avgEvalScore = evals.Any() ? evals.Average() ?? 0 : 0;
            //    double normalizedEvalScore = (MaxPossibleAvgEvaluationScore > 0) ? (avgEvalScore / MaxPossibleAvgEvaluationScore) * 100 : 0;

            //    int projectCount = employee.Performances.Select(p => p.ProjectID).Distinct().Count();
            //    double normalizedProjectScore = (MaxPossibleProjects > 0) ? ((double)projectCount / MaxPossibleProjects) * 100 : 0;

            //    int skillCount = employee.EmployeeSkills?.Count ?? 0;
            //    double normalizedSkillScore = (MaxPossibleSkills > 0) ? ((double)skillCount / MaxPossibleSkills) * 100 : 0;

            //    double totalHours = employee.Performances.Sum(p => p.HoursWorked ?? 0);
            //    double normalizedHoursScore = (MaxPossibleHours > 0) ? (totalHours / MaxPossibleHours) * 100 : 0;

            //    double performanceScore = (normalizedProjectScore * 0.40) +
            //                              (normalizedEvalScore * 0.30) +
            //                              (normalizedSkillScore * 0.20) +
            //                              (normalizedHoursScore * 0.10);
            //    performanceScore = Math.Min(performanceScore, 100.0);

            //    employeeScores.Add(new TopPerformerViewModel
            //    {
            //        EmployeeName = $"{employee.FirstName} {employee.LastName}",
            //        RoleNames = roleNames,
            //        PerformanceScore = performanceScore
            //    });
            //}

            //var topPerformers = employeeScores
            //                    .OrderByDescending(e => e.PerformanceScore)
            //                    .Take(5)
            //                    .Select((e, index) => new TopPerformerViewModel
            //                    {
            //                        Rank = index + 1,
            //                        EmployeeName = e.EmployeeName,
            //                        RoleNames = e.RoleNames,
            //                        PerformanceScore = e.PerformanceScore
            //                    })
            //                    .ToList();

            //// Get unique role list for dropdown
            //var allRoles = await _context.Roles.Select(r => r.RoleName).Distinct().ToListAsync();
            //ViewData["Roles"] = new SelectList(allRoles);
            //ViewData["TopPerformers"] = topPerformers;
            //ViewData["SelectedRole"] = roleFilter;
            // Get Top 5 Performers
            var topPerformers = await GetTopPerformersAsync();
            ViewBag.TopPerformers = topPerformers;
            // Add Role Distribution
            var roleDistribution = await GetRoleDistributionAsync();
            ViewBag.RoleDistribution = roleDistribution;
            return View();
        }
        // New method to get role distribution
        private async Task<List<RoleDistributionViewModel>> GetRoleDistributionAsync()
        {
            var totalEmployees = await _context.People.CountAsync(); // Total employees
            var roleDistribution = await _context.Roles
                .GroupJoin(
                    _context.EmployeeRoles,
                    role => role.RoleID,
                    employeeRole => employeeRole.RoleID,
                    (role, employeeRoles) => new RoleDistributionViewModel
                    {
                        RoleName = role.RoleName,
                        EmployeeCount = employeeRoles.Count(),
                        Percentage = totalEmployees > 0 ? (employeeRoles.Count() * 100.0) / totalEmployees : 0
                    })
                .ToListAsync();
            return roleDistribution;
        }
        private async Task<List<TopPerformerViewModel>> GetTopPerformersAsync()
        {
            var employeesWithPerformanceData = await _context.People
                .Where(p => p.ManagerID != null) // Filter for employees
                .Select(employee => new
                {
                    Employee = employee,
                    Performances = _context.Performances
                        .Include(p => p.Contribution)
                        .Include(p => p.Evaluation)
                        .Where(p => _context.EmployeeRoles.Any(er => er.EmployeeID == employee.PersonID && er.EmployeeRoleID == p.EmployeeRoleID))
                        .ToList() // Execute the performance query for each employee
                })
                .ToListAsync(); // Execute the initial employee and related data query
            var topPerformers = employeesWithPerformanceData
                .Select(data => new TopPerformerViewModel
                {
                    PersonID = data.Employee.PersonID,
                    EmployeeName = data.Employee.FirstName + " " + data.Employee.LastName,
                    PerformanceScore = CalculatePerformanceScoreInMemory(data.Employee, data.Performances)
                })
                .OrderByDescending(tp => tp.PerformanceScore)
                .Take(5)
                .ToList(); // Finally, convert to a list of the view model
            return topPerformers;
        }
        // Helper method to calculate the performance score in memory with updated weights
        private double CalculatePerformanceScoreInMemory(Person employee, List<Performance> performances)
        {
            // Calculate Contribution Score (count of non-null contributions)
            double contributionScore = performances.Count(p => p.ContributionID != null);
            // Calculate Average Evaluation Score
            double averageEvaluationScore = performances
                .Where(p => p.Evaluation != null)
                .Average(p => (double?)p.Evaluation.Score) ?? 0;
            // Calculate Total Hours Worked
            double totalHoursWorked = (double)performances.Sum(p => p.HoursWorked);
            // Apply Weights with new percentages
            double weightedEvaluation = 0.50 * averageEvaluationScore; // 50% weight for evaluation
            double weightedContribution = 0.30 * contributionScore;    // 30% weight for contributions
            double weightedHours = 0.20 * totalHoursWorked;           // 20% weight for hours worked
            return weightedEvaluation + weightedContribution + weightedHours;
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
        public IActionResult Aboutus()
        {
            return View();
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