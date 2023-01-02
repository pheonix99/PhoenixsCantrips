using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixsCantrips
{
    class BlueprintPatchHook
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch
        {

            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                try
                {
                    if (Initialized) return;
                    Initialized = true;
                }
                catch (Exception e)
                {
                    Main.Context.Logger.LogError(e, $"Error caught in early patch");
                }
            }

        }
    }

}
