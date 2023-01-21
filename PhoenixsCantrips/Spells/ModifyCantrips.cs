using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;

using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using PhoenixsCantrips.Util;
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
            if (!Settings.IsEnabled("scaling"))
                return;
           foreach(var v in RegisterCantrips.rangedCantrips)
            {
                ModifyAttackCantrip(v.Value);
            }
           foreach(var v in RegisterCantrips.meleeCantrips)
            {
                ModifyAttackCantrip(v.Value);
            }

         

            ModifyAttackCantrip(BlueprintTool.GetRef<BlueprintAbilityReference>("DivineZap"));//Divine Zap
            ModifyAttackCantrip(BlueprintTool.GetRef<BlueprintAbilityReference>("DisruptUndead"));//Disrupt Undead
            ModifyVirtue();
            // plus another die per two caster levels past first (maximum 6 dice).
            SetDesc("DivineZap", "You unleash your divine powers against a single target. The target takes {g|Encyclopedia:Dice}1d3{/g} points of divine {g|Encyclopedia:Damage}damage{/g}, plus another die per two caster levels past first (maximum 6d3). A successful {g|Encyclopedia:Saving_Throw}save{/g} halves the damage.");//Divine Zap
            SetDesc("Jolt", "You cause a spark of electricity to strike the target with a successful ranged {g|Encyclopedia:TouchAttack}touch attack{/g}. The {g|Encyclopedia:Spell}spell{/g} deals {g|Encyclopedia:Dice}1d3{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g}, plus another die per two caster levels past first (maximum 6d3).");//Jolt
            SetDesc("RayOfFrost", "A ray of freezing air and ice projects from your pointing finger. You must succeed on a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} with the ray to deal {g|Encyclopedia:Damage}damage{/g} to a target. The ray deals {g|Encyclopedia:Dice}1d3{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g}, plus another die per two caster levels past first (maximum 6d3).");//Jolt
            SetDesc("AcidSplash", "You fire a small orb of acid at the target. You must succeed on a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} to hit your target. The orb deals 1–3 ({g|Encyclopedia:Dice}1d3{/g}) points of {g|Encyclopedia:Energy_Damage}acid damage{/g}, plus another die per two caster levels past first (maximum 6d3).");//Acit Splash
            SetDesc("DisruptUndead", "You direct a ray of positive energy. You must make a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} to hit, and if the ray hits an undead creature, it deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} to it, plus another die per two caster levels past first (maximum 6d6)");//Jolt
            SetDesc("d3a852385ba4cd740992d1970170301a", "With a {g|Encyclopedia:TouchAttack}touch{/g}, you infuse a creature with a tiny surge of life, granting the subject 1 temporary hit point, plus another per two caster levels after first to a maximum of 10");//Jolt
            SetBuffDesc("a13ad2502d9e4904082868eb71efb0c5", "With a {g|Encyclopedia:TouchAttack}touch{/g}, you infuse a creature with a tiny surge of life, granting the subject 1 temporary hit point, plus another per two caster levels after first to a maximum of 10");//Jolt


        }

        private static void SetDesc(string guid, string newDesc)
        {
            var spell = BlueprintTool.Get<BlueprintAbility>(guid);
            
            AbilityConfigurator.For(spell).SetDescription(LocalizationTool.CreateString(spell.name + "Phoenix.Desc", newDesc, false)).Configure();
            Main.Context.Logger.LogPatch("Patched description on", spell);
        }

        private static void SetBuffDesc(string guid, string newDesc)
        {
            var spell = BlueprintTool.Get<BlueprintBuff>(guid);
            

            BuffConfigurator.For(spell).SetDescription(LocalizationTool.CreateString(spell.name + "Phoenix.Desc", newDesc, false)).Configure();
            Main.Context.Logger.LogPatch("Patched description on", spell);
        }

        private static void ModifyAttackCantrip(BlueprintAbilityReference reference)
        {


            var spell = reference.Get();
            AbilityEffectStickyTouch sticky = spell.GetComponent<AbilityEffectStickyTouch>();
            if ( sticky != null)
            {
                spell = sticky.TouchDeliveryAbility;
            }

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
            Main.Context.Logger.LogPatch("Patched scaling on", buff);
        }
    }
}
