using System.Configuration;
using System.Data;
using System.Windows;
using SchoolDemoWPF2.Data;

namespace SchoolDemoWPF2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Очистка базы при старте
            /*
            using (var context = new SchoolContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            */
            /*
            using (var context = new SchoolContext())
            {
                context.Database.EnsureCreated();
            }*/
        }
    }

}
