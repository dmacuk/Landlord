using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Landlord.Interface;
using Landlord.Model;
using Landlord.ViewModel.Utils;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Utils.FullyObservableCollection;

namespace Landlord.ViewModel
{
    public class PropertyViewModel : ViewModelBase
    {
        private readonly IPropertyDataService _dataService;

        private Address _address;

        private string _dummy;

        private string _filterValue;

        //        private string _postcode;
        private FullyObservableCollection<Property> _properties;

        private Property _property;

        private bool _showHidden;

        public PropertyViewModel(IPropertyDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetProperties((properties, error) =>
            {
                if (error != null)
                {
                    return;
                }

                _properties = new FullyObservableCollection<Property>(properties);
                Properties = CollectionViewSource.GetDefaultView(_properties);
                Properties.Filter = Filter;
                ((ListCollectionView)Properties).CustomSort = new PropertySorter();
                if (Properties.IsEmpty) return;
                Properties.MoveCurrentToFirst();
                Property = (Property)Properties.CurrentItem;
            });
        }

        public ICommand AddPropertyCommand => new RelayCommand(AddProperty);

        public Address Address
        {
            get { return Property?.Address; }
            set { Set(() => Address, ref _address, value); }
        }

        public ICommand DeletePropertyCommand => new RelayCommand(DeleteProperty);

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
                    Address = value?.Address;
                }
            }
        }

        public bool ShowHidden
        {
            get { return _showHidden; }
            set
            {
                if (Set(() => ShowHidden, ref _showHidden, value)) Properties.Refresh();
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
            Property.Hidden = true;
            Properties.Refresh();
        }

        private bool Filter(object obj)
        {
            var vo = (Property)obj;
            if (!ShowHidden && vo.Hidden) return false;
            return string.IsNullOrWhiteSpace(FilterValue) ||
                   vo.Address.FullAddress.ToLower().Contains(FilterValue.ToLower().Trim());
        }
    }
}