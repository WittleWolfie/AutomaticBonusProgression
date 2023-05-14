using AutomaticBonusProgression.Features;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Components;
using System;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Bane
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bane));

    #region Too Many Constants
    private const string BaneAberrationsName = "LegendaryWeapon.Bane.Aberrations.Name";
    private const string BaneAberrationsEffect = "LegendaryWeapon.Bane.Aberrations.Effect";
    private const string BaneAberrationsOffHandEffect = "LegendaryWeapon.Bane.Aberrations.OffHand.Effect";
    private const string BaneAberrationsBuff = "LegendaryWeapon.Bane.Aberrations.Buff";
    private const string BaneAberrationsOffHandBuff = "LegendaryWeapon.Bane.Aberrations.OffHand.Buff";

    private const string BaneAnimalsName = "LegendaryWeapon.Bane.Animals.Name";
    private const string BaneAnimalsEffect = "LegendaryWeapon.Bane.Animals.Effect";
    private const string BaneAnimalsOffHandEffect = "LegendaryWeapon.Bane.Animals.OffHand.Effect";
    private const string BaneAnimalsBuff = "LegendaryWeapon.Bane.Animals.Buff";
    private const string BaneAnimalsOffHandBuff = "LegendaryWeapon.Bane.Animals.OffHand.Buff";

    private const string BaneConstructsName = "LegendaryWeapon.Bane.Constructs.Name";
    private const string BaneConstructsEffect = "LegendaryWeapon.Bane.Constructs.Effect";
    private const string BaneConstructsOffHandEffect = "LegendaryWeapon.Bane.Constructs.OffHand.Effect";
    private const string BaneConstructsBuff = "LegendaryWeapon.Bane.Constructs.Buff";
    private const string BaneConstructsOffHandBuff = "LegendaryWeapon.Bane.Constructs.OffHand.Buff";

    private const string BaneDragonsName = "LegendaryWeapon.Bane.Dragons.Name";
    private const string BaneDragonsEffect = "LegendaryWeapon.Bane.Dragons.Effect";
    private const string BaneDragonsOffHandEffect = "LegendaryWeapon.Bane.Dragons.OffHand.Effect";
    private const string BaneDragonsBuff = "LegendaryWeapon.Bane.Dragons.Buff";
    private const string BaneDragonsOffHandBuff = "LegendaryWeapon.Bane.Dragons.OffHand.Buff";

    private const string BaneFeyName = "LegendaryWeapon.Bane.Fey.Name";
    private const string BaneFeyEffect = "LegendaryWeapon.Bane.Fey.Effect";
    private const string BaneFeyOffHandEffect = "LegendaryWeapon.Bane.Fey.OffHand.Effect";
    private const string BaneFeyBuff = "LegendaryWeapon.Bane.Fey.Buff";
    private const string BaneFeyOffHandBuff = "LegendaryWeapon.Bane.Fey.OffHand.Buff";

    private const string BaneHumanoidGiantName = "LegendaryWeapon.Bane.Humanoid.Giant.Name";
    private const string BaneHumanoidGiantEffect = "LegendaryWeapon.Bane.Humanoid.Giant.Effect";
    private const string BaneHumanoidGiantOffHandEffect = "LegendaryWeapon.Bane.Humanoid.Giant.OffHand.Effect";
    private const string BaneHumanoidGiantBuff = "LegendaryWeapon.Bane.Humanoid.Giant.Buff";
    private const string BaneHumanoidGiantOffHandBuff = "LegendaryWeapon.Bane.Humanoid.Giant.OffHand.Buff";

    private const string BaneHumanoidReptilianName = "LegendaryWeapon.Bane.Humanoid.Reptilian.Name";
    private const string BaneHumanoidReptilianEffect = "LegendaryWeapon.Bane.Humanoid.Reptilian.Effect";
    private const string BaneHumanoidReptilianOffHandEffect = "LegendaryWeapon.Bane.Humanoid.Reptilian.OffHand.Effect";
    private const string BaneHumanoidReptilianBuff = "LegendaryWeapon.Bane.Humanoid.Reptilian.Buff";
    private const string BaneHumanoidReptilianOffHandBuff = "LegendaryWeapon.Bane.Humanoid.Reptilian.OffHand.Buff";

    private const string BaneHumanoidMonstrousName = "LegendaryWeapon.Bane.Humanoid.Monstrous.Name";
    private const string BaneHumanoidMonstrousEffect = "LegendaryWeapon.Bane.Humanoid.Monstrous.Effect";
    private const string BaneHumanoidMonstrousOffHandEffect = "LegendaryWeapon.Bane.Humanoid.Monstrous.OffHand.Effect";
    private const string BaneHumanoidMonstrousBuff = "LegendaryWeapon.Bane.Humanoid.Monstrous.Buff";
    private const string BaneHumanoidMonstrousOffHandBuff = "LegendaryWeapon.Bane.Humanoid.Monstrous.OffHand.Buff";

    private const string BaneMagicalBeastsName = "LegendaryWeapon.Bane.MagicalBeasts.Name";
    private const string BaneMagicalBeastsEffect = "LegendaryWeapon.Bane.MagicalBeasts.Effect";
    private const string BaneMagicalBeastsOffHandEffect = "LegendaryWeapon.Bane.MagicalBeasts.OffHand.Effect";
    private const string BaneMagicalBeastsBuff = "LegendaryWeapon.Bane.MagicalBeasts.Buff";
    private const string BaneMagicalBeastsOffHandBuff = "LegendaryWeapon.Bane.MagicalBeasts.OffHand.Buff";

    private const string BaneOutsiderGoodName = "LegendaryWeapon.Bane.Outsider.Good.Name";
    private const string BaneOutsiderGoodEffect = "LegendaryWeapon.Bane.Outsider.Good.Effect";
    private const string BaneOutsiderGoodOffHandEffect = "LegendaryWeapon.Bane.Outsider.Good.OffHand.Effect";
    private const string BaneOutsiderGoodBuff = "LegendaryWeapon.Bane.Outsider.Good.Buff";
    private const string BaneOutsiderGoodOffHandBuff = "LegendaryWeapon.Bane.Outsider.Good.OffHand.Buff";

    private const string BaneOutsiderEvilName = "LegendaryWeapon.Bane.Outsider.Evil.Name";
    private const string BaneOutsiderEvilEffect = "LegendaryWeapon.Bane.Outsider.Evil.Effect";
    private const string BaneOutsiderEvilOffHandEffect = "LegendaryWeapon.Bane.Outsider.Evil.OffHand.Effect";
    private const string BaneOutsiderEvilBuff = "LegendaryWeapon.Bane.Outsider.Evil.Buff";
    private const string BaneOutsiderEvilOffHandBuff = "LegendaryWeapon.Bane.Outsider.Evil.OffHand.Buff";

    private const string BaneOutsiderLawfulName = "LegendaryWeapon.Bane.Outsider.Lawful.Name";
    private const string BaneOutsiderLawfulEffect = "LegendaryWeapon.Bane.Outsider.Lawful.Effect";
    private const string BaneOutsiderLawfulOffHandEffect = "LegendaryWeapon.Bane.Outsider.Lawful.OffHand.Effect";
    private const string BaneOutsiderLawfulBuff = "LegendaryWeapon.Bane.Outsider.Lawful.Buff";
    private const string BaneOutsiderLawfulOffHandBuff = "LegendaryWeapon.Bane.Outsider.Lawful.OffHand.Buff";

    private const string BaneOutsiderChaoticName = "LegendaryWeapon.Bane.Outsider.Chaotic.Name";
    private const string BaneOutsiderChaoticEffect = "LegendaryWeapon.Bane.Outsider.Chaotic.Effect";
    private const string BaneOutsiderChaoticOffHandEffect = "LegendaryWeapon.Bane.Outsider.Chaotic.OffHand.Effect";
    private const string BaneOutsiderChaoticBuff = "LegendaryWeapon.Bane.Outsider.Chaotic.Buff";
    private const string BaneOutsiderChaoticOffHandBuff = "LegendaryWeapon.Bane.Outsider.Chaotic.OffHand.Buff";

    private const string BaneOutsiderNeutralName = "LegendaryWeapon.Bane.Outsider.Neutral.Name";
    private const string BaneOutsiderNeutralEffect = "LegendaryWeapon.Bane.Outsider.Neutral.Effect";
    private const string BaneOutsiderNeutralOffHandEffect = "LegendaryWeapon.Bane.Outsider.Neutral.OffHand.Effect";
    private const string BaneOutsiderNeutralBuff = "LegendaryWeapon.Bane.Outsider.Neutral.Buff";
    private const string BaneOutsiderNeutralOffHandBuff = "LegendaryWeapon.Bane.Outsider.Neutral.OffHand.Buff";

    private const string BanePlantsName = "LegendaryWeapon.Bane.Plants.Name";
    private const string BanePlantsEffect = "LegendaryWeapon.Bane.Plants.Effect";
    private const string BanePlantsOffHandEffect = "LegendaryWeapon.Bane.Plants.OffHand.Effect";
    private const string BanePlantsBuff = "LegendaryWeapon.Bane.Plants.Buff";
    private const string BanePlantsOffHandBuff = "LegendaryWeapon.Bane.Plants.OffHand.Buff";

    private const string BaneUndeadName = "LegendaryWeapon.Bane.Undead.Name";
    private const string BaneUndeadEffect = "LegendaryWeapon.Bane.Undead.Effect";
    private const string BaneUndeadOffHandEffect = "LegendaryWeapon.Bane.Undead.OffHand.Effect";
    private const string BaneUndeadBuff = "LegendaryWeapon.Bane.Undead.Buff";
    private const string BaneUndeadOffHandBuff = "LegendaryWeapon.Bane.Undead.OffHand.Buff";

    private const string BaneVerminName = "LegendaryWeapon.Bane.Vermin.Name";
    private const string BaneVerminEffect = "LegendaryWeapon.Bane.Vermin.Effect";
    private const string BaneVerminOffHandEffect = "LegendaryWeapon.Bane.Vermin.OffHand.Effect";
    private const string BaneVerminBuff = "LegendaryWeapon.Bane.Vermin.Buff";
    private const string BaneVerminOffHandBuff = "LegendaryWeapon.Bane.Vermin.OffHand.Buff";
    #endregion

    private const string Description = "LegendaryWeapon.Bane.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Bane");

      // Aberrations
      var aberrationsEnchantInfo = new WeaponEnchantInfo(BaneAberrationsName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        aberrationsEnchantInfo,
        effectBuff: GetBuffInfo(BaneAberrationsEffect, Guids.BaneAberrationsEffect, FeatureRefs.AberrationType.ToString()),
        parentBuff: new(BaneAberrationsBuff, Guids.BaneAberrationsBuff));
      EnchantTool.CreateVariantEnchant(
        aberrationsEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneAberrationsOffHandEffect,
          Guids.BaneAberrationsOffHandEffect,
          FeatureRefs.AberrationType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneAberrationsOffHandBuff, Guids.BaneAberrationsOffHandBuff));

      // Animals
      var animalsEnchantInfo = new WeaponEnchantInfo(BaneAnimalsName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        animalsEnchantInfo,
        effectBuff: GetBuffInfo(BaneAnimalsEffect, Guids.BaneAnimalsEffect, FeatureRefs.AnimalType.ToString()),
        parentBuff: new(BaneAnimalsBuff, Guids.BaneAnimalsBuff));
      EnchantTool.CreateVariantEnchant(
        animalsEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneAnimalsOffHandEffect,
          Guids.BaneAnimalsOffHandEffect,
          FeatureRefs.AnimalType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneAnimalsOffHandBuff, Guids.BaneAnimalsOffHandBuff));

      // Constructs
      var constructsEnchantInfo = new WeaponEnchantInfo(BaneConstructsName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        constructsEnchantInfo,
        effectBuff: GetBuffInfo(BaneConstructsEffect, Guids.BaneConstructsEffect, FeatureRefs.ConstructType.ToString()),
        parentBuff: new(BaneConstructsBuff, Guids.BaneConstructsBuff));
      EnchantTool.CreateVariantEnchant(
        constructsEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneConstructsOffHandEffect,
          Guids.BaneConstructsOffHandEffect,
          FeatureRefs.ConstructType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneConstructsOffHandBuff, Guids.BaneConstructsOffHandBuff));

      // Dragons
      var dragonsEnchantInfo = new WeaponEnchantInfo(BaneDragonsName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        dragonsEnchantInfo,
        effectBuff: GetBuffInfo(BaneDragonsEffect, Guids.BaneDragonsEffect, FeatureRefs.DragonType.ToString()),
        parentBuff: new(BaneDragonsBuff, Guids.BaneDragonsBuff));
      EnchantTool.CreateVariantEnchant(
        dragonsEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneDragonsOffHandEffect,
          Guids.BaneDragonsOffHandEffect,
          FeatureRefs.DragonType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneDragonsOffHandBuff, Guids.BaneDragonsOffHandBuff));

      // Fey
      var feyEnchantInfo = new WeaponEnchantInfo(BaneFeyName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        feyEnchantInfo,
        effectBuff: GetBuffInfo(BaneFeyEffect, Guids.BaneFeyEffect, FeatureRefs.FeyType.ToString()),
        parentBuff: new(BaneFeyBuff, Guids.BaneFeyBuff));
      EnchantTool.CreateVariantEnchant(
        feyEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneFeyOffHandEffect,
          Guids.BaneFeyOffHandEffect,
          FeatureRefs.FeyType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneFeyOffHandBuff, Guids.BaneFeyOffHandBuff));

      // HumanoidGiant
      var humanoidGiantEnchantInfo = new WeaponEnchantInfo(BaneHumanoidGiantName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        humanoidGiantEnchantInfo,
        effectBuff: GetBuffInfo(BaneHumanoidGiantEffect, Guids.BaneHumanoidGiantEffect, FeatureRefs.GiantSubtype.ToString()),
        parentBuff: new(BaneHumanoidGiantBuff, Guids.BaneHumanoidGiantBuff));
      EnchantTool.CreateVariantEnchant(
        humanoidGiantEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneHumanoidGiantOffHandEffect,
          Guids.BaneHumanoidGiantOffHandEffect,
          FeatureRefs.GiantSubtype.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneHumanoidGiantOffHandBuff, Guids.BaneHumanoidGiantOffHandBuff));

      // HumanoidReptilian
      var humanoidReptilianEnchantInfo = new WeaponEnchantInfo(BaneHumanoidReptilianName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        humanoidReptilianEnchantInfo,
        effectBuff: GetBuffInfo(BaneHumanoidReptilianEffect, Guids.BaneHumanoidReptilianEffect, FeatureRefs.ReptilianSubtype.ToString()),
        parentBuff: new(BaneHumanoidReptilianBuff, Guids.BaneHumanoidReptilianBuff));
      EnchantTool.CreateVariantEnchant(
        humanoidReptilianEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneHumanoidReptilianOffHandEffect,
          Guids.BaneHumanoidReptilianOffHandEffect,
          FeatureRefs.ReptilianSubtype.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneHumanoidReptilianOffHandBuff, Guids.BaneHumanoidReptilianOffHandBuff));

      // HumanoidMonstrous
      var humanoidMonstrousEnchantInfo = new WeaponEnchantInfo(BaneHumanoidMonstrousName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        humanoidMonstrousEnchantInfo,
        effectBuff: GetBuffInfo(BaneHumanoidMonstrousEffect, Guids.BaneHumanoidMonstrousEffect, FeatureRefs.MonstrousHumanoidType.ToString()),
        parentBuff: new(BaneHumanoidMonstrousBuff, Guids.BaneHumanoidMonstrousBuff));
      EnchantTool.CreateVariantEnchant(
        humanoidMonstrousEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneHumanoidMonstrousOffHandEffect,
          Guids.BaneHumanoidMonstrousOffHandEffect,
          FeatureRefs.MonstrousHumanoidType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneHumanoidMonstrousOffHandBuff, Guids.BaneHumanoidMonstrousOffHandBuff));

      // MagicalBeasts
      var magicalBeastsEnchantInfo = new WeaponEnchantInfo(BaneMagicalBeastsName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        magicalBeastsEnchantInfo,
        effectBuff: GetBuffInfo(BaneMagicalBeastsEffect, Guids.BaneMagicalBeastsEffect, FeatureRefs.MagicalBeastType.ToString()),
        parentBuff: new(BaneMagicalBeastsBuff, Guids.BaneMagicalBeastsBuff));
      EnchantTool.CreateVariantEnchant(
        magicalBeastsEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneMagicalBeastsOffHandEffect,
          Guids.BaneMagicalBeastsOffHandEffect,
          FeatureRefs.MagicalBeastType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneMagicalBeastsOffHandBuff, Guids.BaneMagicalBeastsOffHandBuff));

      // OutsiderGood
      var outsiderGoodEnchantInfo = new WeaponEnchantInfo(BaneOutsiderGoodName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderGoodEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderGoodEffect,
          Guids.BaneOutsiderGoodEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Good),
        parentBuff: new(BaneOutsiderGoodBuff, Guids.BaneOutsiderGoodBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderGoodEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderGoodOffHandEffect,
          Guids.BaneOutsiderGoodOffHandEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Good,
          toPrimaryWeapon: false),
        variantBuff: new(BaneOutsiderGoodOffHandBuff, Guids.BaneOutsiderGoodOffHandBuff));

      // OutsiderEvil
      var outsiderEvilEnchantInfo = new WeaponEnchantInfo(BaneOutsiderEvilName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderEvilEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderEvilEffect,
          Guids.BaneOutsiderEvilEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Evil),
        parentBuff: new(BaneOutsiderEvilBuff, Guids.BaneOutsiderEvilBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderEvilEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderEvilOffHandEffect,
          Guids.BaneOutsiderEvilOffHandEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Evil,
          toPrimaryWeapon: false),
        variantBuff: new(BaneOutsiderEvilOffHandBuff, Guids.BaneOutsiderEvilOffHandBuff));

      // OutsiderLawful
      var outsiderLawfulEnchantInfo = new WeaponEnchantInfo(BaneOutsiderLawfulName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderLawfulEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderLawfulEffect,
          Guids.BaneOutsiderLawfulEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Lawful),
        parentBuff: new(BaneOutsiderLawfulBuff, Guids.BaneOutsiderLawfulBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderLawfulEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderLawfulOffHandEffect,
          Guids.BaneOutsiderLawfulOffHandEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Lawful,
          toPrimaryWeapon: false),
        variantBuff: new(BaneOutsiderLawfulOffHandBuff, Guids.BaneOutsiderLawfulOffHandBuff));

      // OutsiderChaotic
      var outsiderChaoticEnchantInfo = new WeaponEnchantInfo(BaneOutsiderChaoticName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderChaoticEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderChaoticEffect,
          Guids.BaneOutsiderChaoticEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Chaotic),
        parentBuff: new(BaneOutsiderChaoticBuff, Guids.BaneOutsiderChaoticBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderChaoticEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderChaoticOffHandEffect,
          Guids.BaneOutsiderChaoticOffHandEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Chaotic,
          toPrimaryWeapon: false),
        variantBuff: new(BaneOutsiderChaoticOffHandBuff, Guids.BaneOutsiderChaoticOffHandBuff));

      // OutsiderNeutral
      var outsiderNeutralEnchantInfo = new WeaponEnchantInfo(BaneOutsiderNeutralName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        outsiderNeutralEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderNeutralEffect,
          Guids.BaneOutsiderNeutralEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Neutral),
        parentBuff: new(BaneOutsiderNeutralBuff, Guids.BaneOutsiderNeutralBuff));
      EnchantTool.CreateVariantEnchant(
        outsiderNeutralEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneOutsiderNeutralOffHandEffect,
          Guids.BaneOutsiderNeutralOffHandEffect,
          FeatureRefs.OutsiderType.ToString(),
          alignment: AlignmentComponent.Neutral,
          toPrimaryWeapon: false),
        variantBuff: new(BaneOutsiderNeutralOffHandBuff, Guids.BaneOutsiderNeutralOffHandBuff));

      // Plants
      var plantsEnchantInfo = new WeaponEnchantInfo(BanePlantsName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        plantsEnchantInfo,
        effectBuff: GetBuffInfo(BanePlantsEffect, Guids.BanePlantsEffect, FeatureRefs.PlantType.ToString()),
        parentBuff: new(BanePlantsBuff, Guids.BanePlantsBuff));
      EnchantTool.CreateVariantEnchant(
        plantsEnchantInfo,
        effectBuff: GetBuffInfo(
          BanePlantsOffHandEffect,
          Guids.BanePlantsOffHandEffect,
          FeatureRefs.PlantType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BanePlantsOffHandBuff, Guids.BanePlantsOffHandBuff));

      // Undead
      var undeadEnchantInfo = new WeaponEnchantInfo(BaneUndeadName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        undeadEnchantInfo,
        effectBuff: GetBuffInfo(BaneUndeadEffect, Guids.BaneUndeadEffect, FeatureRefs.UndeadType.ToString()),
        parentBuff: new(BaneUndeadBuff, Guids.BaneUndeadBuff));
      EnchantTool.CreateVariantEnchant(
        undeadEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneUndeadOffHandEffect,
          Guids.BaneUndeadOffHandEffect,
          FeatureRefs.UndeadType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneUndeadOffHandBuff, Guids.BaneUndeadOffHandBuff));

      // Vermin
      var verminEnchantInfo = new WeaponEnchantInfo(BaneVerminName, Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        verminEnchantInfo,
        effectBuff: GetBuffInfo(BaneVerminEffect, Guids.BaneVerminEffect, FeatureRefs.VerminType.ToString()),
        parentBuff: new(BaneVerminBuff, Guids.BaneVerminBuff));
      EnchantTool.CreateVariantEnchant(
        verminEnchantInfo,
        effectBuff: GetBuffInfo(
          BaneVerminOffHandEffect,
          Guids.BaneVerminOffHandEffect,
          FeatureRefs.VerminType.ToString(),
          toPrimaryWeapon: false),
        variantBuff: new(BaneVerminOffHandBuff, Guids.BaneVerminOffHandBuff));

      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneAberration, aberrationsEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneAnimal, animalsEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneConstruct, constructsEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneDragon, dragonsEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneFey, feyEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneHumanoidGiant, humanoidGiantEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneMonstrousHumanoid, humanoidMonstrousEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneHumanoidReptilian, humanoidReptilianEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneMagicalBeast, magicalBeastsEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderChaotic, outsiderChaoticEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderEvil, outsiderEvilEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderGood, outsiderGoodEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderLawful, outsiderLawfulEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderNeutral, outsiderNeutralEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BanePlant, plantsEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneUndead, undeadEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneVermin, verminEnchantInfo);
    }

    private static BlueprintInfo GetBuffInfo(
      string name,
      string guid,
      Blueprint<BlueprintFeatureReference> typeFeature,
      AlignmentComponent? alignment = null,
      bool toPrimaryWeapon = true)
    {
      return new(name, guid, new BaneComponent(typeFeature.Reference, alignment, toPrimaryWeapon));
    }

    [TypeId("fbfd28cf-37f9-4160-85bc-8e4425d7691e")]
    private class BaneComponent :
      UnitBuffComponentDelegate,
      IInitiatorRulebookHandler<RuleCalculateAttackBonus>,
      IInitiatorRulebookHandler<RulePrepareDamage>
    {
      private readonly BlueprintFeatureReference RequisiteFeature;
      private readonly AlignmentComponent? Alignment;
      private readonly DamageDescription Damage;
      private readonly bool ToPrimaryWeapon;

      internal BaneComponent(
        BlueprintFeatureReference requisiteFeature, AlignmentComponent? alignment, bool toPrimaryWeapon)
      {
        RequisiteFeature = requisiteFeature;
        Alignment = alignment;
        Damage = new DamageDescription
        {
          Dice = new(rollsCount: 2, diceType: DiceType.D6),
          TypeDescription = DamageTypes.Force()
        };
        ToPrimaryWeapon = toPrimaryWeapon;
      }

      public void OnEventAboutToTrigger(RuleCalculateAttackBonus evt)
      {
        try
        {
          var isPrimaryWeapon = Common.IsPrimaryWeapon(evt.Weapon);
          if (ToPrimaryWeapon && !isPrimaryWeapon || !ToPrimaryWeapon && isPrimaryWeapon)
          {
            Logger.Verbose(() => $"Wrong weapon: {ToPrimaryWeapon} - {isPrimaryWeapon} - {evt.Weapon.Name}");
            return;
          }

          var target = evt.Target;
          if (target is null)
          {
            Logger.Warning("No target! (attack w/ weapon)");
            return;
          }

          if (Alignment is not null && !target.Alignment.ValueRaw.HasComponent(Alignment.Value))
          {
            Logger.Verbose(() => $"Bane does not apply: {target.Alignment}, {Alignment} required");
            return;
          }

          if (!target.HasFact(RequisiteFeature))
          {
            Logger.Verbose(() => $"Bane does not apply, target does not have {RequisiteFeature}");
            return;
          }

          evt.AddModifier(2, Fact, ModifierDescriptor.UntypedStackable);
        }
        catch (Exception e)
        {
          Logger.LogException("BaneComponent.OnEventAboutToTrigger(RuleAttackWithWeapon)", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateAttackBonus evt) { }

      public void OnEventAboutToTrigger(RulePrepareDamage evt)
      {
        try
        {
          var weapon = evt.ParentRule.AttackRoll?.Weapon;
          if (weapon is null)
            return;

          var isPrimaryWeapon = Common.IsPrimaryWeapon(weapon);
          if (ToPrimaryWeapon && !isPrimaryWeapon || !ToPrimaryWeapon && isPrimaryWeapon)
          {
            Logger.Verbose(() => $"Wrong weapon: {ToPrimaryWeapon} - {isPrimaryWeapon} - {weapon.Name}");
            return;
          }

          var target = evt.Target;
          if (target is null)
          {
            Logger.Warning("No target! (damage)");
            return;
          }

          if (Alignment is not null && !target.Alignment.ValueRaw.HasComponent(Alignment.Value))
          {
            Logger.Verbose(() => $"Bane does not apply: {target.Alignment}, {Alignment} required");
            return;
          }

          if (!target.HasFact(RequisiteFeature))
          {
            Logger.Verbose(() => $"Bane does not apply, target does not have {RequisiteFeature}");
            return;
          }

          evt.Add(Damage.CreateDamage());
        }
        catch (Exception e)
        {
          Logger.LogException("BaneComponent.OnEventAboutToTrigger(RulePrepareDamage)", e);
        }
      }

      public void OnEventDidTrigger(RulePrepareDamage evt) { }
    }
  }
}