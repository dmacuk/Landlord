using Landlord.Model;

namespace Landlord.VOs
{
    public class PropertyVo
    {
        public PropertyVo(Property property, Address address)
        {
            Property = property;
            Address = address;
        }

        public Address Address { get; }

        public string Address1
        {
            get { return Address?.Address1; }
            set { Address.SetAddress1(value); }
        }

        public string Address2
        {
            get { return Address?.Address2; }
            set { Address.SetAddress2(value); }
        }

        public string Address3
        {
            get { return Address?.Address3; }
            set { Address.SetAddress2(value); }
        }

        public string City
        {
            get { return Address?.City; }
            set { Address.SetCity(value); }
        }

        public string Country
        {
            get { return Address?.Country; }
            set { Address.SetCountry(value); }
        }

        public string FullAddress => Address?.FullAddress;

        public string Postcode
        {
            get { return Address?.Postcode; }
            set { Address.SetPostcode(value); }
        }

        public Property Property { get; }

        public bool IsDirty()
        {
            return Property.Id == 0 || Address.IsDirty();
        }
    }
}