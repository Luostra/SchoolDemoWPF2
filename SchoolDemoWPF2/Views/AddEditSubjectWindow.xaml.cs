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

namespace SchoolDemoWPF2.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditSubjectWindow.xaml
    /// </summary>
    public partial class AddEditSubjectWindow : Window
    {
        public string SubjectName { get; private set; }

        public AddEditSubjectWindow()
        {
            InitializeComponent();
            Title = "Add Subject"; // По умолчанию окно для добавления
        }

        // Конструктор для редактирования существующего предмета
        public AddEditSubjectWindow(string existingName) : this()
        {
            Title = "Edit Subject";
            NameTextBox.Text = existingName;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SubjectName = NameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(SubjectName))
            {
                MessageBox.Show("Please enter a subject name.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
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
