using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Features;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
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
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using System;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Bane
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bane));

    #region Too Many Constants
    private const string BaneName = "LegendaryWeapon.Bane";

    private const string BaneAbility = "LegendaryWeapon.Bane.Ability";
    private const string BaneAbilityName = "LegendaryWeapon.Bane.Ability.Name";
    private const string BaneOffHandAbility = "LegendaryWeapon.Bane.OffHand.Ability";
    private const string BaneOffHandAbilityName = "LegendaryOffHand.Bane.Ability.Name";

    private const string BaneAberrationsName = "LegendaryWeapon.Bane.Aberrations.Name";
    private const string BaneAberrationsAbility = "LegendaryWeapon.Bane.Aberrations.Ability";
    private const string BaneAberrationsOffHandAbility = "LegendaryWeapon.Bane.Aberrations.OffHand.Ability";
    private const string BaneAberrationsBuff = "LegendaryWeapon.Bane.Aberrations.Buff";
    private const string BaneAberrationsOffHandBuff = "LegendaryWeapon.Bane.Aberrations.OffHand.Buff";

    private const string BaneAnimalsName = "LegendaryWeapon.Bane.Animals.Name";
    private const string BaneAnimalsAbility = "LegendaryWeapon.Bane.Animals.Ability";
    private const string BaneAnimalsOffHandAbility = "LegendaryWeapon.Bane.Animals.OffHand.Ability";
    private const string BaneAnimalsBuff = "LegendaryWeapon.Bane.Animals.Buff";
    private const string BaneAnimalsOffHandBuff = "LegendaryWeapon.Bane.Animals.OffHand.Buff";

    private const string BaneConstructsName = "LegendaryWeapon.Bane.Constructs.Name";
    private const string BaneConstructsAbility = "LegendaryWeapon.Bane.Constructs.Ability";
    private const string BaneConstructsOffHandAbility = "LegendaryWeapon.Bane.Constructs.OffHand.Ability";
    private const string BaneConstructsBuff = "LegendaryWeapon.Bane.Constructs.Buff";
    private const string BaneConstructsOffHandBuff = "LegendaryWeapon.Bane.Constructs.OffHand.Buff";

    private const string BaneDragonsName = "LegendaryWeapon.Bane.Dragons.Name";
    private const string BaneDragonsAbility = "LegendaryWeapon.Bane.Dragons.Ability";
    private const string BaneDragonsOffHandAbility = "LegendaryWeapon.Bane.Dragons.OffHand.Ability";
    private const string BaneDragonsBuff = "LegendaryWeapon.Bane.Dragons.Buff";
    private const string BaneDragonsOffHandBuff = "LegendaryWeapon.Bane.Dragons.OffHand.Buff";

    private const string BaneFeyName = "LegendaryWeapon.Bane.Fey.Name";
    private const string BaneFeyAbility = "LegendaryWeapon.Bane.Fey.Ability";
    private const string BaneFeyOffHandAbility = "LegendaryWeapon.Bane.Fey.OffHand.Ability";
    private const string BaneFeyBuff = "LegendaryWeapon.Bane.Fey.Buff";
    private const string BaneFeyOffHandBuff = "LegendaryWeapon.Bane.Fey.OffHand.Buff";

    private const string BaneHumanoidGiantName = "LegendaryWeapon.Bane.Humanoid.Giant.Name";
    private const string BaneHumanoidGiantAbility = "LegendaryWeapon.Bane.Humanoid.Giant.Ability";
    private const string BaneHumanoidGiantOffHandAbility = "LegendaryWeapon.Bane.Humanoid.Giant.OffHand.Ability";
    private const string BaneHumanoidGiantBuff = "LegendaryWeapon.Bane.Humanoid.Giant.Buff";
    private const string BaneHumanoidGiantOffHandBuff = "LegendaryWeapon.Bane.Humanoid.Giant.OffHand.Buff";

    private const string BaneHumanoidReptilianName = "LegendaryWeapon.Bane.Humanoid.Reptilian.Name";
    private const string BaneHumanoidReptilianAbility = "LegendaryWeapon.Bane.Humanoid.Reptilian.Ability";
    private const string BaneHumanoidReptilianOffHandAbility = "LegendaryWeapon.Bane.Humanoid.Reptilian.OffHand.Ability";
    private const string BaneHumanoidReptilianBuff = "LegendaryWeapon.Bane.Humanoid.Reptilian.Buff";
    private const string BaneHumanoidReptilianOffHandBuff = "LegendaryWeapon.Bane.Humanoid.Reptilian.OffHand.Buff";

    private const string BaneHumanoidMonstrousName = "LegendaryWeapon.Bane.Humanoid.Monstrous.Name";
    private const string BaneHumanoidMonstrousAbility = "LegendaryWeapon.Bane.Humanoid.Monstrous.Ability";
    private const string BaneHumanoidMonstrousOffHandAbility = "LegendaryWeapon.Bane.Humanoid.Monstrous.OffHand.Ability";
    private const string BaneHumanoidMonstrousBuff = "LegendaryWeapon.Bane.Humanoid.Monstrous.Buff";
    private const string BaneHumanoidMonstrousOffHandBuff = "LegendaryWeapon.Bane.Humanoid.Monstrous.OffHand.Buff";

    private const string BaneMagicalBeastsName = "LegendaryWeapon.Bane.MagicalBeasts.Name";
    private const string BaneMagicalBeastsAbility = "LegendaryWeapon.Bane.MagicalBeasts.Ability";
    private const string BaneMagicalBeastsOffHandAbility = "LegendaryWeapon.Bane.MagicalBeasts.OffHand.Ability";
    private const string BaneMagicalBeastsBuff = "LegendaryWeapon.Bane.MagicalBeasts.Buff";
    private const string BaneMagicalBeastsOffHandBuff = "LegendaryWeapon.Bane.MagicalBeasts.OffHand.Buff";

    private const string BaneOutsiderGoodName = "LegendaryWeapon.Bane.Outsider.Good.Name";
    private const string BaneOutsiderGoodAbility = "LegendaryWeapon.Bane.Outsider.Good.Ability";
    private const string BaneOutsiderGoodOffHandAbility = "LegendaryWeapon.Bane.Outsider.Good.OffHand.Ability";
    private const string BaneOutsiderGoodBuff = "LegendaryWeapon.Bane.Outsider.Good.Buff";
    private const string BaneOutsiderGoodOffHandBuff = "LegendaryWeapon.Bane.Outsider.Good.OffHand.Buff";

    private const string BaneOutsiderEvilName = "LegendaryWeapon.Bane.Outsider.Evil.Name";
    private const string BaneOutsiderEvilAbility = "LegendaryWeapon.Bane.Outsider.Evil.Ability";
    private const string BaneOutsiderEvilOffHandAbility = "LegendaryWeapon.Bane.Outsider.Evil.OffHand.Ability";
    private const string BaneOutsiderEvilBuff = "LegendaryWeapon.Bane.Outsider.Evil.Buff";
    private const string BaneOutsiderEvilOffHandBuff = "LegendaryWeapon.Bane.Outsider.Evil.OffHand.Buff";

    private const string BaneOutsiderLawfulName = "LegendaryWeapon.Bane.Outsider.Lawful.Name";
    private const string BaneOutsiderLawfulAbility = "LegendaryWeapon.Bane.Outsider.Lawful.Ability";
    private const string BaneOutsiderLawfulOffHandAbility = "LegendaryWeapon.Bane.Outsider.Lawful.OffHand.Ability";
    private const string BaneOutsiderLawfulBuff = "LegendaryWeapon.Bane.Outsider.Lawful.Buff";
    private const string BaneOutsiderLawfulOffHandBuff = "LegendaryWeapon.Bane.Outsider.Lawful.OffHand.Buff";

    private const string BaneOutsiderChaoticName = "LegendaryWeapon.Bane.Outsider.Chaotic.Name";
    private const string BaneOutsiderChaoticAbility = "LegendaryWeapon.Bane.Outsider.Chaotic.Ability";
    private const string BaneOutsiderChaoticOffHandAbility = "LegendaryWeapon.Bane.Outsider.Chaotic.OffHand.Ability";
    private const string BaneOutsiderChaoticBuff = "LegendaryWeapon.Bane.Outsider.Chaotic.Buff";
    private const string BaneOutsiderChaoticOffHandBuff = "LegendaryWeapon.Bane.Outsider.Chaotic.OffHand.Buff";

    private const string BaneOutsiderNeutralName = "LegendaryWeapon.Bane.Outsider.Neutral.Name";
    private const string BaneOutsiderNeutralAbility = "LegendaryWeapon.Bane.Outsider.Neutral.Ability";
    private const string BaneOutsiderNeutralOffHandAbility = "LegendaryWeapon.Bane.Outsider.Neutral.OffHand.Ability";
    private const string BaneOutsiderNeutralBuff = "LegendaryWeapon.Bane.Outsider.Neutral.Buff";
    private const string BaneOutsiderNeutralOffHandBuff = "LegendaryWeapon.Bane.Outsider.Neutral.OffHand.Buff";

    private const string BanePlantsName = "LegendaryWeapon.Bane.Plants.Name";
    private const string BanePlantsAbility = "LegendaryWeapon.Bane.Plants.Ability";
    private const string BanePlantsOffHandAbility = "LegendaryWeapon.Bane.Plants.OffHand.Ability";
    private const string BanePlantsBuff = "LegendaryWeapon.Bane.Plants.Buff";
    private const string BanePlantsOffHandBuff = "LegendaryWeapon.Bane.Plants.OffHand.Buff";

    private const string BaneUndeadName = "LegendaryWeapon.Bane.Undead.Name";
    private const string BaneUndeadAbility = "LegendaryWeapon.Bane.Undead.Ability";
    private const string BaneUndeadOffHandAbility = "LegendaryWeapon.Bane.Undead.OffHand.Ability";
    private const string BaneUndeadBuff = "LegendaryWeapon.Bane.Undead.Buff";
    private const string BaneUndeadOffHandBuff = "LegendaryWeapon.Bane.Undead.OffHand.Buff";

    private const string BaneVerminName = "LegendaryWeapon.Bane.Vermin.Name";
    private const string BaneVerminAbility = "LegendaryWeapon.Bane.Vermin.Ability";
    private const string BaneVerminOffHandAbility = "LegendaryWeapon.Bane.Vermin.OffHand.Ability";
    private const string BaneVerminBuff = "LegendaryWeapon.Bane.Vermin.Buff";
    private const string BaneVerminOffHandBuff = "LegendaryWeapon.Bane.Vermin.OffHand.Buff";
    #endregion

    private const string DisplayName = "LegendaryWeapon.Bane.Name";
    private const string Description = "LegendaryWeapon.Bane.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Bane");

      var parent = ActivatableAbilityConfigurator.New(BaneAbility, Guids.BaneParent)
        .SetDisplayName(BaneAbilityName)
        .SetDescription(LegendaryWeapon.LegendaryWeaponAbilityDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants:
            new()
            {
              Guids.BaneAberrationsAbility,
              Guids.BaneAnimalsAbility,
              Guids.BaneConstructsAbility,
              Guids.BaneDragonsAbility,
              Guids.BaneFeyAbility,
              Guids.BaneHumanoidGiantAbility,
              Guids.BaneHumanoidMonstrousAbility,
              Guids.BaneHumanoidReptilianAbility,
              Guids.BaneMagicalBeastsAbility,
              Guids.BaneOutsiderChaoticAbility,
              Guids.BaneOutsiderEvilAbility,
              Guids.BaneOutsiderGoodAbility,
              Guids.BaneOutsiderLawfulAbility,
              Guids.BaneOutsiderNeutralAbility,
              Guids.BanePlantsAbility,
              Guids.BaneUndeadAbility,
              Guids.BaneVerminAbility,
            })
        .AddActivationDisable()
        .Configure();
      var offHandParent = ActivatableAbilityConfigurator.New(BaneOffHandAbility, Guids.BaneOffHandParent)
        .SetDisplayName(BaneOffHandAbilityName)
        .SetDescription(LegendaryWeapon.LegendaryOffHandDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants:
            new()
            {
              Guids.BaneAberrationsOffHandAbility,
              Guids.BaneAnimalsOffHandAbility,
              Guids.BaneConstructsOffHandAbility,
              Guids.BaneDragonsOffHandAbility,
              Guids.BaneFeyOffHandAbility,
              Guids.BaneHumanoidGiantOffHandAbility,
              Guids.BaneHumanoidMonstrousOffHandAbility,
              Guids.BaneHumanoidReptilianOffHandAbility,
              Guids.BaneMagicalBeastsOffHandAbility,
              Guids.BaneOutsiderChaoticOffHandAbility,
              Guids.BaneOutsiderEvilOffHandAbility,
              Guids.BaneOutsiderGoodOffHandAbility,
              Guids.BaneOutsiderLawfulOffHandAbility,
              Guids.BaneOutsiderNeutralOffHandAbility,
              Guids.BanePlantsOffHandAbility,
              Guids.BaneUndeadOffHandAbility,
              Guids.BaneVerminOffHandAbility,
            })
        .AddActivationDisable()
        .Configure();

      var aberrationsEnchantInfo =
        new WeaponEnchantInfo(
          BaneAberrationsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var aberrationsBuffInfo = new BlueprintInfo(BaneAberrationsBuff, Guids.BaneAberrationsBuff);
      var aberrationsAbilityInfo = new BlueprintInfo(BaneAberrationsAbility, Guids.BaneAberrationsAbility);

      var aberrations = EnchantmentTool.CreateEnchantAbility(
        aberrationsEnchantInfo,
        ConfigureBuff(aberrationsEnchantInfo, aberrationsBuffInfo, FeatureRefs.AberrationType.ToString()),
        aberrationsAbilityInfo);
      var aberrationsOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        aberrationsEnchantInfo,
        aberrations,
        buff: new(BaneAberrationsOffHandBuff, Guids.BaneAberrationsOffHandBuff),
        ability: new(BaneAberrationsOffHandAbility, Guids.BaneAberrationsOffHandAbility));

      var animalsEnchantInfo =
        new WeaponEnchantInfo(
          BaneAnimalsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var animalsBuffInfo = new BlueprintInfo(BaneAnimalsBuff, Guids.BaneAnimalsBuff);
      var animalsAbilityInfo = new BlueprintInfo(BaneAnimalsAbility, Guids.BaneAnimalsAbility);

      var animals = EnchantmentTool.CreateEnchantAbility(
        animalsEnchantInfo,
        ConfigureBuff(animalsEnchantInfo, animalsBuffInfo, FeatureRefs.AnimalType.ToString()),
        animalsAbilityInfo);
      var animalsOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        animalsEnchantInfo,
        animals,
        buff: new(BaneAnimalsOffHandBuff, Guids.BaneAnimalsOffHandBuff),
        ability: new(BaneAnimalsOffHandAbility, Guids.BaneAnimalsOffHandAbility));

      var constructsEnchantInfo =
        new WeaponEnchantInfo(
          BaneConstructsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var constructsBuffInfo = new BlueprintInfo(BaneConstructsBuff, Guids.BaneConstructsBuff);
      var constructsAbilityInfo = new BlueprintInfo(BaneConstructsAbility, Guids.BaneConstructsAbility);

      var constructs = EnchantmentTool.CreateEnchantAbility(
        constructsEnchantInfo,
        ConfigureBuff(constructsEnchantInfo, constructsBuffInfo, FeatureRefs.ConstructType.ToString()),
        constructsAbilityInfo);
      var constructsOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        constructsEnchantInfo,
        constructs,
        buff: new(BaneConstructsOffHandBuff, Guids.BaneConstructsOffHandBuff),
        ability: new(BaneConstructsOffHandAbility, Guids.BaneConstructsOffHandAbility));

      var dragonsEnchantInfo =
        new WeaponEnchantInfo(
          BaneDragonsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var dragonsBuffInfo = new BlueprintInfo(BaneDragonsBuff, Guids.BaneDragonsBuff);
      var dragonsAbilityInfo = new BlueprintInfo(BaneDragonsAbility, Guids.BaneDragonsAbility);

      var dragons = EnchantmentTool.CreateEnchantAbility(
        dragonsEnchantInfo,
        ConfigureBuff(dragonsEnchantInfo, dragonsBuffInfo, FeatureRefs.DragonType.ToString()),
        dragonsAbilityInfo);
      var dragonsOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        dragonsEnchantInfo,
        dragons,
        buff: new(BaneDragonsOffHandBuff, Guids.BaneDragonsOffHandBuff),
        ability: new(BaneDragonsOffHandAbility, Guids.BaneDragonsOffHandAbility));

      var feyEnchantInfo =
        new WeaponEnchantInfo(
          BaneFeyName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var feyBuffInfo = new BlueprintInfo(BaneFeyBuff, Guids.BaneFeyBuff);
      var feyAbilityInfo = new BlueprintInfo(BaneFeyAbility, Guids.BaneFeyAbility);

      var fey = EnchantmentTool.CreateEnchantAbility(
        feyEnchantInfo,
        ConfigureBuff(feyEnchantInfo, feyBuffInfo, FeatureRefs.FeyType.ToString()),
        feyAbilityInfo);
      var feyOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        feyEnchantInfo,
        fey,
        buff: new(BaneFeyOffHandBuff, Guids.BaneFeyOffHandBuff),
        ability: new(BaneFeyOffHandAbility, Guids.BaneFeyOffHandAbility));

      var humanoidGiantEnchantInfo =
        new WeaponEnchantInfo(
          BaneHumanoidGiantName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var humanoidGiantBuffInfo = new BlueprintInfo(BaneHumanoidGiantBuff, Guids.BaneHumanoidGiantBuff);
      var humanoidGiantAbilityInfo = new BlueprintInfo(BaneHumanoidGiantAbility, Guids.BaneHumanoidGiantAbility);

      var humanoidGiant = EnchantmentTool.CreateEnchantAbility(
        humanoidGiantEnchantInfo,
        ConfigureBuff(humanoidGiantEnchantInfo, humanoidGiantBuffInfo, FeatureRefs.GiantSubtype.ToString()),
        humanoidGiantAbilityInfo);
      var humanoidGiantOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        humanoidGiantEnchantInfo,
        humanoidGiant,
        buff: new(BaneHumanoidGiantOffHandBuff, Guids.BaneHumanoidGiantOffHandBuff),
        ability: new(BaneHumanoidGiantOffHandAbility, Guids.BaneHumanoidGiantOffHandAbility));

      var humanoidReptilianEnchantInfo =
        new WeaponEnchantInfo(
          BaneHumanoidReptilianName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var humanoidReptilianBuffInfo = new BlueprintInfo(BaneHumanoidReptilianBuff, Guids.BaneHumanoidReptilianBuff);
      var humanoidReptilianAbilityInfo = new BlueprintInfo(BaneHumanoidReptilianAbility, Guids.BaneHumanoidReptilianAbility);

      var humanoidReptilian = EnchantmentTool.CreateEnchantAbility(
        humanoidReptilianEnchantInfo,
        ConfigureBuff(humanoidReptilianEnchantInfo, humanoidReptilianBuffInfo, FeatureRefs.ReptilianSubtype.ToString()),
        humanoidReptilianAbilityInfo);
      var humanoidReptilianOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        humanoidReptilianEnchantInfo,
        humanoidReptilian,
        buff: new(BaneHumanoidReptilianOffHandBuff, Guids.BaneHumanoidReptilianOffHandBuff),
        ability: new(BaneHumanoidReptilianOffHandAbility, Guids.BaneHumanoidReptilianOffHandAbility));

      var humanoidMonstrousEnchantInfo =
        new WeaponEnchantInfo(
          BaneHumanoidMonstrousName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var humanoidMonstrousBuffInfo = new BlueprintInfo(BaneHumanoidMonstrousBuff, Guids.BaneHumanoidMonstrousBuff);
      var humanoidMonstrousAbilityInfo = new BlueprintInfo(BaneHumanoidMonstrousAbility, Guids.BaneHumanoidMonstrousAbility);

      var humanoidMonstrous = EnchantmentTool.CreateEnchantAbility(
        humanoidMonstrousEnchantInfo,
        ConfigureBuff(humanoidMonstrousEnchantInfo, humanoidMonstrousBuffInfo, FeatureRefs.MonstrousHumanoidType.ToString()),
        humanoidMonstrousAbilityInfo);
      var humanoidMonstrousOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        humanoidMonstrousEnchantInfo,
        humanoidMonstrous,
        buff: new(BaneHumanoidMonstrousOffHandBuff, Guids.BaneHumanoidMonstrousOffHandBuff),
        ability: new(BaneHumanoidMonstrousOffHandAbility, Guids.BaneHumanoidMonstrousOffHandAbility));

      var magicalBeastsEnchantInfo =
        new WeaponEnchantInfo(
          BaneMagicalBeastsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var magicalBeastsBuffInfo = new BlueprintInfo(BaneMagicalBeastsBuff, Guids.BaneMagicalBeastsBuff);
      var magicalBeastsAbilityInfo = new BlueprintInfo(BaneMagicalBeastsAbility, Guids.BaneMagicalBeastsAbility);

      var magicalBeasts = EnchantmentTool.CreateEnchantAbility(
        magicalBeastsEnchantInfo,
        ConfigureBuff(magicalBeastsEnchantInfo, magicalBeastsBuffInfo, FeatureRefs.MagicalBeastType.ToString()),
        magicalBeastsAbilityInfo);
      var magicalBeastsOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        magicalBeastsEnchantInfo,
        magicalBeasts,
        buff: new(BaneMagicalBeastsOffHandBuff, Guids.BaneMagicalBeastsOffHandBuff),
        ability: new(BaneMagicalBeastsOffHandAbility, Guids.BaneMagicalBeastsOffHandAbility));

      var outsiderGoodEnchantInfo =
        new WeaponEnchantInfo(
          BaneOutsiderGoodName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderGoodBuffInfo = new BlueprintInfo(BaneOutsiderGoodBuff, Guids.BaneOutsiderGoodBuff);
      var outsiderGoodAbilityInfo = new BlueprintInfo(BaneOutsiderGoodAbility, Guids.BaneOutsiderGoodAbility);

      var outsiderGood = EnchantmentTool.CreateEnchantAbility(
        outsiderGoodEnchantInfo,
        ConfigureBuff(outsiderGoodEnchantInfo, outsiderGoodBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Good),
        outsiderGoodAbilityInfo);
      var outsiderGoodOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        outsiderGoodEnchantInfo,
        outsiderGood,
        buff: new(BaneOutsiderGoodOffHandBuff, Guids.BaneOutsiderGoodOffHandBuff),
        ability: new(BaneOutsiderGoodOffHandAbility, Guids.BaneOutsiderGoodOffHandAbility));

      var outsiderEvilEnchantInfo =
        new WeaponEnchantInfo(
          BaneOutsiderEvilName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderEvilBuffInfo = new BlueprintInfo(BaneOutsiderEvilBuff, Guids.BaneOutsiderEvilBuff);
      var outsiderEvilAbilityInfo = new BlueprintInfo(BaneOutsiderEvilAbility, Guids.BaneOutsiderEvilAbility);

      var outsiderEvil = EnchantmentTool.CreateEnchantAbility(
        outsiderEvilEnchantInfo,
        ConfigureBuff(outsiderEvilEnchantInfo, outsiderEvilBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Evil),
        outsiderEvilAbilityInfo);
      var outsiderEvilOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        outsiderEvilEnchantInfo,
        outsiderEvil,
        buff: new(BaneOutsiderEvilOffHandBuff, Guids.BaneOutsiderEvilOffHandBuff),
        ability: new(BaneOutsiderEvilOffHandAbility, Guids.BaneOutsiderEvilOffHandAbility));

      var outsiderLawfulEnchantInfo =
        new WeaponEnchantInfo(
          BaneOutsiderLawfulName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderLawfulBuffInfo = new BlueprintInfo(BaneOutsiderLawfulBuff, Guids.BaneOutsiderLawfulBuff);
      var outsiderLawfulAbilityInfo = new BlueprintInfo(BaneOutsiderLawfulAbility, Guids.BaneOutsiderLawfulAbility);

      var outsiderLawful = EnchantmentTool.CreateEnchantAbility(
        outsiderLawfulEnchantInfo,
        ConfigureBuff(outsiderLawfulEnchantInfo, outsiderLawfulBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Lawful),
        outsiderLawfulAbilityInfo);
      var outsiderLawfulOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        outsiderLawfulEnchantInfo,
        outsiderLawful,
        buff: new(BaneOutsiderLawfulOffHandBuff, Guids.BaneOutsiderLawfulOffHandBuff),
        ability: new(BaneOutsiderLawfulOffHandAbility, Guids.BaneOutsiderLawfulOffHandAbility));

      var outsiderChaoticEnchantInfo =
        new WeaponEnchantInfo(
          BaneOutsiderChaoticName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderChaoticBuffInfo = new BlueprintInfo(BaneOutsiderChaoticBuff, Guids.BaneOutsiderChaoticBuff);
      var outsiderChaoticAbilityInfo = new BlueprintInfo(BaneOutsiderChaoticAbility, Guids.BaneOutsiderChaoticAbility);

      var outsiderChaotic = EnchantmentTool.CreateEnchantAbility(
        outsiderChaoticEnchantInfo,
        ConfigureBuff(outsiderChaoticEnchantInfo, outsiderChaoticBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Chaotic),
        outsiderChaoticAbilityInfo);
      var outsiderChaoticOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        outsiderChaoticEnchantInfo,
        outsiderChaotic,
        buff: new(BaneOutsiderChaoticOffHandBuff, Guids.BaneOutsiderChaoticOffHandBuff),
        ability: new(BaneOutsiderChaoticOffHandAbility, Guids.BaneOutsiderChaoticOffHandAbility));

      var outsiderNeutralEnchantInfo =
        new WeaponEnchantInfo(
          BaneOutsiderNeutralName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderNeutralBuffInfo = new BlueprintInfo(BaneOutsiderNeutralBuff, Guids.BaneOutsiderNeutralBuff);
      var outsiderNeutralAbilityInfo = new BlueprintInfo(BaneOutsiderNeutralAbility, Guids.BaneOutsiderNeutralAbility);

      var outsiderNeutral = EnchantmentTool.CreateEnchantAbility(
        outsiderNeutralEnchantInfo,
        ConfigureBuff(outsiderNeutralEnchantInfo, outsiderNeutralBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Neutral),
        outsiderNeutralAbilityInfo);
      var outsiderNeutralOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        outsiderNeutralEnchantInfo,
        outsiderNeutral,
        buff: new(BaneOutsiderNeutralOffHandBuff, Guids.BaneOutsiderNeutralOffHandBuff),
        ability: new(BaneOutsiderNeutralOffHandAbility, Guids.BaneOutsiderNeutralOffHandAbility));

      var plantsEnchantInfo =
        new WeaponEnchantInfo(
          BanePlantsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var plantsBuffInfo = new BlueprintInfo(BanePlantsBuff, Guids.BanePlantsBuff);
      var plantsAbilityInfo = new BlueprintInfo(BanePlantsAbility, Guids.BanePlantsAbility);

      var plants = EnchantmentTool.CreateEnchantAbility(
        plantsEnchantInfo,
        ConfigureBuff(plantsEnchantInfo, plantsBuffInfo, FeatureRefs.PlantType.ToString()),
        plantsAbilityInfo);
      var plantsOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        plantsEnchantInfo,
        plants,
        buff: new(BanePlantsOffHandBuff, Guids.BanePlantsOffHandBuff),
        ability: new(BanePlantsOffHandAbility, Guids.BanePlantsOffHandAbility));

      var undeadEnchantInfo =
        new WeaponEnchantInfo(
          BaneUndeadName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var undeadBuffInfo = new BlueprintInfo(BaneUndeadBuff, Guids.BaneUndeadBuff);
      var undeadAbilityInfo = new BlueprintInfo(BaneUndeadAbility, Guids.BaneUndeadAbility);

      var undead = EnchantmentTool.CreateEnchantAbility(
        undeadEnchantInfo,
        ConfigureBuff(undeadEnchantInfo, undeadBuffInfo, FeatureRefs.UndeadType.ToString()),
        undeadAbilityInfo);
      var undeadOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        undeadEnchantInfo,
        undead,
        buff: new(BaneUndeadOffHandBuff, Guids.BaneUndeadOffHandBuff),
        ability: new(BaneUndeadOffHandAbility, Guids.BaneUndeadOffHandAbility));

      var verminEnchantInfo =
        new WeaponEnchantInfo(
          BaneVerminName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var verminBuffInfo = new BlueprintInfo(BaneVerminBuff, Guids.BaneVerminBuff);
      var verminAbilityInfo = new BlueprintInfo(BaneVerminAbility, Guids.BaneVerminAbility);

      var vermin = EnchantmentTool.CreateEnchantAbility(
        verminEnchantInfo,
        ConfigureBuff(verminEnchantInfo, verminBuffInfo, FeatureRefs.VerminType.ToString()),
        verminAbilityInfo);
      var verminOffHand = EnchantmentTool.CreateEnchantOffHandVariant(
        verminEnchantInfo,
        vermin,
        buff: new(BaneVerminOffHandBuff, Guids.BaneVerminOffHandBuff),
        ability: new(BaneVerminOffHandAbility, Guids.BaneVerminOffHandAbility));

      return EnchantmentTool.CreateEnchantFeature(
        new WeaponEnchantInfo(DisplayName, Description, "", EnhancementCost, ranks: 1),
        new(BaneName, Guids.Bane),
        parent,
        offHandParent,
        aberrations,
        aberrationsOffHand,
        animals,
        animalsOffHand,
        constructs,
        constructsOffHand,
        dragons,
        dragonsOffHand,
        fey,
        feyOffHand,
        humanoidGiant,
        humanoidGiantOffHand,
        humanoidReptilian,
        humanoidReptilianOffHand,
        humanoidMonstrous,
        humanoidMonstrousOffHand,
        magicalBeasts,
        magicalBeastsOffHand,
        outsiderGood,
        outsiderGoodOffHand,
        outsiderEvil,
        outsiderEvilOffHand,
        outsiderLawful,
        outsiderLawfulOffHand,
        outsiderChaotic,
        outsiderChaoticOffHand,
        outsiderNeutral,
        outsiderNeutralOffHand,
        plants,
        plantsOffHand,
        undead,
        undeadOffHand,
        vermin,
        verminOffHand);
    }

    private static BlueprintBuff ConfigureBuff(
      WeaponEnchantInfo enchantInfo,
      BlueprintInfo buff,
      Blueprint<BlueprintFeatureReference> typeFeature,
      AlignmentComponent? alignment = null)
    {
      return BuffConfigurator.New(buff.Name, buff.Guid)
        .SetDisplayName(enchantInfo.DisplayName)
        .SetDescription(enchantInfo.Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(enchantInfo))
        .AddComponent(new BaneComponent(typeFeature.Reference, alignment))
        .Configure();
    }

    [TypeId("fbfd28cf-37f9-4160-85bc-8e4425d7691e")]
    private class BaneComponent :
      UnitBuffComponentDelegate,
      IInitiatorRulebookHandler<RuleCalculateWeaponStats>,
      IInitiatorRulebookHandler<RulePrepareDamage>
    {
      private readonly BlueprintFeatureReference RequisiteFeature;
      private readonly AlignmentComponent? Alignment;
      private readonly DamageDescription Damage;

      internal BaneComponent(BlueprintFeatureReference requisiteFeature, AlignmentComponent? alignment)
      {
        RequisiteFeature = requisiteFeature;
        Alignment = alignment;
        Damage = new DamageDescription
        {
          Dice = new(rollsCount: 2, diceType: DiceType.D6),
          TypeDescription = DamageTypes.Force()
        };
      }

      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          var target = evt.GetRuleTarget();
          if (target is null)
          {
            Logger.Warning("No target!");
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
          Logger.LogException("BaneComponent.OnEventAboutToTrigger(RuleCalculateWeaponStats)", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }

      public void OnEventAboutToTrigger(RulePrepareDamage evt)
      {
        try
        {
          var target = evt.GetRuleTarget();
          if (target is null)
          {
            Logger.Warning("No target!");
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