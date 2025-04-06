using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

class PageService : IPageService, IWinUIService<PageService, IPageService>
{
    private Page? _page;
    public Page? CurrentPage => _page;

    public void Initialize(Page page) => _page = page;
}
