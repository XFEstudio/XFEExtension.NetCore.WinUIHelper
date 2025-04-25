using Windows.Foundation;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

internal class SelectorBarService : ISelectorBarService
{
    public event TypedEventHandler<SelectorBar, SelectorBarSelectionChangedEventArgs>? SelectionChanged;
    public void Initialize(SelectorBar selectorBar) => selectorBar.SelectionChanged += (sender, args) => SelectionChanged?.Invoke(sender, args);
}
