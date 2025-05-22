using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SchoolDemoWPF2.Views;

namespace SchoolDemoWPF2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    // MainWindow.xaml.cs
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClassesButton_Click(object sender, RoutedEventArgs e)
        {
            var classesWindow = new ClassesWindow();
            classesWindow.ShowDialog();
        }

        private void SubjectsButton_Click(object sender, RoutedEventArgs e)
        {
            var subjectsWindow = new SubjectsWindow();
            subjectsWindow.ShowDialog();
        }

        private void TeachersButton_Click(object sender, RoutedEventArgs e)
        {
            var teachersWindow = new TeachersWindow();
            teachersWindow.ShowDialog();
        }
        private void ClassroomsButton_Click(object sender, RoutedEventArgs e)
        {
            var classroomsWindow = new ClassroomsWindow();
            classroomsWindow.ShowDialog();
        }
        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            var semesterDialog = new SemesterSelectionWindow();
            if (semesterDialog.ShowDialog() == true)
            {
                var statsWindow = new StatisticsWindow(semesterDialog.SelectedSemester);
                statsWindow.ShowDialog();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}