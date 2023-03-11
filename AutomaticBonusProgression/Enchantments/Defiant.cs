using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Features;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
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
  internal class Defiant
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Defiant));

    #region Too Many Constants
    private const string DefiantName = "LegendaryArmor.Defiant";

    private const string DefiantAbility = "LegendaryArmor.Defiant.Ability";
    private const string DefiantAbilityName = "LegendaryArmor.Defiant.Ability.Name";
    private const string DefiantShieldAbility = "LegendaryArmor.Defiant.Shield.Ability";
    private const string DefiantShieldAbilityName = "LegendaryShield.Defiant.Ability.Name";

    private const string DefiantAberrationsName = "LegendaryArmor.Defiant.Aberrations.Name";
    private const string DefiantAberrationsAbility = "LegendaryArmor.Defiant.Aberrations.Ability";
    private const string DefiantAberrationsShieldAbility = "LegendaryArmor.Defiant.Aberrations.Shield.Ability";
    private const string DefiantAberrationsBuff = "LegendaryArmor.Defiant.Aberrations.Buff";
    private const string DefiantAberrationsShieldBuff = "LegendaryArmor.Defiant.Aberrations.Shield.Buff";

    private const string DefiantAnimalsName = "LegendaryArmor.Defiant.Animals.Name";
    private const string DefiantAnimalsAbility = "LegendaryArmor.Defiant.Animals.Ability";
    private const string DefiantAnimalsShieldAbility = "LegendaryArmor.Defiant.Animals.Shield.Ability";
    private const string DefiantAnimalsBuff = "LegendaryArmor.Defiant.Animals.Buff";
    private const string DefiantAnimalsShieldBuff = "LegendaryArmor.Defiant.Animals.Shield.Buff";

    private const string DefiantConstructsName = "LegendaryArmor.Defiant.Constructs.Name";
    private const string DefiantConstructsAbility = "LegendaryArmor.Defiant.Constructs.Ability";
    private const string DefiantConstructsShieldAbility = "LegendaryArmor.Defiant.Constructs.Shield.Ability";
    private const string DefiantConstructsBuff = "LegendaryArmor.Defiant.Constructs.Buff";
    private const string DefiantConstructsShieldBuff = "LegendaryArmor.Defiant.Constructs.Shield.Buff";

    private const string DefiantDragonsName = "LegendaryArmor.Defiant.Dragons.Name";
    private const string DefiantDragonsAbility = "LegendaryArmor.Defiant.Dragons.Ability";
    private const string DefiantDragonsShieldAbility = "LegendaryArmor.Defiant.Dragons.Shield.Ability";
    private const string DefiantDragonsBuff = "LegendaryArmor.Defiant.Dragons.Buff";
    private const string DefiantDragonsShieldBuff = "LegendaryArmor.Defiant.Dragons.Shield.Buff";

    private const string DefiantFeyName = "LegendaryArmor.Defiant.Fey.Name";
    private const string DefiantFeyAbility = "LegendaryArmor.Defiant.Fey.Ability";
    private const string DefiantFeyShieldAbility = "LegendaryArmor.Defiant.Fey.Shield.Ability";
    private const string DefiantFeyBuff = "LegendaryArmor.Defiant.Fey.Buff";
    private const string DefiantFeyShieldBuff = "LegendaryArmor.Defiant.Fey.Shield.Buff";

    private const string DefiantHumanoidGiantName = "LegendaryArmor.Defiant.Humanoid.Giant.Name";
    private const string DefiantHumanoidGiantAbility = "LegendaryArmor.Defiant.Humanoid.Giant.Ability";
    private const string DefiantHumanoidGiantShieldAbility = "LegendaryArmor.Defiant.Humanoid.Giant.Shield.Ability";
    private const string DefiantHumanoidGiantBuff = "LegendaryArmor.Defiant.Humanoid.Giant.Buff";
    private const string DefiantHumanoidGiantShieldBuff = "LegendaryArmor.Defiant.Humanoid.Giant.Shield.Buff";

    private const string DefiantHumanoidReptilianName = "LegendaryArmor.Defiant.Humanoid.Reptilian.Name";
    private const string DefiantHumanoidReptilianAbility = "LegendaryArmor.Defiant.Humanoid.Reptilian.Ability";
    private const string DefiantHumanoidReptilianShieldAbility = "LegendaryArmor.Defiant.Humanoid.Reptilian.Shield.Ability";
    private const string DefiantHumanoidReptilianBuff = "LegendaryArmor.Defiant.Humanoid.Reptilian.Buff";
    private const string DefiantHumanoidReptilianShieldBuff = "LegendaryArmor.Defiant.Humanoid.Reptilian.Shield.Buff";

    private const string DefiantHumanoidMonstrousName = "LegendaryArmor.Defiant.Humanoid.Monstrous.Name";
    private const string DefiantHumanoidMonstrousAbility = "LegendaryArmor.Defiant.Humanoid.Monstrous.Ability";
    private const string DefiantHumanoidMonstrousShieldAbility = "LegendaryArmor.Defiant.Humanoid.Monstrous.Shield.Ability";
    private const string DefiantHumanoidMonstrousBuff = "LegendaryArmor.Defiant.Humanoid.Monstrous.Buff";
    private const string DefiantHumanoidMonstrousShieldBuff = "LegendaryArmor.Defiant.Humanoid.Monstrous.Shield.Buff";

    private const string DefiantMagicalBeastsName = "LegendaryArmor.Defiant.MagicalBeasts.Name";
    private const string DefiantMagicalBeastsAbility = "LegendaryArmor.Defiant.MagicalBeasts.Ability";
    private const string DefiantMagicalBeastsShieldAbility = "LegendaryArmor.Defiant.MagicalBeasts.Shield.Ability";
    private const string DefiantMagicalBeastsBuff = "LegendaryArmor.Defiant.MagicalBeasts.Buff";
    private const string DefiantMagicalBeastsShieldBuff = "LegendaryArmor.Defiant.MagicalBeasts.Shield.Buff";

    private const string DefiantOutsiderGoodName = "LegendaryArmor.Defiant.Outsider.Good.Name";
    private const string DefiantOutsiderGoodAbility = "LegendaryArmor.Defiant.Outsider.Good.Ability";
    private const string DefiantOutsiderGoodShieldAbility = "LegendaryArmor.Defiant.Outsider.Good.Shield.Ability";
    private const string DefiantOutsiderGoodBuff = "LegendaryArmor.Defiant.Outsider.Good.Buff";
    private const string DefiantOutsiderGoodShieldBuff = "LegendaryArmor.Defiant.Outsider.Good.Shield.Buff";

    private const string DefiantOutsiderEvilName = "LegendaryArmor.Defiant.Outsider.Evil.Name";
    private const string DefiantOutsiderEvilAbility = "LegendaryArmor.Defiant.Outsider.Evil.Ability";
    private const string DefiantOutsiderEvilShieldAbility = "LegendaryArmor.Defiant.Outsider.Evil.Shield.Ability";
    private const string DefiantOutsiderEvilBuff = "LegendaryArmor.Defiant.Outsider.Evil.Buff";
    private const string DefiantOutsiderEvilShieldBuff = "LegendaryArmor.Defiant.Outsider.Evil.Shield.Buff";

    private const string DefiantOutsiderLawfulName = "LegendaryArmor.Defiant.Outsider.Lawful.Name";
    private const string DefiantOutsiderLawfulAbility = "LegendaryArmor.Defiant.Outsider.Lawful.Ability";
    private const string DefiantOutsiderLawfulShieldAbility = "LegendaryArmor.Defiant.Outsider.Lawful.Shield.Ability";
    private const string DefiantOutsiderLawfulBuff = "LegendaryArmor.Defiant.Outsider.Lawful.Buff";
    private const string DefiantOutsiderLawfulShieldBuff = "LegendaryArmor.Defiant.Outsider.Lawful.Shield.Buff";

    private const string DefiantOutsiderChaoticName = "LegendaryArmor.Defiant.Outsider.Chaotic.Name";
    private const string DefiantOutsiderChaoticAbility = "LegendaryArmor.Defiant.Outsider.Chaotic.Ability";
    private const string DefiantOutsiderChaoticShieldAbility = "LegendaryArmor.Defiant.Outsider.Chaotic.Shield.Ability";
    private const string DefiantOutsiderChaoticBuff = "LegendaryArmor.Defiant.Outsider.Chaotic.Buff";
    private const string DefiantOutsiderChaoticShieldBuff = "LegendaryArmor.Defiant.Outsider.Chaotic.Shield.Buff";

    private const string DefiantOutsiderNeutralName = "LegendaryArmor.Defiant.Outsider.Neutral.Name";
    private const string DefiantOutsiderNeutralAbility = "LegendaryArmor.Defiant.Outsider.Neutral.Ability";
    private const string DefiantOutsiderNeutralShieldAbility = "LegendaryArmor.Defiant.Outsider.Neutral.Shield.Ability";
    private const string DefiantOutsiderNeutralBuff = "LegendaryArmor.Defiant.Outsider.Neutral.Buff";
    private const string DefiantOutsiderNeutralShieldBuff = "LegendaryArmor.Defiant.Outsider.Neutral.Shield.Buff";

    private const string DefiantPlantsName = "LegendaryArmor.Defiant.Plants.Name";
    private const string DefiantPlantsAbility = "LegendaryArmor.Defiant.Plants.Ability";
    private const string DefiantPlantsShieldAbility = "LegendaryArmor.Defiant.Plants.Shield.Ability";
    private const string DefiantPlantsBuff = "LegendaryArmor.Defiant.Plants.Buff";
    private const string DefiantPlantsShieldBuff = "LegendaryArmor.Defiant.Plants.Shield.Buff";

    private const string DefiantUndeadName = "LegendaryArmor.Defiant.Undead.Name";
    private const string DefiantUndeadAbility = "LegendaryArmor.Defiant.Undead.Ability";
    private const string DefiantUndeadShieldAbility = "LegendaryArmor.Defiant.Undead.Shield.Ability";
    private const string DefiantUndeadBuff = "LegendaryArmor.Defiant.Undead.Buff";
    private const string DefiantUndeadShieldBuff = "LegendaryArmor.Defiant.Undead.Shield.Buff";

    private const string DefiantVerminName = "LegendaryArmor.Defiant.Vermin.Name";
    private const string DefiantVerminAbility = "LegendaryArmor.Defiant.Vermin.Ability";
    private const string DefiantVerminShieldAbility = "LegendaryArmor.Defiant.Vermin.Shield.Ability";
    private const string DefiantVerminBuff = "LegendaryArmor.Defiant.Vermin.Buff";
    private const string DefiantVerminShieldBuff = "LegendaryArmor.Defiant.Vermin.Shield.Buff";
    #endregion

    private const string DisplayName = "LegendaryArmor.Defiant.Name";
    private const string Description = "LegendaryArmor.Defiant.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Defiant");

      var parent = ActivatableAbilityConfigurator.New(DefiantAbility, Guids.DefiantParent)
        .SetDisplayName(DefiantAbilityName)
        .SetDescription(LegendaryArmor.LegendaryArmorAbilityDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants:
            new()
            {
              Guids.DefiantAberrationsAbility,
              Guids.DefiantAnimalsAbility,
              Guids.DefiantConstructsAbility,
              Guids.DefiantDragonsAbility,
              Guids.DefiantFeyAbility,
              Guids.DefiantHumanoidGiantAbility,
              Guids.DefiantHumanoidMonstrousAbility,
              Guids.DefiantHumanoidReptilianAbility,
              Guids.DefiantMagicalBeastsAbility,
              Guids.DefiantOutsiderChaoticAbility,
              Guids.DefiantOutsiderEvilAbility,
              Guids.DefiantOutsiderGoodAbility,
              Guids.DefiantOutsiderLawfulAbility,
              Guids.DefiantOutsiderNeutralAbility,
              Guids.DefiantPlantsAbility,
              Guids.DefiantUndeadAbility,
              Guids.DefiantVerminAbility,
            })
        .AddActivationDisable()
        .Configure();
      var shieldParent = ActivatableAbilityConfigurator.New(DefiantShieldAbility, Guids.DefiantShieldParent)
        .SetDisplayName(DefiantShieldAbilityName)
        .SetDescription(LegendaryArmor.LegendaryShieldDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants:
            new()
            {
              Guids.DefiantAberrationsShieldAbility,
              Guids.DefiantAnimalsShieldAbility,
              Guids.DefiantConstructsShieldAbility,
              Guids.DefiantDragonsShieldAbility,
              Guids.DefiantFeyShieldAbility,
              Guids.DefiantHumanoidGiantShieldAbility,
              Guids.DefiantHumanoidMonstrousShieldAbility,
              Guids.DefiantHumanoidReptilianShieldAbility,
              Guids.DefiantMagicalBeastsShieldAbility,
              Guids.DefiantOutsiderChaoticShieldAbility,
              Guids.DefiantOutsiderEvilShieldAbility,
              Guids.DefiantOutsiderGoodShieldAbility,
              Guids.DefiantOutsiderLawfulShieldAbility,
              Guids.DefiantOutsiderNeutralShieldAbility,
              Guids.DefiantPlantsShieldAbility,
              Guids.DefiantUndeadShieldAbility,
              Guids.DefiantVerminShieldAbility,
            })
        .AddActivationDisable()
        .Configure();

      var aberrationsEnchantInfo =
        new ArmorEnchantInfo(
          DefiantAberrationsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var aberrationsBuffInfo = new BlueprintInfo(DefiantAberrationsBuff, Guids.DefiantAberrationsBuff);
      var aberrationsAbilityInfo = new BlueprintInfo(DefiantAberrationsAbility, Guids.DefiantAberrationsAbility);

      var aberrations = EnchantmentTool.CreateEnchantAbility(
        aberrationsEnchantInfo,
        ConfigureBuff(aberrationsEnchantInfo, aberrationsBuffInfo, FeatureRefs.AberrationType.ToString()),
        aberrationsAbilityInfo);
      var aberrationsShield = EnchantmentTool.CreateEnchantShieldVariant(
        aberrationsEnchantInfo,
        aberrations,
        buff: new(DefiantAberrationsShieldBuff, Guids.DefiantAberrationsShieldBuff),
        ability: new(DefiantAberrationsShieldAbility, Guids.DefiantAberrationsShieldAbility));

      var animalsEnchantInfo =
        new ArmorEnchantInfo(
          DefiantAnimalsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var animalsBuffInfo = new BlueprintInfo(DefiantAnimalsBuff, Guids.DefiantAnimalsBuff);
      var animalsAbilityInfo = new BlueprintInfo(DefiantAnimalsAbility, Guids.DefiantAnimalsAbility);

      var animals = EnchantmentTool.CreateEnchantAbility(
        animalsEnchantInfo,
        ConfigureBuff(animalsEnchantInfo, animalsBuffInfo, FeatureRefs.AnimalType.ToString()),
        animalsAbilityInfo);
      var animalsShield = EnchantmentTool.CreateEnchantShieldVariant(
        animalsEnchantInfo,
        animals,
        buff: new(DefiantAnimalsShieldBuff, Guids.DefiantAnimalsShieldBuff),
        ability: new(DefiantAnimalsShieldAbility, Guids.DefiantAnimalsShieldAbility));

      var constructsEnchantInfo =
        new ArmorEnchantInfo(
          DefiantConstructsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var constructsBuffInfo = new BlueprintInfo(DefiantConstructsBuff, Guids.DefiantConstructsBuff);
      var constructsAbilityInfo = new BlueprintInfo(DefiantConstructsAbility, Guids.DefiantConstructsAbility);

      var constructs = EnchantmentTool.CreateEnchantAbility(
        constructsEnchantInfo,
        ConfigureBuff(constructsEnchantInfo, constructsBuffInfo, FeatureRefs.ConstructType.ToString()),
        constructsAbilityInfo);
      var constructsShield = EnchantmentTool.CreateEnchantShieldVariant(
        constructsEnchantInfo,
        constructs,
        buff: new(DefiantConstructsShieldBuff, Guids.DefiantConstructsShieldBuff),
        ability: new(DefiantConstructsShieldAbility, Guids.DefiantConstructsShieldAbility));

      var dragonsEnchantInfo =
        new ArmorEnchantInfo(
          DefiantDragonsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var dragonsBuffInfo = new BlueprintInfo(DefiantDragonsBuff, Guids.DefiantDragonsBuff);
      var dragonsAbilityInfo = new BlueprintInfo(DefiantDragonsAbility, Guids.DefiantDragonsAbility);

      var dragons = EnchantmentTool.CreateEnchantAbility(
        dragonsEnchantInfo,
        ConfigureBuff(dragonsEnchantInfo, dragonsBuffInfo, FeatureRefs.DragonType.ToString()),
        dragonsAbilityInfo);
      var dragonsShield = EnchantmentTool.CreateEnchantShieldVariant(
        dragonsEnchantInfo,
        dragons,
        buff: new(DefiantDragonsShieldBuff, Guids.DefiantDragonsShieldBuff),
        ability: new(DefiantDragonsShieldAbility, Guids.DefiantDragonsShieldAbility));

      var feyEnchantInfo =
        new ArmorEnchantInfo(
          DefiantFeyName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var feyBuffInfo = new BlueprintInfo(DefiantFeyBuff, Guids.DefiantFeyBuff);
      var feyAbilityInfo = new BlueprintInfo(DefiantFeyAbility, Guids.DefiantFeyAbility);

      var fey = EnchantmentTool.CreateEnchantAbility(
        feyEnchantInfo,
        ConfigureBuff(feyEnchantInfo, feyBuffInfo, FeatureRefs.FeyType.ToString()),
        feyAbilityInfo);
      var feyShield = EnchantmentTool.CreateEnchantShieldVariant(
        feyEnchantInfo,
        fey,
        buff: new(DefiantFeyShieldBuff, Guids.DefiantFeyShieldBuff),
        ability: new(DefiantFeyShieldAbility, Guids.DefiantFeyShieldAbility));

      var humanoidGiantEnchantInfo =
        new ArmorEnchantInfo(
          DefiantHumanoidGiantName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var humanoidGiantBuffInfo = new BlueprintInfo(DefiantHumanoidGiantBuff, Guids.DefiantHumanoidGiantBuff);
      var humanoidGiantAbilityInfo = new BlueprintInfo(DefiantHumanoidGiantAbility, Guids.DefiantHumanoidGiantAbility);

      var humanoidGiant = EnchantmentTool.CreateEnchantAbility(
        humanoidGiantEnchantInfo,
        ConfigureBuff(humanoidGiantEnchantInfo, humanoidGiantBuffInfo, FeatureRefs.GiantSubtype.ToString()),
        humanoidGiantAbilityInfo);
      var humanoidGiantShield = EnchantmentTool.CreateEnchantShieldVariant(
        humanoidGiantEnchantInfo,
        humanoidGiant,
        buff: new(DefiantHumanoidGiantShieldBuff, Guids.DefiantHumanoidGiantShieldBuff),
        ability: new(DefiantHumanoidGiantShieldAbility, Guids.DefiantHumanoidGiantShieldAbility));

      var humanoidReptilianEnchantInfo =
        new ArmorEnchantInfo(
          DefiantHumanoidReptilianName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var humanoidReptilianBuffInfo = new BlueprintInfo(DefiantHumanoidReptilianBuff, Guids.DefiantHumanoidReptilianBuff);
      var humanoidReptilianAbilityInfo = new BlueprintInfo(DefiantHumanoidReptilianAbility, Guids.DefiantHumanoidReptilianAbility);

      var humanoidReptilian = EnchantmentTool.CreateEnchantAbility(
        humanoidReptilianEnchantInfo,
        ConfigureBuff(humanoidReptilianEnchantInfo, humanoidReptilianBuffInfo, FeatureRefs.ReptilianSubtype.ToString()),
        humanoidReptilianAbilityInfo);
      var humanoidReptilianShield = EnchantmentTool.CreateEnchantShieldVariant(
        humanoidReptilianEnchantInfo,
        humanoidReptilian,
        buff: new(DefiantHumanoidReptilianShieldBuff, Guids.DefiantHumanoidReptilianShieldBuff),
        ability: new(DefiantHumanoidReptilianShieldAbility, Guids.DefiantHumanoidReptilianShieldAbility));

      var humanoidMonstrousEnchantInfo =
        new ArmorEnchantInfo(
          DefiantHumanoidMonstrousName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var humanoidMonstrousBuffInfo = new BlueprintInfo(DefiantHumanoidMonstrousBuff, Guids.DefiantHumanoidMonstrousBuff);
      var humanoidMonstrousAbilityInfo = new BlueprintInfo(DefiantHumanoidMonstrousAbility, Guids.DefiantHumanoidMonstrousAbility);

      var humanoidMonstrous = EnchantmentTool.CreateEnchantAbility(
        humanoidMonstrousEnchantInfo,
        ConfigureBuff(humanoidMonstrousEnchantInfo, humanoidMonstrousBuffInfo, FeatureRefs.MonstrousHumanoidType.ToString()),
        humanoidMonstrousAbilityInfo);
      var humanoidMonstrousShield = EnchantmentTool.CreateEnchantShieldVariant(
        humanoidMonstrousEnchantInfo,
        humanoidMonstrous,
        buff: new(DefiantHumanoidMonstrousShieldBuff, Guids.DefiantHumanoidMonstrousShieldBuff),
        ability: new(DefiantHumanoidMonstrousShieldAbility, Guids.DefiantHumanoidMonstrousShieldAbility));

      var magicalBeastsEnchantInfo =
        new ArmorEnchantInfo(
          DefiantMagicalBeastsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var magicalBeastsBuffInfo = new BlueprintInfo(DefiantMagicalBeastsBuff, Guids.DefiantMagicalBeastsBuff);
      var magicalBeastsAbilityInfo = new BlueprintInfo(DefiantMagicalBeastsAbility, Guids.DefiantMagicalBeastsAbility);

      var magicalBeasts = EnchantmentTool.CreateEnchantAbility(
        magicalBeastsEnchantInfo,
        ConfigureBuff(magicalBeastsEnchantInfo, magicalBeastsBuffInfo, FeatureRefs.MagicalBeastType.ToString()),
        magicalBeastsAbilityInfo);
      var magicalBeastsShield = EnchantmentTool.CreateEnchantShieldVariant(
        magicalBeastsEnchantInfo,
        magicalBeasts,
        buff: new(DefiantMagicalBeastsShieldBuff, Guids.DefiantMagicalBeastsShieldBuff),
        ability: new(DefiantMagicalBeastsShieldAbility, Guids.DefiantMagicalBeastsShieldAbility));

      var outsiderGoodEnchantInfo =
        new ArmorEnchantInfo(
          DefiantOutsiderGoodName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderGoodBuffInfo = new BlueprintInfo(DefiantOutsiderGoodBuff, Guids.DefiantOutsiderGoodBuff);
      var outsiderGoodAbilityInfo = new BlueprintInfo(DefiantOutsiderGoodAbility, Guids.DefiantOutsiderGoodAbility);

      var outsiderGood = EnchantmentTool.CreateEnchantAbility(
        outsiderGoodEnchantInfo,
        ConfigureBuff(outsiderGoodEnchantInfo, outsiderGoodBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Good),
        outsiderGoodAbilityInfo);
      var outsiderGoodShield = EnchantmentTool.CreateEnchantShieldVariant(
        outsiderGoodEnchantInfo,
        outsiderGood,
        buff: new(DefiantOutsiderGoodShieldBuff, Guids.DefiantOutsiderGoodShieldBuff),
        ability: new(DefiantOutsiderGoodShieldAbility, Guids.DefiantOutsiderGoodShieldAbility));

      var outsiderEvilEnchantInfo =
        new ArmorEnchantInfo(
          DefiantOutsiderEvilName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderEvilBuffInfo = new BlueprintInfo(DefiantOutsiderEvilBuff, Guids.DefiantOutsiderEvilBuff);
      var outsiderEvilAbilityInfo = new BlueprintInfo(DefiantOutsiderEvilAbility, Guids.DefiantOutsiderEvilAbility);

      var outsiderEvil = EnchantmentTool.CreateEnchantAbility(
        outsiderEvilEnchantInfo,
        ConfigureBuff(outsiderEvilEnchantInfo, outsiderEvilBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Evil),
        outsiderEvilAbilityInfo);
      var outsiderEvilShield = EnchantmentTool.CreateEnchantShieldVariant(
        outsiderEvilEnchantInfo,
        outsiderEvil,
        buff: new(DefiantOutsiderEvilShieldBuff, Guids.DefiantOutsiderEvilShieldBuff),
        ability: new(DefiantOutsiderEvilShieldAbility, Guids.DefiantOutsiderEvilShieldAbility));

      var outsiderLawfulEnchantInfo =
        new ArmorEnchantInfo(
          DefiantOutsiderLawfulName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderLawfulBuffInfo = new BlueprintInfo(DefiantOutsiderLawfulBuff, Guids.DefiantOutsiderLawfulBuff);
      var outsiderLawfulAbilityInfo = new BlueprintInfo(DefiantOutsiderLawfulAbility, Guids.DefiantOutsiderLawfulAbility);

      var outsiderLawful = EnchantmentTool.CreateEnchantAbility(
        outsiderLawfulEnchantInfo,
        ConfigureBuff(outsiderLawfulEnchantInfo, outsiderLawfulBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Lawful),
        outsiderLawfulAbilityInfo);
      var outsiderLawfulShield = EnchantmentTool.CreateEnchantShieldVariant(
        outsiderLawfulEnchantInfo,
        outsiderLawful,
        buff: new(DefiantOutsiderLawfulShieldBuff, Guids.DefiantOutsiderLawfulShieldBuff),
        ability: new(DefiantOutsiderLawfulShieldAbility, Guids.DefiantOutsiderLawfulShieldAbility));

      var outsiderChaoticEnchantInfo =
        new ArmorEnchantInfo(
          DefiantOutsiderChaoticName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderChaoticBuffInfo = new BlueprintInfo(DefiantOutsiderChaoticBuff, Guids.DefiantOutsiderChaoticBuff);
      var outsiderChaoticAbilityInfo = new BlueprintInfo(DefiantOutsiderChaoticAbility, Guids.DefiantOutsiderChaoticAbility);

      var outsiderChaotic = EnchantmentTool.CreateEnchantAbility(
        outsiderChaoticEnchantInfo,
        ConfigureBuff(outsiderChaoticEnchantInfo, outsiderChaoticBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Chaotic),
        outsiderChaoticAbilityInfo);
      var outsiderChaoticShield = EnchantmentTool.CreateEnchantShieldVariant(
        outsiderChaoticEnchantInfo,
        outsiderChaotic,
        buff: new(DefiantOutsiderChaoticShieldBuff, Guids.DefiantOutsiderChaoticShieldBuff),
        ability: new(DefiantOutsiderChaoticShieldAbility, Guids.DefiantOutsiderChaoticShieldAbility));

      var outsiderNeutralEnchantInfo =
        new ArmorEnchantInfo(
          DefiantOutsiderNeutralName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var outsiderNeutralBuffInfo = new BlueprintInfo(DefiantOutsiderNeutralBuff, Guids.DefiantOutsiderNeutralBuff);
      var outsiderNeutralAbilityInfo = new BlueprintInfo(DefiantOutsiderNeutralAbility, Guids.DefiantOutsiderNeutralAbility);

      var outsiderNeutral = EnchantmentTool.CreateEnchantAbility(
        outsiderNeutralEnchantInfo,
        ConfigureBuff(outsiderNeutralEnchantInfo, outsiderNeutralBuffInfo, FeatureRefs.OutsiderType.ToString(), AlignmentComponent.Neutral),
        outsiderNeutralAbilityInfo);
      var outsiderNeutralShield = EnchantmentTool.CreateEnchantShieldVariant(
        outsiderNeutralEnchantInfo,
        outsiderNeutral,
        buff: new(DefiantOutsiderNeutralShieldBuff, Guids.DefiantOutsiderNeutralShieldBuff),
        ability: new(DefiantOutsiderNeutralShieldAbility, Guids.DefiantOutsiderNeutralShieldAbility));

      var plantsEnchantInfo =
        new ArmorEnchantInfo(
          DefiantPlantsName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var plantsBuffInfo = new BlueprintInfo(DefiantPlantsBuff, Guids.DefiantPlantsBuff);
      var plantsAbilityInfo = new BlueprintInfo(DefiantPlantsAbility, Guids.DefiantPlantsAbility);

      var plants = EnchantmentTool.CreateEnchantAbility(
        plantsEnchantInfo,
        ConfigureBuff(plantsEnchantInfo, plantsBuffInfo, FeatureRefs.PlantType.ToString()),
        plantsAbilityInfo);
      var plantsShield = EnchantmentTool.CreateEnchantShieldVariant(
        plantsEnchantInfo,
        plants,
        buff: new(DefiantPlantsShieldBuff, Guids.DefiantPlantsShieldBuff),
        ability: new(DefiantPlantsShieldAbility, Guids.DefiantPlantsShieldAbility));

      var undeadEnchantInfo =
        new ArmorEnchantInfo(
          DefiantUndeadName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var undeadBuffInfo = new BlueprintInfo(DefiantUndeadBuff, Guids.DefiantUndeadBuff);
      var undeadAbilityInfo = new BlueprintInfo(DefiantUndeadAbility, Guids.DefiantUndeadAbility);

      var undead = EnchantmentTool.CreateEnchantAbility(
        undeadEnchantInfo,
        ConfigureBuff(undeadEnchantInfo, undeadBuffInfo, FeatureRefs.UndeadType.ToString()),
        undeadAbilityInfo);
      var undeadShield = EnchantmentTool.CreateEnchantShieldVariant(
        undeadEnchantInfo,
        undead,
        buff: new(DefiantUndeadShieldBuff, Guids.DefiantUndeadShieldBuff),
        ability: new(DefiantUndeadShieldAbility, Guids.DefiantUndeadShieldAbility));

      var verminEnchantInfo =
        new ArmorEnchantInfo(
          DefiantVerminName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);
      var verminBuffInfo = new BlueprintInfo(DefiantVerminBuff, Guids.DefiantVerminBuff);
      var verminAbilityInfo = new BlueprintInfo(DefiantVerminAbility, Guids.DefiantVerminAbility);

      var vermin = EnchantmentTool.CreateEnchantAbility(
        verminEnchantInfo,
        ConfigureBuff(verminEnchantInfo, verminBuffInfo, FeatureRefs.VerminType.ToString()),
        verminAbilityInfo);
      var verminShield = EnchantmentTool.CreateEnchantShieldVariant(
        verminEnchantInfo,
        vermin,
        buff: new(DefiantVerminShieldBuff, Guids.DefiantVerminShieldBuff),
        ability: new(DefiantVerminShieldAbility, Guids.DefiantVerminShieldAbility));

      return EnchantmentTool.CreateEnchantFeature(
        new(DisplayName, Description, "", EnhancementCost, ranks: 1),
        new(DefiantName, Guids.Defiant),
        parent,
        shieldParent,
        aberrations,
        aberrationsShield,
        animals,
        animalsShield,
        constructs,
        constructsShield,
        dragons,
        dragonsShield,
        fey,
        feyShield,
        humanoidGiant,
        humanoidGiantShield,
        humanoidReptilian,
        humanoidReptilianShield,
        humanoidMonstrous,
        humanoidMonstrousShield,
        magicalBeasts,
        magicalBeastsShield,
        outsiderGood,
        outsiderGoodShield,
        outsiderEvil,
        outsiderEvilShield,
        outsiderLawful,
        outsiderLawfulShield,
        outsiderChaotic,
        outsiderChaoticShield,
        outsiderNeutral,
        outsiderNeutralShield,
        plants,
        plantsShield,
        undead,
        undeadShield,
        vermin,
        verminShield);
    }

    private static BlueprintBuff ConfigureBuff(
      ArmorEnchantInfo enchantInfo,
      BlueprintInfo buff,
      Blueprint<BlueprintFeatureReference> typeFeature,
      AlignmentComponent? alignment = null)
    {
      return BuffConfigurator.New(buff.Name, buff.Guid)
        .SetDisplayName(enchantInfo.DisplayName)
        .SetDescription(enchantInfo.Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalence(enchantInfo))
        .AddComponent(new DefiantComponent(typeFeature.Reference, alignment))
        .AddComponent(new DefiantResistanceComponent(typeFeature.Reference, alignment))
        .Configure();
    }

    [TypeId("dd8ce838-7897-4932-9963-8901b27218ae")]
    private class DefiantComponent : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleCalculateAC>
    {
      private readonly BlueprintFeatureReference RequisiteFeature;
      private readonly AlignmentComponent? Alignment;

      internal DefiantComponent(BlueprintFeatureReference requisiteFeature, AlignmentComponent? alignment)
      {
        RequisiteFeature = requisiteFeature;
        Alignment = alignment;
      }

      public void OnEventAboutToTrigger(RuleCalculateAC evt)
      {
        try
        {
          if (Alignment is not null && !evt.Initiator.Alignment.ValueRaw.HasComponent(Alignment.Value))
          {
            Logger.Verbose(() => $"Defiant does not apply: {evt.Initiator.Alignment}, {Alignment} required");
            return;
          }

          if (!evt.Initiator.HasFact(RequisiteFeature))
          {
            Logger.Verbose(() => $"Defiant does not apply, attacker does not have {RequisiteFeature}");
            return;
          }

          evt.AddModifier(2, Fact, ModifierDescriptor.UntypedStackable);
        }
        catch (Exception e)
        {
          Logger.LogException("DefiantComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateAC evt) { }
    }

    [TypeId("ac8768d8-3995-48e6-b8b0-4048252d0a8e")]
    private class DefiantResistanceComponent : TTAddDamageResistanceBase
    {
      private readonly BlueprintFeatureReference RequisiteFeature;
      private readonly AlignmentComponent? Alignment;

      internal DefiantResistanceComponent(BlueprintFeatureReference requisiteFeature, AlignmentComponent? alignment) : base()
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
            Logger.Verbose(() => $"Defiant DR does not apply: {weapon.Owner.Alignment}, {Alignment} required");
            return true;
          }

          if (!weapon.Owner.HasFact(RequisiteFeature))
          {
            Logger.Verbose(() => $"Defiant does not apply, attacker does not have {RequisiteFeature}");
            return true;
          }

          return false;
        }
        catch (Exception e)
        {
          Logger.LogException("DefiantComponent.OnEventAboutToTrigger", e);
        }
        return true;
      }

      public override bool IsSameDRTypeAs(TTAddDamageResistanceBase other)
      {
        return other is DefiantResistanceComponent;
      }

      protected override void AdditionalInitFromVanillaDamageResistance(AddDamageResistanceBase vanillaResistance) { }
    }
  }
}