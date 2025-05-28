﻿namespace XFEExtension.NetCore.WinUIHelper.TestApp.Utilities.Helper
{
    public static class WindowHelper
    {
        public static IntPtr GetHwndForCurrentWindow()
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            return hwnd;
        }
    }
}
