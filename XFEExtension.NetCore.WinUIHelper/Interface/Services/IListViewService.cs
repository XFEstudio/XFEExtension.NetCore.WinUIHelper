namespace XFEExtension.NetCore.WinUIHelper.Interface.Services;

public interface IListViewService
{
    event SelectionChangedEventHandler SelectionChanged;
    ListView ListView { get; }
    void Initialize(ListView listView);
    void SelectAll() => ListView.SelectAll();
    void RevertSelect();
    void SelectBetween();
}
