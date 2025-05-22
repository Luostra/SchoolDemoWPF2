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
using SchoolDemoWPF2.Data;

namespace SchoolDemoWPF2.Views
{
    /// <summary>
    /// Логика взаимодействия для StatisticsWindow.xaml
    /// </summary>
    // StatisticsWindow.xaml.cs
    public partial class StatisticsWindow : Window
    {
        private SchoolContext _context = new SchoolContext();
        private readonly Semester _semester;
        public StatisticsWindow(Semester semester)
        {
            InitializeComponent();
            _semester = semester;
            Title += $" ({GetSemesterName(semester)} семестр)";
        }

        private string GetSemesterName(Semester semester)
        {
            return semester == Semester.First ? "1" : "2";
        }

        private void SubjectPerformance_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SubjectPerformanceWindow(_semester);
            dialog.ShowDialog();
        }

        private void ClassAverages_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ClassAveragesWindow(_semester);
            dialog.ShowDialog();
        }

        private void WorstTeacher_Click(object sender, RoutedEventArgs e)
        {
            using var context = new SchoolContext();
            var worstTeacher = context.Teachers
                .Select(t => new
                {
                    Teacher = t,
                    AverageGrade = context.Grades
                        .Where(g => g.TeacherId == t.Id && g.Semester == _semester)
                        .Average(g => (double)g.Value)
                })
                .OrderBy(x => x.AverageGrade)
                .FirstOrDefault();

            if (worstTeacher != null)
            {
                MessageBox.Show($"Преподаватель с самыми низкими оценками ({GetSemesterName(_semester)} семестр):\n" +
                                $"{worstTeacher.Teacher.FullName}\n" +
                                $"Средний балл: {worstTeacher.AverageGrade:F2}",
                                "Результат");
            }
        }

        private void PoorStudents_Click(object sender, RoutedEventArgs e)
        {
            var window = new PoorPerformanceStudentsWindow(_semester);
            window.ShowDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
