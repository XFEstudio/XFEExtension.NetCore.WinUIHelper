using Microsoft.UI.Xaml.Data;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

public class ListViewService : IListViewService
{
    private ListView? _listView;

    public event SelectionChangedEventHandler? SelectionChanged;

    public ListView ListView => _listView ?? throw new NullReferenceException();

    public void Initialize(ListView listView)
    {
        _listView = listView;
        listView.SelectionChanged += ListView_SelectionChanged;
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) => SelectionChanged?.Invoke(sender, e);

    public void RevertSelect()
    {
        List<ItemIndexRange> rangeList = [.. ListView.SelectedRanges];
        ListView.SelectAll();
        foreach (var range in rangeList)
            ListView.DeselectRange(range);
    }

    public void SelectBetween()
    {
        if (ListView.SelectedRanges.Count > 1)
        {
            var startRange = ListView.SelectedRanges[0];
            var endRange = ListView.SelectedRanges[ListView.SelectedRanges.Count - 1];
            ListView.SelectRange(new(startRange.FirstIndex, (uint)(endRange.FirstIndex - startRange.FirstIndex)));
        }
    }
}
