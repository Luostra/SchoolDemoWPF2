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
using SchoolDemoWPF2.Models;

namespace SchoolDemoWPF2.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditClassroomWindow.xaml
    /// </summary>
    public partial class AddEditClassroomWindow : Window
    {
        public string ClassName { get; private set; }
        public string Description { get; private set; }

        public AddEditClassroomWindow(Classroom classroom = null)
        {
            InitializeComponent();

            if (classroom != null)
            {
                Title = "Изменить кабинет";
                NameTextBox.Text = classroom.Name;
                DescriptionTextBox.Text = classroom.Description;
            }
            else
            {
                Title = "Добавить кабинет";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ClassName = NameTextBox.Text.Trim();
            Description = DescriptionTextBox.Text.Trim();

            if (string.IsNullOrEmpty(ClassName))
            {
                MessageBox.Show("Пожалуйста, введите название кабинета.");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
