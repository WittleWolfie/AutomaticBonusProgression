using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Replacements
{
  /// <summary>
  /// Enchantments that increase a stat bonus, e.g. increase Deflection to AC by 1.
  /// </summary>
  internal static class IncreasedStatBonus
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(IncreasedStatBonus));

    internal static void Configure()
    {
      Logger.Log($"Configuring {nameof(IncreasedStatBonus)}");

      ConfigureDeflection1();
      ConfigureNaturalArmor1();
      ConfigureResist1();
    }

    private const string Deflection1 = "IncreaseStatBonus.Deflection1";
    private static void ConfigureDeflection1()
    {
      EquipmentEnchantmentConfigurator.New(Deflection1, Guids.IncreaseDeflection1)
        .AddComponent(
          new IncreaseStatBonus(StatType.AC, bonus: 1, descriptor: ModifierDescriptor.Deflection))
        .Configure();
    }

    private const string NaturalArmor1 = "IncreaseStatBonus.NaturalArmor1";
    private static void ConfigureNaturalArmor1()
    {
      EquipmentEnchantmentConfigurator.New(NaturalArmor1, Guids.IncreaseNaturalArmor1)
        .AddComponent(
          new IncreaseStatBonus(StatType.AC, bonus: 1, descriptor: ModifierDescriptor.NaturalArmorEnhancement))
        .Configure();
    }

    private const string Resist1 = "IncreaseStatBonus.Resist1";
    private static void ConfigureResist1()
    {
      EquipmentEnchantmentConfigurator.New(Resist1, Guids.IncreaseResist1)
        .AddComponent(
          new IncreaseStatBonus(StatType.SaveWill, bonus: 1, descriptor: ModifierDescriptor.Resistance))
        .AddComponent(
          new IncreaseStatBonus(StatType.SaveReflex, bonus: 1, descriptor: ModifierDescriptor.Resistance))
        .AddComponent(
          new IncreaseStatBonus(StatType.SaveFortitude, bonus: 1, descriptor: ModifierDescriptor.Resistance))
        .Configure();
    }
  }
}
