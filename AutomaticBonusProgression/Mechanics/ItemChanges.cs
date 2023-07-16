using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Items;

namespace AutomaticBonusProgression.Mechanics
{
  /// <summary>
  /// Modifies items requiring adjustments.
  /// </summary>
  internal static class ItemChanges
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ItemChanges));

    internal static void Configure()
    {
      Logger.Log($"Configuring {nameof(ItemChanges)}");

      // TODO: Replace w/ setting to control the modifier
      Game.Instance.BlueprintRoot.Vendors.SellModifier *= 0.3f;

      ConfigureDeathBelt();
      ConfigureDeathRobe();
    }

    private static void ConfigureDeathBelt()
    {
      ItemEquipmentBeltConfigurator.For(ItemEquipmentBeltRefs.ClaspOfDeathItem)
        .SetDescriptionText("ABP.Item.DeathBelt")
        .SetEnchantments(
          EquipmentEnchantmentRefs.NegativeChanneling2.ToString(), Common.Int2, Common.Wis2, Common.Cha2)
        .Configure();
    }

    private static void ConfigureDeathRobe()
    {
      ItemEquipmentShirtConfigurator.For(ItemEquipmentShirtRefs.ClaspOfDeathRobeItem)
        .SetDescriptionText("ABP.Item.DeathRobe")
        .SetEnchantments(
          EquipmentEnchantmentRefs.NegativeChanneling2.ToString(), Common.Int2, Common.Wis2, Common.Cha2)
        .Configure();
    }
  }
}
