using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.References;
using Kingmaker;

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
      ConfigureDarknessCaress();
      ConfigureLegendaryBracers();
    }

    private static void ConfigureDeathBelt()
    {
      ItemEquipmentBeltConfigurator.For(ItemEquipmentBeltRefs.ClaspOfDeathItem)
        .SetDescriptionText(Text("DeathBelt"))
        .SetEnchantments(
          EquipmentEnchantmentRefs.NegativeChanneling2.ToString(), Common.Int2, Common.Wis2, Common.Cha2)
        .Configure();
    }

    private static void ConfigureDeathRobe()
    {
      ItemEquipmentShirtConfigurator.For(ItemEquipmentShirtRefs.ClaspOfDeathRobeItem)
        .SetDescriptionText(Text("DeathRobe"))
        .SetEnchantments(
          EquipmentEnchantmentRefs.NegativeChanneling2.ToString(), Common.Int2, Common.Wis2, Common.Cha2)
        .Configure();
    }

    private static void ConfigureDarknessCaress()
    {
      ItemEquipmentHeadConfigurator.For(ItemEquipmentHeadRefs.DarknessCaressItem)
        .SetDescriptionText(Text("DarknessCaress"))
        .SetEnchantments(
          EquipmentEnchantmentRefs.DarknessCaressEnchantment.ToString(), Common.Int2, Common.Wis2, Common.Cha2)
        .Configure();
    }

    private static void ConfigureLegendaryBracers()
    {
      ItemEquipmentWristConfigurator.For(ItemEquipmentWristRefs.LegendaryBracersItem)
        .SetDescriptionText(Text("LegendaryBracers"))
        .SetEnchantments(
          Common.Str2,
          Common.Dex2,
          Common.Con2,
          Common.Int2,
          Common.Wis2,
          Common.Cha2,
          Common.IncreaseDeflection1,
          Common.IncreaseNaturalArmor1,
          Common.IncreaseResist1)
        .Configure();
    }

    private static void ConfigurePerfectTiara()
    {
      ItemEquipmentHeadConfigurator.For(ItemEquipmentHeadRefs.PerfectTiaraOfChannelingItem)
        .SetDescriptionText(Text("PerfectTiara"))
        .SetEnchantments(
          )
        .Configure();
    }

    private static string Text(string itemName)
    {
      return $"ABP.Item.{itemName}";
    }
  }
}
