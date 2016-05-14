using GalaSoft.MvvmLight;
using System;
using System.Data;
using System.Text;

namespace Landlord.Model
{
    public class Address : ObservableObject
    {
        private string _address1;
        private string _address2;
        private string _address3;
        private string _city;
        private string _country;
        private bool _dirty;
        private bool _hidden;
        private long _id;
        private string _postcode;

        public Address(IDataRecord rdr)
        {
            _id = (long)rdr["Id"];
            _address1 = (string)rdr["Address1"];
            _address2 = (string)rdr["Address2"];
            _address3 = (string)rdr["Address3"];
            _city = (string)rdr["City"];
            _postcode = (string)rdr["Postcode"];
            _country = (string)rdr["Country"];
            _hidden = (bool)rdr["Hidden"];
        }

        public Address()
        {
            _address1 = "NO DETAILS ENTERED";
            _address2 = "NO DETAILS ENTERED";
            _address3 = "NO DETAILS ENTERED";
            _city = "NO DETAILS ENTERED";
            _postcode = "NO DETAILS ENTERED";
            _country = "NO DETAILS ENTERED";
        }

        public string Address1
        {
            get { return _address1; }
            set
            {
                if (Set(() => Address1, ref _address1, value))
                {
                    _dirty = true;
                    RaisePropertyChanged("FullAddress");
                }
            }
        }

        public string Address2
        {
            get { return _address2; }
            set
            {
                if (Set(() => Address2, ref _address2, value))
                {
                    _dirty = true;
                    RaisePropertyChanged("FullAddress");
                }
            }
        }

        public string Address3
        {
            get { return _address3; }
            set
            {
                if (Set(() => Address3, ref _address3, value))
                {
                    _dirty = true;
                    RaisePropertyChanged("FullAddress");
                }
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (Set(() => City, ref _city, value))
                {
                    _dirty = true;
                    RaisePropertyChanged("FullAddress");
                }
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                if (Set(() => Country, ref _country, value))
                {
                    _dirty = true;
                    RaisePropertyChanged("FullAddress");
                }
            }
        }

        public string FullAddress
        {
            get
            {
                var sb = new StringBuilder();
                AddAddressLine(sb, Address1);
                AddAddressLine(sb, Address2);
                AddAddressLine(sb, Address3);
                AddAddressLine(sb, City);
                AddAddressLine(sb, Postcode);
                AddAddressLine(sb, Country);
                return sb.ToString().Trim();
            }
        }

        public bool Hidden
        {
            get { return _hidden; }
            set
            {
                if (Set(() => Hidden, ref _hidden, value)) _dirty = true;
            }
        }

        public long Id
        {
            get { return _id; }
            set
            {
                if (Set(() => Id, ref _id, value)) _dirty = true;
            }
        }

        public string Postcode
        {
            get { return _postcode; }
            set
            {
                if (Set(() => Postcode, ref _postcode, value)) _dirty = true;
            }
        }

        public static Address GetNewAddress()
        {
            return new Address();
        }

        public bool IsDirty()
        {
            return Id == 0 || _dirty;
        }

        private static void AddAddressLine(StringBuilder sb, string line)
        {
            if (!string.IsNullOrWhiteSpace(line)) sb.Append(line).Append(Environment.NewLine);
        }
    }
}