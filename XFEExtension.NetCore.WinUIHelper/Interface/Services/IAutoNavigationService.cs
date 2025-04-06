using Microsoft.UI.Xaml.Navigation;

namespace XFEExtension.NetCore.WinUIHelper.Interface.Services;

/// <summary>
/// 自动导航实现
/// </summary>
public interface IAutoNavigationService : INavigationService, IAutoNavigable
{
    /// <summary>
    /// 导航完成
    /// </summary>
    event NavigatedEventHandler Navigated;
}
