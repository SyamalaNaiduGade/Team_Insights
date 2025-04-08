namespace TeamInsights.ViewModels
{
    public class SkillCertificationMatrixViewModel
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public List<SkillEntry> Skills { get; set; } = new List<SkillEntry>();
        public List<CertificationEntry> Certifications { get; set; } = new List<CertificationEntry>();
        public List<RoleEntry> Roles { get; set; } = new List<RoleEntry>();
        public List<string> ProjectNames { get; set; } = new List<string>(); // Add Project Names
        public int ContributionsCount { get; set; }
        public List<string> ContributionsDescriptions { get; set; } = new List<string>();
        public double? AverageEvaluationScore { get; set; }
    }
    public class SkillEntry
    {
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }
        public DateTime? AcquiredDate { get; set; }
    }
    public class CertificationEntry
    {
        public string CertificationName { get; set; }
        public DateTime? IssuedDate { get; set; }
    }
    public class RoleEntry
    {
        public string RoleName { get; set; }
        public DateTime? AssignedDate { get; set; }
        public int RoleContributionsCount { get; set; }
        public List<string> RoleContributionsDescriptions { get; set; } = new List<string>();
        public double? AverageEvaluationScore { get; set; }
    }

}
