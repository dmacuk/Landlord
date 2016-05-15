using Landlord.Model;
using System;
using System.Collections;

namespace Landlord.ViewModel.Utils
{
    public class PictureSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            var o1 = (Picture)x;
            var o2 = (Picture)y;
            return string.Compare(o1.Description, o2.Description, StringComparison.OrdinalIgnoreCase);
        }
    }
}