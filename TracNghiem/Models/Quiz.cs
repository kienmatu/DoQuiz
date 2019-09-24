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
            QuizTests = new HashSet<QuizTest>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuizID { get; set; }

        [ForeignKey("Creator")]
        [Required]
        public int CreatorID { get; set; }
        public virtual User Creator { get; set; }

        [StringLength(500)]
        [Required]
        public string content { get; set; }

        [Required]
        public HardType HardType { get; set; }

        [StringLength(100)]
        public string image { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        public virtual Subject Subject { get; set; }

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
        public int status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<QuizTest> QuizTests { get; set; }

    }
    
}