using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CompassReports.Data.Entities;

namespace CompassReports.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("connStr") { }

        // Define Db sets.
        public virtual DbSet<AssessmentDimension> AssessmentDimensions { get; set; }

        public virtual DbSet<AssessmentFact> AssessmentFacts { get; set; }

        public virtual DbSet<AttendanceFact> AttendanceFacts { get; set; }

        public virtual DbSet<DemographicJunkDimension> DemographicJunkDimensions { get; set; }

        public virtual DbSet<EnrollmentFact> EnrollmentFacts { get; set; }

        public virtual DbSet<GoodCauseExemptionJunkDimension> GoodCauseExemptionJunkDimensions { get; set; }

        public virtual DbSet<GraduationFact> GraduationFacts { get; set; }

        public virtual DbSet<GraduationStatusJunkDimension> GraduationStatusJunkDimensions { get; set; }

        public virtual DbSet<PerformanceLevelDimension> PerformanceLevelDimensions { get; set; }

        public virtual DbSet<SchoolDimension> SchoolDimensions { get; set; }

        public virtual DbSet<SchoolYearDimension> SchoolYearDimensions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}