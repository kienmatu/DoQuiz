using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TracNghiem.Models;

namespace TracNghiem.ViewModel
{
    public class QuizTestViewModelAd
    {
        public int TestID { get; set; }

        [Required]
        public TimeQuiz TotalTime { get; set; }

        [Required]
        [Range(10, 1000)]
        public int TotalMark { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public int CreatorID { get; set; }

        public string  CreatorName { get; set; }

        [Required]
        public TestStatusAd status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}