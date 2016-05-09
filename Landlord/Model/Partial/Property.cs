namespace Landlord.Model
{
    public partial class Property
    {
        public bool IsDirty()
        {
            return Id == 0 || Address.IsDirty();
        }
    }
}