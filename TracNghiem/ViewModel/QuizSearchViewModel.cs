using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TracNghiem.Models;

namespace TracNghiem.ViewModel
{
    public class QuizSearchViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public HardType HardType { get; set; }
    }
}