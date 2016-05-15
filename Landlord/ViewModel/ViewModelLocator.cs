/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Landlord.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Landlord.Implementations;
using Landlord.Implementations.Design;
using Landlord.Interface;
using Microsoft.Practices.ServiceLocation;

namespace Landlord.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    ///     <para>
    ///         See http://www.mvvmlight.net
    ///     </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IPictureDataService, DesignPictureDataService>();
                SimpleIoc.Default.Register<IPictureWindowService, DesignPictureWindowService>();
                SimpleIoc.Default.Register<IPropertyDataService, DesignPropertyDataService>();
                SimpleIoc.Default.Register<IPropertyWindowService, DesignPropertyWindowService>();
            }
            else
            {
                SimpleIoc.Default.Register<IPictureDataService, PictureDataService>();
                SimpleIoc.Default.Register<IPictureWindowService, PictureWindowService>();
                SimpleIoc.Default.Register<IPropertyDataService, PropertyDataService>();
                SimpleIoc.Default.Register<IPropertyWindowService, PropertyWindowService>();
            }

            SimpleIoc.Default.Register<PicturesViewModel>();
            SimpleIoc.Default.Register<PropertyViewModel>();
        }

        public PicturesViewModel PicturesViewModel => ServiceLocator.Current.GetInstance<PicturesViewModel>();

        public PropertyViewModel PropertyViewModel => ServiceLocator.Current.GetInstance<PropertyViewModel>();

        /// <summary>
        ///     Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }

        // ReSharper restore MemberCanBeMadeStatic.Global
    }
}