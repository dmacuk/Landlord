using System;
using System.Text;

namespace Landlord.Model
{
    public partial class Address
    {
        private bool _dirty;

        public Address()
        {
            Address1 = "NO ADDRESS ENTERED";
            Address2 = string.Empty;
            Address3 = string.Empty;
            City = string.Empty;
            Postcode = string.Empty;
            Country = string.Empty;
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

        public bool IsDirty()
        {
            return _dirty || Id == 0;
        }

        public void SetAddress1(string value)
        {
            if (SameValue(Address1, value)) return;
            _dirty = true;
            Address1 = value;
        }

        public void SetAddress2(string value)
        {
            if (SameValue(Address2, value)) return;
            _dirty = true;
            Address2 = value;
        }

        public void SetAddress3(string value)
        {
            if (SameValue(Address3, value)) return;
            _dirty = true;
            Address3 = value;
        }

        public void SetCity(string value)
        {
            if (SameValue(City, value)) return;
            _dirty = true;
            City = value;
        }

        public void SetCountry(string value)
        {
            if (SameValue(Country, value)) return;
            _dirty = true;
            Country = value;
        }

        public void SetPostcode(string value)
        {
            if (SameValue(Postcode, value)) return;
            _dirty = true;
            Postcode = value;
        }

        private static void AddAddressLine(StringBuilder sb, string line)
        {
            if (!string.IsNullOrWhiteSpace(line)) sb.Append(line).Append(Environment.NewLine);
        }

        private bool SameValue(string field, string value)
        {
            if (field == null && value == null) return true;
            return field != null && field.Equals(value);
        }
    }
}