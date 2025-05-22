using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolDemoWPF2.Models;


namespace SchoolDemoWPF2.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=school.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация отношения Teacher-Classroom
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Classroom)
                .WithMany(c => c.Teachers)
                .HasForeignKey(t => t.ClassroomId)
                .OnDelete(DeleteBehavior.SetNull);

            // Конфигурация отношения Teacher-Subject
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Subject)
                .WithMany(s => s.Teachers)
                .HasForeignKey(t => t.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация отношения Student-Class
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация отношения Grade-Student
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация отношения Grade-Subject
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация отношения Grade-Teacher
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Teacher)
                .WithMany(t => t.Grades)
                .HasForeignKey(g => g.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
