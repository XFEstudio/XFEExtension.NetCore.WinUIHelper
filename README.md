# XFEExtension.NetCore.WinUIHelper

[![NuGet Version](https://img.shields.io/nuget/v/XFEExtension.NetCore.WinUIHelper?style=flat-square&logo=nuget)](https://www.nuget.org/packages/XFEExtension.NetCore.WinUIHelper)
[![NuGet Downloads](https://img.shields.io/nuget/dt/XFEExtension.NetCore.WinUIHelper?style=flat-square&logo=nuget)](https://www.nuget.org/packages/XFEExtension.NetCore.WinUIHelper)
[![License: MIT](https://img.shields.io/badge/license-MIT-green?style=flat-square)](LICENSE.txt)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)

> 📖 [中文文档](https://github.com/XFEExtension/XFEExtension.NetCore.WinUIHelper/blob/main/README.zh-CN.md)

A WinUI 3 extension library based on .NET 8, providing a set of convenient helper classes, services, and extension methods to streamline WinUI 3 application development.

## Table of Contents

- [Features](#features)
- [Quick Start](#quick-start)
  - [1. Initialization](#1-initialization)
  - [2. Build the Shell Page](#2-build-the-shell-page)
  - [3. Using Services](#3-using-services)
- [Core Features](#core-features)
  - [ServiceManager](#servicemanager)
  - [Navigation Service](#navigation-service)
  - [Messages and Dialogs](#messages-and-dialogs)
- [Utility Classes](#utility-classes)

## Features

- **Lightweight IoC Container**: Built-in `ServiceManager` supporting service registration and global singleton retrieval.
- **Navigation Management**: Wraps `NavigationView` and `Frame` to provide a ViewModel-driven navigation experience.
- **UI Interaction Services**: Offers `DialogService`, `MessageService` (similar to InfoBar), `LoadingService`, and other common interaction services.
- **MVVM Support**: Provides `ObservableObject` extensions and a general-purpose ViewModel base class.

## Quick Start

### 1. Initialization

Initialize in `App.xaml.cs`, including page registration and exception handling setup.

```csharp
public App()
{
    this.InitializeComponent();
    
    // Set application theme
    AppThemeHelper.Theme = ElementTheme.Dark; 

    // Register navigation pages (PageManager)
    // All pages navigable by string or type must be registered here
    PageManager.RegisterPage(typeof(AppShellPage));
    PageManager.RegisterPage(typeof(MainPage));
    PageManager.RegisterPage(typeof(TestPage));
    
    // Global exception handling (optional)
    UnhandledException += App_UnhandledException;
}

private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
{
    // Display error using the message service
    if (ServiceManager.GetService<IMessageService>() is IMessageService messageService)
    {
        messageService.ShowMessage(e.Message, "An error occurred", InfoBarSeverity.Error);
        e.Handled = true;
    }
}
```

### 2. Build the Shell Page

Create a Shell page (e.g. `AppShellPage`) containing a `NavigationView` and bind the relevant services.

**AppShellPageViewModel.cs**:

```csharp
public class AppShellPageViewModel : ObservableObject
{
    // Use GetService to obtain a new service instance
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

        // 1. Initialize navigation service (bind NavigationView and Frame)
        ViewModel.NavigationViewService.Initialize(navigationView, navigationFrame);
        
        // 2. Initialize message service (bind the StackPanel used to display messages)
        ViewModel.MessageService.Initialize(messageStackPanel, DispatcherQueue);
        
        // 3. Initialize loading service (bind loading controls)
        ViewModel.LoadingService.Initialize(loadingGrid, globalLoadingGrid, globalLoadingTextBlock, DispatcherQueue, ViewModel.NavigationViewService.NavigationService);
        
        // 4. Initial navigation
        ViewModel.NavigationViewService.NavigateTo<MainPage>();
    }
}
```

### 3. Using Services

In a child page's ViewModel (e.g. `MainPage`), use `ServiceManager.GetGlobalService<T>()` to retrieve the **global service instance** that was initialized in the Shell page.

```csharp
public partial class MainPageViewModel : ObservableObject
{
    // Retrieve global instances (use GetGlobalService)
    public INavigationViewService? NavigationViewService { get; } = ServiceManager.GetGlobalService<INavigationViewService>();
    public IMessageService? MessageService { get; } = ServiceManager.GetGlobalService<IMessageService>();

    [RelayCommand]
    void DoSomething()
    {
        // Show a message
        MessageService?.ShowMessage("Operation successful!", "Info", InfoBarSeverity.Success);
        
        // Navigate to another page
        NavigationViewService?.NavigateTo<TestPage>("parameter to pass");
    }
}
```

## Core Features

### ServiceManager

A simple dependency injection and service locator.

- `GetService<T>()`: Gets a service instance. If the type follows the naming convention (e.g. `IMyService` → `MyService`), an instance is created automatically.
- `GetGlobalService<T>()`: Gets a **registered** global singleton service. Services inheriting from `GlobalServiceBase` automatically register themselves as global singletons when instantiated (e.g. during Shell page initialization).

### Navigation Service

Manages `NavigationView` selection state and `Frame` page transition synchronization via `INavigationViewService`.

- `Initialize(...)`: Must be called before use to bind UI elements.
- `NavigateTo<TPage>(parameter)`: Navigates to the specified page.
- `NavigationService.CanGoBack`: Checks whether back navigation is available.

### Messages and Dialogs

- **IMessageService**: Displays non-blocking notifications in a designated area of the UI. Requires a `StackPanel` container placed in the Shell page's XAML.
- **IDialogService**: Displays content dialogs.
- **ILoadingService**: Manages loading states, supporting both page-level overlays and global overlays.

## Utility Classes

- **PageManager**: A static class used to register page types so the navigation system can locate them by `Type`.
- **AppThemeHelper**: Manages the application theme (Light / Dark / System).
- **NavigationHelper**: Provides methods such as `SetParameter` for passing parameters between pages.
