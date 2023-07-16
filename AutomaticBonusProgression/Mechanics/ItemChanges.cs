using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;

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
      ConfigureDoublingAnnoyance();
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

    private const string WizardBuff = "ItemChanges.WizardBuff";
    private static void ConfigureDoublingAnnoyance()
    {
      BuffConfigurator.New(WizardBuff, Guids.WizardBuff)
        .SetFlags(BlueprintBuff.Flags.HiddenInUi)
        .AddStatBonus(stat: StatType.Strength, value: 6, descriptor: ModifierDescriptor.UntypedStackable)
        .AddStatBonus(stat: StatType.Dexterity, value: 6, descriptor: ModifierDescriptor.UntypedStackable)
        .AddStatBonus(stat: StatType.Constitution, value: 6, descriptor: ModifierDescriptor.UntypedStackable)
        .AddStatBonus(stat: StatType.Intelligence, value: 6, descriptor: ModifierDescriptor.UntypedStackable)
        .AddStatBonus(stat: StatType.Wisdom, value: 6, descriptor: ModifierDescriptor.UntypedStackable)
        .AddStatBonus(stat: StatType.Charisma, value: 6, descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();

      AbilityConfigurator.For(AbilityRefs.TricksterSummonPerpetuallyAnnoyedWizard)
        .EditComponent<AbilityEffectRunAction>(ApplyWizardBuff)
        .Configure();

      ItemEquipmentHeadConfigurator.For(ItemEquipmentHeadRefs.DoublingAnnoyanceItem)
        .SetDescriptionText(Text("DoublingAnnoyance"))
        .Configure();
    }

    private static void ApplyWizardBuff(AbilityEffectRunAction runAction)
    {
      var conditional = runAction.Actions.Actions[0] as Conditional;
      if (conditional is null)
      {
        Logger.Warning($"Missing spawn conditional for Perpetually Annoyed Wizard: {runAction.Actions.Actions[0]}");
        return;
      }

      var spawnMonsters = conditional.IfTrue.Actions[0] as ContextActionSpawnMonster;
      if (spawnMonsters is null)
      {
        Logger.Warning($"Missing spawn action for Perpetually Annoyed Wizard: {conditional.IfTrue.Actions[0]}");
        return;
      }

      spawnMonsters.AfterSpawn =
        ActionsBuilder.New().AddAll(spawnMonsters.AfterSpawn).ApplyBuffPermanent(Guids.WizardBuff).Build();
    }

    private static string Text(string itemName)
    {
      return $"ABP.Item.{itemName}";
    }
  }
}
