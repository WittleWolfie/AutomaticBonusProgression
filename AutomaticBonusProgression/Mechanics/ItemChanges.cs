using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Equipment;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Menu = ModMenu.ModMenu;

namespace AutomaticBonusProgression.Mechanics
{
  /// <summary>
  /// Modifies items requiring adjustments.
  /// </summary>
  internal static class ItemChanges
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ItemChanges));

    private static double BaseSellModifier;

    internal static void Configure()
    {
      Logger.Log($"Configuring {nameof(ItemChanges)}");

      BaseSellModifier = Game.Instance.BlueprintRoot.Vendors.SellModifier;
      AdjustSellModifier(Settings.GetSellValue());

      ConfigureDeathBelt();
      ConfigureDeathRobe();
      ConfigureDarknessCaress();
      ConfigureLegendaryBracers();
      ConfigurePerfectTiara();
      ConfigurePrimalForce();
      ConfigureGlovesOfDex();
      ConfigureHandsomeHats();
      ConfigureDoublingAnnoyance();
      DisableStatItems();
    }

    internal static void AdjustSellModifier(float modifier)
    {
      Game.Instance.BlueprintRoot.Vendors.SellModifier = BaseSellModifier * modifier;
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
        ActionsBuilder.New()
          .AddAll(spawnMonsters.AfterSpawn)
          .ApplyBuff(Guids.WizardBuff, durationValue: ContextDuration.Fixed(24, DurationRate.Hours))
          .Build();
    }

    #region Disable
    // Makes items replaced by ABP unequippable
    private static void DisableStatItems()
    {
      #region Headbands
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfIntelligence2);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfIntelligence4);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfIntelligence6);

      DisableHead(ItemEquipmentHeadRefs.HeadbandOfWisdom2);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfWisdom4);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfWisdom6);

      DisableHead(ItemEquipmentHeadRefs.HeadbandOfCharisma2);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfCharisma4);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfCharisma6);

      DisableHead(ItemEquipmentHeadRefs.HeadbandOfPerfection2);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfPerfection4);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfPerfection6);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfPerfection8);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfPerfection8Extra);

      DisableHead(ItemEquipmentHeadRefs.HeadbandOfCharismaIntelligence2);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfCharismaWisdom2);
      DisableHead(ItemEquipmentHeadRefs.HeadbandOfWisdomIntelligence2);
      #endregion

      #region Belts
      DisableBelt(ItemEquipmentBeltRefs.BeltOfStrength2);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfStrength4);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfStrength6);

      DisableBelt(ItemEquipmentBeltRefs.BeltOfDexterity2);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfDexterity4);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfDexterity6);

      DisableBelt(ItemEquipmentBeltRefs.BeltOfConstitution2);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfConstitution4);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfConstitution6);

      DisableBelt(ItemEquipmentBeltRefs.BeltOfPerfection2);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfPerfection4);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfPerfection6);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfPerfection8);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfPerfection8Extra);

      DisableBelt(ItemEquipmentBeltRefs.BeltOfStrengthConstitution2);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfStrengthConstitution4);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfStrengthConstitution6);

      DisableBelt(ItemEquipmentBeltRefs.BeltOfStrengthDexterity2);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfStrengthDexterity4);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfStrengthDexterity6);

      DisableBelt(ItemEquipmentBeltRefs.BeltOfDexterityConstitution2);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfDexterityConstitution4);
      DisableBelt(ItemEquipmentBeltRefs.BeltOfDexterityConstitution6);
      #endregion

      #region Amulets / Cloaks / Rings
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfNaturalArmor1);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfNaturalArmor2);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfNaturalArmor3);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfNaturalArmor4);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfNaturalArmor5);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfNaturalArmor6);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfNaturalArmor7);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfNaturalArmor7Extra);

      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfMightyFists1);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfMightyFists2);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfMightyFists3);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfMightyFists4);
      DisableAmulet(ItemEquipmentNeckRefs.AmuletOfMightyFists5);

      DisableCloak(ItemEquipmentShouldersRefs.CloakOfResistance1);
      DisableCloak(ItemEquipmentShouldersRefs.CloakOfResistance2);
      DisableCloak(ItemEquipmentShouldersRefs.CloakOfResistance3);
      DisableCloak(ItemEquipmentShouldersRefs.CloakOfResistance4);
      DisableCloak(ItemEquipmentShouldersRefs.CloakOfResistance5);
      DisableCloak(ItemEquipmentShouldersRefs.CloakOfResistance6);
      DisableCloak(ItemEquipmentShouldersRefs.CloakOfResistance7);
      DisableCloak(ItemEquipmentShouldersRefs.CloakOfResistance7Extra);

      DisableRing(ItemEquipmentRingRefs.RingOfProtection1);
      DisableRing(ItemEquipmentRingRefs.RingOfProtection1_Prologue);
      DisableRing(ItemEquipmentRingRefs.RingOfProtection2);
      DisableRing(ItemEquipmentRingRefs.RingOfProtection3);
      DisableRing(ItemEquipmentRingRefs.RingOfProtection4);
      DisableRing(ItemEquipmentRingRefs.RingOfProtection5);
      DisableRing(ItemEquipmentRingRefs.RingOfProtection6);
      DisableRing(ItemEquipmentRingRefs.RingOfProtection7);
      DisableRing(ItemEquipmentRingRefs.RingOfProtection7Extra);
      #endregion
    }

    private static void DisableHead(Blueprint<BlueprintReference<BlueprintItemEquipmentHead>> head)
    {
      ItemEquipmentHeadConfigurator.For(head).AddComponent<EquipmentRestrictionNoEquip>().Configure();
    }

    private static void DisableBelt(Blueprint<BlueprintReference<BlueprintItemEquipmentBelt>> belt)
    {
      ItemEquipmentBeltConfigurator.For(belt).AddComponent<EquipmentRestrictionNoEquip>().Configure();
    }

    private static void DisableAmulet(Blueprint<BlueprintReference<BlueprintItemEquipmentNeck>> amulet)
    {
      ItemEquipmentNeckConfigurator.For(amulet).AddComponent<EquipmentRestrictionNoEquip>().Configure();
    }

    private static void DisableCloak(Blueprint<BlueprintReference<BlueprintItemEquipmentShoulders>> cloak)
    {
      ItemEquipmentShouldersConfigurator.For(cloak).AddComponent<EquipmentRestrictionNoEquip>().Configure();
    }

    private static void DisableRing(Blueprint<BlueprintReference<BlueprintItemEquipmentRing>> ring)
    {
      ItemEquipmentRingConfigurator.For(ring).AddComponent<EquipmentRestrictionNoEquip>().Configure();
    }
    #endregion

    private static string Text(string itemName)
    {
      return $"ABP.Item.{itemName}";
    }
  }
}
