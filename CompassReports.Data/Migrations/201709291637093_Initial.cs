namespace CompassReports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "cmp.AssessmentDimension",
                c => new
                    {
                        AssessmentKey = c.Int(nullable: false, identity: true),
                        AssessmentTitle = c.String(nullable: false, maxLength: 60),
                        AssessedGradeLevel = c.String(nullable: false, maxLength: 50),
                        AcademicSubject = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AssessmentKey);
            
            CreateTable(
                "cmp.AssessmentFact",
                c => new
                    {
                        DemographicKey = c.Int(nullable: false),
                        SchoolKey = c.Int(nullable: false),
                        SchoolYearKey = c.Short(nullable: false),
                        AssessmentKey = c.Int(nullable: false),
                        PerformanceLevelKey = c.Int(nullable: false),
                        GoodCauseExemptionKey = c.Int(nullable: false),
                        AssessmentStudentCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DemographicKey, t.SchoolKey, t.SchoolYearKey, t.AssessmentKey, t.PerformanceLevelKey, t.GoodCauseExemptionKey })
                .ForeignKey("cmp.AssessmentDimension", t => t.AssessmentKey, cascadeDelete: true)
                .ForeignKey("cmp.DemographicJunkDimension", t => t.DemographicKey, cascadeDelete: true)
                .ForeignKey("cmp.SchoolDimension", t => t.SchoolKey, cascadeDelete: true)
                .ForeignKey("cmp.SchoolYearDimension", t => t.SchoolYearKey, cascadeDelete: true)
                .ForeignKey("cmp.GoodCauseExemptionJunkDimension", t => t.GoodCauseExemptionKey, cascadeDelete: true)
                .ForeignKey("cmp.PerformanceLevelDimension", t => t.PerformanceLevelKey, cascadeDelete: true)
                .Index(t => t.DemographicKey)
                .Index(t => t.SchoolKey)
                .Index(t => t.SchoolYearKey)
                .Index(t => t.AssessmentKey)
                .Index(t => t.PerformanceLevelKey)
                .Index(t => t.GoodCauseExemptionKey);
            
            CreateTable(
                "cmp.DemographicJunkDimension",
                c => new
                    {
                        DemographicKey = c.Int(nullable: false, identity: true),
                        GradeLevel = c.String(nullable: false, maxLength: 50),
                        Ethnicity = c.String(nullable: false, maxLength: 50),
                        FreeReducedLunchStatus = c.String(nullable: false, maxLength: 50),
                        SpecialEducationStatus = c.String(nullable: false, maxLength: 50),
                        EnglishLanguageLearnerStatus = c.String(nullable: false, maxLength: 50),
                        ExpectedGraduationYear = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.DemographicKey);
            
            CreateTable(
                "cmp.AttendanceFact",
                c => new
                    {
                        DemographicKey = c.Int(nullable: false),
                        SchoolKey = c.Int(nullable: false),
                        SchoolYearKey = c.Short(nullable: false),
                        Absences = c.Int(nullable: false),
                        PossibleStudentAttendanceDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DemographicKey, t.SchoolKey, t.SchoolYearKey })
                .ForeignKey("cmp.DemographicJunkDimension", t => t.DemographicKey, cascadeDelete: true)
                .ForeignKey("cmp.SchoolDimension", t => t.SchoolKey, cascadeDelete: true)
                .ForeignKey("cmp.SchoolYearDimension", t => t.SchoolYearKey, cascadeDelete: true)
                .Index(t => t.DemographicKey)
                .Index(t => t.SchoolKey)
                .Index(t => t.SchoolYearKey);
            
            CreateTable(
                "cmp.SchoolDimension",
                c => new
                    {
                        SchoolKey = c.Int(nullable: false, identity: true),
                        NameOfInstitution = c.String(nullable: false, maxLength: 75),
                        StreetNumberName = c.String(nullable: false, maxLength: 150),
                        BuildingSiteNumber = c.String(maxLength: 20),
                        City = c.String(nullable: false, maxLength: 30),
                        StateAbbreviation = c.String(nullable: false, maxLength: 2),
                        PostalCode = c.String(nullable: false, maxLength: 17),
                        NameOfCounty = c.String(nullable: false, maxLength: 30),
                        MainInstitutionTelephone = c.String(nullable: false, maxLength: 24),
                        InstitutionFax = c.String(nullable: false, maxLength: 24),
                        Website = c.String(nullable: false, maxLength: 255),
                        PrincipalName = c.String(nullable: false, maxLength: 182),
                        PrincipalElectronicMailAddress = c.String(nullable: false, maxLength: 128),
                        GradeLevels = c.String(nullable: false, maxLength: 10),
                        AccreditationStatus = c.String(nullable: false, maxLength: 75),
                        LocalEducationAgencyId = c.Int(nullable: false),
                        LEANameOfInstitution = c.String(nullable: false, maxLength: 75),
                        LEAStreetNumberName = c.String(nullable: false, maxLength: 150),
                        LEABuildingSiteNumber = c.String(maxLength: 20),
                        LEACity = c.String(nullable: false, maxLength: 30),
                        LEAStateAbbreviation = c.String(nullable: false, maxLength: 2),
                        LEAPostalCode = c.String(nullable: false, maxLength: 17),
                        LEANameOfCounty = c.String(nullable: false, maxLength: 30),
                        LEAMainInstitutionTelephone = c.String(nullable: false, maxLength: 24),
                        LEAInstitutionFax = c.String(nullable: false, maxLength: 24),
                        LEAWebSite = c.String(nullable: false, maxLength: 255),
                        LEASuperintendentName = c.String(nullable: false, maxLength: 182),
                        LEASuperintendentElectronicMailAddress = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SchoolKey);
            
            CreateTable(
                "cmp.EnrollmentFact",
                c => new
                    {
                        DemographicKey = c.Int(nullable: false),
                        SchoolKey = c.Int(nullable: false),
                        SchoolYearKey = c.Short(nullable: false),
                        EnrollmentStudentCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DemographicKey, t.SchoolKey, t.SchoolYearKey })
                .ForeignKey("cmp.DemographicJunkDimension", t => t.DemographicKey, cascadeDelete: true)
                .ForeignKey("cmp.SchoolDimension", t => t.SchoolKey, cascadeDelete: true)
                .ForeignKey("cmp.SchoolYearDimension", t => t.SchoolYearKey, cascadeDelete: true)
                .Index(t => t.DemographicKey)
                .Index(t => t.SchoolKey)
                .Index(t => t.SchoolYearKey);
            
            CreateTable(
                "cmp.SchoolYearDimension",
                c => new
                    {
                        SchoolYearKey = c.Short(nullable: false, identity: true),
                        SchoolYearDescription = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.SchoolYearKey);
            
            CreateTable(
                "cmp.GraduationFact",
                c => new
                    {
                        DemographicKey = c.Int(nullable: false),
                        SchoolKey = c.Int(nullable: false),
                        SchoolYearKey = c.Short(nullable: false),
                        GraduationStatusKey = c.Int(nullable: false),
                        GraduationStudentCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DemographicKey, t.SchoolKey, t.SchoolYearKey, t.GraduationStatusKey })
                .ForeignKey("cmp.DemographicJunkDimension", t => t.DemographicKey, cascadeDelete: true)
                .ForeignKey("cmp.GraduationStatusJunkDimension", t => t.GraduationStatusKey, cascadeDelete: true)
                .ForeignKey("cmp.SchoolDimension", t => t.SchoolKey, cascadeDelete: true)
                .ForeignKey("cmp.SchoolYearDimension", t => t.SchoolYearKey, cascadeDelete: true)
                .Index(t => t.DemographicKey)
                .Index(t => t.SchoolKey)
                .Index(t => t.SchoolYearKey)
                .Index(t => t.GraduationStatusKey);
            
            CreateTable(
                "cmp.GraduationStatusJunkDimension",
                c => new
                    {
                        GraduationStatusKey = c.Int(nullable: false, identity: true),
                        GraduationStatus = c.String(nullable: false, maxLength: 50),
                        DiplomaType = c.String(nullable: false, maxLength: 15),
                        GraduationWaiver = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.GraduationStatusKey);
            
            CreateTable(
                "cmp.GoodCauseExemptionJunkDimension",
                c => new
                    {
                        GoodCauseExemptionKey = c.Int(nullable: false, identity: true),
                        GoodCauseExemptionGranted = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.GoodCauseExemptionKey);
            
            CreateTable(
                "cmp.PerformanceLevelDimension",
                c => new
                    {
                        PerformanceLevelKey = c.Int(nullable: false, identity: true),
                        PerformanceLevel = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PerformanceLevelKey);
            
        }
        
        public override void Down()
        {
            DropForeignKey("cmp.AssessmentFact", "PerformanceLevelKey", "cmp.PerformanceLevelDimension");
            DropForeignKey("cmp.AssessmentFact", "GoodCauseExemptionKey", "cmp.GoodCauseExemptionJunkDimension");
            DropForeignKey("cmp.GraduationFact", "SchoolYearKey", "cmp.SchoolYearDimension");
            DropForeignKey("cmp.GraduationFact", "SchoolKey", "cmp.SchoolDimension");
            DropForeignKey("cmp.GraduationFact", "GraduationStatusKey", "cmp.GraduationStatusJunkDimension");
            DropForeignKey("cmp.GraduationFact", "DemographicKey", "cmp.DemographicJunkDimension");
            DropForeignKey("cmp.EnrollmentFact", "SchoolYearKey", "cmp.SchoolYearDimension");
            DropForeignKey("cmp.AttendanceFact", "SchoolYearKey", "cmp.SchoolYearDimension");
            DropForeignKey("cmp.AssessmentFact", "SchoolYearKey", "cmp.SchoolYearDimension");
            DropForeignKey("cmp.EnrollmentFact", "SchoolKey", "cmp.SchoolDimension");
            DropForeignKey("cmp.EnrollmentFact", "DemographicKey", "cmp.DemographicJunkDimension");
            DropForeignKey("cmp.AttendanceFact", "SchoolKey", "cmp.SchoolDimension");
            DropForeignKey("cmp.AssessmentFact", "SchoolKey", "cmp.SchoolDimension");
            DropForeignKey("cmp.AttendanceFact", "DemographicKey", "cmp.DemographicJunkDimension");
            DropForeignKey("cmp.AssessmentFact", "DemographicKey", "cmp.DemographicJunkDimension");
            DropForeignKey("cmp.AssessmentFact", "AssessmentKey", "cmp.AssessmentDimension");
            DropIndex("cmp.GraduationFact", new[] { "GraduationStatusKey" });
            DropIndex("cmp.GraduationFact", new[] { "SchoolYearKey" });
            DropIndex("cmp.GraduationFact", new[] { "SchoolKey" });
            DropIndex("cmp.GraduationFact", new[] { "DemographicKey" });
            DropIndex("cmp.EnrollmentFact", new[] { "SchoolYearKey" });
            DropIndex("cmp.EnrollmentFact", new[] { "SchoolKey" });
            DropIndex("cmp.EnrollmentFact", new[] { "DemographicKey" });
            DropIndex("cmp.AttendanceFact", new[] { "SchoolYearKey" });
            DropIndex("cmp.AttendanceFact", new[] { "SchoolKey" });
            DropIndex("cmp.AttendanceFact", new[] { "DemographicKey" });
            DropIndex("cmp.AssessmentFact", new[] { "GoodCauseExemptionKey" });
            DropIndex("cmp.AssessmentFact", new[] { "PerformanceLevelKey" });
            DropIndex("cmp.AssessmentFact", new[] { "AssessmentKey" });
            DropIndex("cmp.AssessmentFact", new[] { "SchoolYearKey" });
            DropIndex("cmp.AssessmentFact", new[] { "SchoolKey" });
            DropIndex("cmp.AssessmentFact", new[] { "DemographicKey" });
            DropTable("cmp.PerformanceLevelDimension");
            DropTable("cmp.GoodCauseExemptionJunkDimension");
            DropTable("cmp.GraduationStatusJunkDimension");
            DropTable("cmp.GraduationFact");
            DropTable("cmp.SchoolYearDimension");
            DropTable("cmp.EnrollmentFact");
            DropTable("cmp.SchoolDimension");
            DropTable("cmp.AttendanceFact");
            DropTable("cmp.DemographicJunkDimension");
            DropTable("cmp.AssessmentFact");
            DropTable("cmp.AssessmentDimension");
        }
    }
}
