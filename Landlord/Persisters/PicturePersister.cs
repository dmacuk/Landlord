using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Landlord.Model;
using Landlord.Persisters.Utils;

namespace Landlord.Persisters
{
    public class PicturePersister
    {
        public async Task Save(IEnumerable<Picture> pictures)
        {
            using (var conn = SqlUtils.GetConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    foreach (var picture in pictures)
                    {
                        cmd.CommandText = picture.Id == 0 ? PictureSqlUtils.AddPicture : PictureSqlUtils.UpdatePicture;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(PictureSqlUtils.GetPictureParameters(cmd, picture));
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }
    }

    internal class PictureSqlUtils : SqlUtils
    {
        public static string AddPicture =
            "INSERT INTO Picture (Data, Description, FurnitureId, PropertyId, RoomId, TennantId) " +
            "VALUES " +
            "(@Data, @Description, @FurnitureId, @PropertyId, @RoomId, @TennantId);";

        public static string UpdatePicture = "UPDATE Picture SET " +
                                             "Data = @Data," +
                                             "Description = @Description," +
                                             "FurnitureId=@FurnitureId," +
                                             "PropertyId=@PropertyId," +
                                             "RoomId=@RoomId," +
                                             "TennantId=TennantId " +
                                             "WHERE Id = @Id;";

        public static Array GetPictureParameters(DbCommand cmd, Picture picture)
        {
            var parameters = new List<DbParameter>
            {
                CreateDbParameter(cmd, "@Id", DbType.Int64, picture.Id),
                CreateDbParameter(cmd, "@Data", DbType.Binary, picture.Data),
                CreateDbParameter(cmd, "@Description", DbType.String, picture.Description),
                CreateDbParameter(cmd, "@FurnitureId", DbType.Int64,
                    picture.FurnitureId == 0 ? (object) null : picture.FurnitureId),
                CreateDbParameter(cmd, "@PropertyId", DbType.Int64,
                    picture.PropertyId == 0 ? (object) null : picture.PropertyId),
                CreateDbParameter(cmd, "@RoomId", DbType.Int64, picture.RoomId == 0 ? (object) null : picture.PropertyId),
                CreateDbParameter(cmd, "@TennantId", DbType.Int64,
                    picture.TennantId == 0 ? (object) null : picture.TennantId)
            };
            return parameters.ToArray();
        }
    }
}