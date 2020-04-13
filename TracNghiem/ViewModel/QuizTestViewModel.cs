
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TracNghiem.Models;

namespace TracNghiem.ViewModel
{
    public class QuizTestViewModel
    {
        public int TestID { get; set; }

        [Required]
        public TimeQuiz TotalTime { get; set; }

        [Required]
        [Range(10,1000)]
        public int TotalMark { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public int CreatorID { get; set; }

        public virtual User Creator { get; set; }

        public string CreatorName { get; set; }
        public DateTime CreateDate { get; set; }

        [Required]
        public TestStatus status { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public IEnumerable<SelectListItem> Lessons { get; set; }
        public HardType HardTypeChoose { get; set; }

        [LimitCount(1,20,ErrorMessage = "Số câu hỏi từ 1 tới 20")]
        public List<int> quizID { get; set; }
    }
}