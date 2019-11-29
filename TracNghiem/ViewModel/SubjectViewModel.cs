using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TracNghiem.ViewModel
{
    public class SubjectViewModel
    {
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public int QuizCount { get; set; }
        public int QuizTestCount { get; set; }
    }
}