using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Landlord.Interface;
using Landlord.Model;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Landlord.ViewModel
{
    public class PropertySorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var o1 = x as Property;
            var o2 = x as Property;
            return string.Compare(o1?.Address?.FullAddress?.ToLower() ?? "", o2?.Address?.FullAddress?.ToLower() ?? "",
                StringComparison.Ordinal);
        }
    }

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class PropertyViewModel : ViewModelBase
    {
        private readonly IPropertyDataService _dataService;
        private string _address1;
        private string _address2;
        private string _address3;
        private string _city;
        private string _country;
        private string _dummy;
        private string _filterValue;
        private string _postcode;
        private ObservableCollection<Property> _properties;

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

                _properties = new ObservableCollection<Property>(properties);
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

        public string Address1
        {
            get { return Property?.Address?.Address1; }
            set
            {
                if (Set(() => Address1, ref _address1, value) && Property != null)
                {
                    Property.Address.Address1 = value;
                }
            }
        }

        public string Address2
        {
            get { return Property?.Address?.Address2; }
            set
            {
                if (Set(() => Address2, ref _address2, value) && Property != null)
                {
                    Property.Address.Address2 = value;
                }
            }
        }

        public string Address3
        {
            get { return Property?.Address?.Address3; }
            set
            {
                if (Set(() => Address3, ref _address3, value) && Property != null)
                {
                    Property.Address.Address3 = value;
                }
            }
        }

        public string City
        {
            get { return Property?.Address?.City; }
            set
            {
                if (Set(() => City, ref _city, value) && Property != null)
                {
                    Property.Address.City = value;
                }
            }
        }

        public string Country
        {
            get { return Property?.Address?.Country; }
            set
            {
                if (Set(() => Country, ref _country, value) && Property != null)
                {
                    Property.Address.Country = value;
                }
            }
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
            get { return Property?.Address?.FullAddress; }
            set { Set(() => FullAddress, ref _dummy, value); }
        }

        public string Postcode
        {
            get { return Property?.Address?.Postcode; }
            set
            {
                if (Set(() => Postcode, ref _postcode, value) && Property != null)
                {
                    Property.Address.Postcode = value;
                }
            }
        }

        public ICollectionView Properties { get; private set; }

        public Property Property
        {
            get { return _property; }
            set
            {
                if (Set(() => Property, ref _property, value))
                {
                    SetTextFields(value);
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
            _properties.Add(new Property { Address = Address.GetNewAddress() });
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

        private void SetTextFields(Property vo)
        {
            var address = vo?.Address;
            Address1 = address?.Address1;
            Address2 = address?.Address2;
            Address3 = address?.Address3;
            City = address?.City;
            Postcode = address?.Postcode;
            Country = address?.Country;
        }
    }
}