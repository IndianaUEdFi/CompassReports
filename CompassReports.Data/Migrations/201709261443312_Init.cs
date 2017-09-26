namespace CompassReports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssessmentDimension",
                c => new
                    {
                        AssessmentId = c.Int(nullable: false, identity: true),
                        AssessmentTitle = c.String(maxLength: 60),
                        AssessmentPeriod = c.String(maxLength: 50),
                        AssessedGradeLevel = c.String(maxLength: 50),
                        AcademicSubject = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.AssessmentId);

            CreateTable(
                    "dbo.AssessmentFact",
                    c => new
                    {
                        DemographicId = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        SchoolYear = c.Short(nullable: false),
                        AssessmentId = c.Int(nullable: false),
                        PerformanceLeveld = c.Int(nullable: false),
                        GoodCauseExemptionId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(
                    t =>
                        new
                        {
                            t.DemographicId,
                            t.SchoolId,
                            t.SchoolYear,
                            t.AssessmentId,
                            t.PerformanceLeveld,
                            t.GoodCauseExemptionId
                        })
                .ForeignKey("dbo.AssessmentDimension", t => t.AssessmentId, cascadeDelete: true)
                .ForeignKey("dbo.DemographicJunkDimension", t => t.DemographicId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolDimension", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolYearDimension", t => t.SchoolYear, cascadeDelete: true)
                .ForeignKey("dbo.PerformanceLevelDimension", t => t.PerformanceLeveld, cascadeDelete: true)
                .ForeignKey("dbo.GoodCauseExemptionJunkDimension", t => t.GoodCauseExemptionId, cascadeDelete: true)
                .Index(t => t.DemographicId)
                .Index(t => t.SchoolId)
                .Index(t => t.SchoolYear)
                .Index(t => t.AssessmentId)
                .Index(t => t.PerformanceLeveld)
                .Index(t => t.GoodCauseExemptionId);
            
            CreateTable(
                "dbo.DemographicJunkDimension",
                c => new
                    {
                        DemographicId = c.Int(nullable: false, identity: true),
                        GradeLevel = c.String(maxLength: 50),
                        Ethnicity = c.String(maxLength: 50),
                        FreeReducedLunchStatus = c.String(maxLength: 50),
                        SpecialEducationStatus = c.String(maxLength: 50),
                        EnglishLanguageLearnerStatus = c.String(maxLength: 50),
                        ExpectedGraduationYear = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.DemographicId);
            
            CreateTable(
                "dbo.AttendanceFact",
                c => new
                    {
                        DemographicId = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        SchoolYear = c.Short(nullable: false),
                        Absences = c.Int(nullable: false),
                        PossibleStudentAttendanceDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DemographicId, t.SchoolId, t.SchoolYear })
                .ForeignKey("dbo.DemographicJunkDimension", t => t.DemographicId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolDimension", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolYearDimension", t => t.SchoolYear, cascadeDelete: true)
                .Index(t => t.DemographicId)
                .Index(t => t.SchoolId)
                .Index(t => t.SchoolYear);
            
            CreateTable(
                "dbo.SchoolDimension",
                c => new
                    {
                        SchoolId = c.Int(nullable: false, identity: true),
                        NameOfInstitution = c.String(maxLength: 75),
                        StreetNumberName = c.String(maxLength: 150),
                        BuildingSiteNumber = c.String(maxLength: 20),
                        City = c.String(maxLength: 30),
                        StateAbbreviation = c.String(maxLength: 2),
                        PostalCode = c.String(maxLength: 17),
                        NameOfCounty = c.String(maxLength: 30),
                        MainInstitutionTelephone = c.String(maxLength: 24),
                        InstitutionFax = c.String(maxLength: 24),
                        Website = c.String(maxLength: 255),
                        PrincipalName = c.String(maxLength: 182),
                        PrincipalElectronicMailAddress = c.String(maxLength: 128),
                        GradeLevels = c.String(maxLength: 10),
                        AccreditationStatus = c.String(maxLength: 75),
                        LocalEducationAgencyId = c.Int(nullable: false),
                        LEANameOfInstitution = c.String(maxLength: 75),
                        LEAStreetNumberName = c.String(maxLength: 150),
                        LEABuildingSiteNumber = c.String(maxLength: 20),
                        LEACity = c.String(maxLength: 30),
                        LEAStateAbbreviation = c.String(maxLength: 2),
                        LEAPostalCode = c.String(maxLength: 17),
                        LEANameOfCounty = c.String(maxLength: 30),
                        LEAMainInstitutionTelephone = c.String(maxLength: 24),
                        LEAInstitutionFax = c.String(maxLength: 24),
                        LEAWebSite = c.String(maxLength: 255),
                        LEASuperintendentName = c.String(maxLength: 182),
                        LEASuperintendentElectronicMailAddress = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SchoolId);
            
            CreateTable(
                "dbo.EnrollmentFact",
                c => new
                    {
                        DemographicId = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        SchoolYear = c.Short(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DemographicId, t.SchoolId, t.SchoolYear })
                .ForeignKey("dbo.DemographicJunkDimension", t => t.DemographicId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolDimension", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolYearDimension", t => t.SchoolYear, cascadeDelete: true)
                .Index(t => t.DemographicId)
                .Index(t => t.SchoolId)
                .Index(t => t.SchoolYear);
            
            CreateTable(
                "dbo.SchoolYearDimension",
                c => new
                    {
                        SchoolYear = c.Short(nullable: false, identity: true),
                        SchoolYearDescription = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.SchoolYear);
            
            CreateTable(
                "dbo.GraduationFact",
                c => new
                    {
                        DemographicId = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                        SchoolYear = c.Short(nullable: false),
                        GraduationStatusId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DemographicId, t.SchoolId, t.SchoolYear, t.GraduationStatusId })
                .ForeignKey("dbo.DemographicJunkDimension", t => t.DemographicId, cascadeDelete: true)
                .ForeignKey("dbo.GraduationStatusJunkDimension", t => t.GraduationStatusId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolDimension", t => t.SchoolId, cascadeDelete: true)
                .ForeignKey("dbo.SchoolYearDimension", t => t.SchoolYear, cascadeDelete: true)
                .Index(t => t.DemographicId)
                .Index(t => t.SchoolId)
                .Index(t => t.SchoolYear)
                .Index(t => t.GraduationStatusId);
            
            CreateTable(
                "dbo.GraduationStatusJunkDimension",
                c => new
                    {
                        GraduationStatusId = c.Int(nullable: false, identity: true),
                        GraduationStatus = c.String(maxLength: 50),
                        DiplomaType = c.String(maxLength: 15),
                        GraduationWaiver = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.GraduationStatusId);
            
            CreateTable(
                "dbo.GoodCauseExemptionJunkDimension",
                c => new
                    {
                        GoodCauseExemptionId = c.Int(nullable: false, identity: true),
                        GoodCauseExemptionGranted = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.GoodCauseExemptionId);
            
            CreateTable(
                "dbo.PerformanceLevelDimension",
                c => new
                    {
                        PerformanceLevelId = c.Int(nullable: false, identity: true),
                        PerformanceLevel = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PerformanceLevelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssessmentFact", "PerformanceLevel_PerformanceLevelId", "dbo.PerformanceLevelDimension");
            DropForeignKey("dbo.AssessmentFact", "GoodCauseExemptionId", "dbo.GoodCauseExemptionJunkDimension");
            DropForeignKey("dbo.GraduationFact", "SchoolYear", "dbo.SchoolYearDimension");
            DropForeignKey("dbo.GraduationFact", "SchoolId", "dbo.SchoolDimension");
            DropForeignKey("dbo.GraduationFact", "GraduationStatusId", "dbo.GraduationStatusJunkDimension");
            DropForeignKey("dbo.GraduationFact", "DemographicId", "dbo.DemographicJunkDimension");
            DropForeignKey("dbo.EnrollmentFact", "SchoolYear", "dbo.SchoolYearDimension");
            DropForeignKey("dbo.AttendanceFact", "SchoolYear", "dbo.SchoolYearDimension");
            DropForeignKey("dbo.AssessmentFact", "SchoolYear", "dbo.SchoolYearDimension");
            DropForeignKey("dbo.EnrollmentFact", "SchoolId", "dbo.SchoolDimension");
            DropForeignKey("dbo.EnrollmentFact", "DemographicId", "dbo.DemographicJunkDimension");
            DropForeignKey("dbo.AttendanceFact", "SchoolId", "dbo.SchoolDimension");
            DropForeignKey("dbo.AssessmentFact", "SchoolId", "dbo.SchoolDimension");
            DropForeignKey("dbo.AttendanceFact", "DemographicId", "dbo.DemographicJunkDimension");
            DropForeignKey("dbo.AssessmentFact", "DemographicId", "dbo.DemographicJunkDimension");
            DropForeignKey("dbo.AssessmentFact", "AssessmentId", "dbo.AssessmentDimension");
            DropIndex("dbo.GraduationFact", new[] { "GraduationStatusId" });
            DropIndex("dbo.GraduationFact", new[] { "SchoolYear" });
            DropIndex("dbo.GraduationFact", new[] { "SchoolId" });
            DropIndex("dbo.GraduationFact", new[] { "DemographicId" });
            DropIndex("dbo.EnrollmentFact", new[] { "SchoolYear" });
            DropIndex("dbo.EnrollmentFact", new[] { "SchoolId" });
            DropIndex("dbo.EnrollmentFact", new[] { "DemographicId" });
            DropIndex("dbo.AttendanceFact", new[] { "SchoolYear" });
            DropIndex("dbo.AttendanceFact", new[] { "SchoolId" });
            DropIndex("dbo.AttendanceFact", new[] { "DemographicId" });
            DropIndex("dbo.AssessmentFact", new[] { "PerformanceLevelId" });
            DropIndex("dbo.AssessmentFact", new[] { "GoodCauseExemptionId" });
            DropIndex("dbo.AssessmentFact", new[] { "AssessmentId" });
            DropIndex("dbo.AssessmentFact", new[] { "SchoolYear" });
            DropIndex("dbo.AssessmentFact", new[] { "SchoolId" });
            DropIndex("dbo.AssessmentFact", new[] { "DemographicId" });
            DropTable("dbo.PerformanceLevelDimension");
            DropTable("dbo.GoodCauseExemptionJunkDimension");
            DropTable("dbo.GraduationStatusJunkDimension");
            DropTable("dbo.GraduationFact");
            DropTable("dbo.SchoolYearDimension");
            DropTable("dbo.EnrollmentFact");
            DropTable("dbo.SchoolDimension");
            DropTable("dbo.AttendanceFact");
            DropTable("dbo.DemographicJunkDimension");
            DropTable("dbo.AssessmentFact");
            DropTable("dbo.AssessmentDimension");
        }
    }
}
