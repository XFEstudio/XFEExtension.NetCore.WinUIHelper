using CommunityToolkit.Mvvm.ComponentModel;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace XFEExtension.NetCore.WinUIHelper.TestApp.ViewModels;

public partial class TestPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? parameter;
    public IAutoNavigationParameterService<string> AutoNavigationParameterService { get; } = ServiceManager.GetService<IAutoNavigationParameterService<string>>();
    public TestPageViewModel()=> AutoNavigationParameterService.ParameterChange += AutoNavigationParameterService_ParameterChange;

    private void AutoNavigationParameterService_ParameterChange(object? sender, string? e)
    {
        Parameter = e;
    }
}
