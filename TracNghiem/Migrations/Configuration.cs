namespace TracNghiem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TracNghiem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TracNghiem.Models.QuizContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TracNghiem.Models.QuizContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 1,
                username = "admin",
                email = "ngocbellacntt@gmail.com",
                fullname = "Nguyễn Ngọc",
                type = UserType.Admin,
                role = "admin",
                status = UserStatus.Activated,
                register_date = DateTime.Parse("2019-09-09"),
                AvatarImage = "default-avatar.jpg",
                password = "0192023A7BBD73250516F069DF18B500", // = admin123
                gender = Gender.Female,
            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 2,
                username = "teacher1",
                email = "teacherdemo@gmail.com",
                fullname = "Lục Thiếu Du",
                type = UserType.Teacher,
                role = "teacher",
                status = UserStatus.Activated,
                register_date = DateTime.Parse("2019-09-09"),
                AvatarImage = "default-avatar.jpg",
                password = "0192023A7BBD73250516F069DF18B500", // = admin123
                gender = Gender.Male,
            });
            context.Users.AddOrUpdate(e => e.ID, new User
            {
                ID = 3,
                username = "student1",
                email = "studentdemo@gmail.com",
                fullname = "Giang Tiểu Xuyên",
                type = UserType.Student,
                role = "student",
                status = UserStatus.Activated,
                register_date = DateTime.Parse("2019-09-09"),
                AvatarImage = "default-avatar.jpg",
                password = "0192023A7BBD73250516F069DF18B500", // = admin123
                gender = Gender.Male,
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 1,
                name = "Lập Trình Nhúng",
                status = CommonStatus.active,
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 2,
                name = "Lập Trình C#",
                status = CommonStatus.active,
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 3,
                name = "Toán rời rạc",
                status = CommonStatus.active,
            });
            context.Subjects.AddOrUpdate(e => e.ID, new Subject
            {
                ID = 4,
                name = "Cấu trúc dữ liệu và giải thuật",
                status = CommonStatus.active,
            });
        }
    }
}
