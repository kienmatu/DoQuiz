using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TracNghiem.Models
{
    public class Lesson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lesson()
        {
            QuizTests = new HashSet<QuizTest>();
            Quizzes = new HashSet<Quiz>();
            CreatedDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int Time { get; set; }
        [ForeignKey("Creator")]
        [Required]
        public int CreatorID { get; set; }
        public virtual User Creator { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        public string File { get; set; }
        [Required]
        public string YoutubeLink { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public LessonStatus Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quiz> Quizzes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<QuizTest> QuizTests { get; set; }
    }
}