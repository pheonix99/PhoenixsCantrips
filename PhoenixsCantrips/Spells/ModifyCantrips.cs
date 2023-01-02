using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;

namespace PhoenixsCantrips.Spells
{
    class ModifyCantrips
    {
        public static void Do()
        {
            if (!ModMenu.ModMenu.GetSettingValue<bool>("phoenixcantripssettings-master"))
                return;

            ModifyAttackCantrip("0c852a2405dd9f14a8bbcfaf245ff823");//Acid Splash
            ModifyAttackCantrip("8a1992f59e06dd64ab9ba52337bf8cb5");//Divine Zap
            ModifyAttackCantrip("16e23c7a8ae53cc42a93066d19766404");//Jolt
            ModifyAttackCantrip("9af2ab69df6538f4793b2f9c3cc85603");//Ray Of Frost
            ModifyAttackCantrip("652739779aa05504a9ad5db1db6d02ae");//Disrupt Undead
            ModifyVirtue();

            


        }

        private static void ModifyAttackCantrip(string guid)
        {
            var spell = BlueprintTool.Get<BlueprintAbility>(guid);
            var dmg = spell.GetComponent<AbilityEffectRunAction>().Actions.Actions.FirstOrDefault(x => x is ContextActionDealDamage) as ContextActionDealDamage;
            dmg.Value.DiceCountValue.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank;
            dmg.Value.DiceCountValue.ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice;
            
            spell.AddContextRankConfig(x =>
            {
                x.m_Type = Kingmaker.Enums.AbilityRankType.DamageDice;
                x.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                x.m_Progression = ContextRankProgression.OnePlusDiv2;
                x.m_StepLevel = 0;
                x.m_StartLevel = 0;
                x.m_UseMax = true;
                x.m_Max = 6;
            });
            Main.Context.Logger.LogPatch("Patched damage on", spell);
        }

        private static void ModifyVirtue()
        {
            var buff = BlueprintTool.Get<BlueprintBuff>("a13ad2502d9e4904082868eb71efb0c5");
            var temphp = buff.GetComponent<TemporaryHitPointsFromAbilityValue>();
            temphp.Value.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank;
            temphp.Value.ValueRank = Kingmaker.Enums.AbilityRankType.StatBonus;
            buff.AddContextRankConfig(x =>
            {
                x.m_Type = Kingmaker.Enums.AbilityRankType.StatBonus;
                x.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                x.m_Progression = ContextRankProgression.OnePlusDiv2;
                x.m_UseMin = true;
                x.m_Min = 1;
                x.m_UseMax = true;
                x.m_Max = 10;
            });
        }
    }
}
