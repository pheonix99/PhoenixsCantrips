using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixsCantrips.Spells
{
    public class RegisterCantrips
    {

        public static Dictionary<DamageEnergyType, BlueprintAbilityReference> rangedCantrips = new();
        public static Dictionary<DamageEnergyType, BlueprintAbilityReference> meleeCantrips = new();
        public static void Do()
        {
            RegisterRay(DamageEnergyType.Cold, "9af2ab69df6538f4793b2f9c3cc85603");
            RegisterRay(DamageEnergyType.Acid, "0c852a2405dd9f14a8bbcfaf245ff823");
            RegisterRay(DamageEnergyType.Electricity, "16e23c7a8ae53cc42a93066d19766404");

            BlueprintTool.AddGuidsByName(("RayOfFrost", "9af2ab69df6538f4793b2f9c3cc85603"));
            BlueprintTool.AddGuidsByName(("AcidSplash", "0c852a2405dd9f14a8bbcfaf245ff823"));
            BlueprintTool.AddGuidsByName(("Jolt", "16e23c7a8ae53cc42a93066d19766404"));
            BlueprintTool.AddGuidsByName(("DivineZap", "8a1992f59e06dd64ab9ba52337bf8cb5"));
            BlueprintTool.AddGuidsByName(("DisruptUndead", "652739779aa05504a9ad5db1db6d02ae"));


        }

        public static void RegisterTouch(DamageEnergyType damage, string guid)
        {
            if (!meleeCantrips.ContainsKey(damage))
            {
                var refernece = BlueprintTool.GetRef<BlueprintAbilityReference>(guid);
                Main.Context.Logger.Log($"Registered {refernece.NameSafe()} as {damage} melee touch");
                meleeCantrips.Add(damage, refernece);
            }
        }
        public static void RegisterRay(DamageEnergyType damage, string guid)
        {
            if (!rangedCantrips.ContainsKey(damage))
            {
                var refernece = BlueprintTool.GetRef<BlueprintAbilityReference>(guid);
                Main.Context.Logger.Log($"Registered {refernece.NameSafe()} as {damage} ranged touch");
                rangedCantrips.Add(damage, refernece);
            }
        }
    }
}
