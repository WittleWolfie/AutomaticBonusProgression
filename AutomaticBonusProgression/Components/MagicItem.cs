using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.UI.Common;
using System;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// Used to flag items as "magic" as a replacement for the enhancement bonus.
  /// </summary>
  [TypeId("0916acbe-f994-4839-9a48-51f9b4ae1ae9")]
  internal class MagicItem : ItemEnchantmentComponentDelegate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(MagicItem));
    
    [HarmonyPatch(typeof(BlueprintItemArmor))]
    static class BlueprintItemArmor_Patch
    {
      [HarmonyPatch(nameof(BlueprintItemArmor.IsMagic), MethodType.Getter)]
      static bool IsMagic(BlueprintItemArmor __instance, ref bool __result)
      {
        try
        {
          foreach (var enchantment in __instance.m_Enchantments)
          {
            var actual = enchantment.Get();
            if (actual is null)
            {
              Logger.Warning($"Enchantment missing: {enchantment}");
              continue;
            }

            if (actual.GetComponent<MagicItem>() is not null)
            {
              Logger.Verbose(() => $"{__instance.Name} is magic!");
              __result = true;
              return false;
            }
          }
        } catch (Exception e)
        {
          Logger.LogException("BlueprintItemArmor_Patch.IsMagic", e);
        }
        return true;
      }
    }

    [HarmonyPatch(typeof(UIUtility))]
    static class UIUtility_Patch
    {
      [HarmonyPatch(nameof(UIUtility.IsMagicItem))]
      static bool IsMagicItem(ItemEntity item, ref bool __result)
      {
        try
        {
          foreach (var enchantment in item.m_Enchantments)
          {
            if (enchantment.GetComponent<MagicItem>() is not null)
            {
              Logger.Verbose(() => $"{item.Name} is magic!");
              __result = true;
              return false;
            }
          }
        }
        catch (Exception e)
        {
          Logger.LogException("UIUtility_Patch.IsMagicItem", e);
        }
        return true;
      }
    }
  }
}
