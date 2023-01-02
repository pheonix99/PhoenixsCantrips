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
            settingsBuilder.AddToggle(Toggle.New("phoenixcantripssettings-proliferate-elementalmystery", true, LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-ElementalMystery.Name", "Proliferate: Mysteries")).WithLongDescription(LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-ElementalMystery.Desc", "Spread elemental ranged touch cantrips to related oracle mysteries. May not work on mod-added archetypes with custom mystery logic.")));
            settingsBuilder.AddToggle(Toggle.New("phoenixcantripssettings-proliferate-winterpatron", true, LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-WinterPatron.Name", "Proliferate: Winter Patron")).WithLongDescription(LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-WinterPatron.Desc", "Spread Ray Of Frost to Witch's Winter Patron")));
            //settingsBuilder.AddToggle(Toggle.New("phoenixcantripssettings-proliferate-shamanspirits", true, LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-ShamanSpirits.Name", "Spread Elemental Damage Cantrips To Elemental Shaman Spirits")));
            settingsBuilder.AddToggle(Toggle.New("phoenixcantripssettings-proliferate-winterwitchprc", true, LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-WinterWitchPRC.Name", "Proliferate: Winter Witch")).WithLongDescription(LocalizationTool.CreateString("PhoenixCantripsSettings-Proliferate-WinterWitchPRC.Desc", "Spread Ray Of Frost to the Winter Witch prestige class")));
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
