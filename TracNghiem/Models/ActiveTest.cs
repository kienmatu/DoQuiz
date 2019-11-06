using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TracNghiem.Models
{
    public class ActiveTest
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("QuizTest")]
        [Required]
        public int QuizTestID { get; set; }

        [StringLength(20)]
        [MinLength(4)]
        [Required]
        public string Code { get; set; }
        [Required]
        public bool IsActive { get; set; }

        [ForeignKey("Creator")]
        [Required]
        public int CreatorID { get; set; }
        public virtual User Creator { get; set; }
        public virtual QuizTest QuizTest { get; set; }
        [Required]
        public DateTime FromTime { get; set; }
        [Required]
        public DateTime ToTime { get; set; }
    }
}