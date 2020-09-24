using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentScore.Models
{
    public class BaseEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public DateTime DateTime { get; set; }=DateTime.Now;

        public bool IsRemove { get; set; }
    }
}
