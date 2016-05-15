namespace Landlord.Interface
{
    public enum PictureType
    {
        Furniture,
        Property,
        Room,
        Tennant
    }

    public interface IPropertyWindowService
    {
        void ShowPictures(PictureType pictureType, long id);
    }
}