namespace XFEExtension.NetCore.WinUIHelper.Utilities.Additions;

/// <summary>
/// Provides an attached property for specifying the required permission level on UI elements.
/// </summary>
/// <remarks>The PermissionAddition class enables developers to associate a required permission value with any
/// UIElement using the RequiredPermission attached property. This can be used to control access or visibility of UI
/// elements based on user permissions in WPF applications.</remarks>
public class PermissionAddition
{
    /// <summary>
    /// Retrieves the required permission level associated with the specified UI element.
    /// </summary>
    /// <param name="item">The UI element from which to obtain the required permission level. Must not be null.</param>
    /// <returns>An integer representing the required permission level for the specified UI element.</returns>
    public static int GetRequiredPermission(UIElement item) => (int)item.GetValue(RequiredPermissionProperty);
    /// <summary>
    /// Sets the required permission value for the specified UI element.
    /// </summary>
    /// <remarks>This method attaches a permission value to the UI element using the RequiredPermission
    /// attached property. Use this to control access or visibility based on permission levels in your
    /// application.</remarks>
    /// <param name="item">The UI element on which to set the required permission. Cannot be null.</param>
    /// <param name="value">The permission value to assign to the UI element.</param>
    public static void SetRequiredPermission(UIElement item, int value) => item.SetValue(RequiredPermissionProperty, value);
    /// <summary>
    /// Identifies the RequiredPermission attached dependency property, which specifies the required permission level
    /// for a UI element.
    /// </summary>
    /// <remarks>This property can be attached to any DependencyObject to indicate the minimum permission
    /// level necessary for interaction or visibility. It is typically used in scenarios where UI elements should be
    /// enabled, visible, or accessible only to users with sufficient permissions. The default value is 0, representing
    /// no required permission.</remarks>

    public static readonly DependencyProperty RequiredPermissionProperty = DependencyProperty.RegisterAttached("RequiredPermission", typeof(int), typeof(PermissionAddition), new PropertyMetadata(0));
}
