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
      string prerequisiteFeature = "",
      int prerequisiteCost = 1)
    {
      var ability = ActivatableAbilityConfigurator.New(abilityName, abilityGuid)
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

      var configurator = FeatureConfigurator.New(featureName, featureGuid)
        .SetIsClassFeature()
        .SetDisplayName(displayName)
        .SetDescription(description)
        //.SetIcon(icon)
        ;

      var requiredRanks = enhancementCost;
      if (!string.IsNullOrEmpty(prerequisiteFeature))
        requiredRanks -= prerequisiteCost;
      if (requiredRanks > 1)
      {
        configurator.SetRanks(requiredRanks)
          .AddRecommendationHasFeature(featureGuid)
          .AddComponent(new AddFactsOnRank(rank: requiredRanks, ability));
      }
      else
        configurator.AddFacts(new() { ability });

      if (!string.IsNullOrEmpty(prerequisiteFeature))
      {
        if (prerequisiteCost > 1)
          configurator.AddComponent(new PrerequisiteHasFeatureRanks(prerequisiteFeature, prerequisiteCost));
        else
          configurator.AddPrerequisiteFeature(prerequisiteFeature);
      }

      return configurator.Configure();
    }

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
      string prerequisiteFeature = "",
      int prerequisiteCost = 1,
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
        prerequisiteFeature: prerequisiteFeature,
        prerequisiteCost: prerequisiteCost);
    }
  }
}
