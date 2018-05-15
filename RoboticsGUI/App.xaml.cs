using Robotics.GUI.View;
using System.Windows;

namespace Robotics
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MatchUpsWindow window = new MatchUpsWindow();
            window.Top = 100;
            window.Left = 100;
            window.Show();
        }
    }
}
