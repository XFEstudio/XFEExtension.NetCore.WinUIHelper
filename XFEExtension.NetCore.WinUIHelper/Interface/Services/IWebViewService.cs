using Windows.Foundation;

namespace XFEExtension.NetCore.WinUIHelper.Interface.Services;

/// <summary>
/// WebView服务
/// </summary>
public interface IWebViewService
{
    /// <summary>
    /// WebView控件
    /// </summary>
    public WebView2 WebView { get; }
    /// <summary>
    /// 初始化完成
    /// </summary>
    public event TypedEventHandler<WebView2, CoreWebView2InitializedEventArgs> Initialized;
    /// <summary>
    /// 初始化WebView控件
    /// </summary>
    /// <param name="webView2"></param>
    public void Initialize(WebView2 webView2);
}
