using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace XFEExtension.NetCore.WinUIHelper.Implements;

public abstract partial class ServiceBaseViewModelBase<T> : AutoNavigatableViewModelBase<T>
{
    public IDialogService DialogService { get; } = ServiceManager.GetService<IDialogService>();
    public INavigationViewService? NavigationViewService { get; } = ServiceManager.GetGlobalService<INavigationViewService>();
    public IMessageService? MessageService { get; } = ServiceManager.GetGlobalService<IMessageService>();
    public ILoadingService? LoadingService { get; } = ServiceManager.GetGlobalService<ILoadingService>();
}
