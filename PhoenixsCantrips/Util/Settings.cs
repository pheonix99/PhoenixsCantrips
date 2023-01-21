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
using UnityModManagerNet;
using Menu = ModMenu.ModMenu;

namespace PhoenixsCantrips.Util
{
    internal static class Settings
    {
        private static readonly string RootKey = "phoenixcantrips.settings";
        private static readonly string RootStringKey = "PhoenixCantrips.Settings";
        private static LocalizedString GetString(string key, bool usePrefix = true)
        {
            var fullKey = usePrefix ? $"{RootStringKey}.{key}" : key;
            return LocalizationTool.GetString(fullKey);
        }
        internal static bool IsEnabled(string key)
        {
            return Menu.GetSettingValue<bool>(GetKey(key.ToLower()));
        }
        private static string GetKey(string partialKey)
        {
            return ($"{RootKey}.{partialKey}").ToLower();
        }


        internal static bool IsCharOpPlusEnabled()
        {
            return UnityModManager.modEntries.Where(
                mod => mod.Info.Id.Equals("CharacterOptionsPlus") && mod.Enabled && !mod.ErrorOnLoading)
              .Any();
        }

        internal static bool IsLevelableAivuEnabled()
        {
            return UnityModManager.modEntries.Where(
                mod => mod.Info.Id.Equals("LevelableAivu") && mod.Enabled && !mod.ErrorOnLoading)
              .Any();
        }
        internal static void Init()
        {
            var settingsBuilder = SettingsBuilder.New(RootKey, LocalizationTool.CreateString("PhoenixCantripsSettings.Name", "Pheonix's Cantrips"));
            settingsBuilder.AddDefaultButton(OnDefaultsApplied);
            settingsBuilder.AddToggle(MakeToggle("scaling", "Activate Scaling", true, "Enable Cantrip Scaling."));
            settingsBuilder.AddNewToggle("sonictoarrowsong", "Proliferate: Arrowsong Minstrel", true, "Spread Painful Note from More Cantrips to Arrowsong Minstrel from Character Options Plus. Requires both mods to be enabled");
            settingsBuilder.AddNewToggle("sonictoaivu", "Proliferate: Aivu", true, "Spread Painful Note and Dissonant Touch from More Cantrips to Aivu if Levelable Aivu is installed. Requires both mods to be enabled");
           
            settingsBuilder.AddNewToggle("joltformagus", "Proliferate: Jolt For Magus", true, "Adds Jolt To Magus.");

            settingsBuilder.AddNewToggle("cursesblackened", "Proliferate: Blackened", true, "Adds Firebolt to Blackened if MoreCantrips is enabled");

            settingsBuilder.AddNewToggle("elementalmystery", "Proliferate: Elemental Mysteries", true, "Spread elemental ranged touch cantrips to related oracle mysteries. May not work on mod-added archetypes with custom mystery logic.");

            settingsBuilder.AddNewToggle("elementalpatron", "Proliferate: Elemental Patrons", true, "Spread elemental ranged touch cantrips to related witch patrons.");

            settingsBuilder.AddNewToggle("winterwitch", "Proliferate: Winter Witch", true, "Spreads Ray Of Frost to the Winter Witch PRC.");
            
         
            ModMenu.ModMenu.AddSettings(settingsBuilder);
        }

        private static SettingsBuilder AddNewToggle(this SettingsBuilder settingsBuilder, string keyStub, string name, bool defaultVal, string desc)
        {

            var toggle = Toggle.New($"{RootKey}.{keyStub.ToLower()}", defaultVal, LocalizationTool.CreateString($"{RootStringKey}.{keyStub}", name));
            if (desc != null)
                toggle.WithLongDescription(LocalizationTool.CreateString($"{RootStringKey}.{keyStub}.Desc", desc));
            settingsBuilder.AddToggle(toggle);
            return settingsBuilder;
        }

        private static Toggle MakeToggle(string keyStub, string name, bool defaultVal, string desc)
        {

            var toggle = Toggle.New($"{RootKey}.{keyStub.ToLower()}", defaultVal, LocalizationTool.CreateString($"{RootStringKey}.{keyStub}", name));
            if (desc != null)
                toggle.WithLongDescription(LocalizationTool.CreateString($"{RootStringKey}.{keyStub}.Desc", desc));

            return toggle;
        }

        private static void OnDefaultsApplied()
        {
            Main.Context.Logger.Log($"Default settings restored.");
        }

       
    }
}
