﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompassReports.Data.Entities
{
    [Table("PerformanceDimension", Schema = "cmp")]
    public class PerformanceDimension : EntityBase
    {
        [Key]
        public int PerformanceKey { get; set; }

        [Required]
        [MaxLength(50)]
        public string PerformanceLevel { get; set; }

        [Required]
        [MaxLength(25)]
        public string ScoreResult { get; set; }

        public ICollection<AssessmentFact> AssessmentFacts { get; set; }
    }
}