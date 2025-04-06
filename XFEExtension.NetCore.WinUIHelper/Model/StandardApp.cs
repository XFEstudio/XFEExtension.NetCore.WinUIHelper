namespace XFEExtension.NetCore.WinUIHelper.Model;

/// <summary>
/// 标准应用程序
/// </summary>
public abstract partial class StandardApp : Application
{
    /// <summary>
    /// 应用程序主窗口
    /// </summary>
    public static StandardMainWindow? MainWindow { get; set; }
}
