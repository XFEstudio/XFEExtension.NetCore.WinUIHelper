﻿using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Utilities;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

/// <summary>
/// 全局服务接口基类
/// </summary>
public abstract class GlobalServiceBase : IGlobalService
{
    ///<inheritdoc/>
    protected GlobalServiceBase() => ServiceManager.RegisterGlobalService(this);
}