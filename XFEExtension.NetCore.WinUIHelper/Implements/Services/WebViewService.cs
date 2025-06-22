using Windows.Foundation;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

class WebViewService : IWebViewService
{
    private WebView2? _webView2;
    public WebView2 WebView => _webView2 ?? throw new InvalidOperationException("WebView2 has not been initialized.");

    public event TypedEventHandler<WebView2, CoreWebView2InitializedEventArgs>? Initialized;

    public void Initialize(WebView2 webView2)
    {
        _webView2 = webView2;
        _webView2.CoreWebView2Initialized += WebView2_CoreWebView2Initialized; ;
    }

    private void WebView2_CoreWebView2Initialized(WebView2 sender, CoreWebView2InitializedEventArgs args) => Initialized?.Invoke(sender, args);
}