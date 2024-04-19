using Microsoft.Extensions.DependencyInjection;
using SG.Server.Services.Interfaces;
using System;

namespace SG.Server.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void InitialSetupCheck()
        {
            if (NeedInitialSetup())
            {
                CurrentPage = _serviceProvider.GetRequiredService<InitialSetupView>();
            }
            else
            {
                CurrentPage = _serviceProvider.GetRequiredService<HomePageView>();
            }
        }

        private bool NeedInitialSetup()
        {
            var deviceGeneration = _serviceProvider.GetRequiredService<IDeviceIDGenerationService>();
            return deviceGeneration.NeedInitialSetup();
        }
    }
}
