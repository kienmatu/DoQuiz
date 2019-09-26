using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TracNghiem.Models
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
    public enum CommonStatus
    {
        active = 0,
        notactive = 1,
    }
    public enum Gender
    {
        [Display(Name = "Nam")]
        Male = 0,
        [Display(Name = "Nữ")]
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
        [Display(Name = "Đáp án A")]
        A = 0,
        [Display(Name = "Đáp án B")]
        B = 1,
        [Display(Name = "Đáp án C")]
        C = 2,
        [Display(Name = "Đáp án D")]
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
        [Display(Name = "Dễ")]
        Easy = 0,
        [Display(Name = "Trung bình")]
        Normal = 1,
        [Display(Name = "Khá")]
        QuiteHard = 2,
        [Display(Name = "Khó")]
        Hard = 3,
    }
    public enum QuizStatus
    {
        [Display(Name ="Nháp")]
        NotActive = 0,
        [Display(Name = "Hiển thị")]
        Active = 1,
    }
    public enum QuizStatusAd
    {
        [Display(Name = "Nháp")]
        NotActive = 0,
        [Display(Name = "Hiển thị")]
        Active = 1,
        [Display(Name = "Đã Xóa")]
        Deleted = 2,
    }

}