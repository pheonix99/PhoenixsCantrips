using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Localization;
using ModMenu.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Menu = ModMenu.ModMenu;

namespace PhoenixsCantrips.Util
{
    internal static class Settings
    {
        private static readonly string RootKey = "phoenixcantrips.settings";
        private static readonly string RootStringKey = "PhoenixCantrips.Settings";
        internal static void Init()
        {
            ModMenu.ModMenu.AddSettings(SettingsBuilder.New("phoenixcantripssettings", LocalizationTool.CreateString("PhoenixCantripsSettings.Name", "Pheonix's Cantrips")).AddToggle(Toggle.New("phoenixcantripssettings-master", true, LocalizationTool.CreateString("PhoenixCantripsSettings-Master.Name", "Enable Mod Functions"))));
        }

        private static void OnDefaultsApplied()
        {
            Main.Context.Logger.Log($"Default settings restored.");
        }

        private static LocalizedString GetString(string key, bool usePrefix = true)
        {
            var fullKey = usePrefix ? $"{RootStringKey}.{key}" : key;
            return LocalizationTool.GetString(fullKey);
        }

        private static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}";
        }
    }
}
