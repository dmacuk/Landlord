using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Landlord.Interface;
using Landlord.ViewModel;

namespace Landlord.Views
{
    /// <summary>
    ///     Description for PictureView.
    /// </summary>
    public partial class PictureView : Window
    {
        private readonly PicturesViewModel _model;

        /// <summary>
        ///     Initializes a new instance of the PictureView class.
        /// </summary>
        public PictureView()
        {
            InitializeComponent();
            _model = (PicturesViewModel)DataContext;
        }

        public PictureView(PictureType pictureType, long id) : this()
        {
            _model.Initialise(pictureType, id);
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

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (!_model.IsDirty()) return;
            var opt = MessageBox.Show(this, "Save changes?", "", MessageBoxButton.YesNoCancel);
            switch (opt)
            {
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;

                case MessageBoxResult.Yes:
                    _model.Save();
                    break;

                case MessageBoxResult.No:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WindowOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                var newWidth = (int)e.NewSize.Width / 200;
                var displayGrid = GetVisualChild<UniformGrid>(PictureGrid);
                if (displayGrid == null) return;
                displayGrid.BeginInit();
                displayGrid.Columns = newWidth;
                displayGrid.EndInit();
            }
        }
    }
}