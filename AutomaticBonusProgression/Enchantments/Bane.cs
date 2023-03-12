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
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using System;
using TabletopTweaks.Core.NewComponents.OwlcatReplacements.DamageResistance;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Bane
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bane));

    #region Too Many Constants
    private const string BaneName = "LegendaryArmor.Bane";

    private const string BaneAbility = "LegendaryArmor.Bane.Ability";
    private const string BaneAbilityName = "LegendaryArmor.Bane.Ability.Name";
    private const string BaneOffHandAbility = "LegendaryArmor.Bane.OffHand.Ability";
    private const string BaneOffHandAbilityName = "LegendaryOffHand.Bane.Ability.Name";

    private const string BaneAberrationsName = "LegendaryArmor.Bane.Aberrations.Name";
    private const string BaneAberrationsAbility = "LegendaryArmor.Bane.Aberrations.Ability";
    private const string BaneAberrationsOffHandAbility = "LegendaryArmor.Bane.Aberrations.OffHand.Ability";
    private const string BaneAberrationsBuff = "LegendaryArmor.Bane.Aberrations.Buff";
    private const string BaneAberrationsOffHandBuff = "LegendaryArmor.Bane.Aberrations.OffHand.Buff";

    private const string BaneAnimalsName = "LegendaryArmor.Bane.Animals.Name";
    private const string BaneAnimalsAbility = "LegendaryArmor.Bane.Animals.Ability";
    private const string BaneAnimalsOffHandAbility = "LegendaryArmor.Bane.Animals.OffHand.Ability";
    private const string BaneAnimalsBuff = "LegendaryArmor.Bane.Animals.Buff";
    private const string BaneAnimalsOffHandBuff = "LegendaryArmor.Bane.Animals.OffHand.Buff";

    private const string BaneConstructsName = "LegendaryArmor.Bane.Constructs.Name";
    private const string BaneConstructsAbility = "LegendaryArmor.Bane.Constructs.Ability";
    private const string BaneConstructsOffHandAbility = "LegendaryArmor.Bane.Constructs.OffHand.Ability";
    private const string BaneConstructsBuff = "LegendaryArmor.Bane.Constructs.Buff";
    private const string BaneConstructsOffHandBuff = "LegendaryArmor.Bane.Constructs.OffHand.Buff";

    private const string BaneDragonsName = "LegendaryArmor.Bane.Dragons.Name";
    private const string BaneDragonsAbility = "LegendaryArmor.Bane.Dragons.Ability";
    private const string BaneDragonsOffHandAbility = "LegendaryArmor.Bane.Dragons.OffHand.Ability";
    private const string BaneDragonsBuff = "LegendaryArmor.Bane.Dragons.Buff";
    private const string BaneDragonsOffHandBuff = "LegendaryArmor.Bane.Dragons.OffHand.Buff";

    private const string BaneFeyName = "LegendaryArmor.Bane.Fey.Name";
    private const string BaneFeyAbility = "LegendaryArmor.Bane.Fey.Ability";
    private const string BaneFeyOffHandAbility = "LegendaryArmor.Bane.Fey.OffHand.Ability";
    private const string BaneFeyBuff = "LegendaryArmor.Bane.Fey.Buff";
    private const string BaneFeyOffHandBuff = "LegendaryArmor.Bane.Fey.OffHand.Buff";

    private const string BaneHumanoidGiantName = "LegendaryArmor.Bane.Humanoid.Giant.Name";
    private const string BaneHumanoidGiantAbility = "LegendaryArmor.Bane.Humanoid.Giant.Ability";
    private const string BaneHumanoidGiantOffHandAbility = "LegendaryArmor.Bane.Humanoid.Giant.OffHand.Ability";
    private const string BaneHumanoidGiantBuff = "LegendaryArmor.Bane.Humanoid.Giant.Buff";
    private const string BaneHumanoidGiantOffHandBuff = "LegendaryArmor.Bane.Humanoid.Giant.OffHand.Buff";

    private const string BaneHumanoidReptilianName = "LegendaryArmor.Bane.Humanoid.Reptilian.Name";
    private const string BaneHumanoidReptilianAbility = "LegendaryArmor.Bane.Humanoid.Reptilian.Ability";
    private const string BaneHumanoidReptilianOffHandAbility = "LegendaryArmor.Bane.Humanoid.Reptilian.OffHand.Ability";
    private const string BaneHumanoidReptilianBuff = "LegendaryArmor.Bane.Humanoid.Reptilian.Buff";
    private const string BaneHumanoidReptilianOffHandBuff = "LegendaryArmor.Bane.Humanoid.Reptilian.OffHand.Buff";

    private const string BaneHumanoidMonstrousName = "LegendaryArmor.Bane.Humanoid.Monstrous.Name";
    private const string BaneHumanoidMonstrousAbility = "LegendaryArmor.Bane.Humanoid.Monstrous.Ability";
    private const string BaneHumanoidMonstrousOffHandAbility = "LegendaryArmor.Bane.Humanoid.Monstrous.OffHand.Ability";
    private const string BaneHumanoidMonstrousBuff = "LegendaryArmor.Bane.Humanoid.Monstrous.Buff";
    private const string BaneHumanoidMonstrousOffHandBuff = "LegendaryArmor.Bane.Humanoid.Monstrous.OffHand.Buff";

    private const string BaneMagicalBeastsName = "LegendaryArmor.Bane.MagicalBeasts.Name";
    private const string BaneMagicalBeastsAbility = "LegendaryArmor.Bane.MagicalBeasts.Ability";
    private const string BaneMagicalBeastsOffHandAbility = "LegendaryArmor.Bane.MagicalBeasts.OffHand.Ability";
    private const string BaneMagicalBeastsBuff = "LegendaryArmor.Bane.MagicalBeasts.Buff";
    private const string BaneMagicalBeastsOffHandBuff = "LegendaryArmor.Bane.MagicalBeasts.OffHand.Buff";

    private const string BaneOutsiderGoodName = "LegendaryArmor.Bane.Outsider.Good.Name";
    private const string BaneOutsiderGoodAbility = "LegendaryArmor.Bane.Outsider.Good.Ability";
    private const string BaneOutsiderGoodOffHandAbility = "LegendaryArmor.Bane.Outsider.Good.OffHand.Ability";
    private const string BaneOutsiderGoodBuff = "LegendaryArmor.Bane.Outsider.Good.Buff";
    private const string BaneOutsiderGoodOffHandBuff = "LegendaryArmor.Bane.Outsider.Good.OffHand.Buff";

    private const string BaneOutsiderEvilName = "LegendaryArmor.Bane.Outsider.Evil.Name";
    private const string BaneOutsiderEvilAbility = "LegendaryArmor.Bane.Outsider.Evil.Ability";
    private const string BaneOutsiderEvilOffHandAbility = "LegendaryArmor.Bane.Outsider.Evil.OffHand.Ability";
    private const string BaneOutsiderEvilBuff = "LegendaryArmor.Bane.Outsider.Evil.Buff";
    private const string BaneOutsiderEvilOffHandBuff = "LegendaryArmor.Bane.Outsider.Evil.OffHand.Buff";

    private const string BaneOutsiderLawfulName = "LegendaryArmor.Bane.Outsider.Lawful.Name";
    private const string BaneOutsiderLawfulAbility = "LegendaryArmor.Bane.Outsider.Lawful.Ability";
    private const string BaneOutsiderLawfulOffHandAbility = "LegendaryArmor.Bane.Outsider.Lawful.OffHand.Ability";
    private const string BaneOutsiderLawfulBuff = "LegendaryArmor.Bane.Outsider.Lawful.Buff";
    private const string BaneOutsiderLawfulOffHandBuff = "LegendaryArmor.Bane.Outsider.Lawful.OffHand.Buff";

    private const string BaneOutsiderChaoticName = "LegendaryArmor.Bane.Outsider.Chaotic.Name";
    private const string BaneOutsiderChaoticAbility = "LegendaryArmor.Bane.Outsider.Chaotic.Ability";
    private const string BaneOutsiderChaoticOffHandAbility = "LegendaryArmor.Bane.Outsider.Chaotic.OffHand.Ability";
    private const string BaneOutsiderChaoticBuff = "LegendaryArmor.Bane.Outsider.Chaotic.Buff";
    private const string BaneOutsiderChaoticOffHandBuff = "LegendaryArmor.Bane.Outsider.Chaotic.OffHand.Buff";

    private const string BaneOutsiderNeutralName = "LegendaryArmor.Bane.Outsider.Neutral.Name";
    private const string BaneOutsiderNeutralAbility = "LegendaryArmor.Bane.Outsider.Neutral.Ability";
    private const string BaneOutsiderNeutralOffHandAbility = "LegendaryArmor.Bane.Outsider.Neutral.OffHand.Ability";
    private const string BaneOutsiderNeutralBuff = "LegendaryArmor.Bane.Outsider.Neutral.Buff";
    private const string BaneOutsiderNeutralOffHandBuff = "LegendaryArmor.Bane.Outsider.Neutral.OffHand.Buff";

    private const string BanePlantsName = "LegendaryArmor.Bane.Plants.Name";
    private const string BanePlantsAbility = "LegendaryArmor.Bane.Plants.Ability";
    private const string BanePlantsOffHandAbility = "LegendaryArmor.Bane.Plants.OffHand.Ability";
    private const string BanePlantsBuff = "LegendaryArmor.Bane.Plants.Buff";
    private const string BanePlantsOffHandBuff = "LegendaryArmor.Bane.Plants.OffHand.Buff";

    private const string BaneUndeadName = "LegendaryArmor.Bane.Undead.Name";
    private const string BaneUndeadAbility = "LegendaryArmor.Bane.Undead.Ability";
    private const string BaneUndeadOffHandAbility = "LegendaryArmor.Bane.Undead.OffHand.Ability";
    private const string BaneUndeadBuff = "LegendaryArmor.Bane.Undead.Buff";
    private const string BaneUndeadOffHandBuff = "LegendaryArmor.Bane.Undead.OffHand.Buff";

    private const string BaneVerminName = "LegendaryArmor.Bane.Vermin.Name";
    private const string BaneVerminAbility = "LegendaryArmor.Bane.Vermin.Ability";
    private const string BaneVerminOffHandAbility = "LegendaryArmor.Bane.Vermin.OffHand.Ability";
    private const string BaneVerminBuff = "LegendaryArmor.Bane.Vermin.Buff";
    private const string BaneVerminOffHandBuff = "LegendaryArmor.Bane.Vermin.OffHand.Buff";
    #endregion

    private const string DisplayName = "LegendaryArmor.Bane.Name";
    private const string Description = "LegendaryArmor.Bane.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Bane");

      var parent = ActivatableAbilityConfigurator.New(BaneAbility, Guids.BaneParent)
        .SetDisplayName(BaneAbilityName)
        .SetDescription(LegendaryArmor.LegendaryArmorAbilityDescription)
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
        .AddComponent(new BaneResistanceComponent(typeFeature.Reference, alignment))
        .Configure();
    }

    [TypeId("dd8ce838-7897-4932-9963-8901b27218ae")]
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
          if (Alignment is not null && !evt.Initiator.Alignment.ValueRaw.HasComponent(Alignment.Value))
          {
            Logger.Verbose(() => $"Bane does not apply: {evt.Initiator.Alignment}, {Alignment} required");
            return;
          }

          if (!evt.Initiator.HasFact(RequisiteFeature))
          {
            Logger.Verbose(() => $"Bane does not apply, attacker does not have {RequisiteFeature}");
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
          if (Alignment is not null && !evt.Initiator.Alignment.ValueRaw.HasComponent(Alignment.Value))
          {
            Logger.Verbose(() => $"Bane does not apply: {evt.Initiator.Alignment}, {Alignment} required");
            return;
          }

          if (!evt.Initiator.HasFact(RequisiteFeature))
          {
            Logger.Verbose(() => $"Bane does not apply, attacker does not have {RequisiteFeature}");
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

    [TypeId("ac8768d8-3995-48e6-b8b0-4048252d0a8e")]
    private class BaneResistanceComponent : TTAddDamageResistanceBase
    {
      private readonly BlueprintFeatureReference RequisiteFeature;
      private readonly AlignmentComponent? Alignment;

      internal BaneResistanceComponent(BlueprintFeatureReference requisiteFeature, AlignmentComponent? alignment) : base()
      {
        RequisiteFeature = requisiteFeature;
        Alignment = alignment;
        Value = 2;
      }

      protected override bool Bypassed(ComponentRuntime runtime, BaseDamage damage, ItemEntityWeapon weapon)
      {
        try
        {
          if (Alignment is not null && !weapon.Owner.Alignment.ValueRaw.HasComponent(Alignment.Value))
          {
            Logger.Verbose(() => $"Bane DR does not apply: {weapon.Owner.Alignment}, {Alignment} required");
            return true;
          }

          if (!weapon.Owner.HasFact(RequisiteFeature))
          {
            Logger.Verbose(() => $"Bane does not apply, attacker does not have {RequisiteFeature}");
            return true;
          }

          return false;
        }
        catch (Exception e)
        {
          Logger.LogException("BaneComponent.OnEventAboutToTrigger", e);
        }
        return true;
      }

      public override bool IsSameDRTypeAs(TTAddDamageResistanceBase other)
      {
        return other is BaneResistanceComponent;
      }

      protected override void AdditionalInitFromVanillaDamageResistance(AddDamageResistanceBase vanillaResistance) { }
    }
  }
}