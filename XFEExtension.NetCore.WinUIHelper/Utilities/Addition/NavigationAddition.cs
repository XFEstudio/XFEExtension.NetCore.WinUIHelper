namespace XFEExtension.NetCore.WinUIHelper.Utilities.Addition;

/// <summary>
/// Provides attached properties for associating navigation targets and parameters with NavigationViewItem controls.
/// </summary>
/// <remarks>Use the attached properties defined by this class to specify navigation destinations and parameters
/// for individual NavigationViewItem instances in a NavigationView. These properties enable declarative navigation
/// configuration in XAML or code-behind, allowing navigation logic to retrieve target information and parameters when a
/// navigation item is invoked.</remarks>
public class NavigationAddition
{
    /// <summary>
    /// Retrieves the navigation target associated with the specified navigation view item.
    /// </summary>
    /// <param name="item">The navigation view item from which to obtain the navigation target. Cannot be null.</param>
    /// <returns>A string representing the navigation target for the specified item, or null if no target is set.</returns>
    public static string GetNavigateTo(NavigationViewItem item) => (string)item.GetValue(NavigateToProperty);
    /// <summary>
    /// Sets the navigation target value for the specified NavigationViewItem.
    /// </summary>
    /// <remarks>Use this method to attach a navigation target to a NavigationViewItem, enabling navigation
    /// logic to identify the destination when the item is selected.</remarks>
    /// <param name="item">The NavigationViewItem to associate with the navigation target value. Cannot be null.</param>
    /// <param name="value">The navigation target value to assign. This value typically represents a page key or URI used for navigation.</param>
    public static void SetNavigateTo(NavigationViewItem item, string value) => item.SetValue(NavigateToProperty, value);
    /// <summary>
    /// Identifies the NavigateTo attached property, which specifies the navigation target as a URI or page name for
    /// supported UI elements.
    /// </summary>
    /// <remarks>This dependency property can be set on UI elements to enable navigation behavior, typically
    /// in frameworks such as WPF or UWP. The value should be a valid URI or page identifier recognized by the
    /// navigation system. The default value is an empty string.</remarks>

    public static readonly DependencyProperty NavigateToProperty = DependencyProperty.RegisterAttached("NavigateTo", typeof(string), typeof(NavigationAddition), new PropertyMetadata(""));

    /// <summary>
    /// Retrieves the navigation parameter associated with the specified NavigationViewItem.
    /// </summary>
    /// <param name="item">The NavigationViewItem from which to obtain the navigation parameter. Cannot be null.</param>
    /// <returns>An object representing the navigation parameter for the specified item, or null if no parameter is set.</returns>
    public static object GetNavigateParameter(NavigationViewItem item) => item.GetValue(NavigateParameterProperty);
    /// <summary>
    /// Sets the navigation parameter value for the specified NavigationViewItem.
    /// </summary>
    /// <remarks>Use this method to attach contextual data to a NavigationViewItem, which can be accessed
    /// during navigation events. This is useful for passing parameters between navigation targets in a
    /// NavigationView.</remarks>
    /// <param name="item">The NavigationViewItem for which to set the navigation parameter. Cannot be null.</param>
    /// <param name="value">The value to associate with the navigation parameter. This value will be stored and can be retrieved when
    /// navigating.</param>
    public static void SetNavigateParameter(NavigationViewItem item, object value) => item.SetValue(NavigateParameterProperty, value);

    /// <summary>
    /// Identifies the NavigateParameter attached dependency property, which enables passing a navigation parameter to a
    /// target element in XAML.
    /// </summary>
    /// <remarks>This property is typically used in navigation scenarios to associate additional data with a
    /// UI element when initiating navigation. The value can be any object and is intended to be retrieved by navigation
    /// logic or handlers. This property is commonly set in XAML using property element syntax.</remarks>
    public static readonly DependencyProperty NavigateParameterProperty = DependencyProperty.RegisterAttached("NavigateParameter", typeof(object), typeof(NavigationAddition), new PropertyMetadata(null));
}