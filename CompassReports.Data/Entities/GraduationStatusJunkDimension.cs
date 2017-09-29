using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompassReports.Data.Entities
{
    [Table("GraduationStatusJunkDimension", Schema = "cmp")]
    public class GraduationStatusJunkDimension : EntityBase
    {
        [Key]
        public int GraduationStatusKey { get; set; }

        [Required]
        [MaxLength(50)]
        public string GraduationStatus { get; set; }

        [Required]
        [MaxLength(15)]
        public string DiplomaType { get; set; }

        [Required]
        [MaxLength(15)]
        public string GraduationWaiver { get; set; }

        public ICollection<GraduationFact> GraduationFacts { get; set; }
    }
}