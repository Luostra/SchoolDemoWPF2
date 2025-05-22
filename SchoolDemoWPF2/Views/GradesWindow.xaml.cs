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
    /// Логика взаимодействия для GradesWindow.xaml
    /// </summary>
    // GradesWindow.xaml.cs
    public partial class GradesWindow : Window
    {
        private int _studentId;
        private SchoolContext _context = new SchoolContext();
        private string _studentName;

        public GradesWindow(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;
            LoadStudentInfo();
            LoadGrades();
        }

        private void LoadStudentInfo()
        {
            var student = _context.Students
                .Include(s => s.Class)
                .FirstOrDefault(s => s.Id == _studentId);

            _studentName = $"{student.FullName} ({student.Class.Name})";
            DataContext = this;
        }

        public string StudentName => _studentName;

        private void LoadGrades()
        {
            GradesGrid.ItemsSource = _context.Grades
                .Include(g => g.Subject)
                .Include(g => g.Teacher)
                .Where(g => g.StudentId == _studentId)
                .ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditGradeWindow(_studentId);
            if (dialog.ShowDialog() == true)
            {
                var newGrade = new Grade
                {
                    StudentId = _studentId,
                    SubjectId = dialog.SelectedSubjectId,
                    TeacherId = dialog.SelectedTeacherId,
                    Value = dialog.GradeValue,
                    Semester = (Semester)dialog.SelectedSemester,
                };
                _context.Grades.Add(newGrade);
                _context.SaveChanges();
                LoadGrades();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (GradesGrid.SelectedItem is Grade selectedGrade)
            {
                var dialog = new AddEditGradeWindow(_studentId, selectedGrade);
                if (dialog.ShowDialog() == true)
                {
                    selectedGrade.SubjectId = dialog.SelectedSubjectId;
                    selectedGrade.TeacherId = dialog.SelectedTeacherId;
                    selectedGrade.Value = dialog.GradeValue;
                    selectedGrade.Semester = (Semester)dialog.SelectedSemester;
                    _context.SaveChanges();
                    LoadGrades();
                }
            }
            else
            {
                MessageBox.Show("Please select a grade to edit.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (GradesGrid.SelectedItem is Grade selectedGrade)
            {
                _context.Grades.Remove(selectedGrade);
                _context.SaveChanges();
                LoadGrades();
            }
            else
            {
                MessageBox.Show("Please select a grade to delete.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
