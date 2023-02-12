using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers;
using Kingmaker.Designers.Mechanics.EquipmentEnchants;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Reflection;
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

      // Overwrite TTT's logic or else it just ignores our patch
      [HarmonyAfter("TabletopTweaks-Base")]
      [HarmonyPatch(nameof(GameHelper.GetItemEnhancementBonus), typeof(ItemEntity)), HarmonyPostfix]
      static void GetItemEnhancementBonus(ItemEntity item, ref int __result)
      {
        try
        {
          var wielder = item.Wielder?.Unit;
          if (wielder is null)
            return;

          if (!Common.IsAffectedByABP(wielder))
            return;

          var calculator = wielder.GetFact(Calculator)?.GetComponent<EnhancementBonusCalculator>();
          if (calculator is null)
          {
            Logger.Warning($"{wielder.CharacterName} does not have an enhancement bonus calculator!");
            return;
          }

          if (item is ItemEntityArmor armor)
          {
            __result = calculator.GetEnhancementBonus(armor);
            return;
          }
        }
        catch (Exception e)
        {
          Logger.LogException("GameHelper_Patch.GetItemEnhancementBonus", e);
        }
      }
    }

    private static IEnumerable<CodeInstruction> GetEnchantRunAction(
      IEnumerable<CodeInstruction> instructions,
      FieldInfo durationValue)
    {
      // Current:
      //   ldc.i4.0
      //   stloc.3
      // New:
      //   ldloc.2
      //   call      GameHelper.GetItemEnhancementBonus(ItemEntity)
      //   stloc.3
      // Delete:
      //   ldloc.2
      //   callvirt  getEnchantments()
      //   call      Any()
      //   brfalse.s IL_00D2
      //   ... (until IL_00D2)
      var code = new List<CodeInstruction>(instructions);

      // Search backwards for this.DurationValue which is just after the deleted section
      var index = code.Count - 1;
      var deleteEnd = 0;
      for (; index >= 0; index--)
      {
        if (code[index].LoadsField(durationValue))
        {
          deleteEnd = index - 1; // Need to keep the ldarg call just before this step
          break;
        }
      }

      // Search backwards to find where the armor is stored
      CodeInstruction loadArmor = null;
      var getEnchantments = AccessTools.PropertyGetter(typeof(ItemEntity), nameof(ItemEntity.Enchantments));
      for (; index >= 0; index--)
      {
        if (code[index].Calls(getEnchantments))
        {
          // Instruction just before must load the armor
          loadArmor = code[index - 1].Clone();
          break;
        }
      }

      // Search backwards for GetGroupSize() which is called just before the deleted section
      var deleteStart = 0;
      var getGroupSize =
        AccessTools.Method(typeof(UnitPartActivatableAbility), nameof(UnitPartActivatableAbility.GetGroupSize));
      for (; index >= 0; index--)
      {
        if (code[index].Calls(getGroupSize))
        {
          deleteStart = index + 2; // Need to keep the stloc called just after this
          break;
        }
      }

      // search backwards for load 0 which is where the enhancement bonus is first set
      var loadEnhancementBonus = 0;
      for (; index >= 0; index--)
      {
        if (code[index].LoadsConstant())
        {
          loadArmor.MoveLabelsFrom(code[index]);
          loadEnhancementBonus = index;
          break;
        }
      }

      // First do the delete (work backwards so the indices are stable)
      code.RemoveRange(deleteStart, deleteEnd - deleteStart);

      // Now add thew new load calculation
      var newCalculation =
        new List<CodeInstruction>()
        {
              loadArmor,
              CodeInstruction.Call(
                typeof(GameHelper),
                nameof(GameHelper.GetItemEnhancementBonus),
                parameters: new[] { typeof(ItemEntity) })
        };
      code.InsertRange(loadEnhancementBonus + 1, newCalculation);

      // Finally delete the load 0
      code.RemoveAt(loadEnhancementBonus);
      return code;
    }

    [HarmonyPatch(typeof(ContextActionArmorEnchantPool))]
    static class ContextActionArmorEnchantPool_Patch
    {
      [HarmonyPatch(nameof(ContextActionArmorEnchantPool.RunAction)), HarmonyTranspiler]
      static IEnumerable<CodeInstruction> RunAction(IEnumerable<CodeInstruction> instructions)
      {
        try
        {
          var durationValue =
            AccessTools.Field(
              typeof(ContextActionArmorEnchantPool), nameof(ContextActionArmorEnchantPool.DurationValue));
          return GetEnchantRunAction(instructions, durationValue);
        } catch (Exception e)
        {
          Logger.LogException("ContextActionArmorEnchantPool_Patch.RunAction", e);
        }
        return instructions;
      }
    }

    [HarmonyPatch(typeof(ContextActionShieldArmorEnchantPool))]
    static class ContextActionShieldArmorEnchantPool_Patch
    {
      [HarmonyPatch(nameof(ContextActionShieldArmorEnchantPool.RunAction)), HarmonyTranspiler]
      static IEnumerable<CodeInstruction> RunAction(IEnumerable<CodeInstruction> instructions)
      {
        try
        {
          var durationValue =
            AccessTools.Field(
              typeof(ContextActionShieldArmorEnchantPool), nameof(ContextActionShieldArmorEnchantPool.DurationValue));
          return GetEnchantRunAction(instructions, durationValue);
        }
        catch (Exception e)
        {
          Logger.LogException("ContextActionArmorEnchantPool_Patch.RunAction", e);
        }
        return instructions;
      }
    }
  }
}
