using Landlord.Interface;
using Landlord.Model;
using Landlord.Persisters;
using Landlord.Persisters.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Landlord.Implementations
{
    public class PictureDataService : IPictureDataService
    {
        public async void DeletePicture(Picture picture)
        {
            using (var connection = SqlUtils.GetConnection())
            {
                using (var propertyCmd = connection.CreateCommand())
                {
                    propertyCmd.CommandText = $"DELETE FROM Picture WHERE Id = " + picture.Id;
                    await propertyCmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task GetPictures(PictureType pictureType, long id, Action<List<Picture>, Exception> action)
        {
            string fk;
            switch (pictureType)
            {
                case PictureType.Furniture:
                    fk = "FurnitureId";
                    break;

                case PictureType.Property:
                    fk = "PropertyId";
                    break;

                case PictureType.Room:
                    fk = "RoomId";
                    break;

                case PictureType.Tennant:
                    fk = "TennantId";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(pictureType), pictureType, null);
            }
            var results = new List<Picture>();
            using (var connection = SqlUtils.GetConnection())
            {
                using (var propertyCmd = connection.CreateCommand())
                {
                    propertyCmd.CommandText = $"SELECT * FROM Picture WHERE {fk} = " + id;
                    using (var pictureRdr = await propertyCmd.ExecuteReaderAsync())
                    {
                        while (pictureRdr.Read())
                        {
                            results.Add(new Picture(pictureRdr));
                        }
                    }
                }
            }
            action(results, null);
        }

        public async void Save(IEnumerable<Picture> pictures)
        {
            var persister = new PicturePersister();
            await persister.Save(pictures);
        }
    }
}