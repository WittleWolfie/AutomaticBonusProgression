using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Parts;
using System;

namespace AutomaticBonusProgression.Patches
{
  internal class ExpandedActivatableAbilityGroup
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ExpandedActivatableAbilityGroup));

    internal const ActivatableAbilityGroup LegendaryArmor = (ActivatableAbilityGroup)1150;
    internal const ActivatableAbilityGroup LegendaryShield = (ActivatableAbilityGroup)1151;
    internal const ActivatableAbilityGroup LegendaryMainHand = (ActivatableAbilityGroup)1152;
    internal const ActivatableAbilityGroup LegendaryOffHand = (ActivatableAbilityGroup)1153;

    [HarmonyPatch(typeof(UnitPartActivatableAbility))]
    static class UnitPartActivatableAbility_Patch
    {
      [HarmonyPatch(nameof(UnitPartActivatableAbility.GetGroupSize)), HarmonyPrefix]
      static bool GetGroupSize(ActivatableAbilityGroup group, ref int __result)
      {
        try
        {
          if (group == LegendaryArmor || group == LegendaryShield || group == LegendaryMainHand || group == LegendaryOffHand)
          {
            Logger.Verbose(() => $"Returning group size for legendary enchantments: {group}");
            __result = 999;
            return false;
          }
        }
        catch (Exception e)
        {
          Logger.LogException("UnitPartActivatableAbility_Patch.GetGroupSize", e);
        }
        return true;
      }
    }
  }
}
