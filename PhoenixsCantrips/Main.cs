using BlueprintCore.Utils;
using BlueprintCore.Blueprints.Configurators.Root;
using HarmonyLib;
using Kingmaker.PubSubSystem;
using ModMenu.Settings;
using PhoenixsCantrips.ModContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.NewEvents;
using TabletopTweaks.Core.Utilities;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager.ModEntry;
using PhoenixsCantrips.Util;
using PhoenixsCantrips.Spells;

namespace PhoenixsCantrips
{
    static class Main
    {
        public static bool Enabled;
        public static CantripsContext Context;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Enabled = true;

                Harmony harmony = new Harmony(modEntry.Info.Id);
                Context = new CantripsContext(modEntry);

                EventBus.Subscribe(new BlueprintCacheInitHandler());
#if DEBUG

#endif

                harmony.PatchAll();

                

                return true;
            }
            catch (Exception e)
            {
                Main.Context.Logger.LogError(e, e.Message);
                return false;
            }
        }

        class BlueprintCacheInitHandler : IBlueprintCacheInitHandler
        {
            private static bool Initialized = false;
            private static bool InitializeDelayed = false;

            public void AfterBlueprintCacheInit()
            {
                try
                {
                    if (Initialized)
                    {
                        Main.Context.Logger.Log("Already initialized blueprints cache.");
                        return;
                    }
                    Initialized = true;
                   
                    // First strings
                

                    // Then settings
                    Settings.Init();
                    RegisterCantrips.Do();
                    ModifySpells();

                    ModifySpellcasters();
                }
                catch (Exception e)
                {
                    Main.Context.Logger.LogError(e,"Failed to initialize.");
                }
            }

            public void AfterBlueprintCachePatches()
            {
                try
                {
                    if (InitializeDelayed)
                    {
                        Context.Logger.Log("Already initialized blueprints cache.");
                        return;
                    }
                    InitializeDelayed = true;

                    //ConfigureFeatsDelayed();

                    RootConfigurator.ConfigureDelayedBlueprints();
                }
                catch (Exception e)
                {
                    Context.Logger.LogError(e, "Delayed blueprint configuration failed.");
                }
            }

            public void BeforeBlueprintCacheInit()
            {
                
            }

            public void BeforeBlueprintCachePatches()
            {
               
            }
        }


        private static void ModifySpells()
        {
            ModifyCantrips.Do();

        }

        private static void ModifySpellcasters()
        {
            ProliferateCantrips.Proliferate();
        }
            
    }
}
