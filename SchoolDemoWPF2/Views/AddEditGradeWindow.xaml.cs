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
    /// Логика взаимодействия для AddEditGradeWindow.xaml
    /// </summary>
    // AddEditGradeWindow.xaml.cs
    public partial class AddEditGradeWindow : Window
    {
        private int _studentId;
        private SchoolContext _context = new SchoolContext();

        public int SelectedSubjectId { get; private set; }
        public int SelectedTeacherId { get; private set; }
        public int GradeValue { get; private set; }
        public int SelectedSemester { get; private set; }

        public AddEditGradeWindow(int studentId, Grade gradeToEdit = null)
        {
            InitializeComponent();
            _studentId = studentId;

            // Load subjects and teachers
            SubjectComboBox.ItemsSource = _context.Subjects.ToList();
            SubjectComboBox.DisplayMemberPath = "Name";

            if (gradeToEdit != null)
            {
                Title = "Edit Grade";
                SubjectComboBox.SelectedValue = gradeToEdit.SubjectId;
                TeacherComboBox.SelectedValue = gradeToEdit.TeacherId;
                GradeTextBox.Text = gradeToEdit.Value.ToString();
                // Устанавливаем выбранный семестр
                foreach (ComboBoxItem item in SemesterComboBox.Items)
                {
                    if ((int)gradeToEdit.Semester == int.Parse(item.Tag.ToString()))
                    {
                        SemesterComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                // По умолчанию выбираем 1 семестр
                SemesterComboBox.SelectedIndex = 0;
            }
        }

        private void SubjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SubjectComboBox.SelectedItem is Subject selectedSubject)
            {
                TeacherComboBox.ItemsSource = _context.Teachers
                    .Where(t => t.SubjectId == selectedSubject.Id)
                    .ToList();
                TeacherComboBox.DisplayMemberPath = "FullName";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectComboBox.SelectedItem == null || TeacherComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select both subject and teacher.");
                return;
            }

            if (!int.TryParse(GradeTextBox.Text, out int gradeValue) || gradeValue < 2 || gradeValue > 5)
            {
                MessageBox.Show("Please enter a valid grade (2-5).");
                return;
            }

            // Получаем выбранный семестр
            var selectedItem = (ComboBoxItem)SemesterComboBox.SelectedItem;
            var semesterValue = int.Parse(selectedItem.Tag.ToString());
            SelectedSemester = semesterValue;

            SelectedSubjectId = ((Subject)SubjectComboBox.SelectedItem).Id;
            SelectedTeacherId = ((Teacher)TeacherComboBox.SelectedItem).Id;
            GradeValue = gradeValue;

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
