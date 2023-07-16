using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Replacements
{
  /// <summary>
  /// New stat bonus enchantments that are not Enhancement based, for use with items that previously granted +8.
  /// </summary>
  internal static class StatBonus
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(StatBonus));

    internal static void Configure()
    {
      Logger.Log($"Configuring {nameof(StatBonus)}");

      ConfigureStr2();
      ConfigureDex2();
      ConfigureCon2();
      ConfigureInt2();
      ConfigureWis2();
      ConfigureCha2();
    }

    private const string Str2 = "StatBonus.Str2";
    private static void ConfigureStr2()
    {
      EquipmentEnchantmentConfigurator.New(Str2, Guids.Str2)
        .AddStatBonusEquipment(stat: StatType.Strength, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();
    }

    private const string Dex2 = "StatBonus.Dex2";
    private static void ConfigureDex2()
    {
      EquipmentEnchantmentConfigurator.New(Dex2, Guids.Dex2)
        .AddStatBonusEquipment(stat: StatType.Dexterity, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();
    }

    private const string Con2 = "StatBonus.Con2";
    private static void ConfigureCon2()
    {
      EquipmentEnchantmentConfigurator.New(Con2, Guids.Con2)
        .AddStatBonusEquipment(stat: StatType.Constitution, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();
    }

    private const string Int2 = "StatBonus.Int2";
    private static void ConfigureInt2()
    {
      EquipmentEnchantmentConfigurator.New(Int2, Guids.Int2)
        .AddStatBonusEquipment(stat: StatType.Intelligence, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();
    }

    private const string Wis2 = "StatBonus.Wis2";
    private static void ConfigureWis2()
    {
      EquipmentEnchantmentConfigurator.New(Wis2, Guids.Wis2)
        .AddStatBonusEquipment(stat: StatType.Wisdom, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();
    }

    private const string Cha2 = "StatBonus.Cha2";
    private static void ConfigureCha2()
    {
      EquipmentEnchantmentConfigurator.New(Cha2, Guids.Cha2)
        .AddStatBonusEquipment(stat: StatType.Charisma, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();
    }
  }
}
