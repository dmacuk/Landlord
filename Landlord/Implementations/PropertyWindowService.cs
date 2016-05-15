using Landlord.Interface;
using Landlord.Views;

namespace Landlord.Implementations
{
    public class PropertyWindowService : IPropertyWindowService
    {
        public void ShowPictures(PictureType pictureType, long id)
        {
            var view = new PictureView(pictureType, id);
            view.ShowDialog();
        }
    }
}