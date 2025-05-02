using System.Diagnostics.CodeAnalysis;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

class AutoNavigationParameterService<T> : IAutoNavigationParameterService<T>
{
    private bool sameAsLast;
    private T? parameter;
    private Page? _page;
    public T? Parameter
    {
        get => parameter;
        set
        {
            if (_page is not null)
                NavigationHelper.SetParameter(_page, value);
            parameter = value;
            OnParameterChange(value);
        }
    }

    public bool SameAsLast => sameAsLast;

    public Page? CurrentPage => _page;

    public event EventHandler<T?>? ParameterChange;

    [MemberNotNull(nameof(_page))]
    public void Initialize(Page page) => _page = page;

    public void OnParameterChange(object? inParameter)
    {
        sameAsLast = inParameter is null ? parameter is null : inParameter.Equals(parameter);
        if (inParameter is T parameterValue)
            parameter = parameterValue;
        else
            parameter = default;
        ParameterChange?.Invoke(this, Parameter);
    }
}
