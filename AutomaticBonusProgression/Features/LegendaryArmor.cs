using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryArmor));

    // GENERAL Approach:
    //  - Create EnhancementEquivalent buff which stacks to limit enhancement bonus
    //  - Update existing enchantments to apply the buff appropriately
    //  - Create buffs (as needed) to apply the enchantment effects
    //    - These are used by LegendaryX abilities to apply the enchantment so it doesn't need to apply to items

    // Armor In Game
    // - ArcaneArmorBalanced [DONE]
    // - Fortification
    // - Shadow
    // - Spell Resistance
    // - Invulnerability
    // - Energy Resistance
    //
    // Maybe Add
    // - Bolstering
    // - Champion
    // - Dastard
    // - Deathless
    // - Defiant
    // - Expeditious
    // - Creeping
    // - Rallying?
    // - Brawling
    // - Putrid?
    // - Ghost
    // - Martyring
    // - Righteous
    // - Unbound?
    // - Unrighteous?
    // - Vigilant?
    // - Determination
    // - Etherealness
    // (Shield)
    // - Bashing
    // - Blinding
    // - Wyrmsbreath
    // - Reflecting

    private const string LegendaryArmorName = "LegendaryArmor";
    private const string LegendaryArmorDisplayName = "LegendaryArmor.Name";
    private const string LegendaryArmorDescription = "LegendaryArmor.Description";

    internal static BlueprintFeature ConfigureArmor()
    {
      Logger.Log("Configuring Legendary Armor");

      return FeatureSelectionConfigurator.New(LegendaryArmorName, Guids.LegendaryArmor)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryArmorDisplayName)
        .SetDescription(LegendaryArmorDescription)
        //.SetIcon()
        .AddToAllFeatures(ConfigureBalancedArmor())
        .Configure();
    }

    private const string LegendaryShieldName = "LegendaryShield";
    private const string LegendaryShieldDisplayName = "LegendaryShield.Name";
    private const string LegendaryShieldDescription = "LegendaryShield.Description";

    internal static BlueprintFeature ConfigureShield()
    {
      Logger.Log("Configuring Legendary Shield");

      return FeatureSelectionConfigurator.New(LegendaryShieldName, Guids.LegendaryShield)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryShieldDisplayName)
        .SetDescription(LegendaryShieldDescription)
        //.SetIcon()
        .Configure();
    }

    private const string BalancedArmorName = "LegendaryArmor.Balanced";
    private const string BalancedArmorAbilityName = "LegendaryArmor.Balanced.Ability";
    private static BlueprintFeature ConfigureBalancedArmor()
    {
      Logger.Log("Configuring Balanced Legendary Armor");

      var balancedEnchant = ArmorEnchantmentRefs.ArcaneArmorBalancedEnchant.Reference.Get();
      var ability = ActivatableAbilityConfigurator.New(BalancedArmorAbilityName, Guids.BalancedArmorAbility)
        .SetDisplayName(balancedEnchant.m_EnchantName)
        .SetDescription(balancedEnchant.m_Description)
        //.SetIcon()
        .SetBuff(BalancedArmor.Configure())
       // .AddActivatableAbilityVariants()
        .AddComponent(new EnhancementEquivalenceRestriction(EnhancementType.Armor, 1))
        .Configure();

      return FeatureConfigurator.New(BalancedArmorName, Guids.BalancedArmor)
        .SetIsClassFeature()
        .SetDisplayName(balancedEnchant.m_EnchantName)
        .SetDescription(balancedEnchant.m_Description)
        //.SetIcon()
        .AddFacts(new() { ability } )
        .Configure();
    }
  }
}
