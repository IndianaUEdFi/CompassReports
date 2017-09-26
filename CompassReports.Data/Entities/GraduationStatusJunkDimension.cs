using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    public class GraduationStatusJunkDimension
    {
        [Key]
        public int GraduationStatusId { get; set; }

        [MaxLength(50)]
        public string GraduationStatus { get; set; }

        [MaxLength(15)]
        public string DiplomaType { get; set; }

        [MaxLength(15)]
        public string GraduationWaiver { get; set; }

        public ICollection<GraduationFact> GraduationFacts { get; set; }
    }
}