using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  internal class ShadowArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ShadowArmor));

    private const string ShadowArmorName = "LegendaryArmor.Shadow";
    private const string BuffName = "LegendaryArmor.Shadow.Buff";
    private const string AbilityName = "LegendaryArmor.Shadow.Ability";

    private const string DisplayName = "LegendaryArmor.Shadow.Name";
    private const int Enhancement = 5;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Shadow Armor");

      var equivalence = new EnhancementEquivalenceComponent(EnhancementType.Armor, Enhancement); 
      var shadowFeature = FeatureConfigurator.For(FeatureRefs.ArcaneArmorShadowFeature)
        .AddComponent(equivalence) 
        .Configure();
      ArmorEnchantmentConfigurator.For(ArmorEnchantmentRefs.ShadowArmor)
        .AddComponent(equivalence)
        .Configure();

      var enchant = ArmorEnchantmentRefs.ArcaneArmorShadowEnchant.Reference.Get();
      var buff = BuffConfigurator.New(BuffName, Guids.ShadowArmorBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddComponent(shadowFeature.GetComponent<AddStatBonus>())
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, Enhancement))
        .Configure();

      var ability = ActivatableAbilityConfigurator.New(AbilityName, Guids.ShadowArmorAbility)
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .SetBuff(buff)
        // .AddActivatableAbilityVariants()
        .AddComponent(new EnhancementEquivalentRestriction(EnhancementType.Armor, Enhancement))
        .Configure();

      return FeatureConfigurator.New(ShadowArmorName, Guids.ShadowArmor)
        .SetIsClassFeature()
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddFacts(new() { ability })
        .Configure();
    }

    private const string GreaterShadowArmorName = "LegendaryArmor.Shadow.Greater";
    private const string GreaterBuffName = "LegendaryArmor.Shadow.Greater.Buff";
    private const string GreaterAbilityName = "LegendaryArmor.Shadow.Greater.Ability";

    private const string GreaterDisplayName = "LegendaryArmor.Shadow.Name";
    private const int GreaterEnhancement = 5;

    internal static BlueprintFeature ConfigureGreater()
    {
      Logger.Log($"Configuring Shadow Armor (Greater)");

      var equivalence = new EnhancementEquivalenceComponent(EnhancementType.Armor, GreaterEnhancement);
      var shadowFeature = FeatureConfigurator.For(FeatureRefs.ArcaneArmorShadowGreaterFeature)
        .AddComponent(equivalence)
        .Configure();
      ArmorEnchantmentConfigurator.For(ArmorEnchantmentRefs.GreaterShadow)
        .AddComponent(equivalence)
        .Configure();

      var enchant = ArmorEnchantmentRefs.ArcaneArmorShadowEnchant.Reference.Get();
      var buff = BuffConfigurator.New(GreaterBuffName, Guids.GreaterShadowArmorBuff)
        .SetDisplayName(GreaterDisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddComponent(shadowFeature.GetComponent<AddStatBonus>())
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, GreaterEnhancement))
        .Configure();

      var ability = ActivatableAbilityConfigurator.New(GreaterAbilityName, Guids.GreaterShadowArmorAbility)
        .SetDisplayName(GreaterDisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .SetBuff(buff)
        // .AddActivatableAbilityVariants()
        .AddComponent(new EnhancementEquivalentRestriction(EnhancementType.Armor, GreaterEnhancement))
        .Configure();

      return FeatureConfigurator.New(GreaterShadowArmorName, Guids.GreaterShadowArmor)
        .SetIsClassFeature()
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddFacts(new() { ability })
        .Configure();
    }
  }
}
