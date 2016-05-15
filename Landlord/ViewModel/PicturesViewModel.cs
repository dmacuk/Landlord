using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Landlord.Interface;
using Landlord.Model;
using Landlord.ViewModel.Utils;
using Microsoft.Win32;
using Utils.FullyObservableCollection;

namespace Landlord.ViewModel
{
    public class PicturesViewModel : ViewModelBase
    {
        private readonly IPictureDataService _dataService;
        private readonly IPictureWindowService _windowService;
        private FullyObservableCollection<Picture> _currentPictures;
        private long _id;
        private Picture _picture;

        private ICollectionView _pictures;
        private PictureType _pictureType;

        public PicturesViewModel(IPictureDataService dataService, IPictureWindowService windowService)
        {
            _dataService = dataService;
            _windowService = windowService;
        }

        public RelayCommand AddImage => new RelayCommand(AddImageExecute);

        public Picture Picture
        {
            get { return _picture; }
            set { Set(() => Picture, ref _picture, value); }
        }

        public ICollectionView Pictures
        {
            get { return _pictures; }
            set { Set(() => Pictures, ref _pictures, value); }
        }

        public RelayCommand<object> RemoveImage
            => new RelayCommand<object>(RemoveImageExecute);

        public async void Initialise(PictureType pictureType, long id)
        {
            _pictureType = pictureType;
            _id = id;

            await _dataService.GetPictures(pictureType, id, (pictures, error) =>
            {
                _currentPictures = new FullyObservableCollection<Picture>(pictures);
                _currentPictures.CollectionChanged += UpdateCollection;
                Pictures = CollectionViewSource.GetDefaultView(_currentPictures);
                ((ListCollectionView)Pictures).CustomSort = new PictureSorter();
                if (Pictures.IsEmpty) return;
                Pictures.MoveCurrentToFirst();
                Picture = (Picture)Pictures.CurrentItem;
            });
        }

        public bool IsDirty()
        {
            return _currentPictures.Any(picture => picture.IsDirty());
        }

        public void Save()
        {
            var pictures = _currentPictures.Where(p => p.IsDirty());
            _dataService.Save(pictures);
        }

        private void AddImageExecute()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter =
                    "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif",
                Multiselect = true
            };
            // Set filter for file extension and default file extension

            // Display OpenFileDialog by calling ShowDialog method
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                foreach (var fileName in dlg.FileNames)
                {
                    _currentPictures.Add(new Picture
                    {
                        Data = File.ReadAllBytes(fileName),
                        Description = fileName,
                        FurnitureId = _pictureType == PictureType.Furniture ? _id : 0L,
                        PropertyId = _pictureType == PictureType.Property ? _id : 0L,
                        RoomId = _pictureType == PictureType.Room ? _id : 0L,
                        TennantId = _pictureType == PictureType.Tennant ? _id : 0L
                    });
                }
            }
        }

        private void RemoveImageExecute(object o)
        {
            if (o == null)
            {
                // Tell the user
                return;
            }
            var images = ((ObservableCollection<object>)o).Cast<Picture>().ToList();
            if (!images.Any())
            {
                // Tell the user
                return;
            }

            foreach (var image in images)
            {
                _currentPictures.Remove(image);
            }
        }

        private void UpdateCollection(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    var pictures = e.OldItems.Cast<Picture>();
                    foreach (var picture in pictures)
                    {
                        if (picture.Id != 0) _dataService.DeletePicture(Picture);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}