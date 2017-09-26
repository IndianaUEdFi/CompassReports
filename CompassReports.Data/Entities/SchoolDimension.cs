using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    public class SchoolDimension
    {
        [Key]
        public int SchoolId { get; set; }

        [MaxLength(75)]
        public string NameOfInstitution { get; set; }

        [MaxLength(150)]
        public string StreetNumberName { get; set; }

        [MaxLength(20)]
        public string BuildingSiteNumber { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        [MaxLength(2)]
        public string StateAbbreviation { get; set; }

        [MaxLength(17)]
        public string PostalCode { get; set; }

        [MaxLength(30)]
        public string NameOfCounty { get; set; }

        [MaxLength(24)]
        public string MainInstitutionTelephone { get; set; }

        [MaxLength(24)]
        public string InstitutionFax { get; set; }

        [MaxLength(255)]
        public string Website { get; set; }

        [MaxLength(182)]
        public string PrincipalName { get; set; }

        [MaxLength(128)]
        public string PrincipalElectronicMailAddress { get; set; }

        [MaxLength(10)]
        public string GradeLevels { get; set; }

        [MaxLength(75)]
        public string AccreditationStatus { get; set; }

        public int LocalEducationAgencyId { get; set; }

        [MaxLength(75)]
        public string LEANameOfInstitution { get; set; }

        [MaxLength(150)]
        public string LEAStreetNumberName { get; set; }

        [MaxLength(20)]
        public string LEABuildingSiteNumber { get; set; }

        [MaxLength(30)]
        public string LEACity { get; set; }

        [MaxLength(2)]
        public string LEAStateAbbreviation { get; set; }

        [MaxLength(17)]
        public string LEAPostalCode { get; set; }

        [MaxLength(30)]
        public string LEANameOfCounty { get; set; }

        [MaxLength(24)]
        public string LEAMainInstitutionTelephone { get; set; }

        [MaxLength(24)]
        public string LEAInstitutionFax { get; set; }

        [MaxLength(255)]
        public string LEAWebSite { get; set; }

        [MaxLength(182)]
        public string LEASuperintendentName { get; set; }

        [MaxLength(128)]
        public string LEASuperintendentElectronicMailAddress { get; set; }

        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
        public ICollection<AttendanceFact> AttendanceFacts { get; set; }
        public ICollection<EnrollmentFact> EnrollmentFacts { get; set; }
        public ICollection<GraduationFact> GraduationFacts { get; set; }

    }
}