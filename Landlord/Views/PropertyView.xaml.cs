using Landlord.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;

namespace Landlord.Views
{
    /// <summary>
    ///     Description for PropertyView.
    /// </summary>
    public partial class PropertyView : Window
    {
        /// <summary>
        ///     Initializes a new instance of the PropertyView class.
        /// </summary>
        public PropertyView()
        {
            InitializeComponent();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            var model = (PropertyViewModel)DataContext;
            if (model.IsDirty())
            {
                var opt = MessageBox.Show(this, "Save changes?", "", MessageBoxButton.YesNoCancel);
                switch (opt)
                {
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;

                    case MessageBoxResult.Yes:
                        model.Save();
                        break;

                    case MessageBoxResult.No:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}