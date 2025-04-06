using Windows.UI.ViewManagement;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

namespace XFEExtension.NetCore.WinUIHelper.Model;

/// <summary>
/// 标准主窗口
/// </summary>
public abstract partial class StandardMainWindow : Window
{
    /// <summary>
    /// UI设置
    /// </summary>
    public static UISettings UISettings { get; set; } = new UISettings();

    /// <summary>
    /// 标准主窗口
    /// </summary>
    /// <param name="iconPath">应用图标</param>
    public StandardMainWindow(string iconPath) => Initialize(iconPath);

    /// <summary>
    /// 标准主窗口
    /// </summary>
    public StandardMainWindow() => Initialize(Path.Combine(AppContext.BaseDirectory, "Assets/Icons/Icon.ico"));

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="iconPath">应用图标</param>
    protected void Initialize(string iconPath)
    {
        ExtendsContentIntoTitleBar = true;
        AppWindow.SetIcon(iconPath);
        UISettings.ColorValuesChanged += (_, _) => DispatcherQueue.TryEnqueue(() => AppThemeHelper.ChangeTheme(AppThemeHelper.Theme));
    }
}
