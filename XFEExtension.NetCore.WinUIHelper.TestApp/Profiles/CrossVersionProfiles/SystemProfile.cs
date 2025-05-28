﻿using XFEExtension.NetCore.AutoConfig;
using XFEExtension.NetCore.WinUIHelper.Utilities.Helper;

namespace XFEExtension.NetCore.WinUIHelper.TestApp.Profiles.CrossVersionProfiles
{
    public partial class SystemProfile : XFEProfile
    {
        public SystemProfile() => ProfilePath = $@"{AppPathHelper.LocalProfile}\{nameof(SystemProfile)}";

        /// <summary>
        /// 主题颜色
        /// </summary>
        [ProfileProperty]
        private ElementTheme theme = ElementTheme.Default;
        /// <summary>
        /// 是否自动启动
        /// </summary>
        [ProfileProperty]
        private bool autoStart = false;

        static partial void SetThemeProperty(ref ElementTheme value) => AppThemeHelper.ChangeTheme(value);
    }
}
