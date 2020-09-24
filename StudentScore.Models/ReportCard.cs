using System;
using System.Collections.Generic;
using System.Text;

namespace StudentScore.Models
{
    public class ReportCard:BaseEntity
    {

        public int Chinese { get; set; }

        public int Math { get; set; }

        public int English { get; set; }
    }
}
