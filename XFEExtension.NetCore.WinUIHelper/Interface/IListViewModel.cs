using System.Collections.ObjectModel;

namespace XFEExtension.NetCore.WinUIHelper.Interface;

/// <summary>
/// 指定类型列表的视图模型接口
/// </summary>
/// <typeparam name="T">模型类型</typeparam>
public interface IListViewModel<T> where T : class
{
    ObservableCollection<T> ViewList { get; set; }
    List<T> ModelList { get; set; }

    /// <summary>
    /// 加载到视图列表
    /// </summary>
    /// <param name="list"></param>
    void LoadToViewList(IEnumerable<T> list)
    {
        Clear();
        foreach (var item in list)
            Add(item);
    }

    /// <summary>
    /// 添加到视图列表
    /// </summary>
    /// <param name="list"></param>
    void AddToViewList(IEnumerable<T> list)
    {
        foreach (var item in list)
            ViewList.Add(item);
    }

    /// <summary>
    /// 清除视图列表
    /// </summary>
    void Clear() => ViewList.Clear();

    /// <summary>
    /// 向视图列表添加一个元素
    /// </summary>
    /// <param name="item"></param>
    void Add(T item) => ViewList.Add(item);

    /// <summary>
    /// 从视图列表中移除指定元素
    /// </summary>
    /// <param name="item"></param>
    void Remove(T item) => ViewList.Remove(item);

    /// <summary>
    /// 在视图列表指定位置移除一个元素
    /// </summary>
    /// <param name="index"></param>
    void RemoveAt(int index) => ViewList.RemoveAt(index);

    /// <summary>
    /// 视图列表中是否包含指定元素
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    bool Contains(T item) => ViewList.Contains(item);

    /// <summary>
    /// 使用指定表达式搜索模型列表（而非视图列表）
    /// </summary>
    /// <param name="predicte"></param>
    /// <returns></returns>
    IEnumerable<T> Search(Func<T, bool> predicte) => ModelList.Where(item => predicte(item));
}
