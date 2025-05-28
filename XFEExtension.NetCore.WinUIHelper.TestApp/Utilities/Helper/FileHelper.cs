namespace XFEExtension.NetCore.WinUIHelper.TestApp.Utilities.Helper
{
    public static class FileHelper
    {
        public static string[] RootPath { get; set; } = [@"C:\", @"D:\", @"E:\", @"F:\", @"G:\", @"H:\", @"I:\", @"J:\", @"K:\", @"L:\", @"M:\", @"N:\", @"O:\", @"P:\", @"Q:\", @"R:\", @"S:\", @"T:\", @"U:\", @"V:\", @"W:\", @"X:\", @"Y:\", @"Z:\",];
        public static long GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;
            FileInfo[] files = directoryInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                size += file.Length;
            }
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                size += GetDirectorySize(directory);
            }
            return size;
        }

        public static bool IsRootPath(string path) => RootPath.Any(rootPath => rootPath == path);
    }
}
