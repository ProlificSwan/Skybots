using System.Windows;
using Robotics.GUI.ViewModel;

namespace Robotics.GUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="MainWindowView"/>.
        /// </summary>
        public MainWindowView()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            Closing += ((MainWindowViewModel)this.DataContext).OnClosing;
        }
    }
}
