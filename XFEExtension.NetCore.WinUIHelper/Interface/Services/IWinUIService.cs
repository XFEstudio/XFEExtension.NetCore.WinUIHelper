using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace XFEExtension.NetCore.WinUIHelper.Interface.Services;

/// <summary>
/// WinUI服务接口
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="R"></typeparam>
public interface IWinUIService<in T, out R> where T : R, new()
{
    static IWinUIService() => ServiceManager.RegisterService<R>(() => new T());
}