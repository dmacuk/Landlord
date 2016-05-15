using Landlord.Interface;
using Landlord.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landlord.Implementations.Design
{
    public class DesignPictureDataService : IPictureDataService
    {
        public void DeletePicture(Picture picture)
        {
            throw new NotImplementedException();
        }

        public Task GetPictures(PictureType pictureType, long id, Action<List<Picture>, Exception> action)
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<Picture> pictures)
        {
            throw new NotImplementedException();
        }
    }
}