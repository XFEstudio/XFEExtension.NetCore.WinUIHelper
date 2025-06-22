using Microsoft.UI.Xaml.Navigation;

namespace XFEExtension.NetCore.WinUIHelper.TestApp.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class WebViewTestPage : Page
{
    public static WebViewTestPage? Current { get; set; }
    public WebViewTestPageViewModel ViewModel { get; set; } = new();
    public WebViewTestPage()
    {
        Current = this;
        InitializeComponent();
        ViewModel.AutoNavigationParameterService.Initialize(this);
        ViewModel.WebViewService.Initialize(webView);
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        ViewModel.AutoNavigationParameterService.OnParameterChange(e.Parameter);
    }
}
