using Microsoft.UI;
using Windows.UI;
using Windows.UI.ViewManagement;
using XFEExtension.NetCore.WinUIHelper.Model;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

/// <summary>
/// 应用程序主题帮助类
/// </summary>
public static class AppThemeHelper
{
    /// <summary>
    /// 应用程序主题
    /// </summary>
    public static ElementTheme Theme { get; set; } = ElementTheme.Default;

    /// <summary>
    /// 更改应用程序主题
    /// </summary>
    /// <param name="theme">目标主题</param>
    public static void ChangeTheme(ElementTheme theme)
    {
        if (StandardApp.MainWindow is null)
            return;

        if (StandardApp.MainWindow.Content is FrameworkElement rootElement)
        {
            rootElement.RequestedTheme = theme;
        }

        if (theme == ElementTheme.Default)
        {
            var uiSettings = new UISettings();
            var background = uiSettings.GetColorValue(UIColorType.Background);

            theme = background == Colors.White ? ElementTheme.Light : ElementTheme.Dark;
        }

        if (theme == ElementTheme.Default)
        {
            theme = Application.Current.RequestedTheme == ApplicationTheme.Light ? ElementTheme.Light : ElementTheme.Dark;
        }

        StandardApp.MainWindow.AppWindow.TitleBar.ButtonForegroundColor = theme switch
        {
            ElementTheme.Dark => Colors.White,
            ElementTheme.Light => Colors.Black,
            _ => Colors.Transparent
        };

        StandardApp.MainWindow.AppWindow.TitleBar.ButtonHoverForegroundColor = theme switch
        {
            ElementTheme.Dark => Colors.White,
            ElementTheme.Light => Colors.Black,
            _ => Colors.Transparent
        };

        StandardApp.MainWindow.AppWindow.TitleBar.ButtonHoverBackgroundColor = theme switch
        {
            ElementTheme.Dark => Color.FromArgb(0x33, 0xFF, 0xFF, 0xFF),
            ElementTheme.Light => Color.FromArgb(0x33, 0x00, 0x00, 0x00),
            _ => Colors.Transparent
        };

        StandardApp.MainWindow.AppWindow.TitleBar.ButtonPressedBackgroundColor = theme switch
        {
            ElementTheme.Dark => Color.FromArgb(0x66, 0xFF, 0xFF, 0xFF),
            ElementTheme.Light => Color.FromArgb(0x66, 0x00, 0x00, 0x00),
            _ => Colors.Transparent
        };

        StandardApp.MainWindow.AppWindow.TitleBar.BackgroundColor = Colors.Transparent;
    }

    /// <summary>
    /// 转换为应用程序主题
    /// </summary>
    /// <param name="theme">目标主题</param>
    /// <returns>应用程序主题</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static ApplicationTheme ToAppTheme(this ElementTheme theme) => theme switch
    {
        ElementTheme.Default => Application.Current.RequestedTheme,
        ElementTheme.Light => ApplicationTheme.Light,
        ElementTheme.Dark => ApplicationTheme.Dark,
        _ => throw new NotImplementedException()
    };
}
