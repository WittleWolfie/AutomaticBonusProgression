using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System.Collections.Generic;
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

    // TODO: Finish this refactor cause damn I need it.

    internal static BlueprintFeature CreateEnchant(
      ArmorEnchantInfo enchant, BlueprintInfo buff, BlueprintInfo ability, BlueprintInfo feature)
    {
      var buffConfigurator = BuffConfigurator.New(buff.Name, buff.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        //.SetIcon(enchant.Icon)
        .AddComponent(new EnhancementEquivalenceComponent(enchant));

      if (enchant.AllowedTypes.Any())
        buffConfigurator.AddComponent(new RequireArmorType(enchant.AllowedTypes));

      foreach (var component in buff.Components)
        buffConfigurator.AddComponent(component);

      return CreateEnchant(enchant, buffConfigurator.Configure(), ability, feature);
    }

    internal static BlueprintFeature CreateEnchant(
      ArmorEnchantInfo enchant, BlueprintBuff buff, BlueprintInfo ability, BlueprintInfo feature)
    {
      return CreateEnchantFeature(enchant, feature, CreateEnchantAbility(enchant, buff, ability));
    }

    internal static BlueprintActivatableAbility CreateEnchantAbility(
      ArmorEnchantInfo enchant, BlueprintBuff buff, BlueprintInfo ability)
    {
      var abilityConfigurator = ActivatableAbilityConfigurator.New(ability.Name, ability.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        //.SetIcon(enchant.Icon)
        .SetBuff(buff)
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddComponent(new EnhancementEquivalentRestriction(enchant))
        .AddComponent<OutOfCombatRestriction>()
        .SetHiddenInUI();

      if (enchant.AllowedTypes.Any())
        abilityConfigurator.AddComponent(new ArmorTypeRestriction(enchant.AllowedTypes));

      foreach (var component in ability.Components)
        abilityConfigurator.AddComponent(component);

      return abilityConfigurator.Configure();
    }

    internal static BlueprintFeature CreateEnchantFeature(
      ArmorEnchantInfo enchant,
      BlueprintInfo feature,
      params BlueprintActivatableAbility[] abilities)
    {
      var featureConfigurator = FeatureConfigurator.New(feature.Name, feature.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        //.SetIcon(enchant.Icon)
        ;

      // Unarmored works as Light Armor and also everyone can use Light so ignore that.
      if (enchant.AllowedTypes.Any() && !enchant.AllowedTypes.Contains(ArmorProficiencyGroup.Light))
      {
        List<Blueprint<BlueprintFeatureReference>> proficiencies = new();
        foreach (var type in enchant.AllowedTypes)
        {
          switch (type)
          {
            case ArmorProficiencyGroup.Medium:
              proficiencies.Add(FeatureRefs.MediumArmorProficiency.ToString());
              break;
            case ArmorProficiencyGroup.Heavy:
              proficiencies.Add(FeatureRefs.HeavyArmorProficiency.ToString());
              break;
            case ArmorProficiencyGroup.Buckler:
              proficiencies.Add(FeatureRefs.BucklerProficiency.ToString());
              break;
            case ArmorProficiencyGroup.LightShield:
            case ArmorProficiencyGroup.HeavyShield:
              proficiencies.Add(FeatureRefs.ShieldsProficiency.ToString());
              break;
            case ArmorProficiencyGroup.TowerShield:
              proficiencies.Add(FeatureRefs.TowerShieldProficiency.ToString());
              break;
          }
        }
        featureConfigurator.AddPrerequisiteFeaturesFromList(proficiencies, amount: 1);
      }

      if (enchant.Ranks > 1)
      {
        featureConfigurator.SetRanks(enchant.Ranks)
          .AddRecommendationHasFeature(feature.Guid)
          .AddComponent(new AddFactsOnRank(rank: enchant.Ranks, abilities));
      }
      else
        featureConfigurator.AddFacts(abilities.ToList());

      if (enchant.Prerequisite is not null)
      {
        if (enchant.Prerequisite.Ranks > 1)
          featureConfigurator.AddComponent(
            new PrerequisiteHasFeatureRanks(enchant.Prerequisite.Feature, enchant.Prerequisite.Ranks));
        else
          featureConfigurator.AddPrerequisiteFeature(enchant.Prerequisite.Feature);
      }

      return featureConfigurator.Configure();
    }

    /// <summary>
    /// Creates the buff and activatable ability.
    /// </summary>
    internal static BlueprintActivatableAbility CreateArmorEnchantAbility(
      string buffName,
      string buffGuid,
      string displayName,
      string description,
      //string icon,
      EnhancementType type,
      int enhancementCost,
      string abilityName,
      string abilityGuid,
      List<ArmorProficiencyGroup> armorTypes = null,
      params BlueprintComponent[] buffComponents)
    {
      var buffConfigurator = BuffConfigurator.New(buffName, buffGuid)
        .SetDisplayName(displayName)
        .SetDescription(description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(type, enhancementCost));

      if (armorTypes is not null)
        buffConfigurator.AddComponent(new RequireArmorType(armorTypes));

      foreach (var component in buffComponents)
        buffConfigurator.AddComponent(component);

      var buff = buffConfigurator.Configure();
      return CreateArmorEnchantAbility(
        buff,
        displayName: displayName,
        description: description,
        //icon: icon,
        type: type,
        enhancementCost: enhancementCost,
        abilityName: abilityName,
        abilityGuid: abilityGuid,
        armorTypes: armorTypes);
    }

    /// <summary>
    /// Creates a version of the specified armor enchant for use with shields.
    /// </summary>
    internal static BlueprintActivatableAbility CreateEnchantShieldVariant(
      BlueprintActivatableAbility armorEnchant,
      string buffName,
      string buffGuid,
      string abilityName,
      string abilityGuid)
    {
      var enhancementCost = armorEnchant.GetComponent<EnhancementEquivalentRestriction>().Enhancement;
      var buff = BuffConfigurator.New(buffName, buffGuid)
        .CopyFrom(armorEnchant.Buff, c => c is not EnhancementEquivalenceComponent && c is not RequireArmorType)
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Shield, enhancementCost))
        .AddComponent<RequireShield>()
        .Configure();

      return ActivatableAbilityConfigurator.New(abilityName, abilityGuid)
        .CopyFrom(armorEnchant, c => c is not EnhancementEquivalentRestriction && c is not ArmorTypeRestriction)
        .AddComponent(new EnhancementEquivalentRestriction(EnhancementType.Shield, enhancementCost))
        .AddComponent<ShieldEquippedRestriction>()
        .SetBuff(buff)
        .Configure();
    }

    /// <summary>
    /// Creates the activatable ability which applies the specified buff.
    /// </summary>
    internal static BlueprintActivatableAbility CreateArmorEnchantAbility(
      BlueprintBuff buff,
      string displayName,
      string description,
      //string icon,
      EnhancementType type,
      int enhancementCost,
      string abilityName,
      string abilityGuid,
      List<ArmorProficiencyGroup> armorTypes = null,
      params BlueprintComponent[] components)
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
        .AddComponent<OutOfCombatRestriction>()
        .SetHiddenInUI();

      if (armorTypes != null)
        ability.AddComponent(new ArmorTypeRestriction(armorTypes));

      foreach (var component in components)
        ability.AddComponent(component);

      return ability.Configure();
    }

    /// <summary>
    /// Creates the enchant feature and ability using the specified buff.
    /// </summary>
    internal static BlueprintFeature CreateArmorEnchant(
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
      int prerequisiteRanks = 1,
      List<ArmorProficiencyGroup> armorTypes = null)
    {
      var ability = CreateArmorEnchantAbility(
        buff,
        displayName: displayName,
        description: description,
        //icon: icon,
        type: type,
        enhancementCost: enhancementCost,
        abilityName: abilityName,
        abilityGuid: abilityGuid,
        armorTypes: armorTypes);

      return CreateArmorEnchantFeature(
        displayName: displayName,
        description: description,
        // icon: icon,
        featureName: featureName,
        featureGuid: featureGuid,
        featureRanks: featureRanks,
        prerequisiteFeature: prerequisiteFeature,
        prerequisiteRanks: prerequisiteRanks,
        abilities: ability,
        armorTypes: armorTypes);
    }

    /// <summary>
    /// Creates the enchant feature which grants the specified abilities.
    /// </summary>
    internal static BlueprintFeature CreateArmorEnchantFeature(
      string displayName,
      string description,
      //string icon,
      string featureName,
      string featureGuid,
      int featureRanks,
      string prerequisiteFeature = "",
      int prerequisiteRanks = 1,
      List<ArmorProficiencyGroup> armorTypes = null,
      params Blueprint<BlueprintUnitFactReference>[] abilities)
    {
      var configurator = FeatureConfigurator.New(featureName, featureGuid)
        .SetIsClassFeature()
        .SetDisplayName(displayName)
        .SetDescription(description)
        //.SetIcon(icon)
        ;

      // Unarmored works as Light Armor and also everyone can use Light so ignore that.
      if (armorTypes is not null && !armorTypes.Contains(ArmorProficiencyGroup.Light))
      {
        List<Blueprint<BlueprintFeatureReference>> proficiencies = new();
        foreach (var type in armorTypes)
        {
          switch (type)
          {
            case ArmorProficiencyGroup.Medium:
              proficiencies.Add(FeatureRefs.MediumArmorProficiency.ToString());
              break;
            case ArmorProficiencyGroup.Heavy:
              proficiencies.Add(FeatureRefs.HeavyArmorProficiency.ToString());
              break;
            case ArmorProficiencyGroup.Buckler:
              proficiencies.Add(FeatureRefs.BucklerProficiency.ToString());
              break;
            case ArmorProficiencyGroup.LightShield:
            case ArmorProficiencyGroup.HeavyShield:
              proficiencies.Add(FeatureRefs.ShieldsProficiency.ToString());
              break;
            case ArmorProficiencyGroup.TowerShield:
              proficiencies.Add(FeatureRefs.TowerShieldProficiency.ToString());
              break;
          }
        }
        configurator.AddPrerequisiteFeaturesFromList(proficiencies, amount: 1);
      }

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
    /// Creates the armor enchant feature, buff, and ability.
    /// </summary>
    internal static BlueprintFeature CreateArmorEnchant(
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
      List<ArmorProficiencyGroup> armorTypes = null,
      params BlueprintComponent[] buffComponents)
    {
      var buffConfigurator = BuffConfigurator.New(buffName, buffGuid)
        .SetDisplayName(displayName)
        .SetDescription(description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(type, enhancementCost));

      if (armorTypes is not null)
        buffConfigurator.AddComponent(new RequireArmorType(armorTypes));

      foreach (var component in buffComponents)
        buffConfigurator.AddComponent(component);

      var buff = buffConfigurator.Configure();
      return CreateArmorEnchant(
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
