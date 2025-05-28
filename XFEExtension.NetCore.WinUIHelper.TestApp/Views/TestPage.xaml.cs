using Microsoft.UI.Xaml.Navigation;

namespace XFEExtension.NetCore.WinUIHelper.TestApp.Views;

/// <summary>
/// ≤‚ ‘“≥√Ê
/// </summary>
public sealed partial class TestPage : Page
{
    public static TestPage? Current { get; set; }
    public TestPageViewModel ViewModel { get; set; } = new();

    public TestPage()
    {
        Current = this;
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        ViewModel.AutoNavigationParameterService.OnParameterChange(e.Parameter);
    }
}
