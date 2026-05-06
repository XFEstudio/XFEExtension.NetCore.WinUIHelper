# XFEExtension.NetCore.WinUIHelper

[![NuGet Version](https://img.shields.io/nuget/v/XFEExtension.NetCore.WinUIHelper?style=flat-square&logo=nuget)](https://www.nuget.org/packages/XFEExtension.NetCore.WinUIHelper)
[![NuGet Downloads](https://img.shields.io/nuget/dt/XFEExtension.NetCore.WinUIHelper?style=flat-square&logo=nuget)](https://www.nuget.org/packages/XFEExtension.NetCore.WinUIHelper)
[![License: MIT](https://img.shields.io/badge/license-MIT-green?style=flat-square)](LICENSE.txt)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

> 📖 [English Documentation](README.md) | 中文文档

基于 .NET 8 的 WinUI 3 扩展库，提供了一系列便捷的帮助类、服务和扩展方法，旨在简化 WinUI 3 应用的开发流程。

## 目录

- [特性](#特性)
- [快速开始](#快速开始)
  - [1. 初始化配置](#1-初始化配置)
  - [2. 构建 Shell 页](#2-构建-shell-页)
  - [3. 使用服务](#3-使用服务)
- [核心功能](#核心功能)
  - [ServiceManager](#servicemanager)
  - [导航服务](#导航服务)
  - [消息与对话框](#消息与对话框)
- [常用工具类](#常用工具类)

## 特性

- **轻量级 IOC 容器**：内置 `ServiceManager`，支持服务注册与全局单例获取。
- **导航管理**：封装 `NavigationView` 和 `Frame`，提供基于 ViewModel 的导航体验。
- **UI交互服务**：提供 `DialogService`、`MessageService` (类似 InfoBar)、`LoadingService` 等常用交互服务。
- **MVVM 支持**：提供 `ObservableObject` 扩展及通用 ViewModel 基类。

## 快速开始

### 1. 初始化配置

在 `App.xaml.cs` 中进行初始化，包括注册页面和配置异常处理。

```csharp
public App()
{
    this.InitializeComponent();
    
    // 设置应用主题
    AppThemeHelper.Theme = ElementTheme.Dark; 

    // 注册导航页面 (PageManager)
    // 必须在此处注册所有可通过字符串或类型导航的页面
    PageManager.RegisterPage(typeof(AppShellPage));
    PageManager.RegisterPage(typeof(MainPage));
    PageManager.RegisterPage(typeof(TestPage));
    
    // 全局异常捕获（可选）
    UnhandledException += App_UnhandledException;
}

private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
{
    // 使用消息服务显示错误
    if (ServiceManager.GetService<IMessageService>() is IMessageService messageService)
    {
        messageService.ShowMessage(e.Message, "发生错误", InfoBarSeverity.Error);
        e.Handled = true;
    }
}
```

### 2. 构建 Shell 页

创建一个包含 `NavigationView` 的 Shell 页（例如 `AppShellPage`），并绑定相关服务。

**AppShellPageViewModel.cs**:

```csharp
public class AppShellPageViewModel : ObservableObject
{
    // 使用 GetService 获取服务新实例
    public INavigationViewService NavigationViewService { get; set; } = ServiceManager.GetService<INavigationViewService>();
    public IMessageService MessageService { get; set; } = ServiceManager.GetService<IMessageService>();
    public ILoadingService LoadingService { get; set; } = ServiceManager.GetService<ILoadingService>();
}
```

**AppShellPage.xaml.cs**:

```csharp
public sealed partial class AppShellPage : Page
{
    public AppShellPageViewModel ViewModel { get; set; } = new();

    public AppShellPage()
    {
        Current = this;
        this.InitializeComponent();

        // 1. 初始化导航服务 (绑定 NavigationView 和 Frame)
        ViewModel.NavigationViewService.Initialize(navigationView, navigationFrame);
        
        // 2. 初始化消息服务 (绑定用于显示消息的 StackPanel)
        ViewModel.MessageService.Initialize(messageStackPanel, DispatcherQueue);
        
        // 3. 初始化加载服务 (绑定 Loading 控件)
        ViewModel.LoadingService.Initialize(loadingGrid, globalLoadingGrid, globalLoadingTextBlock, DispatcherQueue, ViewModel.NavigationViewService.NavigationService);
        
        // 4. 初始导航
        ViewModel.NavigationViewService.NavigateTo<MainPage>();
    }
}
```

### 3. 使用服务

在子页面（如 `MainPage`）的 ViewModel 中，可以通过 `ServiceManager.GetGlobalService<T>()` 获取在 Shell 页已初始化的**全局服务实例**。

```csharp
public partial class MainPageViewModel : ObservableObject
{
    // 获取全局实例 (注意使用 GetGlobalService)
    public INavigationViewService? NavigationViewService { get; } = ServiceManager.GetGlobalService<INavigationViewService>();
    public IMessageService? MessageService { get; } = ServiceManager.GetGlobalService<IMessageService>();

    [RelayCommand]
    void DoSomething()
    {
        // 显示消息
        MessageService?.ShowMessage("操作成功！", "提示", InfoBarSeverity.Success);
        
        // 页面跳转
        NavigationViewService?.NavigateTo<TestPage>("传递的参数");
    }
}
```

## 核心功能

### ServiceManager (服务管理器)

简单的依赖注入及服务定位器。

- `GetService<T>()`: 获取服务实例。如果该服务类型遵循命名约定（如 `IMyService` -> `MyService`），则会自动创建实例。
- `GetGlobalService<T>()`: 获取**已注册**的全局单例服务。通常继承自 `GlobalServiceBase` 的服务在实例化时（如在 Shell 页初始化时）会自动注册为全局单例。

### 导航服务 (INavigationViewService)

用于管理 `NavigationView` 的选中状态与 `Frame` 的页面跳转同步。

- `Initialize(...)`: 必须在使用前调用，绑定 UI 元素。
- `NavigateTo<TPage>(parameter)`: 导航到指定页面。
- `NavigationService.CanGoBack`: 检查是否可后退。

### 消息与对话框

- **IMessageService**: 在界面特定区域显示非阻塞通知。需在 Shell 页的 XAML 中放置一个 `StackPanel` 作为容器。
- **IDialogService**: 显示内容对话框。
- **ILoadingService**: 管理加载状态，支持页面级遮罩和全局遮罩。

## 常用工具类

- **PageManager**: 静态类，用于注册页面类型，使导航系统能通过 Type 找到对应的页面。
- **AppThemeHelper**: 用于管理应用的主题（Light/Dark/System）。
- **NavigationHelper**: 提供了 `SetParameter` 等方法用于页面间参数传递。
