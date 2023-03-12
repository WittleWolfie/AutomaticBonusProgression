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
      Blueprint<BlueprintReference<BlueprintFeature>> feature, EnchantInfo enchant)
    {
      return FeatureConfigurator.For(feature)
        .AddComponent(new EnhancementEquivalenceComponent(enchant))
        .Configure();
    }

    internal static BlueprintArmorEnchantment AddEnhancementEquivalenceArmor(
      Blueprint<BlueprintReference<BlueprintArmorEnchantment>> enchantment, EnchantInfo enchant)
    {
      return ArmorEnchantmentConfigurator.For(enchantment)
        .AddComponent(new EnhancementEquivalenceComponent(enchant))
        .Configure();
    }

    internal static BlueprintWeaponEnchantment AddEnhancementEquivalenceWeapon(
      Blueprint<BlueprintReference<BlueprintWeaponEnchantment>> enchantment, EnchantInfo enchant)
    {
      return WeaponEnchantmentConfigurator.For(enchantment)
        .AddComponent(new EnhancementEquivalenceComponent(enchant))
        .Configure();
    }

    /// <summary>
    /// Creates the armor enchant feature, buff, and ability.
    /// </summary>
    internal static BlueprintFeature CreateEnchant(
      ArmorEnchantInfo enchant, BlueprintInfo buff, BlueprintInfo ability, BlueprintInfo feature)
    {
      var buffConfigurator = BuffConfigurator.New(buff.Name, buff.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        //.SetIcon(enchant.Icon)
        .AddComponent(new EnhancementEquivalenceComponent(enchant));

      if (enchant.AllowedTypes.Any())
        buffConfigurator.AddComponent(new RequireArmor(enchant.AllowedTypes));

      foreach (var component in buff.Components)
        buffConfigurator.AddComponent(component);

      return CreateEnchant(enchant, buffConfigurator.Configure(), ability, feature);
    }

    /// <summary>
    /// Creates the armor enchant feature and ability using the specified buff.
    /// </summary>
    internal static BlueprintFeature CreateEnchant(
      ArmorEnchantInfo enchant, BlueprintBuff buff, BlueprintInfo ability, BlueprintInfo feature)
    {
      return CreateEnchantFeature(enchant, feature, CreateEnchantAbility(enchant, buff, ability));
    }

    /// <summary>
    /// Creates the armor enchant buff and ability.
    /// </summary>
    internal static BlueprintActivatableAbility CreateEnchantAbility(
      ArmorEnchantInfo enchant, BlueprintInfo buff, BlueprintInfo ability)
    {
      var buffConfigurator = BuffConfigurator.New(buff.Name, buff.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        //.SetIcon(enchant.Icon)
        .AddComponent(new EnhancementEquivalenceComponent(enchant));

      if (enchant.AllowedTypes.Any())
        buffConfigurator.AddComponent(new RequireArmor(enchant.AllowedTypes));

      foreach (var component in buff.Components)
        buffConfigurator.AddComponent(component);

      return CreateEnchantAbility(enchant, buffConfigurator.Configure(), ability);
    }

    /// <summary>
    /// Creates the armor enchant ability which activates the buff.
    /// </summary>
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
        .AddComponent(new EnhancementRestriction(enchant))
        .AddComponent<OutOfCombatRestriction>()
        .SetHiddenInUI();

      if (enchant.AllowedTypes.Any())
        abilityConfigurator.AddComponent(new ArmorRestriction(enchant.AllowedTypes));

      foreach (var component in ability.Components)
        abilityConfigurator.AddComponent(component);

      return abilityConfigurator.Configure();
    }

    /// <summary>
    /// Creates the armor enchant feature which grants the specified abilities.
    /// </summary>
    internal static BlueprintFeature CreateEnchantFeature(
      ArmorEnchantInfo enchant, BlueprintInfo feature, params Blueprint<BlueprintUnitFactReference>[] abilities)
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

      foreach (var component in feature.Components)
        featureConfigurator.AddComponent(component);

      return featureConfigurator.Configure();
    }

    /// <summary>
    /// Creates a variant of the armor enchant for use with shields
    /// </summary>
    internal static BlueprintActivatableAbility CreateEnchantShieldVariant(
      ArmorEnchantInfo enchant, BlueprintActivatableAbility armorEnchant, BlueprintInfo buff, BlueprintInfo ability)
    {
      var buffConfigurator = BuffConfigurator.New(buff.Name, buff.Guid)
        .CopyFrom(armorEnchant.Buff, c => c is not EnhancementEquivalenceComponent && c is not RequireArmor)
        .AddComponent(new EnhancementEquivalenceComponent(enchant, typeOverride: EnhancementType.Shield))
        .AddComponent(new RequireShield(enchant.AllowedTypes));

      foreach (var component in buff.Components)
        buffConfigurator.AddComponent(component);

      var abilityConfigurator = ActivatableAbilityConfigurator.New(ability.Name, ability.Guid)
        .CopyFrom(armorEnchant, c => c is not EnhancementRestriction && c is not ArmorRestriction)
        .AddComponent(new EnhancementRestriction(enchant, typeOverride: EnhancementType.Shield))
        .AddComponent(new ShieldRestriction(enchant.AllowedTypes))
        .SetBuff(buffConfigurator.Configure());

      foreach (var component in ability.Components)
        abilityConfigurator.AddComponent(component);

      return abilityConfigurator.Configure();
    }

    /// <summary>
    /// Creates a variant of the armor enchant for use with shields
    /// </summary>
    internal static BlueprintActivatableAbility CreateEnchantShieldVariant(
      ArmorEnchantInfo enchant, BlueprintInfo buff, BlueprintInfo ability)
    {
      var buffConfigurator = BuffConfigurator.New(buff.Name, buff.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        //.SetIcon(enchant.Icon)
        .AddComponent(new EnhancementEquivalenceComponent(enchant, typeOverride: EnhancementType.Shield))
        .AddComponent(new RequireShield(enchant.AllowedTypes));

      foreach (var component in buff.Components)
        buffConfigurator.AddComponent(component);

      var abilityConfigurator = ActivatableAbilityConfigurator.New(ability.Name, ability.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        //.SetIcon(icon)
        .SetHiddenInUI()
        .SetBuff(buffConfigurator.Configure())
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddComponent(new EnhancementRestriction(enchant, typeOverride: EnhancementType.Shield))
        .AddComponent(new ShieldRestriction(enchant.AllowedTypes))
        .AddComponent<OutOfCombatRestriction>();

      foreach (var component in ability.Components)
        abilityConfigurator.AddComponent(component);

      return abilityConfigurator.Configure();
    }
  }
}
