using Microsoft.UI.Xaml.Media;
using XFEExtension.NetCore.WinUIHelper.Utilities.Additions;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Helpers;

/// <summary>
/// 权限帮助类
/// </summary>
public static class PermissionHelper
{
    /// <summary>
    /// 设置可见性
    /// </summary>
    /// <param name="uIElement"></param>
    /// <param name="currentPermission"></param>
    public static void SetVisibility(UIElement uIElement, int currentPermission) => uIElement.Visibility = PermissionAddition.GetRequiredPermission(uIElement) <= currentPermission ? Visibility.Visible : Visibility.Collapsed;

    /// <summary>
    /// 设置可见性
    /// </summary>
    /// <param name="menuItems"></param>
    /// <param name="currentPermission"></param>
    public static void SetVisibility(IList<object> menuItems, int currentPermission)
    {
        foreach (var menuItem in menuItems.OfType<NavigationViewItem>())
        {
            SetVisibility(menuItem, currentPermission);
            if (menuItem.MenuItems.Count > 0)
            {
                SetVisibility(menuItem.MenuItems, currentPermission);
            }
        }
    }

    /// <summary>
    /// 设置子节点可见性
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="currentPermission"></param>
    public static void SetChildVisibility(UIElement parent, int currentPermission)
    {
        foreach (var child in FindElementsWithAttachedProperty(parent, PermissionAddition.RequiredPermissionProperty))
        {
            SetVisibility(child, currentPermission);
        }
    }

    /// <summary>
    /// 寻找带有指定附加属性的控件
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="attachedProperty"></param>
    /// <returns></returns>
    public static IEnumerable<UIElement> FindElementsWithAttachedProperty(UIElement parent, DependencyProperty attachedProperty)
    {
        if (parent is null)
            yield break;
        int count = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < count; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is UIElement uIElement)
            {
                var value = uIElement.GetValue(attachedProperty);
                var metadata = attachedProperty.GetMetadata(uIElement.GetType());
                if (!Equals(value, metadata.DefaultValue))
                {
                    yield return uIElement;
                }
                foreach (var descendant in FindElementsWithAttachedProperty(uIElement, attachedProperty))
                {
                    yield return descendant;
                }
            }
        }
    }
}
