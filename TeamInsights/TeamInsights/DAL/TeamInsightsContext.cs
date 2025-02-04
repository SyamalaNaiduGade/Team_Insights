using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TeamInsights.DAL
{
    public class TeamInsightsContext: IdentityDbContext
    {
        public TeamInsightsContext(DbContextOptions<TeamInsightsContext> options) : base(options) { }

    }
}
