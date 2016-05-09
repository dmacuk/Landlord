using System;

namespace Landlord.Exceptions
{
    [Serializable]
    public class AddressTypeException : Exception
    {
        public AddressTypeException(string msg) : base(msg)
        {
        }
    }
}