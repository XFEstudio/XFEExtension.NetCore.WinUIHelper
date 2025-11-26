using CommunityToolkit.Mvvm.ComponentModel;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace XFEExtension.NetCore.WinUIHelper.Implements;

/// <summary>
/// 自动导航视图模型基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract partial class AutoNavigatableViewModelBase<T> : ObservableObject
{
    /// <summary>
    /// 自动导航参数服务
    /// </summary>
    public IAutoNavigationParameterService<T> AutoNavigationParameterService { get; } = ServiceManager.GetService<IAutoNavigationParameterService<T>>();
}
