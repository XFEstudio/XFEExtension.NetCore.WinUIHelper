using XFEExtension.NetCore.WinUIHelper.Implements.Services;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace XFEExtension.NetCore.WinUIHelper.Utilities;

/// <summary>
/// 服务管理器
/// </summary>
public static class ServiceManager
{
    private static readonly List<IGlobalService> globalServices = [];
    private static readonly Dictionary<string, Func<object>> services = new()
    {
        { nameof(IAutoNavigationService), () => new AutoNavigationService() },
        { nameof(IDialogService), () => new DialogService() },
        { nameof(IListViewService), () => new ListViewService() },
        { nameof(ILoadingService), () => new LoadingService() },
        { nameof(IMessageService), () => new MessageService() },
        { nameof(INavigationService), () => new AutoNavigationService() },
        { nameof(INavigationViewService), () => new NavigationViewService() },
        { nameof(IPageService), () => new PageService() },
        { nameof(ISettingService), () => new SettingService() },
        { nameof(IUserLoginService), () => new UserLoginService() }
    };
    /// <summary>
    /// 注册全局服务
    /// </summary>
    /// <param name="service">全局服务</param>
    public static void RegisterGlobalService(IGlobalService service) => globalServices.Add(service);
    /// <summary>
    /// 注册服务
    /// </summary>
    public static void RegisterService<T>(Func<object> ctr) => services.Add(nameof(T), ctr);
    /// <summary>
    /// 获取全局服务
    /// </summary>
    /// <typeparam name="T">全局服务泛型</typeparam>
    /// <returns></returns>
    public static T? GetGlobalService<T>() where T : IGlobalService => globalServices.OfType<T>().FirstOrDefault();
    /// <summary>
    /// 获取全局服务
    /// </summary>
    /// <typeparam name="T">全局服务泛型</typeparam>
    /// <returns></returns>
    public static List<T> GetGlobalServices<T>() where T : IGlobalService => [.. globalServices.OfType<T>()];
    /// <summary>
    /// 获取服务
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetService<T>()
    {
        var type = typeof(T);
        if (type.IsGenericType)
            return (T?)Activator.CreateInstance((type.Assembly.GetType($"XFEExtension.NetCore.WinUIHelper.Implements.Services.{type.Name![1..]}") ?? throw new NullReferenceException("无法找到指定服务的实现类")).MakeGenericType(type.GenericTypeArguments)) ?? throw new NullReferenceException("无法找到指定服务的实现类");
        var typeName = typeof(T).Name;
        if (services.TryGetValue(typeName, out var ctr))
            return (T)ctr();
        throw new NullReferenceException("无法找到指定服务的实现类");
    }
}
