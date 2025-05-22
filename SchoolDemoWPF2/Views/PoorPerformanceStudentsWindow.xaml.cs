using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using SchoolDemoWPF2.Data;
using SchoolDemoWPF2.Models;

namespace SchoolDemoWPF2.Views
{
    /// <summary>
    /// Логика взаимодействия для PoorPerformanceStudentsWindow.xaml
    /// </summary>
    public partial class PoorPerformanceStudentsWindow : Window
    {
        public PoorPerformanceStudentsWindow(Semester semester)
        {
            InitializeComponent();
            LoadPoorPerformanceStudents(semester);
        }

        private void LoadPoorPerformanceStudents(Semester semester)
        {
            using var context = new SchoolContext();

            var poorStudents = context.Students
                .Include(s => s.Class)
                .Include(s => s.Grades)
                .Where(s => s.Grades.Any(g => g.Semester == semester))
                .Select(s => new
                {
                    Student = s,
                    SubjectAverages = s.Grades
                        .Where(g => g.Semester == semester)
                        .GroupBy(g => g.Subject)
                        .Select(g => new
                        {
                            Subject = g.Key,
                            Average = g.Average(gr => gr.Value)
                        })
                })
                .Where(x => x.SubjectAverages.Any(sa => sa.Average < 2.67))
                .Select(x => new PoorStudentViewModel
                {
                    Id = x.Student.Id,
                    FullName = x.Student.FullName,
                    Class = x.Student.Class,
                    AverageGrade = x.SubjectAverages.Average(sa => sa.Average)
                })
                .OrderBy(s => s.AverageGrade)
                .ToList();

            StudentsGrid.ItemsSource = poorStudents;
            SummaryTextBlock.Text = $"Количество неуспевающих учеников ({GetSemesterName(semester)} семестр): {poorStudents.Count}";
        }

        private string GetSemesterName(Semester semester)
        {
            return semester == Semester.First ? "1" : "2";
        }
    }

    public class PoorStudentViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public Class Class { get; set; }
        public double AverageGrade { get; set; }
    }
}
