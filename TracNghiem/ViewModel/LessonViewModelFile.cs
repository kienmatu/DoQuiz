using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TracNghiem.ViewModel
{
    public class LessonViewModelFile
    {
        public int Id { get; set; }
        [Required]
        public string File { get; set; }
    }
}