using AutomaticBonusProgression.Components;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System.Linq;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal static class EnchantmentTool
  {
    internal static BlueprintFeature AddEnhancementEquivalence(
      Blueprint<BlueprintReference<BlueprintFeature>> feature, EnhancementType type, int enhancement)
    {
      return FeatureConfigurator.For(feature)
        .AddComponent(new EnhancementEquivalenceComponent(type, enhancement))
        .Configure();
    }

    internal static BlueprintArmorEnchantment AddEnhancementEquivalence(
      Blueprint<BlueprintReference<BlueprintArmorEnchantment>> enchantment, EnhancementType type, int enhancement)
    {
      return ArmorEnchantmentConfigurator.For(enchantment)
        .AddComponent(new EnhancementEquivalenceComponent(type, enhancement))
        .Configure();
    }

    /// <summary>
    /// Creates the buff and activatable ability.
    /// </summary>
    internal static BlueprintActivatableAbility CreateEnchantAbility(
      string buffName,
      string buffGuid,
      string displayName,
      string description,
      //string icon,
      EnhancementType type,
      int enhancementCost,
      string abilityName,
      string abilityGuid,
      params BlueprintComponent[] buffComponents)
    {
      var buffConfigurator = BuffConfigurator.New(buffName, buffGuid)
        .SetDisplayName(displayName)
        .SetDescription(description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(type, enhancementCost));

      foreach (var component in buffComponents)
        buffConfigurator.AddComponent(component);

      var buff = buffConfigurator.Configure();
      return CreateEnchantAbility(
        buff,
        displayName: displayName,
        description: description,
        //icon: icon,
        type: type,
        enhancementCost: enhancementCost,
        abilityName: abilityName,
        abilityGuid: abilityGuid);
    }

    /// <summary>
    /// Creates the activatable ability which applies the specified buff.
    /// </summary>
    internal static BlueprintActivatableAbility CreateEnchantAbility(
      BlueprintBuff buff,
      string displayName,
      string description,
      //string icon,
      EnhancementType type,
      int enhancementCost,
      string abilityName,
      string abilityGuid)
    {
      return ActivatableAbilityConfigurator.New(abilityName, abilityGuid)
        .SetDisplayName(displayName)
        .SetDescription(description)
      //.SetIcon(icon)
        .SetBuff(buff)
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddComponent(new EnhancementEquivalentRestriction(type, enhancementCost))
        .SetHiddenInUI()
        .Configure();
    }

    /// <summary>
    /// Creates the enchant feature and ability using the specified buff.
    /// </summary>
    internal static BlueprintFeature CreateEnchant(
      BlueprintBuff buff,
      string displayName,
      string description,
      //string icon,
      EnhancementType type,
      int enhancementCost,
      string abilityName,
      string abilityGuid,
      string featureName,
      string featureGuid,
      int featureRanks,
      string prerequisiteFeature = "",
      int prerequisiteRanks = 1)
    {
      var ability = CreateEnchantAbility(
        buff,
        displayName: displayName,
        description: description,
        //icon: icon,
        type: type,
        enhancementCost: enhancementCost,
        abilityName: abilityName,
        abilityGuid: abilityGuid);

      return CreateEnchantFeature(
        displayName: displayName,
        description: description,
        // icon: icon,
        featureName: featureName,
        featureGuid: featureGuid,
        featureRanks: featureRanks,
        prerequisiteFeature: prerequisiteFeature,
        prerequisiteRanks: prerequisiteRanks,
        abilities: ability);
    }

    /// <summary>
    /// Creates the enchant feature which grants the specified abilities.
    /// </summary>
    internal static BlueprintFeature CreateEnchantFeature(
      string displayName,
      string description,
      //string icon,
      string featureName,
      string featureGuid,
      int featureRanks,
      string prerequisiteFeature = "",
      int prerequisiteRanks = 1,
      params Blueprint<BlueprintUnitFactReference>[] abilities)
    {
      var configurator = FeatureConfigurator.New(featureName, featureGuid)
        .SetIsClassFeature()
        .SetDisplayName(displayName)
        .SetDescription(description)
        //.SetIcon(icon)
        ;

      if (featureRanks > 1)
      {
        configurator.SetRanks(featureRanks)
          .AddRecommendationHasFeature(featureGuid)
          .AddComponent(new AddFactsOnRank(rank: featureRanks, abilities));
      }
      else
        configurator.AddFacts(abilities.ToList());

      if (!string.IsNullOrEmpty(prerequisiteFeature))
      {
        if (prerequisiteRanks > 1)
          configurator.AddComponent(new PrerequisiteHasFeatureRanks(prerequisiteFeature, prerequisiteRanks));
        else
          configurator.AddPrerequisiteFeature(prerequisiteFeature);
      }

      return configurator.Configure();
    }

    /// <summary>
    /// Creates the enchant feature, buff, and ability.
    /// </summary>
    internal static BlueprintFeature CreateEnchant(
      string buffName,
      string buffGuid,
      string displayName,
      string description,
      //string icon,
      EnhancementType type,
      int enhancementCost,
      string abilityName,
      string abilityGuid,
      string featureName,
      string featureGuid,
      int featureRanks = 1,
      string prerequisiteFeature = "",
      int prerequisiteRanks = 1,
      params BlueprintComponent[] buffComponents)
    {
      var buffConfigurator = BuffConfigurator.New(buffName, buffGuid)
        .SetDisplayName(displayName)
        .SetDescription(description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(type, enhancementCost));

      foreach (var component in buffComponents)
        buffConfigurator.AddComponent(component);

      var buff = buffConfigurator.Configure();
      return CreateEnchant(
        buff,
        displayName,
        description,
        //icon,
        type,
        enhancementCost,
        abilityName,
        abilityGuid,
        featureName,
        featureGuid,
        featureRanks: featureRanks,
        prerequisiteFeature: prerequisiteFeature,
        prerequisiteRanks: prerequisiteRanks);
    }
  }
}
