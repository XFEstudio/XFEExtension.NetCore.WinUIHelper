﻿using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;
using System.Diagnostics.CodeAnalysis;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

/// <inheritdoc cref="INavigationParameterService{T}"/>
internal class NavigationParameterService<T> : INavigationParameterService<T>
{
    private bool sameAsLast;
    private T? currentParameter;
    private Page? _page;
    public T? Parameter
    {
        get => _page is not null ? NavigationHelper.GetParameter(_page) is T t ? t : default : default;
        set
        {
            if (_page is not null)
                NavigationHelper.SetParameter(_page, value);
        }
    }

    public bool SameAsLast => sameAsLast;

    public Page? CurrentPage => _page;

    public event EventHandler<T?>? ParameterChange;

    [MemberNotNull(nameof(_page))]
    public void Initialize(Page page) => _page = page;

    public void OnParameterChange(object? parameter)
    {
        if (parameter is T parameterValue)
            Parameter = parameterValue;
        else
            Parameter = default;
        sameAsLast = Parameter is null ? currentParameter is null : Parameter.Equals(currentParameter);
        if (!sameAsLast)
            currentParameter = Parameter;
        ParameterChange?.Invoke(this, Parameter);
    }
}
