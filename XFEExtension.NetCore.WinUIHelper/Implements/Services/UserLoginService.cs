using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

class UserLoginService : GlobalServiceBase, IUserLoginService, IWinUIService<UserLoginService, IUserLoginService>
{
    public Func<Task<bool>>? LoginFunc { get; set; }
    public Func<bool>? LogoutFunc { get; set; }

    public event EventHandler<bool>? UserStateChanged;

    public async Task<bool> TryLogin()
    {
        if (LoginFunc is null)
            return false;
        var result = await LoginFunc();
        UserStateChanged?.Invoke(this, result);
        return result;
    }

    public bool TryLogout()
    {
        if (LogoutFunc is null)
            return false;
        var result = LogoutFunc();
        UserStateChanged?.Invoke(this, !result);
        return !result;
    }
}
