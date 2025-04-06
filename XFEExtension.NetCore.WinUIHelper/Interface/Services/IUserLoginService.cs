namespace XFEExtension.NetCore.WinUIHelper.Interface.Services;

/// <summary>
/// 用户登录服务接口
/// </summary>
public interface IUserLoginService : IGlobalService
{
    /// <summary>
    /// 用户登录状态改变事件
    /// </summary>
    event EventHandler<bool>? UserStateChanged;
    /// <summary>
    /// 登录函数
    /// </summary>
    Func<Task<bool>>? LoginFunc { get; set; }
    /// <summary>
    /// 登出函数
    /// </summary>
    Func<bool>? LogoutFunc { get; set; }
    /// <summary>
    /// 尝试登录
    /// </summary>
    /// <returns></returns>
    Task<bool> TryLogin();
    /// <summary>
    /// 尝试登出
    /// </summary>
    /// <returns></returns>
    bool TryLogout();
}
