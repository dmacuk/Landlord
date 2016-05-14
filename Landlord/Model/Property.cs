using GalaSoft.MvvmLight;
using System.Data;

namespace Landlord.Model
{
    public class Property : ObservableObject
    {
        private Address _address;
        private long _addressId;
        private bool _dirty;
        private bool _hidden;
        private long _id;

        public Property()
        {
            Address = new Address();
        }

        public Property(IDataRecord rdr, Address address)
        {
            _id = (long)rdr["Id"];
            _addressId = (long)rdr["AddressId"];
            _hidden = (bool)rdr["Hidden"];
            _address = address;
        }

        private Property(Address address)
        {
            Address = address;
        }

        public Address Address
        {
            get { return _address; }
            set
            {
                if (Set(() => Address, ref _address, value)) _dirty = true;
            }
        }

        public long AddressId
        {
            get { return _addressId; }
            set
            {
                if (Set(() => AddressId, ref _addressId, value)) _dirty = true;
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

        public static Property GetNewProperty()
        {
            return new Property(Address.GetNewAddress());
        }

        public bool IsDirty()
        {
            return Id == 0 || _dirty || Address.IsDirty();
        }
    }
}