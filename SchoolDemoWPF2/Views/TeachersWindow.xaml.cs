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
    /// Логика взаимодействия для TeachersWindow.xaml
    /// </summary>
    // TeachersWindow.xaml.cs
    public partial class TeachersWindow : Window
    {
        private SchoolContext _context = new SchoolContext();

        public TeachersWindow()
        {
            InitializeComponent();
            LoadTeachers();
        }

        private void LoadTeachers()
        {
            TeachersGrid.ItemsSource = _context.Teachers
                .Include(t => t.Subject)
                .Include(t => t.Classroom)
                .ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditTeacherWindow();
            if (dialog.ShowDialog() == true)
            {
                var newTeacher = new Teacher
                {
                    FullName = dialog.FullName,
                    PhoneNumber = dialog.PhoneNumber,
                    Email = dialog.Email,
                    ClassroomId = dialog.SelectedClassroomId,
                    SubjectId = dialog.SelectedSubjectId
                };
                _context.Teachers.Add(newTeacher);
                _context.SaveChanges();
                LoadTeachers();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersGrid.SelectedItem is Teacher selectedTeacher)
            {
                var dialog = new AddEditTeacherWindow(selectedTeacher);
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        selectedTeacher.FullName = dialog.FullName;
                        selectedTeacher.PhoneNumber = dialog.PhoneNumber;
                        selectedTeacher.Email = dialog.Email;
                        selectedTeacher.ClassroomId = dialog.SelectedClassroomId;
                        selectedTeacher.SubjectId = dialog.SelectedSubjectId;

                        _context.SaveChanges();
                        LoadTeachers();
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
                MessageBox.Show("Пожалуйста, выберите учителя для редактирования.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersGrid.SelectedItem is Teacher selectedTeacher)
            {
                // Check if teacher has grades
                var hasGrades = _context.Grades.Any(g => g.TeacherId == selectedTeacher.Id);

                if (hasGrades)
                {
                    MessageBox.Show("Cannot delete teacher who has assigned grades.");
                    return;
                }

                var result = MessageBox.Show($"Delete teacher '{selectedTeacher.FullName}'?",
                    "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Teachers.Remove(selectedTeacher);
                    _context.SaveChanges();
                    LoadTeachers();
                }
            }
            else
            {
                MessageBox.Show("Please select a teacher to delete.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
