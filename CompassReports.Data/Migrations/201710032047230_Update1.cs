namespace CompassReports.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("cmp.DemographicJunkDimension", "GradeLevelSort", c => c.String(nullable: false, maxLength: 10));
            RenameColumn("cmp.AttendanceFact", "Absences", "TotalAbsences");
            RenameColumn("cmp.AttendanceFact", "PossibleStudentAttendanceDays", "TotalInstructionalDays");
        }
        
        public override void Down()
        {
            RenameColumn("cmp.AttendanceFact", "TotalAbsences", "Absences");
            RenameColumn("cmp.AttendanceFact", "TotalInstructionalDays", "PossibleStudentAttendanceDays");
            DropColumn("cmp.DemographicJunkDimension", "GradeLevelSort");
        }
    }
}
