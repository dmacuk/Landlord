using System.Data;
using GalaSoft.MvvmLight;

namespace Landlord.Model
{
    public class Picture : ObservableObject
    {
        private byte[] _data;
        private string _description;
        private bool _dirty;
        private long _furnitureId;
        private long _id;
        private long _propertyId;
        private long _roomId;
        private long _tennantId;

        public Picture()
        {
        }

        public Picture(IDataRecord rdr)
        {
            _id = (long)rdr["Id"];
            _data = (byte[])rdr["Data"];
            _description = (string)rdr["Description"];
            _furnitureId = HandleNull(rdr["FurnitureId"]);
            _propertyId = HandleNull(rdr["PropertyId"]);
            _roomId = HandleNull(rdr["RoomId"]);
            _tennantId = HandleNull(rdr["TennantId"]);
        }

        public byte[] Data
        {
            get { return _data; }
            set
            {
                if (Set(() => Data, ref _data, value)) _dirty = true;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (Set(() => Description, ref _description, value)) _dirty = true;
            }
        }

        public long FurnitureId
        {
            get { return _furnitureId; }
            set
            {
                if (Set(() => FurnitureId, ref _furnitureId, value)) _dirty = true;
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

        public long PropertyId
        {
            get { return _propertyId; }
            set
            {
                if (Set(() => PropertyId, ref _propertyId, value)) _dirty = true;
            }
        }

        public long RoomId
        {
            get { return _roomId; }
            set
            {
                if (Set(() => RoomId, ref _roomId, value)) _dirty = true;
            }
        }

        public long TennantId
        {
            get { return _tennantId; }
            set
            {
                if (Set(() => TennantId, ref _tennantId, value)) _dirty = true;
            }
        }

        public bool IsDirty()
        {
            return Id == 0 || _dirty;
        }

        private static long HandleNull(object fk)
        {
            return (long?)fk ?? 0;
        }
    }
}