using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using System;
using static AutomaticBonusProgression.BonusProgression;

namespace AutomaticBonusProgression
{
  /// <summary>
  /// Collection of patches removing enhancement bonuses from items.
  /// </summary>
  internal class BonusPatches
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BonusPatches));

    // TODO: Need to handle features that muck w/ enhancement bonuses e.g. AddWeaponEnhancementBonusToStat
    // - GameHelper.GetArmorEnhancementBonus / ItemEnhancementBonus
    // - BlueprintItemArmor.IsMagic For this to work I need to not remove ArmorEnhancementBonus / WeaponEnhancementBonus

    // TODO: Amulet of Fists use UnarmedEnhancementX which uses EquipmentWeaponTypeEnhancement. Because of what this is
    // used for I shouldn't patch--instead I should replace the UnarmedEnhancement1 buffs w/ new components.
    // TODO: Similarly, WeaponEnhancementBonus should not be done via patch but by updating EnhancementX
    // TODO: Update PerfectStormFeature to support new logic
    // TODO: Similarly, ArmorEnhancementBonus should not be done via patch but by updating ArmorEnhancementBonusX and
    // ShieldEnhancementBonusX

    // Ring of Deflection, Headbands / Belts
    [HarmonyPatch(typeof(AddStatBonusEquipment))]
    static class AddStatBonusEquipment_Patch
    {
      [HarmonyPatch(nameof(AddStatBonusEquipment.OnTurnOn)), HarmonyPrefix]
      static bool OnTurnOn(AddStatBonusEquipment __instance)
      {
        try
        {
          if (!Common.IsReplacedByABP(__instance.Stat, __instance.Descriptor))
          {
            Logger.Verbose(() => $"{__instance.Stat} - {__instance.Descriptor} is not affected by ABP");
            return true;
          }

          var unit = __instance.Owner.Wielder.Unit;
          if (Common.IsAffectedByABP(unit))
          {
            Logger.Verbose(() => $"Skipping {__instance.Stat} - {__instance.Descriptor} for {unit.CharacterName}");
            return false;
          }
        } catch (Exception e)
        {
          Logger.LogException("AddStatBonusEquipment_Patch.OnTurnOn", e);
        }
        return true;
      }
    }

    // Cloak of Resistance
    [HarmonyPatch(typeof(AllSavesBonusEquipment))]
    static class AllSavesBonusEquipment_Patch
    {
      [HarmonyPatch(nameof(AllSavesBonusEquipment.OnTurnOn)), HarmonyPrefix]
      static bool OnTurnOn(AllSavesBonusEquipment __instance)
      {
        try
        {
          if (!Common.IsReplacedByABP(StatType.SaveFortitude, __instance.Descriptor))
          {
            Logger.Verbose(() => $"All Saves - {__instance.Descriptor} is not affected by ABP");
            return true;
          }

          var unit = __instance.Owner.Wielder.Unit;
          if (Common.IsAffectedByABP(unit))
          {
            Logger.Verbose(() => $"Skipping All Saves - {__instance.Descriptor} for {unit.CharacterName}");
            return false;
          }
        }
        catch (Exception e)
        {
          Logger.LogException("AllSavesBonusEquipment_Patch.OnTurnOn", e);
        }
        return true;
      }
    }

    // Armor / Shield / Weapon
    [HarmonyPatch(typeof(GameHelper))]
    static class GameHelper_Patch
    {
      private static BlueprintFeature _calculator;
      private static BlueprintFeature Calculator
      {
        get
        {
          _calculator ??= BlueprintTool.Get<BlueprintFeature>(Guids.EnhancementCalculator);
          return _calculator;
        }
      }

      [HarmonyPatch(nameof(GameHelper.GetItemEnhancementBonus), typeof(ItemEntity)), HarmonyPrefix]
      static bool GetItemEnhancementBonus(ItemEntity item, ref int __result)
      {
        try
        {
          var wielder = item.Wielder?.Unit;
          if (wielder is null)
            return true;

          if (!Common.IsAffectedByABP(wielder))
            return true;

          var calculator = wielder.GetFact(Calculator)?.GetComponent<EnhancementBonusCalculator>();
          if (calculator is null)
          {
            Logger.Warning($"{wielder.CharacterName} does not have an enhancement bonus calculator!");
            return true;
          }

          if (item is ItemEntityArmor armor)
          {
            __result = calculator.GetEnhancementBonus(armor);
            return false;
          }
        } catch (Exception e)
        {
          Logger.LogException("GameHelper_Patch.GetItemEnhancementBonus", e);
        }
        return true;
      }
    }
  }
}
