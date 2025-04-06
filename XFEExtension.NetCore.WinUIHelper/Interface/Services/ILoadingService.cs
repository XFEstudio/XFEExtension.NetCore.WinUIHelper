using Microsoft.UI.Dispatching;

namespace XFEExtension.NetCore.WinUIHelper.Interface.Services;

/// <summary>
/// 全局页面加载显示服务
/// </summary>
public interface ILoadingService : IGlobalService
{
    IAutoNavigationService? NavigationService { get; }
    /// <summary>
    /// 初始化加载显示服务
    /// </summary>
    /// <param name="loadingGrid">加载用网格</param>
    /// <param name="globalLoadingGrid">全局加载网格</param>
    /// <param name="globalLoadingTextBox">全局加载文本</param>
    /// <param name="dispatcherQueue">Dispatcher</param>
    /// <param name="navigationService">导航服务</param>
    void Initialize(Grid loadingGrid, Grid globalLoadingGrid, TextBlock globalLoadingTextBox, DispatcherQueue dispatcherQueue, IAutoNavigationService navigationService);
    /// <summary>
    /// 开始加载
    /// </summary>
    /// <typeparam name="T">需要显示的页面</typeparam>
    /// <param name="showText">加载提示文本呢</param>
    /// <returns>该页面上是否已经有了加载项</returns>
    bool StartLoading<T>(string showText = "Loading...") where T : Page;
    /// <summary>
    /// 开始全局加载
    /// </summary>
    /// <param name="showText">加载提示文本</param>
    void StartLoading(string showText = "Loading...");
    /// <summary>
    /// 停止全局加载服务
    /// </summary>
    void StopLoading();
    /// <summary>
    /// 停止加载
    /// </summary>
    /// <typeparam name="T">需要显示的页面</typeparam>
    /// <returns>是否成功停止</returns>
    bool StopLoading<T>() where T : Page;
}
