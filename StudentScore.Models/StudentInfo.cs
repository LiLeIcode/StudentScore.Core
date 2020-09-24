﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentScore.Models.Attribute;

namespace StudentScore.Models
{
    public class StudentInfo : BaseEntity
    {
        [StringLength(20,MinimumLength = 2)]
        
        public string Name { get; set; }
        [Unique]
        [StringLength(30, MinimumLength = 2)]
        public string StudentNumber { get; set; }

        public char Sex { get; set; }

        public int Age { get; set; }

        [ForeignKey(nameof(ReportCard))]
        public long ReportCardID { get; set; }

        public ReportCard ReportCard { get; set; }

        [ForeignKey(nameof(StudentClass))]
        public long StudentClassID { get; set; }

        public StudentClass StudentClass { get; set; }


    }
}