using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using SchoolDemoWPF2.Models;

namespace SchoolDemoWPF2.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditStudentWindow.xaml
    /// </summary>
    public partial class AddEditStudentWindow : Window
    {
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public DateTime Birthday { get; private set; }
        public int ClassId { get; }

        public AddEditStudentWindow(int classId, Student studentToEdit = null)
        {
            InitializeComponent();
            ClassId = classId;

            if (studentToEdit != null)
            {
                Title = "Изменить ученика";
                FullNameTextBox.Text = studentToEdit.FullName;
                PhoneTextBox.Text = studentToEdit.PhoneNumber;
                EmailTextBox.Text = studentToEdit.Email;
                BirthdayPicker.SelectedDate = studentToEdit.Birthday;

            }
            else
            {
                Title = "Добавить ученика";
                BirthdayPicker.SelectedDate = DateTime.Today;
            }

            Birthday = BirthdayPicker.SelectedDate ?? DateTime.Today;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FullName = FullNameTextBox.Text.Trim();
            PhoneNumber = PhoneTextBox.Text.Trim();
            Email = EmailTextBox.Text.Trim();
            Birthday = BirthdayPicker.SelectedDate ?? DateTime.Today;

            if (!DataValidation.IsValidPhoneNumber(PhoneNumber))
            {
                MessageBox.Show("Пожалуйста, введите номер телефона в формате 8XXXXXXXXXX");
                return;
            }
            if (!DataValidation.IsValidEmail(Email))
            {
                MessageBox.Show("Пожалуйста, введите корректный email.");
                return;
            }
            if (!DataValidation.IsValidFullName(FullName))
            {
                MessageBox.Show("Пожалуйста, введите ФИО ученика корректно.");
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
