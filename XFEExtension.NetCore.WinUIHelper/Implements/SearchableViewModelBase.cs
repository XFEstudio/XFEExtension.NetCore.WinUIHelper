using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using XFEExtension.NetCore.DelegateExtension;
using XFEExtension.NetCore.WinUIHelper.Interface;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

namespace XFEExtension.NetCore.WinUIHelper.Implements;

public abstract partial class SearchableViewModelBase<T, F> : ServiceBaseViewModelBase<F>, ISearchableViewModel<T> where T : class
{
    [ObservableProperty]
    private T? selectedItem;
    [ObservableProperty]
    private string searchText = string.Empty;
    /// <summary>
    /// 文本内容改变时自动搜索
    /// </summary>
    public bool AutoSearch { get; set; } = true;
    /// <summary>
    /// 前端展示视图列表
    /// </summary>
    public ObservableCollection<T> ViewList { get; set; } = [];
    /// <summary>
    /// 后端模型列表
    /// </summary>
    public List<T> ModelList { get; set; } = [];
    /// <summary>
    /// 搜索预测
    /// </summary>
    public Func<string, T, bool> SearchPredicate { get; set; } = (text, item) => ObjectHelper.Search(item, text);

    /// <summary>
    /// 搜索文本改变事件
    /// </summary>
    public event XFEEventHandler<string>? SearchTextChanged;

    partial void OnSearchTextChanged(string value)
    {
        SearchTextChanged?.Invoke(value);
        if (AutoSearch)
            Search();
    }

    protected virtual void Add() { }
    protected virtual async Task AddAsync() => await Task.CompletedTask;
    protected virtual void Remove() { }
    protected virtual async Task RemoveAsync() => await Task.CompletedTask;
    protected virtual void Edit() { }
    protected virtual async Task EditAsync() => await Task.CompletedTask;
    protected void Search() => (this as ISearchableViewModel<T>).SearchAndLoadToList();
}
