using System.Reflection;
using XFEExtension.NetCore.AutoPath;

namespace XFEExtension.NetCore.WinUIHelper.Utilities;

public partial class AppPathHelper
{
    /// <summary>
    /// 应用程序名称
    /// </summary>
    public static string? ApplicationName { get; set; } = Assembly.GetCallingAssembly().GetName().Name;
    /// <summary>
    /// 应用程序同步路径
    /// </summary>
    [AutoPath]
    private static readonly string appSynData = @$"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\{ApplicationName}";
    /// <summary>
    /// 应用程序本地路径
    /// </summary>
    [AutoPath]
    private static readonly string appLocalData = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\{ApplicationName}\CrossVersion";
    /// <summary>
    /// 应用程序本地当前版本路径
    /// </summary>
    [AutoPath]
    private static readonly string appLocalVersionData = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\{ApplicationName}\Versions\{(Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "UnknownVersion")}";
    /// <summary>
    /// 应用程序缓存路径
    /// </summary>
    [AutoPath]
    private static readonly string appCache = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Temp\{ApplicationName}";
    /// <summary>
    /// 应用程序本地配置文件路径
    /// </summary>
    [AutoPath]
    private static readonly string localProfile = @$"{AppLocalData}\Profiles";
    /// <summary>
    /// 应用程序本地当前版本配置文件路径
    /// </summary>
    [AutoPath]
    private static readonly string localVersionProfile = $@"{AppLocalVersionData}\Profiles";
    /// <summary>
    /// 应用程序同步配置文件路径
    /// </summary>
    [AutoPath]
    private static readonly string synProfile = $@"{AppSynData}\Profiles";
    /// <summary>
    /// 应用程序缓存路径
    /// </summary>
    [AutoPath]
    private static readonly string cacheProfile = $@"{AppCache}\Profiles";
    /// <summary>
    /// 应用程序下载缓存
    /// </summary>
    [AutoPath]
    private static readonly string downloadCache = @$"{AppCache}\Download";
}
