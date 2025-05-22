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
using SchoolDemoWPF2.Models;

namespace SchoolDemoWPF2.Views
{
    /// <summary>
    /// Логика взаимодействия для SubjectsWindow.xaml
    /// </summary>
    // SubjectsWindow.xaml.cs
    public partial class SubjectsWindow : Window
    {
        private SchoolContext _context = new SchoolContext();

        public SubjectsWindow()
        {
            InitializeComponent();
            LoadSubjects();
        }

        private void LoadSubjects()
        {
            var subjectsWithAverages = _context.Subjects
                .Select(s => new SubjectWithAverage
                {
                    Name = s.Name,
                    AverageGrade = s.Grades.Any() ? s.Grades.Average(g => g.Value) : 0
                })
                .ToList();

            SubjectsGrid.ItemsSource = subjectsWithAverages;
        }

        public class SubjectWithAverage
        {
            public string Name { get; set; }
            public double AverageGrade { get; set; }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditSubjectWindow();
            if (dialog.ShowDialog() == true)
            {
                var newSubject = new Subject { Name = dialog.SubjectName };
                _context.Subjects.Add(newSubject);
                _context.SaveChanges();
                LoadSubjects();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectsGrid.SelectedItem is Subject selectedSubject)
            {
                // Check if subject is used by teachers or grades
                var isUsed = _context.Teachers.Any(t => t.SubjectId == selectedSubject.Id) ||
                             _context.Grades.Any(g => g.SubjectId == selectedSubject.Id);

                if (isUsed)
                {
                    MessageBox.Show("Cannot delete subject that is assigned to teachers or has grades.");
                    return;
                }

                var result = MessageBox.Show($"Delete subject '{selectedSubject.Name}'?",
                    "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Subjects.Remove(selectedSubject);
                    _context.SaveChanges();
                    LoadSubjects();
                }
            }
            else
            {
                MessageBox.Show("Please select a subject to delete.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
