using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TracNghiem.Models
{
    public class Quiz
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quiz()
        {
            QuizTest = new HashSet<QuizTest>();
            CreateDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizID { get; set; }
        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [ForeignKey("Creator")]
        [Required]
        public int CreatorID { get; set; }
        public virtual User Creator { get; set; }
        public DateTime CreateDate { get; set; }
        [StringLength(500)]
        [Required]
        public string content { get; set; }

        [Required]
        public HardType HardType { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        [StringLength(200)]
        [Required]
        public string answerA { get; set; }
        [StringLength(200)]
        [Required]
        public string answerB { get; set; }
        [StringLength(200)]
        [Required]
        public string answerC { get; set; }
        [StringLength(200)]
        [Required]
        public string answerD { get; set; }
        [Required]
        public Answer trueAnswer { get; set; }
        [Required]
        public QuizStatusAd status { get; set; }
        [ForeignKey("lesson")]
        [Required]
        public int LessonId { get; set; }
        public virtual Lesson lesson { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuizTest> QuizTest { get; set; }

    }

}
