using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TracNghiem.Models;

namespace TracNghiem.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(20)]
        public string password { get; set; }
        [Required]
        public Gender gender { get; set; }
        [Required]
        [MaxLength(20)]
        public string username { get; set; }
        [Required]
        [MaxLength(50)]
        public string fullname { get; set; }
        [Required]
        [MaxLength(50)]
        public string email { get; set; }
        [Required]
        public RegisterType type { get; set; }
        public string role { get; set; }
        public int CountQuestion = 0;
        public int CountTest = 0;
        public int CountDoneTest = 0;
        public DateTime? register_date { get; set; }

        [StringLength(200)]
        public string AvatarImage { get; set; }
    }
    public enum RegisterType
    {
        [Display(Name = "Giáo viên")]
        Teacher = 1,
        [Display(Name = "Học sinh / Sinh viên")]
        Student = 2,
    }
}