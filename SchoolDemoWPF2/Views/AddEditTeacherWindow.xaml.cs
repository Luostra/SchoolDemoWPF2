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
using SchoolDemoWPF2.Data;
using SchoolDemoWPF2.Models;

namespace SchoolDemoWPF2.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditTeacherWindow.xaml
    /// </summary>
    public partial class AddEditTeacherWindow : Window
    {
        private SchoolContext _context = new SchoolContext();
        private Teacher _teacherToEdit;

        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public int? SelectedClassroomId { get; private set; }
        public int SelectedSubjectId { get; private set; }

        public AddEditTeacherWindow(Teacher teacherToEdit = null)
        {
            InitializeComponent();
            _teacherToEdit = teacherToEdit;
            LoadClassrooms();
            LoadSubjects();

            if (teacherToEdit != null)
            {
                Title = "Изменить учителя";
                FullNameTextBox.Text = teacherToEdit.FullName;
                PhoneTextBox.Text = teacherToEdit.PhoneNumber;
                EmailTextBox.Text = teacherToEdit.Email;
                ClassroomComboBox.SelectedValue = teacherToEdit.ClassroomId;
                SubjectComboBox.SelectedValue = teacherToEdit.SubjectId;
            }
            else
            {
                Title = "Добавить учителя";
            }
        }
        private void LoadClassrooms()
        {
            ClassroomComboBox.ItemsSource = _context.Classrooms.ToList();
        }
        private void LoadSubjects()
        {
            SubjectComboBox.ItemsSource = _context.Subjects.ToList();
            SubjectComboBox.DisplayMemberPath = "Name";
            SubjectComboBox.SelectedValuePath = "Id";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FullName = FullNameTextBox.Text.Trim();
            PhoneNumber = PhoneTextBox.Text.Trim();
            Email = EmailTextBox.Text.Trim();

            if (!DataValidation.IsValidFullName(FullName))
            {
                MessageBox.Show("Пожалуйста, введите ФИО учителя корректно.",
                    "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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

            if (SubjectComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите предмет.",
                    "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SelectedClassroomId = (int?)ClassroomComboBox.SelectedValue;
            SelectedSubjectId = (int)SubjectComboBox.SelectedValue;

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
