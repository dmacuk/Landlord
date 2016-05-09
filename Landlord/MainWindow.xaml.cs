using Landlord.ViewModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Landlord
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            var child = default(T);

            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                var newWidth = (int)e.NewSize.Width / 200;
                var displayGrid = GetVisualChild<UniformGrid>(MovieGrid);
                if (displayGrid == null) return;
                displayGrid.BeginInit();
                displayGrid.Columns = newWidth;
                displayGrid.EndInit();
            }
        }
    }
}