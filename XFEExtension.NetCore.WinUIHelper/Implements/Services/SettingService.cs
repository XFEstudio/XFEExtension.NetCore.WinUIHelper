﻿using XFEExtension.NetCore.WinUIHelper.Interface;
using XFEExtension.NetCore.WinUIHelper.Interface.Services;
using XFEExtension.NetCore.WinUIHelper.Model;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

namespace XFEExtension.NetCore.WinUIHelper.Implements.Services;

/// <inheritdoc cref="ISettingService"/>
internal class SettingService : GlobalServiceBase, ISettingService
{
    private readonly Dictionary<object, IProfileInfoEntry> settingControls = [];

    public Dictionary<object, IProfileInfoEntry> SettingControls => settingControls;

    public void AddComboBox(ComboBox comboBox, Func<string, object?> saveFunc, Func<List<object>, object?, object?> loadFunc)
    {
        if (comboBox.Tag is string profilePath)
            settingControls.Add(comboBox, new ComboBoxProfileInfoEntry(profilePath, saveFunc, loadFunc));
        else
            throw new NullReferenceException();
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (settingControls.TryGetValue(sender, out var profileInfo) && profileInfo is ComboBoxProfileInfoEntry comboBoxProfileInfo && e.AddedItems.Count > 0 && e.AddedItems[0] is ComboBoxItem comboBoxItem && comboBoxItem.Tag is string value)
            ProfileHelper.SetProfileValue(comboBoxProfileInfo.ProfilePath, comboBoxProfileInfo.ProfileSaveFunc(value));
    }

    public void Initialize()
    {
        foreach (var item in settingControls)
        {
            if (item.Key is ComboBox comboBox && item.Value is ComboBoxProfileInfoEntry comboBoxProfileInfo)
                comboBox.SelectedItem = comboBoxProfileInfo.ProfileLoadFunc([.. comboBox.Items], ProfileHelper.GetProfileValue(comboBoxProfileInfo.ProfilePath));
        }
    }

    public void RegisterEvents()
    {
        foreach (var comboBox in settingControls.Keys.OfType<ComboBox>())
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
    }
}
