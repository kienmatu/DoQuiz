using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TracNghiem.Models;

namespace TracNghiem.ViewModel
{
    public class QuizViewModel
    {
        [Required]
        [StringLength(100)]
        public string name { get; set; }
        [StringLength(500)]
        [Required]
        public string content { get; set; }

        [Required]
        public HardType HardType { get; set; }

        //[StringLength(100)]
        //public string image { get; set; }

        
        public IEnumerable<SelectListItem> Subject { get; set; }
        [Required(ErrorMessage = "Chưa chọn môn học")]
        public int SubjectID { get; set; }
        public int ID { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreateDate { get; set; }
        public string SubjectName { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Chưa nhập đáp án A")]
        public string answerA { get; set; }
        [StringLength(200)]
        [Required(ErrorMessage = "Chưa nhập đáp án B")]
        public string answerB { get; set; }
        [StringLength(200)]
        [Required(ErrorMessage = "Chưa nhập đáp án C")]
        public string answerC { get; set; }
        [StringLength(200)]
        [Required(ErrorMessage = "Chưa nhập đáp án D")]
        public string answerD { get; set; }
        [Required]
        public Answer trueAnswer { get; set; }
        [Required]
        public QuizStatus status { get; set; }
    }
}