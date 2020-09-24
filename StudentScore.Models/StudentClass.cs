using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using StudentScore.Models.Attribute;

namespace StudentScore.Models
{
    public class StudentClass:BaseEntity
    {
        [Unique]
        [StringLength(20, MinimumLength = 2)]
        public string Grades { get; set; }
    }
}
