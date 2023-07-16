using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;

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
      ConfigurePerfectTiara();
      ConfigurePrimalForce();
      ConfigureGlovesOfDex();
      ConfigureHandsomeHats();
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
          EquipmentEnchantmentRefs.PositiveChanneling2.ToString(),
          EquipmentEnchantmentRefs.NegativeChanneling2.ToString(),
          Common.Int2,
          Common.Wis2,
          Common.Cha2)
        .Configure();
    }

    private static void ConfigurePrimalForce()
    {
      FeatureConfigurator.For(FeatureRefs.BeltOfPrimalForceFeatureFinal)
        .RemoveComponents(c => c is AddContextStatBonus)
        .AddStatBonus(stat: StatType.Strength, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
        .AddStatBonus(stat: StatType.Dexterity, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
        .AddStatBonus(stat: StatType.Constitution, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();

      ItemEquipmentBeltConfigurator.For(ItemEquipmentBeltRefs.BeltOfPrimalForceItem)
        .SetDescriptionText(Text("PrimalForce"))
        .Configure();
    }

    private static void ConfigureGlovesOfDex()
    {
      ItemEquipmentGlovesConfigurator.For(ItemEquipmentGlovesRefs.GlovesOfDexterityItem)
        .SetDescriptionText(Text("GlovesOfDex"))
        .SetEnchantments(Common.Trickery2, Common.Mobility2)
        .Configure();
    }

    private static void ConfigureHandsomeHats()
    {
      ConfigureHat(ItemEquipmentHeadRefs.HatOfHandsomenessItem);
      ConfigureHat(ItemEquipmentHeadRefs.RedHatOfHandsomenessItem);
      ConfigureHat(ItemEquipmentHeadRefs.BlueHatOfHandsomenessItem);
      ConfigureHat(ItemEquipmentHeadRefs.GreenHatOfHandsomenessItem);
      ConfigureHat(ItemEquipmentHeadRefs.BlackHatOfHandsomenessItem);
    }

    private static void ConfigureHat(Blueprint<BlueprintReference<BlueprintItemEquipmentHead>> hat)
    {
      ItemEquipmentHeadConfigurator.For(hat)
        .SetDescriptionText(Text("HandsomeHat"))
        .SetEnchantments(Common.Persuasion2)
        .Configure();
    }

    private static string Text(string itemName)
    {
      return $"ABP.Item.{itemName}";
    }
  }
}
