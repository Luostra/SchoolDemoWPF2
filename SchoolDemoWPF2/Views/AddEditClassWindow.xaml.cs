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
using SchoolDemoWPF2.Controller;

namespace SchoolDemoWPF2.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditClassWindow.xaml
    /// </summary>
    // AddEditClassWindow.xaml.cs
    public partial class AddEditClassWindow : Window
    {
        public string ClassName { get; private set; }

        public AddEditClassWindow(string existingName = null)
        {
            InitializeComponent();

            if (existingName != null)
            {
                Title = "Изменить класс";
                NameTextBox.Text = existingName;
            }
            else
            {
                Title = "Добавить класс";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ClassName = NameTextBox.Text.Trim();

            if (!DataValidation.IsValidClassName(ClassName))
            {
                MessageBox.Show("Пожалуйста, введите название класса корректно.",
                    "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
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
