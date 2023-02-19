using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Patches;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.FactLogic;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class ShadowArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ShadowArmor));

    private const string ShadowArmorName = "LegendaryArmor.Shadow";
    private const string BuffName = "LegendaryArmor.Shadow.Buff";
    private const string AbilityName = "LegendaryArmor.Shadow.Ability";

    private const string DisplayName = "LegendaryArmor.Shadow.Name";
    private const int Enhancement = 2;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Shadow Armor");

      var shadowFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ArcaneArmorShadowFeature, EnhancementType.Armor, Enhancement);
      EnchantmentTool.AddEnhancementEquivalence(ArmorEnchantmentRefs.ShadowArmor, EnhancementType.Armor, Enhancement);

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
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .SetGroup(ExpandedActivatableAbilityGroup.LegendaryArmor)
        .SetHiddenInUI()
        .AddComponent(new EnhancementEquivalentRestriction(EnhancementType.Armor, Enhancement))
        .Configure();

      return FeatureConfigurator.New(ShadowArmorName, Guids.ShadowArmor)
        .SetIsClassFeature()
        .SetDisplayName(DisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .SetRanks(Enhancement)
        .AddRecommendationHasFeature(Guids.ShadowArmor)
        .AddComponent(new AddFactsOnRank(rank: Enhancement, ability))
        .Configure();
    }

    private const string ImprovedShadowArmorName = "LegendaryArmor.Shadow.Improved";
    private const string ImprovedBuffName = "LegendaryArmor.Shadow.Improved.Buff";
    private const string ImprovedAbilityName = "LegendaryArmor.Shadow.Improved.Ability";

    private const string ImprovedDisplayName = "LegendaryArmor.Shadow.Improved.Name";
    private const int ImprovedEnhancement = 4;

    internal static BlueprintFeature ConfigureImproved()
    {
      Logger.Log($"Configuring Shadow Armor (Improved)");

      var shadowFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ArcaneArmorShadowGreaterFeature, EnhancementType.Armor, ImprovedEnhancement);
      EnchantmentTool.AddEnhancementEquivalence(
        ArmorEnchantmentRefs.GreaterShadow, EnhancementType.Armor, ImprovedEnhancement);

      var enchant = ArmorEnchantmentRefs.ArcaneArmorShadowGreaterEnchant.Reference.Get();
      var buff = BuffConfigurator.New(ImprovedBuffName, Guids.ImprovedShadowArmorBuff)
        .SetDisplayName(ImprovedDisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddComponent(shadowFeature.GetComponent<AddStatBonus>())
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, ImprovedEnhancement))
        .Configure();

      var ability = ActivatableAbilityConfigurator.New(ImprovedAbilityName, Guids.ImprovedShadowArmorAbility)
        .SetDisplayName(ImprovedDisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .SetBuff(buff)
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .SetGroup(ExpandedActivatableAbilityGroup.LegendaryArmor)
        .SetHiddenInUI()
        .AddComponent(new EnhancementEquivalentRestriction(EnhancementType.Armor, ImprovedEnhancement))
        .Configure();

      return FeatureConfigurator.New(ImprovedShadowArmorName, Guids.ImprovedShadowArmor)
        .SetIsClassFeature()
        .SetDisplayName(ImprovedDisplayName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .SetRanks(2) // Require shadow Armor + 2 more to get this. Basically turns this into a chain of things.
        .AddRecommendationHasFeature(Guids.ImprovedShadowArmor)
        .AddComponent(new PrerequisiteHasFeatureRanks(Guids.ShadowArmor, 2))
        .AddComponent(new AddFactsOnRank(rank: 2, ability))
        .Configure();
    }

    private const string GreaterShadowArmorName = "LegendaryArmor.Shadow.Greater";
    private const string GreaterBuffName = "LegendaryArmor.Shadow.Greater.Buff";
    private const string GreaterAbilityName = "LegendaryArmor.Shadow.Greater.Ability";

    private const string GreaterDisplayName = "LegendaryArmor.Shadow.Greater.Name";
    private const string GreaterDescription = "LegendaryArmor.Shadow.Greater.Description";
    private const int GreaterEnhancement = 5;

    internal static BlueprintFeature ConfigureGreater()
    {
      Logger.Log($"Configuring Shadow Armor (Greater)");

      var buff = BuffConfigurator.New(GreaterBuffName, Guids.GreaterShadowArmorBuff)
        .SetDisplayName(GreaterDisplayName)
        .SetDescription(GreaterDescription)
        //.SetIcon()
        .AddStatBonus(stat: StatType.SkillStealth, value: 15, descriptor: ModifierDescriptor.Competence)
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, GreaterEnhancement))
        .Configure();

      var ability = ActivatableAbilityConfigurator.New(GreaterAbilityName, Guids.GreaterShadowArmorAbility)
        .SetDisplayName(GreaterDisplayName)
        .SetDescription(GreaterDescription)
        //.SetIcon()
        .SetBuff(buff)
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .SetGroup(ExpandedActivatableAbilityGroup.LegendaryArmor)
        .SetHiddenInUI()
        .AddComponent(new EnhancementEquivalentRestriction(EnhancementType.Armor, GreaterEnhancement))
        .Configure();

      return FeatureConfigurator.New(GreaterShadowArmorName, Guids.GreaterShadowArmor)
        .SetIsClassFeature()
        .SetDisplayName(GreaterDisplayName)
        .SetDescription(GreaterDescription)
        //.SetIcon()
        .AddComponent(new PrerequisiteHasFeatureRanks(Guids.ImprovedShadowArmor, 2))
        .AddFacts(new() { ability })
        .Configure();
    }
  }
}
