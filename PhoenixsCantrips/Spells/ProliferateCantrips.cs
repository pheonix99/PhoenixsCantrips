using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.FactLogic;
using PhoenixsCantrips.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TabletopTweaks.Core.Utilities;

namespace PhoenixsCantrips.Spells
{
    class ProliferateCantrips
    {
       
        static string oracleGUID = "20ce9bf8af32bee4c8557a045ab499b1";

        


        public static void Proliferate()
        {
          
           



            ProliferateOracle();
            ProliferatePatrons();
            JoltForMagus();
            ProliferateSonic();
            ProliferateCurse();
            ProliferateWinterWitch();
            //ProliferateShaman();
        }

        private static void ProliferateSonic()
        {
            if (!Settings.MoreCantripsEnabled())
                return;

            string arroswongGuid = "74f9286f6e8e46ba8de8995759759b2f";
            string aivuGUID = "5972da2ee7724377b85fc91363c4b6c8";
            if (RegisterCantrips.rangedCantrips.ContainsKey(DamageEnergyType.Sonic) && Settings.IsCharOpPlusEnabled() && Settings.IsEnabled("sonictoarrowsong") )
            {
                FeatureConfigurator.For("23d3d21793cc4ae6a034860c89561253").AddKnownSpell("b704d577abe54873b9228f56c2319b54", "772c83a25e2268e448e841dcd548235f", RegisterCantrips.rangedCantrips[DamageEnergyType.Sonic], 0).Configure(delayed:true);

                //AbilityConfigurator.For(RegisterCantrips.rangedCantrips[DamageEnergyType.Sonic]).AddToSpellList(0, arroswongGuid, true).Configure();
                Main.Context.Logger.Log($"Patched {RegisterCantrips.rangedCantrips[DamageEnergyType.Sonic].NameSafe()} onto arrowsong");
            }
            if (RegisterCantrips.rangedCantrips.ContainsKey(DamageEnergyType.Sonic) && Settings.IsLevelableAivuEnabled() && Settings.IsEnabled("sonictoaivu"))
            {
                AbilityConfigurator.For(RegisterCantrips.rangedCantrips[DamageEnergyType.Sonic]).AddToSpellList(0, aivuGUID, true).Configure();
                Main.Context.Logger.Log($"Patched {RegisterCantrips.rangedCantrips[DamageEnergyType.Sonic].NameSafe()} onto aivu");
            }
            if (RegisterCantrips.meleeCantrips.ContainsKey(DamageEnergyType.Sonic) && Settings.IsLevelableAivuEnabled() && Settings.IsEnabled("sonictoaivu"))
            {
                AbilityConfigurator.For(RegisterCantrips.meleeCantrips[DamageEnergyType.Sonic]).AddToSpellList(0, aivuGUID, true).Configure();
                Main.Context.Logger.Log($"Patched {RegisterCantrips.meleeCantrips[DamageEnergyType.Sonic].NameSafe()} onto aivu");
            }
           

        }


        private static void ProliferateShaman()
        {
            if (ModMenu.ModMenu.GetSettingValue<bool>("phoenixcantripssettings-proliferate-shamanspirits"))
            {
                AddToSpellList("RayOfFrost", "bbae401660bbad94c865d71029d8439e");
                AddToSpellList("AcidSplash", "87a3e296757412e45910493e5fed1417");
                AddToSpellList("Jolt", "0bf6f90fdcb864b4486344100391b478");
            }

        }

        private static void JoltForMagus()
        {
            if (Settings.IsEnabled("joltformagus"))
            {
                AbilityConfigurator.For("Jolt").AddToSpellLists(0, SpellList.Magus).Configure();
                Main.Context.Logger.Log("Patched Jolt onto Magus");
            }
        }

        private static void AddToSpellList(string spell, string spellList)
        {
            SpellListConfigurator.For(spellList).ModifySpellsByLevel(x =>
            {
                if (x.SpellLevel == 0)
                {
                    if (x.m_Spells == null)
                        x.m_Spells = new();
                    x.m_Spells.Add(BlueprintTool.GetRef<BlueprintAbilityReference>(spell));
                    Main.Context.Logger.Log($"Patched {spell} onto spell list");
                }

            }).Configure();
        }

        private static void ProliferateWinterWitch()
        {
            if (Settings.IsEnabled("winterwitch"))
            {
                var spellbookselecter = BlueprintTool.Get<BlueprintFeatureSelection>("ea20b26d9d0ede540af3c74246dade41");
                if (spellbookselecter == null)
                    return;
                foreach (var feature in spellbookselecter.m_AllFeatures)
                {
                    BlueprintFeature pog = feature.Get();
                    if (pog != null)
                    {
                        var classRequirement = pog.GetComponent<PrerequisiteClassSpellLevel>(x => true);
                        if (classRequirement != null)
                        {
                            FeatureConfigurator.For(pog).AddKnownSpell(characterClass: classRequirement.m_CharacterClass, spell: "RayOfFrost", spellLevel: 0).Configure();
                            Main.Context.Logger.LogPatch($"Patched Ray Of Frost onto Winter Witch", pog);
                        }
                    }
                }
            }
        }

        private static void ProliferateCurse()
        {
            if (Settings.IsEnabled("cursesblackened") && RegisterCantrips.rangedCantrips.ContainsKey(DamageEnergyType.Fire) && Settings.MoreCantripsEnabled())
            {

                ProgressionConfigurator.For("ca1e4627a1ad8fc4f83158f6497d5c54").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: RegisterCantrips.rangedCantrips[DamageEnergyType.Fire], spellLevel: 0).Configure();
                ProgressionConfigurator.For("ca1e4627a1ad8fc4f83158f6497d5c54").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc",  archetype: "c5f6e53e71059fb4d802ce81a277a12d", spell: RegisterCantrips.rangedCantrips[DamageEnergyType.Fire], spellLevel: 0).Configure();
                Main.Context.Logger.Log($"Patched Firebolt onto Blackened Curse");
            }

        }

        private static void ProliferatePatrons()
        {
            //TODO improve to use the setup from oracle
            if (Settings.IsEnabled("elementalpatron"))
            {
                ProgressionConfigurator.For("e98d8d9f907c1814aa7376d6cdaac012").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: "RayOfFrost", spellLevel: 0).Configure();
                Main.Context.Logger.Log($"Patched Ray Of Frost onto Winter Patron");
            }
        }
        private static void ProliferateOracle()
        {
            if (!Settings.IsEnabled("ElementalMystery"))
                return;

            Dictionary<string, DamageEnergyType> SpellsFeatureToGroupPairing = new();
           
          
                SpellsFeatureToGroupPairing.Add("9a70e449c1f5c7548ab210a40c5f1890", DamageEnergyType.Cold);
                SpellsFeatureToGroupPairing.Add("efe346f6fec1ea84d84daa9eefdef204", DamageEnergyType.Fire);
                SpellsFeatureToGroupPairing.Add("f482b5b69aaab72489d1f0da74743106", DamageEnergyType.Electricity);
                SpellsFeatureToGroupPairing.Add("210fd7d1314eabb45b8b51b41937d315", DamageEnergyType.Acid);
         
            List<string> OracleMysterySelectors = new() { "5531b975dcdf0e24c98f1ff7e017e741", "c11ff5dbd8518c941849b3112d4d6b68", "9d5fdd3b4a6cd4f40beddbc72b2c07a0" };
            List<string> patched = new();
            foreach (string s in OracleMysterySelectors)
            {
                BlueprintFeatureSelection selection = BlueprintTool.Get<BlueprintFeatureSelection>(s);
                foreach (var f in selection.m_AllFeatures)
                {
                    BlueprintFeature mystery = f.Get();
                    if (patched.Contains(mystery.AssetGuid.ToString()))
                        continue;
                    var l2 = mystery.GetComponent<AddFeatureOnClassLevel>(x => x.Level == 2);
                    if (l2 != null)
                    {
                        bool patchedThis = false;
                        if (SpellsFeatureToGroupPairing.TryGetValue(l2.m_Feature.deserializedGuid.ToString().Replace("-", ""), out var groupkey))
                        {
                            if (RegisterCantrips.rangedCantrips.TryGetValue(groupkey, out var spells))
                            {
                                
                                    FeatureConfigurator.For(mystery).AddKnownSpell(characterClass: oracleGUID, spell: spells, spellLevel: 0).Configure();
                                    Main.Context.Logger.LogPatch($"Patched {spells.NameSafe()} onto mystery", mystery);
                                    patchedThis = true;
                                
                            }
                        }
                        if (patchedThis)
                            patched.Add(mystery.AssetGuid.ToString());
                    }
                    

                }

            }
        }

        private static void AddToOracleMystery(string mysteryFeature, string spell)
        {
            FeatureConfigurator.For(mysteryFeature).AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: spell, spellLevel: 0).Configure();
        }
    }
}
