﻿
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
        public int TotalMark { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public int CreatorID { get; set; }

        public virtual User Creator { get; set; }

        public virtual string SubjectName { get; set; }

        [Required]
        public TestStatus status { get; set; }
        public IEnumerable<SelectListItem> Subject { get; set; }
        [Required(ErrorMessage = "Chưa chọn môn học")]
        public int SubjectID { get; set; }
        public HardType HardTypeChoose { get; set; }

        public List<int> quesID { get; set; }
    }
}