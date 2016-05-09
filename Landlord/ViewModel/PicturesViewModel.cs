using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Landlord.Interface;
using Landlord.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace Landlord.ViewModel
{
    public class PicturesViewModel : ViewModelBase
    {
        private readonly IPictureDataService _pictureDataService;
        private readonly List<Picture> _toBeDeleted;

        public PicturesViewModel(IPictureDataService pictureDataService)
        {
            if (IsInDesignMode) return;

            _pictureDataService = pictureDataService;

            Pictures = new ObservableCollection<Picture>();
            _toBeDeleted = new List<Picture>();
            var filenames = new List<string>(Directory.EnumerateFiles(@"d:\Pictures", "*.jpg"));
            //            var pictures = (from filename in filenames
            //                            let data = File.ReadAllBytes(filename)
            //                            select new Picture(new FileInfo(filename).Name, data, 0)).ToList();
            //            Initialise(pictures);
        }

        public RelayCommand AddImage => new RelayCommand(AddImageExecute);

        public ObservableCollection<Picture> Pictures { get; set; }

        public RelayCommand<object> RemoveImage
            => new RelayCommand<object>(RemoveImageExecute);

        public void Initialise(List<Picture> pictures)
        {
            Pictures.CollectionChanged -= UpdateCollection;
            Pictures.Clear();
            foreach (var picture in pictures)
            {
                Pictures.Add(picture);
            }
            Pictures.CollectionChanged += UpdateCollection;
        }

        public bool IsDirty()
        {
            return _toBeDeleted.Count > 0; // || Pictures.Any(picture => picture.IsDirty());
        }

        private void AddImageExecute()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter =
                    "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };
            // Set filter for file extension and default file extension

            // Display OpenFileDialog by calling ShowDialog method
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                var filename = dlg.FileName;
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
                Pictures.Remove(image);
            }
        }

        private void UpdateCollection(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    _toBeDeleted.AddRange(e.OldItems.Cast<Picture>());
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