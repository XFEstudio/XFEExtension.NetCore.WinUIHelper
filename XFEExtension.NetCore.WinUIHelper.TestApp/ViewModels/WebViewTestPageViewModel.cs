using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace XFEExtension.NetCore.WinUIHelper.TestApp.ViewModels;

public partial class WebViewTestPageViewModel : ViewModelBase
{
    public IAutoNavigationParameterService<string> AutoNavigationParameterService { get; set; } = ServiceManager.GetService<IAutoNavigationParameterService<string>>();

    public WebViewTestPageViewModel() => AutoNavigationParameterService.ParameterChange += AutoNavigationParameterService_ParameterChange;

    private void AutoNavigationParameterService_ParameterChange(object? sender, string? e)
    {

    }
}
