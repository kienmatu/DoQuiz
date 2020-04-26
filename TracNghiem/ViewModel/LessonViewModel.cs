using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TracNghiem.Models;

namespace TracNghiem.ViewModel
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int Time { get; set; }
        public string File { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string YoutubeLink { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatorID { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
        public LessonStatus Status { get; set; }
    }
}