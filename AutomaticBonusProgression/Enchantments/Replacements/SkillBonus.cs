using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Replacements
{
  internal static class SkillBonus
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(SkillBonus));

    internal static void Configure()
    {
      Logger.Log($"Configuring {nameof(SkillBonus)}");

      ConfigureTrickery2();
      ConfigureMobility2();
      ConfigurePersuasion2();
    }

    private const string Trickery2 = "SkillBonus.Trickery2";
    private static void ConfigureTrickery2()
    {
      EquipmentEnchantmentConfigurator.New(Trickery2, Guids.Trickery2)
        .AddStatBonusEquipment(stat: StatType.SkillThievery, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }

    private const string Mobility2 = "SkillBonus.Mobility2";
    private static void ConfigureMobility2()
    {
      EquipmentEnchantmentConfigurator.New(Mobility2, Guids.Mobility2)
        .AddStatBonusEquipment(stat: StatType.SkillMobility, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }

    private const string Persuasion2 = "SkillBonus.Persuasion2";
    private static void ConfigurePersuasion2()
    {
      EquipmentEnchantmentConfigurator.New(Persuasion2, Guids.Persuasion2)
        .AddStatBonusEquipment(stat: StatType.SkillPersuasion, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }
  }
}
