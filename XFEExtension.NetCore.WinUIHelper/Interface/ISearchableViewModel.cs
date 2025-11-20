namespace XFEExtension.NetCore.WinUIHelper.Interface;

/// <summary>
/// 可搜索视图模型接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISearchableViewModel<T> : IListViewModel<T> where T : class
{
    /// <summary>
    /// 搜索文本
    /// </summary>
    string SearchText { get; set; }
    /// <summary>
    /// 搜索预测
    /// </summary>
    Func<string, T, bool> SearchPredicate { get; set; }

    /// <summary>
    /// 搜索模型
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> SearchModels(string searchText) => ModelList.Where(item => SearchPredicate(searchText, item));

    /// <summary>
    /// 搜索模型
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> SearchModels() => ModelList.Where(item => SearchPredicate(SearchText, item));

    /// <summary>
    /// 搜索并加载模型
    /// </summary>
    void SearchAndLoadToList() => LoadToViewList(SearchModels());
}
