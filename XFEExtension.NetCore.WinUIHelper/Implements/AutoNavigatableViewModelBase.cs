using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace XFEExtension.NetCore.WinUIHelper.Implements;

public abstract partial class AutoNavigatableViewModelBase<T> : ViewModelBase
{
    public IAutoNavigationParameterService<T> AutoNavigationParameterService { get; } = ServiceManager.GetService<IAutoNavigationParameterService<T>>();
}
