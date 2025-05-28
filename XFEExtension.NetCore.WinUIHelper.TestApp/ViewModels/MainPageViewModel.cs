using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace XFEExtension.NetCore.WinUIHelper.TestApp.ViewModels
{
    public partial class MainPageViewModel : ViewModelBase
    {
        [ObservableProperty]
        int clickCount = 0;

        public IAutoNavigationParameterService<string> AutoNavigationParameterService { get; set; } = ServiceManager.GetService<IAutoNavigationParameterService<string>>();
        public INavigationViewService? NavigationViewService { get; } = ServiceManager.GetGlobalService<INavigationViewService>();
        public IMessageService? MessageService { get; } = ServiceManager.GetGlobalService<IMessageService>();

        [RelayCommand]
        void IncrementClickCount()
        {
            ClickCount++;
            MessageService?.ShowMessage($"Click count: {ClickCount}", "Info");
        }

        [RelayCommand]
        void NavigateToTestPage()
        {
            NavigationViewService?.NavigateTo<TestPage>("MyParameter");
        }
    }
}
