namespace StudentScore.Models.ResponseModels
{
    public class GradesStudentInfoModel
    {
        public string Grades { get; set; }
        public GradesStudentModel GradesStudent { get; set; }
        public StudentReportCardModel StudentReportCard { get; set; }
    }
}