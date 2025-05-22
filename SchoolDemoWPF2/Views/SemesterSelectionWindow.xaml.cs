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
    /// Логика взаимодействия для SemesterSelectionWindow.xaml
    /// </summary>
    public partial class SemesterSelectionWindow : Window
    {
        public Semester SelectedSemester { get; private set; }

        public SemesterSelectionWindow()
        {
            InitializeComponent();
        }

        private void FirstSemester_Click(object sender, RoutedEventArgs e)
        {
            SelectedSemester = Semester.First;
            DialogResult = true;
            Close();
        }

        private void SecondSemester_Click(object sender, RoutedEventArgs e)
        {
            SelectedSemester = Semester.Second;
            DialogResult = true;
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
