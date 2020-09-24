using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentScore.Models
{
    public class UserRole:BaseEntity
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public long RoleId { get; set; }
    }
}
