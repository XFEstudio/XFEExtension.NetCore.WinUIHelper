namespace XFEExtension.NetCore.WinUIHelper.Interface.Services;

/// <summary>
/// Provides functionality to refresh the visibility state of permissions based on the specified permission level.
/// </summary>
public interface IPermissionVisibilityService : IGlobalService, IPageService
{
    /// <summary>
    /// Refreshes the current state based on the specified permission level.
    /// </summary>
    /// <param name="permissionLevel">The permission level to apply during the refresh operation. Must be a non-negative integer; higher values may
    /// grant access to additional resources.</param>
    void Refresh(int permissionLevel);
}
