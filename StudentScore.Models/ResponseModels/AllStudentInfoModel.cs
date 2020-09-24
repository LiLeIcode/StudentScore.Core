using System;
using System.Collections.Generic;
using System.Text;

namespace StudentScore.Models.ResponseModels
{
    public class AllStudentInfoModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string StringNumber { get; set; }
        public char Sex { get; set; }
        public int Age { get; set; }
        public AllStudentClassModel AllStudentClass { get; set; }
        public StudentReportCardModel StudentReportCard { get; set; }
    }
}
