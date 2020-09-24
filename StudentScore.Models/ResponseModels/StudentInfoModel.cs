using System.ComponentModel.DataAnnotations;

namespace StudentScore.Models.ResponseModels
{
    public class StudentInfoModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string StudentNumber { get; set; }
        [Required]
        public char Sex { get; set; }
        [Required]
        public int Age { get; set; }

    }
}