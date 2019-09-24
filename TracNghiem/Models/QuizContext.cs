using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TracNghiem.Models
{
    public partial class QuizContext : DbContext
    {
        public QuizContext()
            : base("QuizContext")
        {
        }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizTest> QuizTests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizTest>()
                .HasMany(e => e.Quizzes)
                .WithMany(e => e.QuizTests)
                .Map(m => m.ToTable("Quiz_Of_Test").MapLeftKey("TestID").MapRightKey("QuizID"));
            modelBuilder.Entity<User>()
                .HasIndex(e => e.username)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(e => e.email)
                .IsUnique();
            modelBuilder.Entity<QuizTest>()
                .HasMany(t => t.Quizzes)
                .WithRequired()
                .WillCascadeOnDelete(false);
        }
    }
}