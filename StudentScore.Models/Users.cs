using System;
using System.ComponentModel.DataAnnotations;
using StudentScore.Models.Attribute;

namespace StudentScore.Models
{
    public class Users:BaseEntity
    {
        [Required]
        [Unique]
        //[Index(IsUnique=true)]
        //[MaxLength(50)]
        [StringLength(20,MinimumLength = 6)]
        public string Account { get; set; }
        [StringLength(20, MinimumLength = 6)]
        [Required]
        public string Password { get; set; }
        
    }
}
