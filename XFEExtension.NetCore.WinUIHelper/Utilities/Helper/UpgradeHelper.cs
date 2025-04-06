using System.Diagnostics;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

public static class UpgradeHelper
{
    public static void StartUpdate()
    {
        var startInfo = new ProcessStartInfo("PDDShopManagementSystem.Installer.exe")
        {
            UseShellExecute = true,
            Verb = "runas"
        };
        startInfo.ArgumentList.Add("Upgrade");
        startInfo.ArgumentList.Add(UserManager.RequestAddress);
        startInfo.ArgumentList.Add("");
        Process.Start(startInfo);
        Application.Current.Exit();
    }
}
