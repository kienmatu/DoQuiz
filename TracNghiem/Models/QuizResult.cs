﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TracNghiem.Models
{
    public class QuizResult
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Student")]
        [Required]
        public int StudentID { get; set; }
        public virtual User Student { get; set; }
        [Required]
        public int Score { get; set; }
        public DateTime DoneAt { get; set; }

        public DoQuizStatus Status { get; set; }

    }
}