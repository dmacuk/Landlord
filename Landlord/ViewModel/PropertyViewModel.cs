using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Landlord.Interface;
using Landlord.Model;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Utils.FullyObservableCollection;

namespace Landlord.ViewModel
{
    public class PropertySorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var o1 = (Property)x;
            var o2 = (Property)y;
            return string.Compare(o1.Address.FullAddress, o2.Address.FullAddress,
                StringComparison.CurrentCultureIgnoreCase);
        }
    }

    public class PropertyViewModel : ViewModelBase
    {
        private readonly IPropertyDataService _dataService;

        private Address _address;

        private string _dummy;

        private string _filterValue;

        //        private string _postcode;
        private FullyObservableCollection<Property> _properties;

        private Property _property;

        public PropertyViewModel(IPropertyDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetProperties((properties, error) =>
            {
                if (error != null)
                {
                    // Report error here
                    return;
                }

                _properties = new FullyObservableCollection<Property>(properties);
                Properties = CollectionViewSource.GetDefaultView(_properties);
                Properties.Filter = Filter;
                ((ListCollectionView)Properties).CustomSort = new PropertySorter();
                if (!Properties.IsEmpty)
                {
                    Properties.MoveCurrentToFirst();
                    Property = (Property)Properties.CurrentItem;
                }
            });
        }

        public ICommand AddPropertyCommand => new RelayCommand(AddProperty);

        public Address Address
        {
            get { return Property.Address; }
            set { Set(() => Address, ref _address, value); }
        }

        public ICommand DeletePropertyCommand => new RelayCommand(DeleteProperty);

        public ICommand EditPropertyCommand => new RelayCommand(EditProperty);

        public string FilterValue
        {
            get { return _filterValue; }
            set
            {
                if (Set(() => FilterValue, ref _filterValue, value)) Properties.Refresh();
            }
        }

        public string FullAddress
        {
            get { return Property.Address.FullAddress; }
            set { Set(() => FullAddress, ref _dummy, value); }
        }

        public ICollectionView Properties { get; private set; }

        public Property Property
        {
            get { return _property; }
            set
            {
                if (Set(() => Property, ref _property, value))
                {
                    Address = value.Address;
                }
            }
        }

        public bool IsDirty()
        {
            return _properties.Any(vo => vo.IsDirty());
        }

        public void Save()
        {
            foreach (var vo in _properties.Where(p => p.IsDirty()))
            {
                _dataService.Save(vo);
            }
        }

        private void AddProperty()
        {
            var property = new Property();
            _properties.Add(property);
        }

        private void DeleteProperty()
        {
            //            _dataService.Delete(Property);
            _properties.Remove(Property);
        }

        private void EditProperty()
        {
        }

        private bool Filter(object obj)
        {
            var vo = (Property)obj;
            return string.IsNullOrWhiteSpace(FilterValue) ||
                   vo.Address.FullAddress.ToLower().Contains(FilterValue.ToLower().Trim());
        }
    }
}