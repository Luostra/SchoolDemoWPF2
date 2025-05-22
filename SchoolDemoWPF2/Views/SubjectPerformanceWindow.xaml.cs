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
    /// Логика взаимодействия для SubjectPerformanceWindow.xaml
    /// </summary>
    // SubjectPerformanceWindow.xaml.cs
    public partial class SubjectPerformanceWindow : Window
    {
        private readonly Semester _semester;
        private readonly SchoolContext _context = new SchoolContext();

        public string SemesterText => $"Статистика за {(_semester == Semester.First ? "1" : "2")} семестр";

        public SubjectPerformanceWindow(Semester semester)
        {
            InitializeComponent();
            _semester = semester;
            DataContext = this;
            LoadSubjects();
        }

        private void LoadSubjects()
        {
            SubjectComboBox.ItemsSource = _context.Subjects.ToList();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectComboBox.SelectedItem is Subject selectedSubject)
            {
                var average = _context.Grades
                    .Where(g => g.SubjectId == selectedSubject.Id && g.Semester == _semester)
                    .Average(g => (double?)g.Value) ?? 0;

                ResultTextBlock.Text = $"Средний балл по {selectedSubject.Name}: {average:F2}";
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите предмет.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
