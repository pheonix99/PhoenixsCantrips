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
            var settingsBuilder = SettingsBuilder.New(RootKey, LocalizationTool.CreateString("PhoenixCantripsSettings.Name", "Pheonix's Cantrips"));
            settingsBuilder.AddToggle(Toggle.New("phoenixcantripssettings-master", true, LocalizationTool.CreateString("PhoenixCantripsSettings-Master.Name", "Enable Mod Functions")));
            settingsBuilder.AddToggle(Toggle.New("phoenixcantripssettings-proliferate-elementalmystery", true, LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-ElementalMystery.Name", "Spread Elemental Damage Cantrips To Elemental Oracle Mysteries")));
            settingsBuilder.AddToggle(Toggle.New("phoenixcantripssettings-proliferate-winterpatron", true, LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-WinterPatron.Name", "Spread Ray Of Frost to Winter Witch Patron")));
            settingsBuilder.AddToggle(Toggle.New("phoenixcantripssettings-proliferate-winterwitchprc", true, LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-WinterWitchPRC.Name", "Spread Ray Of Frost to Winter Witch Prestige Class")));
            ModMenu.ModMenu.AddSettings(settingsBuilder);
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
