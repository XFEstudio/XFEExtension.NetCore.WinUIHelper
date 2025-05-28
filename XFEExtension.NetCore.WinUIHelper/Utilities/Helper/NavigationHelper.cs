using XFEExtension.NetCore.WinUIHelper.Implements.Services;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities.Addition;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

/// <summary>
/// 导航帮助类
/// </summary>
public static class NavigationHelper
{
    /// <summary>
    /// 设置目标页面的导航参数
    /// </summary>
    /// <param name="page">目标页面</param>
    /// <param name="parameter">导航参数</param>
    public static void SetParameter(this Page page, object? parameter) => page.GetType().GetProperty("Parameter")?.SetValue(page, parameter);
    /// <summary>
    /// 获取导航参数
    /// </summary>
    /// <param name="page">目标页面</param>
    /// <returns>导航参数</returns>
    public static object? GetParameter(this Page page)
    {
        var viewModelProperty = page.GetType().GetProperty("ViewModel");
        var parameterProperty = viewModelProperty?.PropertyType.GetProperties().FirstOrDefault(property => property.PropertyType.Name == typeof(IAutoNavigationParameterService<object>).Name);
        return parameterProperty?.PropertyType.GetInterfaces()[0].GetProperty("Parameter")?.GetValue(parameterProperty.GetValue(viewModelProperty?.GetValue(page))) ?? page.GetType().GetProperty("Parameter")?.GetValue(page);
    }
    /// <summary>
    /// 获取导航目标附加值
    /// </summary>
    /// <param name="navigationViewItem">导航项</param>
    /// <returns>导航目标</returns>
    public static string? GetNavigateTo(this NavigationViewItem navigationViewItem) => navigationViewItem.GetValue(NavigationAddition.NavigateToProperty).ToString();
    /// <summary>
    /// 获取导航参数附加值
    /// </summary>
    /// <param name="navigationViewItem">导航项</param>
    /// <returns>导航参数</returns>
    public static object? GetNavigationParameter(this NavigationViewItem navigationViewItem) => navigationViewItem.GetValue(NavigationAddition.NavigateParameterProperty);
}
