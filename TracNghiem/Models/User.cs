using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TracNghiem.Models
{
    public class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Quizzes = new HashSet<Quiz>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int status { get; set; }

        [Required]
        [StringLength(20)]
        public string username { get; set; }

        [StringLength(100)]
        public string fullname { get; set; }

        [StringLength(100)]
        public string email { get; set; }
        [Required]
        public UserType type { get; set; }

        [Required]
        [StringLength(15)]
        public string role { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }

        public DateTime? register_date { get; set; }

        [StringLength(200)]
        public string AvatarImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Quiz> Quizzes { get; set; }



    }
   
}