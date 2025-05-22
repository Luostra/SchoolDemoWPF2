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

namespace SchoolDemoWPF2.Views
{
    /// <summary>
    /// Логика взаимодействия для ClassAveragesWindow.xaml
    /// </summary>
    // ClassAveragesWindow.xaml.cs
    public partial class ClassAveragesWindow : Window
    {
        private SchoolContext _context = new SchoolContext();

        public ClassAveragesWindow(Semester semester)
        {
            InitializeComponent();
            CalculateAverages(semester);
        }

        private void CalculateAverages(Semester semester)
        {

            var classAverages = _context.Classes
                .Select(c => new
                {
                    Class = c,
                    Average = _context.Students
                        .Where(s => s.ClassId == c.Id)
                        .SelectMany(s => s.Grades)
                        .Where(g => g.Semester == semester)
                        .Average(g => (double?)g.Value) ?? 0
                })
                .OrderByDescending(x => x.Average)
                .ToList();

            var overallAverage = classAverages.Any() ? classAverages.Average(x => x.Average) : 0;

            OverallTextBlock.Text = $"Общий средний балл по школе: {overallAverage:F2}";

            if (classAverages.Any())
            {
                BestClassTextBlock.Text = $"{classAverages.First().Class.Name} ({classAverages.First().Average:F2})";
                WorstClassTextBlock.Text = $"{classAverages.Last().Class.Name} ({classAverages.Last().Average:F2})";
            }

            AveragesDataGrid.ItemsSource = classAverages;
        }

    }
}
