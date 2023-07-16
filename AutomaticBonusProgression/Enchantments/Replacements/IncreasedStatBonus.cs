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

      ConfigureStr2();
      ConfigureDex2();
      ConfigureCon2();
      ConfigureInt2();
      ConfigureWis2();
      ConfigureCha2();

      ConfigureDeflection1();
      ConfigureNaturalArmor1();
      ConfigureResist1();
    }

    private const string Str2 = "IncreaseStatBonus.Str2";
    private static void ConfigureStr2()
    {
      EquipmentEnchantmentConfigurator.New(Str2, Guids.IncreaseStr2)
        .AddComponent(
          new IncreaseStatBonus(StatType.Strength, bonus: 2, descriptor: ModifierDescriptor.Enhancement))
        .Configure();
    }

    private const string Dex2 = "IncreaseStatBonus.Dex2";
    private static void ConfigureDex2()
    {
      EquipmentEnchantmentConfigurator.New(Dex2, Guids.IncreaseDex2)
        .AddComponent(
          new IncreaseStatBonus(StatType.Dexterity, bonus: 2, descriptor: ModifierDescriptor.Enhancement))
        .Configure();
    }

    private const string Con2 = "IncreaseStatBonus.Con2";
    private static void ConfigureCon2()
    {
      EquipmentEnchantmentConfigurator.New(Con2, Guids.IncreaseCon2)
        .AddComponent(
          new IncreaseStatBonus(StatType.Constitution, bonus: 2, descriptor: ModifierDescriptor.Enhancement))
        .Configure();
    }

    private const string Int2 = "IncreaseStatBonus.Int2";
    private static void ConfigureInt2()
    {
      EquipmentEnchantmentConfigurator.New(Int2, Guids.IncreaseInt2)
        .AddComponent(
          new IncreaseStatBonus(StatType.Intelligence, bonus: 2, descriptor: ModifierDescriptor.Enhancement))
        .Configure();
    }

    private const string Wis2 = "IncreaseStatBonus.Wis2";
    private static void ConfigureWis2()
    {
      EquipmentEnchantmentConfigurator.New(Wis2, Guids.IncreaseWis2)
        .AddComponent(
          new IncreaseStatBonus(StatType.Wisdom, bonus: 2, descriptor: ModifierDescriptor.Enhancement))
        .Configure();
    }

    private const string Cha2 = "IncreaseStatBonus.Cha2";
    private static void ConfigureCha2()
    {
      EquipmentEnchantmentConfigurator.New(Cha2, Guids.IncreaseCha2)
        .AddComponent(
          new IncreaseStatBonus(StatType.Charisma, bonus: 2, descriptor: ModifierDescriptor.Enhancement))
        .Configure();
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
