using System.ComponentModel.DataAnnotations;

namespace StudentScore.Models.ResponseModels
{
    public class LoginUser
    {
        [Required]
        public string Account { get; set; }
        [Required]
        public string Password { get; set; }

    }
}