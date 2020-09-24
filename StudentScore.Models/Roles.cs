using System.ComponentModel.DataAnnotations;
using StudentScore.Models.Attribute;

namespace StudentScore.Models
{
    public class Roles:BaseEntity
    {
        [Required, Unique, StringLength(maximumLength: 15, MinimumLength = 1)]
        public string RoleName { get; set; }
    }
}
