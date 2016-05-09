using Landlord.Model;
using System;

namespace Landlord.Interface
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}