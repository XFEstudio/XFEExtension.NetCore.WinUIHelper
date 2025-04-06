using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Media;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

class LoadingService : GlobalServiceBase, ILoadingService
{
    private Grid? _loadingGrid;
    private Grid? _globalLoadingGrid;
    private TextBlock? _globalLoadingTextBox;
    private DispatcherQueue? _dispatcherQueue;
    private IAutoNavigationService? _navigationService;
    private readonly Dictionary<Type, Grid> pageGridDictionary = [];

    public IAutoNavigationService? NavigationService => _navigationService;

    public void Initialize(Grid loadingGrid, Grid globalLoadingGrid, TextBlock globalLoadingTextBox, DispatcherQueue dispatcherQueue, IAutoNavigationService navigationService)
    {
        _loadingGrid = loadingGrid;
        _globalLoadingGrid = globalLoadingGrid;
        _globalLoadingTextBox = globalLoadingTextBox;
        _dispatcherQueue = dispatcherQueue;
        _navigationService = navigationService;
        _navigationService.Navigated += NavigationService_Navigated;
    }

    private void NavigationService_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
    {
        foreach (var grid in pageGridDictionary.Values)
            grid.Visibility = Visibility.Collapsed;
        if (pageGridDictionary.TryGetValue(e.SourcePageType, out var currentGrid))
            _dispatcherQueue?.TryEnqueue(() => currentGrid.Visibility = Visibility.Visible);
    }

    public bool StartLoading<T>(string showText = "Loading...") where T : Page
    {
        try
        {
            if (pageGridDictionary.TryGetValue(typeof(T), out var grid) && grid.FindName("loadingTextBlock") is TextBlock textBlock)
            {
                textBlock.Text = showText;
                return true;
            }
            else
            {
                var newGrid = CreateLoadingGrid(showText);
                if (NavigationService?.Frame is not null && NavigationService.Frame.CurrentSourcePageType == typeof(T))
                    newGrid.Visibility = Visibility.Visible;
                _loadingGrid?.Children.Add(newGrid);
                pageGridDictionary.Add(typeof(T), newGrid);
                return false;
            }
        }
        catch
        {
            if (pageGridDictionary.TryGetValue(typeof(T), out var grid) && grid.FindName("loadingTextBlock") is TextBlock textBlock)
            {
                _dispatcherQueue?.TryEnqueue(() => textBlock.Text = showText);
                return true;
            }
            else
            {
                _dispatcherQueue?.TryEnqueue(() =>
                {
                    var newGrid = CreateLoadingGrid(showText);
                    if (NavigationService?.Frame is not null && NavigationService.Frame.CurrentSourcePageType == typeof(T))
                        newGrid.Visibility = Visibility.Visible;
                    _loadingGrid?.Children.Add(newGrid);
                    pageGridDictionary.Add(typeof(T), newGrid);
                });
                return false;
            }
        }
    }

    public bool StopLoading<T>() where T : Page
    {
        try
        {
            if (pageGridDictionary.TryGetValue(typeof(T), out var grid) && _loadingGrid is not null)
            {
                return _loadingGrid.Children.Remove(grid) && pageGridDictionary.Remove(typeof(T));
            }
            return false;
        }
        catch
        {
            if (pageGridDictionary.TryGetValue(typeof(T), out var grid) && _loadingGrid is not null)
            {
                return _dispatcherQueue?.TryEnqueue(() =>
                {
                    _loadingGrid.Children.Remove(grid);
                    pageGridDictionary.Remove(typeof(T));
                }) ?? false;
            }
            return false;
        }
    }

    public void StartLoading(string showText = "Loading...")
    {
        if (_globalLoadingGrid is not null && _globalLoadingTextBox is not null)
        {
            _globalLoadingTextBox.Text = showText;
            _globalLoadingGrid.Visibility = Visibility.Visible;
        }
    }

    public void StopLoading()
    {
        if (_globalLoadingGrid is not null)
            _globalLoadingGrid.Visibility = Visibility.Collapsed;
    }

    private static Grid CreateLoadingGrid(string loadingText)
    {
        var loadingStackPanel = new StackPanel
        {
            Spacing = 30
        };
        loadingStackPanel.Children.Add(new TextBlock
        {
            Foreground = new SolidColorBrush(Colors.White),
            FontSize = 34,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Name = "loadingTextBlock",
            Text = loadingText
        });
        loadingStackPanel.Children.Add(new ProgressRing
        {
            IsActive = true,
            Width = 60,
            Height = 60,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        });
        var loadingGrid = new Grid
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            CornerRadius = new(15),
            Padding = new(100),
            Background = new SolidColorBrush(Colors.Black)
            {
                Opacity = 0.5
            }
        };
        loadingGrid.Children.Add(loadingStackPanel);
        var containerGrid = new Grid
        {
            Margin = new(0, -72, 0, 0),
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Visibility = Visibility.Collapsed,
            Background = (Brush)Application.Current.Resources["SmokeFillColorDefaultBrush"]
        };
        containerGrid.Children.Add(loadingGrid);
        return containerGrid;
    }
}
