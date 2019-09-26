using System;
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
            Quizzes = new HashSet<Quiz>();
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

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        public virtual Subject Subject { get; set; }

        [Required]
        public TestStatus status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Quiz> Quizzes { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}