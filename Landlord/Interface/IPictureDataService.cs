using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Landlord.Model;

namespace Landlord.Interface
{
    public interface IPictureDataService
    {
        void DeletePicture(Picture picture);

        Task GetPictures(PictureType pictureType, long id, Action<List<Picture>, Exception> action);

        void Save(IEnumerable<Picture> pictures);
    }
}