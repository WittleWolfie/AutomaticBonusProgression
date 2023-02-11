using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using System;

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

    private static bool IsReplacedByABP(StatType stat, ModifierDescriptor descriptor)
    {
      switch (stat)
      {
        case StatType.AC:
          return descriptor == ModifierDescriptor.ArmorEnhancement
            || descriptor == ModifierDescriptor.NaturalArmorEnhancement
            || descriptor == ModifierDescriptor.ShieldEnhancement
            || descriptor == ModifierDescriptor.Deflection;
        case StatType.Charisma:
        case StatType.Constitution:
        case StatType.Dexterity:
        case StatType.Intelligence:
        case StatType.Strength:
        case StatType.Wisdom:
          return descriptor == ModifierDescriptor.Enhancement;
        case StatType.SaveFortitude:
        case StatType.SaveReflex:
        case StatType.SaveWill:
          return descriptor == ModifierDescriptor.Resistance;
      }
      return false;
    }

    private static bool IsAffectedByABP(UnitEntityData unit)
    {
      return unit.IsInCompanionRoster() || (unit.Master is not null && unit.Master.IsInCompanionRoster());
    }

    // Ring of Deflection, Headbands / Belts
    [HarmonyPatch(typeof(AddStatBonusEquipment))]
    static class AddStatBonusEquipment_Patch
    {
      [HarmonyPatch(nameof(AddStatBonusEquipment.OnTurnOn)), HarmonyPrefix]
      static bool OnTurnOn(AddStatBonusEquipment __instance)
      {
        try
        {
          if (!IsReplacedByABP(__instance.Stat, __instance.Descriptor))
          {
            Logger.Verbose(() => $"{__instance.Stat} - {__instance.Descriptor} is not affected by ABP");
            return true;
          }

          var unit = __instance.Owner.Wielder.Unit;
          if (IsAffectedByABP(unit))
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
          if (!IsReplacedByABP(StatType.SaveFortitude, __instance.Descriptor))
          {
            Logger.Verbose(() => $"All Saves - {__instance.Descriptor} is not affected by ABP");
            return true;
          }

          var unit = __instance.Owner.Wielder.Unit;
          if (IsAffectedByABP(unit))
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
  }
}
