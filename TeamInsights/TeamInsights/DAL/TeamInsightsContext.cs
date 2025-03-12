using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamInsights.Models;

namespace TeamInsights.DAL
{
    public class TeamInsightsContext: IdentityDbContext
    {
        public TeamInsightsContext(DbContextOptions<TeamInsightsContext> options) : base(options) { }
        public DbSet<Person> People { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<EmployeeCertification> EmployeeCertifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for EmployeeSkills
            //modelBuilder.Entity<EmployeeSkills>()
            //    .HasKey(es => new { es.EmployeeID, es.SkillID });

            // Disable cascade delete on FK_Performances_People_ManagerID
            modelBuilder.Entity<Performance>()
                .HasOne(p => p.Manager)
                .WithMany(pe => pe.Performances) // Assuming you create the Collection Navigation Properly in Person Class
                .OnDelete(DeleteBehavior.Restrict);  // Or DeleteBehavior.NoAction

            //Keep Cascade on other Relations if desired

            modelBuilder.Entity<Performance>()
               .HasOne(p => p.EmployeeRole)
               .WithMany(er => er.Performances)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Performance>()
               .HasOne(p => p.Project)
               .WithMany(pr => pr.Performances)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Performance>()
               .HasOne(p => p.Contribution)
               .WithMany(c => c.Performances)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Performance>()
               .HasOne(p => p.Evaluation)
               .WithMany(ev => ev.Performances)
               .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }


    }
}
