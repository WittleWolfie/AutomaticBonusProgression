using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;

namespace AutomaticBonusProgression.Enchantments
{
  internal class BalancedArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BalancedArmor));

    private const string BalancedArmorName = "LegendaryArmor.Balanced";
    private const string BuffName = "LegendaryArmor.Balanced.Buff";
    private const string AbilityName = "LegendaryArmor.Balanced.Ability";
    private const string ShieldAbilityName = "LegendaryArmor.Balanced.Ability.Shield";

    private const string DisplayName = "LegendaryArmor.Balanced.Name";
    private const int Enhancement = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Balanced Armor");

      var balancedFeature =
        Common.AddEnhancementEquivalence(
          FeatureRefs.ArcaneArmorBalancedFeature, EnhancementType.Armor, Enhancement);

      var enchant = ArmorEnchantmentRefs.ArcaneArmorBalancedEnchant.Reference.Get();
      var buff = BuffConfigurator.New(BuffName, Guids.BalancedArmorBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddComponent(balancedFeature.GetComponent<CMDBonusAgainstManeuvers>())
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, Enhancement))
        .Configure();

      var ability = ActivatableAbilityConfigurator.New(AbilityName, Guids.BalancedArmorAbility)
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .SetBuff(buff)
        // .AddActivatableAbilityVariants()
        .AddComponent(new EnhancementEquivalentRestriction(EnhancementType.Armor, Enhancement))
        .Configure();

      var shieldAbility = ActivatableAbilityConfigurator.New(ShieldAbilityName, Guids.BalancedArmorShieldAbility)
        .CopyFrom(ability)
        .AddComponent(new EnhancementEquivalentRestriction(EnhancementType.Shield, Enhancement))
        .Configure();

      return FeatureConfigurator.New(BalancedArmorName, Guids.BalancedArmor)
        .SetIsClassFeature()
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddFacts(new() { ability, shieldAbility })
        .Configure();
    }
  }
}
