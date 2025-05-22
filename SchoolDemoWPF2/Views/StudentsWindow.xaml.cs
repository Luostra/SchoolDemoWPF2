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
    /// Логика взаимодействия для StudentsWindow.xaml
    /// </summary>
    // StudentsWindow.xaml.cs
    public partial class StudentsWindow : Window
    {
        private int _classId;
        private SchoolContext _context = new SchoolContext();

        public StudentsWindow(int classId)
        {
            InitializeComponent();
            _classId = classId;
            LoadStudents();
            Title = $"Students of {_context.Classes.Find(classId)?.Name}";
        }

        private void LoadStudents()
        {
            StudentsGrid.ItemsSource = _context.Students
                .Where(s => s.ClassId == _classId)
                .ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditStudentWindow(_classId);
            if (dialog.ShowDialog() == true)
            {
                var newStudent = new Student
                {
                    FullName = dialog.FullName,
                    PhoneNumber = dialog.PhoneNumber,
                    Email = dialog.Email,
                    Birthday = dialog.Birthday,
                    ClassId = _classId
                };
                _context.Students.Add(newStudent);
                _context.SaveChanges();
                LoadStudents();
            }
        }

        private void GradesButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsGrid.SelectedItem is Student selectedStudent)
            {
                var gradesWindow = new GradesWindow(selectedStudent.Id);
                gradesWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a student to view grades.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsGrid.SelectedItem is Student selectedStudent)
            {
                var result = MessageBox.Show($"Delete student '{selectedStudent.FullName}' and all their grades?",
                    "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Students.Remove(selectedStudent);
                    _context.SaveChanges();
                    LoadStudents();
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsGrid.SelectedItem is Student selectedStudent)
            {
                var dialog = new AddEditStudentWindow(_classId, selectedStudent);
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        selectedStudent.FullName = dialog.FullName;
                        selectedStudent.PhoneNumber = dialog.PhoneNumber;
                        selectedStudent.Email = dialog.Email;
                        selectedStudent.Birthday = dialog.Birthday;

                        _context.SaveChanges();
                        LoadStudents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите ученика для редактирования.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
