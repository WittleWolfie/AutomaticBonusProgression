using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Bane
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bane));

    #region Too Many Constants
    private const string BaneAberrationsName = "LW.Bane.Aberrations.Name";
    private const string BaneAberrationsEffect = "LW.Bane.Aberrations.Effect";
    private const string BaneAberrationsOffHandEffect = "LW.Bane.Aberrations.OffHand.Effect";
    private const string BaneAberrationsBuff = "LW.Bane.Aberrations.Buff";
    private const string BaneAberrationsOffHandBuff = "LW.Bane.Aberrations.OffHand.Buff";

    private const string BaneAnimalsName = "LW.Bane.Animals.Name";
    private const string BaneAnimalsEffect = "LW.Bane.Animals.Effect";
    private const string BaneAnimalsOffHandEffect = "LW.Bane.Animals.OffHand.Effect";
    private const string BaneAnimalsBuff = "LW.Bane.Animals.Buff";
    private const string BaneAnimalsOffHandBuff = "LW.Bane.Animals.OffHand.Buff";

    private const string BaneConstructsName = "LW.Bane.Constructs.Name";
    private const string BaneConstructsEffect = "LW.Bane.Constructs.Effect";
    private const string BaneConstructsOffHandEffect = "LW.Bane.Constructs.OffHand.Effect";
    private const string BaneConstructsBuff = "LW.Bane.Constructs.Buff";
    private const string BaneConstructsOffHandBuff = "LW.Bane.Constructs.OffHand.Buff";

    private const string BaneDragonsName = "LW.Bane.Dragons.Name";
    private const string BaneDragonsEffect = "LW.Bane.Dragons.Effect";
    private const string BaneDragonsOffHandEffect = "LW.Bane.Dragons.OffHand.Effect";
    private const string BaneDragonsBuff = "LW.Bane.Dragons.Buff";
    private const string BaneDragonsOffHandBuff = "LW.Bane.Dragons.OffHand.Buff";

    private const string BaneFeyName = "LW.Bane.Fey.Name";
    private const string BaneFeyEffect = "LW.Bane.Fey.Effect";
    private const string BaneFeyOffHandEffect = "LW.Bane.Fey.OffHand.Effect";
    private const string BaneFeyBuff = "LW.Bane.Fey.Buff";
    private const string BaneFeyOffHandBuff = "LW.Bane.Fey.OffHand.Buff";

    private const string BaneHumanoidGiantName = "LW.Bane.Humanoid.Giant.Name";
    private const string BaneHumanoidGiantEffect = "LW.Bane.Humanoid.Giant.Effect";
    private const string BaneHumanoidGiantOffHandEffect = "LW.Bane.Humanoid.Giant.OffHand.Effect";
    private const string BaneHumanoidGiantBuff = "LW.Bane.Humanoid.Giant.Buff";
    private const string BaneHumanoidGiantOffHandBuff = "LW.Bane.Humanoid.Giant.OffHand.Buff";

    private const string BaneHumanoidReptilianName = "LW.Bane.Humanoid.Reptilian.Name";
    private const string BaneHumanoidReptilianEffect = "LW.Bane.Humanoid.Reptilian.Effect";
    private const string BaneHumanoidReptilianOffHandEffect = "LW.Bane.Humanoid.Reptilian.OffHand.Effect";
    private const string BaneHumanoidReptilianBuff = "LW.Bane.Humanoid.Reptilian.Buff";
    private const string BaneHumanoidReptilianOffHandBuff = "LW.Bane.Humanoid.Reptilian.OffHand.Buff";

    private const string BaneHumanoidMonstrousName = "LW.Bane.Humanoid.Monstrous.Name";
    private const string BaneHumanoidMonstrousEffect = "LW.Bane.Humanoid.Monstrous.Effect";
    private const string BaneHumanoidMonstrousOffHandEffect = "LW.Bane.Humanoid.Monstrous.OffHand.Effect";
    private const string BaneHumanoidMonstrousBuff = "LW.Bane.Humanoid.Monstrous.Buff";
    private const string BaneHumanoidMonstrousOffHandBuff = "LW.Bane.Humanoid.Monstrous.OffHand.Buff";

    private const string BaneMagicalBeastsName = "LW.Bane.MagicalBeasts.Name";
    private const string BaneMagicalBeastsEffect = "LW.Bane.MagicalBeasts.Effect";
    private const string BaneMagicalBeastsOffHandEffect = "LW.Bane.MagicalBeasts.OffHand.Effect";
    private const string BaneMagicalBeastsBuff = "LW.Bane.MagicalBeasts.Buff";
    private const string BaneMagicalBeastsOffHandBuff = "LW.Bane.MagicalBeasts.OffHand.Buff";

    private const string BaneOutsiderGoodName = "LW.Bane.Outsider.Good.Name";
    private const string BaneOutsiderGoodEffect = "LW.Bane.Outsider.Good.Effect";
    private const string BaneOutsiderGoodOffHandEffect = "LW.Bane.Outsider.Good.OffHand.Effect";
    private const string BaneOutsiderGoodBuff = "LW.Bane.Outsider.Good.Buff";
    private const string BaneOutsiderGoodOffHandBuff = "LW.Bane.Outsider.Good.OffHand.Buff";

    private const string BaneOutsiderEvilName = "LW.Bane.Outsider.Evil.Name";
    private const string BaneOutsiderEvilEffect = "LW.Bane.Outsider.Evil.Effect";
    private const string BaneOutsiderEvilOffHandEffect = "LW.Bane.Outsider.Evil.OffHand.Effect";
    private const string BaneOutsiderEvilBuff = "LW.Bane.Outsider.Evil.Buff";
    private const string BaneOutsiderEvilOffHandBuff = "LW.Bane.Outsider.Evil.OffHand.Buff";

    private const string BaneOutsiderLawfulName = "LW.Bane.Outsider.Lawful.Name";
    private const string BaneOutsiderLawfulEffect = "LW.Bane.Outsider.Lawful.Effect";
    private const string BaneOutsiderLawfulOffHandEffect = "LW.Bane.Outsider.Lawful.OffHand.Effect";
    private const string BaneOutsiderLawfulBuff = "LW.Bane.Outsider.Lawful.Buff";
    private const string BaneOutsiderLawfulOffHandBuff = "LW.Bane.Outsider.Lawful.OffHand.Buff";

    private const string BaneOutsiderChaoticName = "LW.Bane.Outsider.Chaotic.Name";
    private const string BaneOutsiderChaoticEffect = "LW.Bane.Outsider.Chaotic.Effect";
    private const string BaneOutsiderChaoticOffHandEffect = "LW.Bane.Outsider.Chaotic.OffHand.Effect";
    private const string BaneOutsiderChaoticBuff = "LW.Bane.Outsider.Chaotic.Buff";
    private const string BaneOutsiderChaoticOffHandBuff = "LW.Bane.Outsider.Chaotic.OffHand.Buff";

    private const string BaneOutsiderNeutralName = "LW.Bane.Outsider.Neutral.Name";
    private const string BaneOutsiderNeutralEffect = "LW.Bane.Outsider.Neutral.Effect";
    private const string BaneOutsiderNeutralOffHandEffect = "LW.Bane.Outsider.Neutral.OffHand.Effect";
    private const string BaneOutsiderNeutralBuff = "LW.Bane.Outsider.Neutral.Buff";
    private const string BaneOutsiderNeutralOffHandBuff = "LW.Bane.Outsider.Neutral.OffHand.Buff";

    private const string BanePlantsName = "LW.Bane.Plants.Name";
    private const string BanePlantsEffect = "LW.Bane.Plants.Effect";
    private const string BanePlantsOffHandEffect = "LW.Bane.Plants.OffHand.Effect";
    private const string BanePlantsBuff = "LW.Bane.Plants.Buff";
    private const string BanePlantsOffHandBuff = "LW.Bane.Plants.OffHand.Buff";

    private const string BaneUndeadName = "LW.Bane.Undead.Name";
    private const string BaneUndeadEffect = "LW.Bane.Undead.Effect";
    private const string BaneUndeadOffHandEffect = "LW.Bane.Undead.OffHand.Effect";
    private const string BaneUndeadBuff = "LW.Bane.Undead.Buff";
    private const string BaneUndeadOffHandBuff = "LW.Bane.Undead.OffHand.Buff";

    private const string BaneVerminName = "LW.Bane.Vermin.Name";
    private const string BaneVerminEffect = "LW.Bane.Vermin.Effect";
    private const string BaneVerminOffHandEffect = "LW.Bane.Vermin.OffHand.Effect";
    private const string BaneVerminBuff = "LW.Bane.Vermin.Buff";
    private const string BaneVerminOffHandBuff = "LW.Bane.Vermin.OffHand.Buff";
    #endregion

    private const string Description = "LW.Bane.Description";
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
    private class BaneComponent : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>
    {
      private readonly BlueprintFeatureReference RequisiteFeature;
      private readonly AlignmentComponent? Alignment;
      private readonly bool ToPrimaryWeapon;

      internal BaneComponent(
        BlueprintFeatureReference requisiteFeature, AlignmentComponent? alignment, bool toPrimaryWeapon)
      {
        RequisiteFeature = requisiteFeature;
        Alignment = alignment;
        ToPrimaryWeapon = toPrimaryWeapon;
      }

      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          if (evt.AttackWithWeapon is null)
            return;

          var isPrimaryWeapon = Common.IsPrimaryWeapon(evt.Weapon);
          if (ToPrimaryWeapon && !isPrimaryWeapon || !ToPrimaryWeapon && isPrimaryWeapon)
          {
            Logger.Verbose(() => $"Wrong weapon: {ToPrimaryWeapon} - {isPrimaryWeapon} - {evt.Weapon.Name}");
            return;
          }

          var target = evt.AttackWithWeapon.Target;
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

          evt.Enhancement.AddExtraModifier(new(2, Fact, ModifierDescriptor.UntypedStackable));
        }
        catch (Exception e)
        {
          Logger.LogException("BaneComponent.OnEventAboutToTrigger(RuleCalculateWeaponStats)", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }
    }
  }
}