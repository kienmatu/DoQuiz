using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TracNghiem.Models;

namespace TracNghiem.ViewModel
{
    public class QuizResultViewModel
    {
        public string StudentName { get; set; }
        public string Name { get; set; }
        public string LessonName { get; set; }
        public DateTime SubmitDate { get; set; }
        public DoQuizStatus Status { get; set; }
        public double Score { get; set; }
        public double MaxScore { get; set; }
    }
}