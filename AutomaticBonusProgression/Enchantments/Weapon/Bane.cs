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

    private const string DisplayName = "LegendaryWeapon.Bane.Name";
    private const string Description = "LegendaryWeapon.Bane.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Bane");

      //var aberrationsEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneAberrationsName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var aberrations = EnchantTool.CreateEnchantAbility(
      //  aberrationsEnchantInfo,
      //  GetBuffInfo(BaneAberrationsBuff, Guids.BaneAberrationsBuff, FeatureRefs.AberrationType.ToString()),
      //  new(BaneAberrationsEffect, Guids.BaneAberrationsEffect));
      //var aberrationsOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  aberrationsEnchantInfo,
      //  GetBuffInfo(BaneAberrationsOffHandBuff, Guids.BaneAberrationsOffHandBuff, FeatureRefs.AberrationType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneAberrationsOffHandEffect, Guids.BaneAberrationsOffHandEffect));

      //var animalsEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneAnimalsName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var animals = EnchantTool.CreateEnchantAbility(
      //  animalsEnchantInfo,
      //  GetBuffInfo(BaneAnimalsBuff, Guids.BaneAnimalsBuff, FeatureRefs.AnimalType.ToString()),
      //  new(BaneAnimalsAbility, Guids.BaneAnimalsAbility));
      //var animalsOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  animalsEnchantInfo,
      //  GetBuffInfo(BaneAnimalsOffHandBuff, Guids.BaneAnimalsOffHandBuff, FeatureRefs.AnimalType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneAnimalsOffHandAbility, Guids.BaneAnimalsOffHandAbility));

      //var constructsEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneConstructsName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var constructs = EnchantTool.CreateEnchantAbility(
      //  constructsEnchantInfo,
      //  GetBuffInfo(BaneConstructsBuff, Guids.BaneConstructsBuff, FeatureRefs.ConstructType.ToString()),
      //  new(BaneConstructsAbility, Guids.BaneConstructsAbility));
      //var constructsOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  constructsEnchantInfo,
      //  GetBuffInfo(BaneConstructsOffHandBuff, Guids.BaneConstructsOffHandBuff, FeatureRefs.ConstructType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneConstructsOffHandAbility, Guids.BaneConstructsOffHandAbility));

      //var dragonsEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneDragonsName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var dragons = EnchantTool.CreateEnchantAbility(
      //  dragonsEnchantInfo,
      //  GetBuffInfo(BaneDragonsBuff, Guids.BaneDragonsBuff, FeatureRefs.DragonType.ToString()),
      //  new(BaneDragonsAbility, Guids.BaneDragonsAbility));
      //var dragonsOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  dragonsEnchantInfo,
      //  GetBuffInfo(BaneDragonsOffHandBuff, Guids.BaneDragonsOffHandBuff, FeatureRefs.DragonType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneDragonsOffHandAbility, Guids.BaneDragonsOffHandAbility));

      //var feyEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneFeyName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var fey = EnchantTool.CreateEnchantAbility(
      //  feyEnchantInfo,
      //  GetBuffInfo(BaneFeyBuff, Guids.BaneFeyBuff, FeatureRefs.FeyType.ToString()),
      //  new(BaneFeyAbility, Guids.BaneFeyAbility));
      //var feyOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  feyEnchantInfo,
      //  GetBuffInfo(BaneFeyOffHandBuff, Guids.BaneFeyOffHandBuff, FeatureRefs.FeyType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneFeyOffHandAbility, Guids.BaneFeyOffHandAbility));

      //var humanoidGiantEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneHumanoidGiantName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var humanoidGiant = EnchantTool.CreateEnchantAbility(
      //  humanoidGiantEnchantInfo,
      //  GetBuffInfo(BaneHumanoidGiantBuff, Guids.BaneHumanoidGiantBuff, FeatureRefs.GiantSubtype.ToString()),
      //  new(BaneHumanoidGiantAbility, Guids.BaneHumanoidGiantAbility));
      //var humanoidGiantOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  humanoidGiantEnchantInfo,
      //  GetBuffInfo(BaneHumanoidGiantOffHandBuff, Guids.BaneHumanoidGiantOffHandBuff, FeatureRefs.GiantSubtype.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneHumanoidGiantOffHandAbility, Guids.BaneHumanoidGiantOffHandAbility));

      //var humanoidReptilianEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneHumanoidReptilianName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var humanoidReptilian = EnchantTool.CreateEnchantAbility(
      //  humanoidReptilianEnchantInfo,
      //  GetBuffInfo(BaneHumanoidReptilianBuff, Guids.BaneHumanoidReptilianBuff, FeatureRefs.ReptilianSubtype.ToString()),
      //  new(BaneHumanoidReptilianAbility, Guids.BaneHumanoidReptilianAbility));
      //var humanoidReptilianOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  humanoidReptilianEnchantInfo,
      //  GetBuffInfo(BaneHumanoidReptilianOffHandBuff, Guids.BaneHumanoidReptilianOffHandBuff, FeatureRefs.ReptilianSubtype.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneHumanoidReptilianOffHandAbility, Guids.BaneHumanoidReptilianOffHandAbility));

      //var humanoidMonstrousEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneHumanoidMonstrousName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var humanoidMonstrous = EnchantTool.CreateEnchantAbility(
      //  humanoidMonstrousEnchantInfo,
      //  GetBuffInfo(BaneHumanoidMonstrousBuff, Guids.BaneHumanoidMonstrousBuff, FeatureRefs.MonstrousHumanoidType.ToString()),
      //  new(BaneHumanoidMonstrousAbility, Guids.BaneHumanoidMonstrousAbility));
      //var humanoidMonstrousOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  humanoidMonstrousEnchantInfo,
      //  GetBuffInfo(BaneHumanoidMonstrousOffHandBuff, Guids.BaneHumanoidMonstrousOffHandBuff, FeatureRefs.MonstrousHumanoidType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneHumanoidMonstrousOffHandAbility, Guids.BaneHumanoidMonstrousOffHandAbility));

      //var magicalBeastsEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneMagicalBeastsName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var magicalBeasts = EnchantTool.CreateEnchantAbility(
      //  magicalBeastsEnchantInfo,
      //  GetBuffInfo(BaneMagicalBeastsBuff, Guids.BaneMagicalBeastsBuff, FeatureRefs.MagicalBeastType.ToString()),
      //  new(BaneMagicalBeastsAbility, Guids.BaneMagicalBeastsAbility));
      //var magicalBeastsOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  magicalBeastsEnchantInfo,
      //  GetBuffInfo(BaneMagicalBeastsOffHandBuff, Guids.BaneMagicalBeastsOffHandBuff, FeatureRefs.MagicalBeastType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneMagicalBeastsOffHandAbility, Guids.BaneMagicalBeastsOffHandAbility));

      //var outsiderGoodEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneOutsiderGoodName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var outsiderGood = EnchantTool.CreateEnchantAbility(
      //  outsiderGoodEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderGoodBuff,
      //    Guids.BaneOutsiderGoodBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Chaotic),
      //  new(BaneOutsiderGoodAbility, Guids.BaneOutsiderGoodAbility));
      //var outsiderGoodOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  outsiderGoodEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderGoodOffHandBuff,
      //    Guids.BaneOutsiderGoodOffHandBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Good,
      //    toPrimaryWeapon: false),
      //  ability: new(BaneOutsiderGoodOffHandAbility, Guids.BaneOutsiderGoodOffHandAbility));

      //var outsiderEvilEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneOutsiderEvilName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var outsiderEvil = EnchantTool.CreateEnchantAbility(
      //  outsiderEvilEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderEvilBuff,
      //    Guids.BaneOutsiderEvilBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Chaotic),
      //  new(BaneOutsiderEvilAbility, Guids.BaneOutsiderEvilAbility));
      //var outsiderEvilOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  outsiderEvilEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderEvilOffHandBuff,
      //    Guids.BaneOutsiderEvilOffHandBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Evil,
      //    toPrimaryWeapon: false),
      //  ability: new(BaneOutsiderEvilOffHandAbility, Guids.BaneOutsiderEvilOffHandAbility));

      //var outsiderLawfulEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneOutsiderLawfulName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var outsiderLawful = EnchantTool.CreateEnchantAbility(
      //  outsiderLawfulEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderLawfulBuff,
      //    Guids.BaneOutsiderLawfulBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Chaotic),
      //  new(BaneOutsiderLawfulAbility, Guids.BaneOutsiderLawfulAbility));
      //var outsiderLawfulOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  outsiderLawfulEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderLawfulOffHandBuff,
      //    Guids.BaneOutsiderLawfulOffHandBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Lawful,
      //    toPrimaryWeapon: false),
      //  ability: new(BaneOutsiderLawfulOffHandAbility, Guids.BaneOutsiderLawfulOffHandAbility));

      //var outsiderChaoticEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneOutsiderChaoticName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var outsiderChaotic = EnchantTool.CreateEnchantAbility(
      //  outsiderChaoticEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderChaoticBuff,
      //    Guids.BaneOutsiderChaoticBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Chaotic),
      //  new(BaneOutsiderChaoticAbility, Guids.BaneOutsiderChaoticAbility));
      //var outsiderChaoticOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  outsiderChaoticEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderChaoticOffHandBuff,
      //    Guids.BaneOutsiderChaoticOffHandBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Chaotic,
      //    toPrimaryWeapon: false),
      //  ability: new(BaneOutsiderChaoticOffHandAbility, Guids.BaneOutsiderChaoticOffHandAbility));

      //var outsiderNeutralEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneOutsiderNeutralName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var outsiderNeutral = EnchantTool.CreateEnchantAbility(
      //  outsiderNeutralEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderNeutralBuff,
      //    Guids.BaneOutsiderNeutralBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Chaotic),
      //  new(BaneOutsiderNeutralAbility, Guids.BaneOutsiderNeutralAbility));
      //var outsiderNeutralOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  outsiderNeutralEnchantInfo,
      //  GetBuffInfo(
      //    BaneOutsiderNeutralOffHandBuff,
      //    Guids.BaneOutsiderNeutralOffHandBuff,
      //    FeatureRefs.OutsiderType.ToString(),
      //    alignment: AlignmentComponent.Neutral,
      //    toPrimaryWeapon: false),
      //  ability: new(BaneOutsiderNeutralOffHandAbility, Guids.BaneOutsiderNeutralOffHandAbility));

      //var plantsEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BanePlantsName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var plants = EnchantTool.CreateEnchantAbility(
      //  plantsEnchantInfo,
      //  GetBuffInfo(BanePlantsBuff, Guids.BanePlantsBuff, FeatureRefs.PlantType.ToString()),
      //  new(BanePlantsAbility, Guids.BanePlantsAbility));
      //var plantsOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  plantsEnchantInfo,
      //  GetBuffInfo(BanePlantsOffHandBuff, Guids.BanePlantsOffHandBuff, FeatureRefs.PlantType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BanePlantsOffHandAbility, Guids.BanePlantsOffHandAbility));

      //var undeadEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneUndeadName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var undead = EnchantTool.CreateEnchantAbility(
      //  undeadEnchantInfo,
      //  GetBuffInfo(BaneUndeadBuff, Guids.BaneUndeadBuff, FeatureRefs.UndeadType.ToString()),
      //  new(BaneUndeadAbility, Guids.BaneUndeadAbility));
      //var undeadOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  undeadEnchantInfo,
      //  GetBuffInfo(BaneUndeadOffHandBuff, Guids.BaneUndeadOffHandBuff, FeatureRefs.UndeadType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneUndeadOffHandAbility, Guids.BaneUndeadOffHandAbility));

      //var verminEnchantInfo =
      //  new WeaponEnchantInfo(
      //    BaneVerminName,
      //    Description,
      //    "",
      //    EnhancementCost,
      //    ranks: 1);

      //var vermin = EnchantTool.CreateEnchantAbility(
      //  verminEnchantInfo,
      //  GetBuffInfo(BaneVerminBuff, Guids.BaneVerminBuff, FeatureRefs.VerminType.ToString()),
      //  new(BaneVerminAbility, Guids.BaneVerminAbility));
      //var verminOffHand = EnchantTool.CreateEnchantOffHandVariant(
      //  verminEnchantInfo,
      //  GetBuffInfo(BaneVerminOffHandBuff, Guids.BaneVerminOffHandBuff, FeatureRefs.VerminType.ToString(), toPrimaryWeapon: false),
      //  ability: new(BaneVerminOffHandAbility, Guids.BaneVerminOffHandAbility));

      //var enchantInfo = new WeaponEnchantInfo(DisplayName, Description, "", EnhancementCost, ranks: 1);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneAberration, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneAnimal, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneConstruct, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneDragon, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneFey, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneHumanoidGiant, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneMonstrousHumanoid, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneHumanoidReptilian, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneMagicalBeast, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderChaotic, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderEvil, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderGood, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderLawful, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneOutsiderNeutral, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BanePlant, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneUndead, enchantInfo);
      //EnchantTool.AddEnhancementEquivalenceWeapon(WeaponEnchantmentRefs.BaneVermin, enchantInfo);

      //return EnchantTool.CreateEnchantFeature(
      //  enchantInfo,
      //  new(BaneName, Guids.Bane),
      //  parent,
      //  offHandParent,
      //  aberrations,
      //  aberrationsOffHand,
      //  animals,
      //  animalsOffHand,
      //  constructs,
      //  constructsOffHand,
      //  dragons,
      //  dragonsOffHand,
      //  fey,
      //  feyOffHand,
      //  humanoidGiant,
      //  humanoidGiantOffHand,
      //  humanoidReptilian,
      //  humanoidReptilianOffHand,
      //  humanoidMonstrous,
      //  humanoidMonstrousOffHand,
      //  magicalBeasts,
      //  magicalBeastsOffHand,
      //  outsiderGood,
      //  outsiderGoodOffHand,
      //  outsiderEvil,
      //  outsiderEvilOffHand,
      //  outsiderLawful,
      //  outsiderLawfulOffHand,
      //  outsiderChaotic,
      //  outsiderChaoticOffHand,
      //  outsiderNeutral,
      //  outsiderNeutralOffHand,
      //  plants,
      //  plantsOffHand,
      //  undead,
      //  undeadOffHand,
      //  vermin,
      //  verminOffHand);
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