﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TracNghiem.Models
{
    public class QuizTest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuizTest()
        {
            Quiz = new HashSet<Quiz>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestID { get; set; }

        [Required]
        public int TotalTime { get; set; }

        [Required]
        public int TotalMark { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [ForeignKey("Creator")]
        [Required]
        public int CreatorID { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        public TestStatusAd status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quiz> Quiz { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }
        [ForeignKey("Lesson")]
        public int LessonId { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}