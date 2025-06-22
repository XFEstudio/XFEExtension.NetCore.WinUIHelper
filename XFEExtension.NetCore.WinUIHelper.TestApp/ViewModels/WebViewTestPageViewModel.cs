using Microsoft.UI.Xaml.Controls;
using System.Threading.Tasks;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace XFEExtension.NetCore.WinUIHelper.TestApp.ViewModels;

public partial class WebViewTestPageViewModel : ViewModelBase
{
    public IAutoNavigationParameterService<string> AutoNavigationParameterService { get; } = ServiceManager.GetService<IAutoNavigationParameterService<string>>();
    public IWebViewService WebViewService { get; } = ServiceManager.GetService<IWebViewService>();

    public WebViewTestPageViewModel()
    {
        WebViewService.Initialized += WebViewService_Initialized;
        AutoNavigationParameterService.ParameterChange += AutoNavigationParameterService_ParameterChange;
    }

    private void AutoNavigationParameterService_ParameterChange(object? sender, string? e)
    {

    }


    private async void WebViewService_Initialized(WebView2 sender, CoreWebView2InitializedEventArgs args)
    {
        if (args.Exception is null)
        {
            while (await sender.CoreWebView2.CookieManager.GetCookiesAsync("https://mms.pinduoduo.com") is { } cookies && !cookies.Any(cookie => cookie.Name == "JSESSIONID"))
            {
                Console.WriteLine();
            }
        }
    }
}
