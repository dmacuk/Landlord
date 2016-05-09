using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Landlord.Interface;
using Landlord.Model;
using Landlord.VOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Landlord.ViewModel
{
    public class PropertySorter : IComparer<PropertyVo>, IComparer
    {
        public int Compare(object x, object y)
        {
            var o1 = x as PropertyVo;
            var o2 = x as PropertyVo;
            return string.Compare(o1?.FullAddress?.ToLower() ?? "", o2?.FullAddress?.ToLower() ?? "",
                StringComparison.Ordinal);
        }

        public int Compare(PropertyVo x, PropertyVo y)
        {
            return string.Compare(x.FullAddress.ToLower(), y.FullAddress.ToLower(), StringComparison.Ordinal);
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
        private ObservableCollection<PropertyVo> _properties;

        private PropertyVo _propertyVo;

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

                _properties = new ObservableCollection<PropertyVo>(properties);
                Properties = CollectionViewSource.GetDefaultView(_properties);
                Properties.Filter = Filter;
                ((ListCollectionView)Properties).CustomSort = new PropertySorter();
                if (!Properties.IsEmpty)
                {
                    Properties.MoveCurrentToFirst();
                    PropertyVo = (PropertyVo)Properties.CurrentItem;
                }
            });
        }

        public ICommand AddPropertyCommand => new RelayCommand(AddProperty);

        public string Address1
        {
            get { return PropertyVo?.Address1; }
            set
            {
                if (Set(() => Address1, ref _address1, value) && PropertyVo != null)
                {
                    PropertyVo.Address1 = value;
                }
            }
        }

        public string Address2
        {
            get { return PropertyVo?.Address2; }
            set
            {
                if (Set(() => Address2, ref _address2, value) && PropertyVo != null)
                {
                    PropertyVo.Address2 = value;
                }
            }
        }

        public string Address3
        {
            get { return PropertyVo?.Address3; }
            set
            {
                if (Set(() => Address3, ref _address3, value) && PropertyVo != null)
                {
                    PropertyVo.Address3 = value;
                }
            }
        }

        public string City
        {
            get { return PropertyVo?.City; }
            set
            {
                if (Set(() => City, ref _city, value) && PropertyVo != null)
                {
                    PropertyVo.City = value;
                }
            }
        }

        public string Country
        {
            get { return PropertyVo?.Country; }
            set
            {
                if (Set(() => Country, ref _country, value) && PropertyVo != null)
                {
                    PropertyVo.Country = value;
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
            get { return PropertyVo?.FullAddress; }
            set { Set(() => FullAddress, ref _dummy, value); }
        }

        public string Postcode
        {
            get { return PropertyVo?.Postcode; }
            set
            {
                if (Set(() => Postcode, ref _postcode, value) && PropertyVo != null)
                {
                    PropertyVo.Postcode = value;
                }
            }
        }

        public ICollectionView Properties { get; private set; }

        public PropertyVo PropertyVo
        {
            get { return _propertyVo; }
            set
            {
                if (Set(() => PropertyVo, ref _propertyVo, value))
                {
                    SetTextFields(value);
                }
            }
        }

        private void AddProperty()
        {
            _properties.Add(new PropertyVo(null, new Address()));
        }

        private void DeleteProperty()
        {
        }

        private void EditProperty()
        {
        }

        private bool Filter(object obj)
        {
            var vo = (PropertyVo)obj;
            return string.IsNullOrWhiteSpace(FilterValue) ||
                   vo.FullAddress.ToLower().Contains(FilterValue.ToLower().Trim());
        }

        private void SetTextFields(PropertyVo vo)
        {
            Address1 = vo?.Address1;
            Address2 = vo?.Address2;
            Address3 = vo?.Address3;
            City = vo?.City;
            Postcode = vo?.Postcode;
            Country = vo?.Country;
        }
    }
}