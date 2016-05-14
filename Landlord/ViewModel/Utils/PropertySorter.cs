using System;
using System.Collections;
using Landlord.Model;

namespace Landlord.ViewModel.Utils
{
    public class PropertySorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var o1 = (Property) x;
            var o2 = (Property) y;
            return string.Compare(o1.Address.FullAddress, o2.Address.FullAddress,
                StringComparison.CurrentCultureIgnoreCase);
        }
    }
}