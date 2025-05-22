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
    /// Логика взаимодействия для ClassroomsWindow.xaml
    /// </summary>
    public partial class ClassroomsWindow : Window
    {
        private SchoolContext _context = new SchoolContext();

        public ClassroomsWindow()
        {
            InitializeComponent();
            LoadClassrooms();
        }

        private void LoadClassrooms()
        {
            ClassroomsGrid.ItemsSource = _context.Classrooms.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditClassroomWindow();
            if (dialog.ShowDialog() == true)
            {
                var newClassroom = new Classroom
                {
                    Name = dialog.ClassName,
                    Description = dialog.Description
                };
                _context.Classrooms.Add(newClassroom);
                _context.SaveChanges();
                LoadClassrooms();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassroomsGrid.SelectedItem is Classroom selectedClassroom)
            {
                var dialog = new AddEditClassroomWindow(selectedClassroom);
                if (dialog.ShowDialog() == true)
                {
                    selectedClassroom.Name = dialog.ClassName;
                    selectedClassroom.Description = dialog.Description;
                    _context.SaveChanges();
                    LoadClassrooms();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите кабинет для редактирования.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassroomsGrid.SelectedItem is Classroom selectedClassroom)
            {
                // Проверяем, используется ли кабинет учителями
                if (_context.Teachers.Any(t => t.ClassroomId == selectedClassroom.Id))
                {
                    MessageBox.Show("Невозможно удалить кабинет, так как он назначен одному или нескольким учителям.");
                    return;
                }

                var result = MessageBox.Show($"Удалить кабинет '{selectedClassroom.Name}'?",
                    "Подтверждение удаления", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Classrooms.Remove(selectedClassroom);
                    _context.SaveChanges();
                    LoadClassrooms();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите кабинет для удаления.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
