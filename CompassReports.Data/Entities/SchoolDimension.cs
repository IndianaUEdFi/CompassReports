using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    [Table("SchoolDimension", Schema = "cmp")]
    public class SchoolDimension
    {
        [Key]
        public int SchoolKey { get; set; }

        [Required]
        [MaxLength(75)]
        public string NameOfInstitution { get; set; }

        [Required]
        [MaxLength(150)]
        public string StreetNumberName { get; set; }

        [MaxLength(20)]
        public string BuildingSiteNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string City { get; set; }

        [Required]
        [MaxLength(2)]
        public string StateAbbreviation { get; set; }

        [Required]
        [MaxLength(17)]
        public string PostalCode { get; set; }

        [Required]
        [MaxLength(30)]
        public string NameOfCounty { get; set; }

        [Required]
        [MaxLength(24)]
        public string MainInstitutionTelephone { get; set; }

        [Required]
        [MaxLength(24)]
        public string InstitutionFax { get; set; }

        [Required]
        [MaxLength(255)]
        public string Website { get; set; }

        [Required]
        [MaxLength(182)]
        public string PrincipalName { get; set; }

        [Required]
        [MaxLength(128)]
        public string PrincipalElectronicMailAddress { get; set; }

        [Required]
        [MaxLength(10)]
        public string GradeLevels { get; set; }

        [Required]
        [MaxLength(75)]
        public string AccreditationStatus { get; set; }

        [Required]
        public int LocalEducationAgencyId { get; set; }

        [Required]
        [MaxLength(75)]
        public string LEANameOfInstitution { get; set; }

        [Required]
        [MaxLength(150)]
        public string LEAStreetNumberName { get; set; }

        [MaxLength(20)]
        public string LEABuildingSiteNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string LEACity { get; set; }

        [Required]
        [MaxLength(2)]
        public string LEAStateAbbreviation { get; set; }

        [Required]
        [MaxLength(17)]
        public string LEAPostalCode { get; set; }

        [Required]
        [MaxLength(30)]
        public string LEANameOfCounty { get; set; }

        [Required]
        [MaxLength(24)]
        public string LEAMainInstitutionTelephone { get; set; }

        [Required]
        [MaxLength(24)]
        public string LEAInstitutionFax { get; set; }

        [Required]
        [MaxLength(255)]
        public string LEAWebSite { get; set; }

        [Required]
        [MaxLength(182)]
        public string LEASuperintendentName { get; set; }

        [Required]
        [MaxLength(128)]
        public string LEASuperintendentElectronicMailAddress { get; set; }

        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
        public ICollection<AttendanceFact> AttendanceFacts { get; set; }
        public ICollection<EnrollmentFact> EnrollmentFacts { get; set; }
        public ICollection<GraduationFact> GraduationFacts { get; set; }

    }
}