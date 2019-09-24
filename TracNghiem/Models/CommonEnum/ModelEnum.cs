using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TracNghiem.Models
{
    public enum CommonStatus
    {
        active = 0,
        notactive = 1,
    }
    public enum Gender
    {
        Male = 0,
        Female = 1,
    }
    public enum UserStatus
    {
        NotActivated = 0,
        Activated = 1,
        Blocked = 2,
        Deleted = 3,

    }
    public enum UserType
    {
        Admin = 0,
        Teacher = 1,
        Student = 2,

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