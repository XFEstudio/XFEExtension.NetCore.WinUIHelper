using Microsoft.UI.Xaml.Media;

namespace XFEExtension.NetCore.WinUIHelper.Interface.Services;

/// <summary>
/// 列表视图服务接口
/// </summary>
public interface IListViewService
{
    /// <summary>
    /// 选中项改变事件
    /// </summary>
    event SelectionChangedEventHandler SelectionChanged;
    /// <summary>
    /// 列表视图
    /// </summary>
    ListView ListView { get; }
    /// <summary>
    /// 列表视图滚动条
    /// </summary>
    ScrollViewer? ListViewScrollViewer { get; }
    /// <summary>
    /// 初始化列表视图服务
    /// </summary>
    /// <param name="listView"></param>
    void Initialize(ListView listView);
    /// <summary>
    /// 选择所有项
    /// </summary>
    void SelectAll() => ListView.SelectAll();
    /// <summary>
    /// 反选
    /// </summary>
    void RevertSelect();
    /// <summary>
    /// 选择范围
    /// </summary>
    void SelectBetween();
    /// <summary>
    /// 获取列表视图滚动条
    /// </summary>
    /// <returns></returns>
    ScrollViewer? GetScrollViewer()
    {
        if (VisualTreeHelper.GetChildrenCount(ListView) > 0)
            return VisualTreeHelper.GetChild(ListView, 0) is Border border && border.Child is ScrollViewer scrollViewer ? scrollViewer : null;
        else
            return null;
    }
}
