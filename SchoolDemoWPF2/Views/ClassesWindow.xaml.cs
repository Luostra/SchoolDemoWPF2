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
    /// Логика взаимодействия для ClassesWindow.xaml
    /// </summary>
    // ClassesWindow.xaml.cs
    public partial class ClassesWindow : Window
    {
        private SchoolContext _context = new SchoolContext();

        public ClassesWindow()
        {
            InitializeComponent();
            LoadClasses();
        }

        private void LoadClasses()
        {
            ClassesGrid.ItemsSource = _context.Classes.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddEditClassWindow();
            if (dialog.ShowDialog() == true)
            {
                var newClass = new Class { Name = dialog.ClassName };
                _context.Classes.Add(newClass);
                _context.SaveChanges();
                LoadClasses();
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassesGrid.SelectedItem is Class selectedClass)
            {
                var studentsWindow = new StudentsWindow(selectedClass.Id);
                studentsWindow.ShowDialog();
                LoadClasses();
            }
            else
            {
                MessageBox.Show("Please select a class to open.");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassesGrid.SelectedItem is Class selectedClass)
            {
                var dialog = new AddEditClassWindow(selectedClass.Name);
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        selectedClass.Name = dialog.ClassName;
                        _context.SaveChanges();
                        LoadClasses();
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
                MessageBox.Show("Пожалуйста, выберите класс для редактирования.",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassesGrid.SelectedItem is Class selectedClass)
            {
                var result = MessageBox.Show($"Delete class '{selectedClass.Name}' and all its students?",
                    "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Classes.Remove(selectedClass);
                    _context.SaveChanges();
                    LoadClasses();
                }
            }
            else
            {
                MessageBox.Show("Please select a class to delete.");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
