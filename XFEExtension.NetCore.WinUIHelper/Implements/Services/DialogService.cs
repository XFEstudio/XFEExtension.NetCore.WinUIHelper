using XFEExtension.NetCore.WinUIHelper.Interface.Services;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

class DialogService : IDialogService
{
    private readonly Dictionary<string, ContentDialog> dialogDictionary = [];
    public Dictionary<string, ContentDialog> DialogDictionary => dialogDictionary;
}
