using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using Microsoft.Phone.Controls;
using Demo.ViewModels;

namespace Demo
{
    public class Bootstrapper : PhoneBootstrapper
    {
        PhoneContainer container;

        protected override void Configure()
        {
            container = new PhoneContainer();

            container.RegisterPhoneServices(RootFrame);
            container.PerRequest<HomeViewModel>();

            AddCustomConventions();
        }

        static void AddCustomConventions()
        {
            //ellided  
            ConventionManager.AddElementConvention<BindableAppBarButton>(
                Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
                Control.IsEnabledProperty, "DataContext", "Click");
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
