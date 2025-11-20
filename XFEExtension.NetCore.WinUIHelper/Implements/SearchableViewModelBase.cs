using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using XFEExtension.NetCore.DelegateExtension;
using XFEExtension.NetCore.WinUIHelper.Interface;

namespace XFEExtension.NetCore.WinUIHelper.Implements;

public abstract partial class SearchableViewModelBase<T, F> : ServiceBaseViewModelBase<F>, ISearchableViewModel<T> where T : class
{
    [ObservableProperty]
    private T? selectedItem;
    [ObservableProperty]
    private string searchText = string.Empty;
    public bool AutoSearch { get; set; } = true;
    public ObservableCollection<T> ViewList { get; set; } = [];
    public List<T> ModelList { get; set; } = [];
    public Func<string, T, bool> SearchPredicate { get; set; } = (text, item) => ObjectHelper.Search(item, text);

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
