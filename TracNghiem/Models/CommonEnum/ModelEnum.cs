using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TracNghiem.Models
{
    public enum UserType
    {
        Admin = 0,
        Mod = 1,
        Teacher = 2,
        Student = 3,

    }
    public enum Answer
    {
        A = 0,
        B = 1,
        C = 2,
        D = 4
    }
    public enum TestStatus
    {
        Draft = 0,
        Active = 1,
        NotActive = 2,
        Deleted = 3,
    }
    public enum HardType
    {
        Easy = 0,
        Normal = 1,
        QuiteHard = 2,
        Hard = 3,
    }
    
}