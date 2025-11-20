using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helpers;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

/// <summary>
/// Provides functionality to manage the visibility of page elements based on permission levels within the application.
/// </summary>
/// <remarks>Use this service to initialize the current page context and update the visibility of its child
/// elements according to the specified permission level. This service is typically used to control access to UI
/// components depending on user roles or permissions.</remarks>
public class PermissionVisibilityService : GlobalServiceBase, IPermissionVisibilityService
{
    private Page? currentPage;
    /// <inheritdoc/>
    public Page? CurrentPage => currentPage;

    /// <inheritdoc/>
    public void Initialize(Page page)
    {
        currentPage = page;
    }

    /// <inheritdoc/>
    public void Refresh(int permissionLevel) => PermissionHelper.SetChildVisibility(currentPage!, permissionLevel);
}
