using GalaSoft.MvvmLight;
using Landlord.Interface;

namespace Landlord.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         See http://www.mvvmlight.net
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        ///     The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private readonly IDataService _dataService;
        private byte[] _picture;
        private string _welcomeTitle = string.Empty;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                    //                    Picture = item.Picture;
                });
        }

        public byte[] Picture
        {
            get { return _picture; }
            set { Set("Picture", ref _picture, value); }
        }

        public string WelcomeTitle
        {
            get { return _welcomeTitle; }
            set { Set(ref _welcomeTitle, value); }
        }

        ////    base.Cleanup();
        ////    // Clean up if needed
        ////{
        ////public override void Cleanup()
    }
}