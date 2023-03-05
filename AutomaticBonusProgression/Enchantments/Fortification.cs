using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Fortification
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Fortification));

    private const string FortificationName = "LegendaryArmor.Fortification";
    private const string BuffName = "LegendaryArmor.Fortification.Buff";
    private const string AbilityName = "LegendaryArmor.Fortification.Ability";
    private const string BuffShieldName = "LegendaryArmor.Fortification.Shield.Buff";
    private const string AbilityShieldName = "LegendaryArmor.Fortification.Shield.Ability";

    private const string DisplayName = "LegendaryArmor.Fortification.Name";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Fortification");

      var fortificationFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.Fortification25Feature, EnhancementType.Armor, EnhancementCost);

      var enchant = ArmorEnchantmentRefs.Fortification25Enchant.Reference.Get();
      var ability = EnchantmentTool.CreateEnchantAbility(
        buffName: BuffName,
        buffGuid: Guids.FortificationBuff,
        displayName: DisplayName,
        description: enchant.m_Description.m_Key,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: AbilityName,
        abilityGuid: Guids.FortificationAbility,
        buffComponents: fortificationFeature.GetComponent<AddFortification>());
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        ability,
        buffName: BuffShieldName,
        buffGuid: Guids.FortificationShieldBuff,
        abilityName: AbilityShieldName,
        abilityGuid: Guids.FortificationShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName,
        description: enchant.m_Description.m_Key,
        //icon: ??,
        featureName: FortificationName,
        Guids.Fortification,
        featureRanks: EnhancementCost,
        prerequisiteFeature: "",
        prerequisiteRanks: 1,
        ability,
        abilityShield);
    }

    private const string ImprovedFortificationName = "LegendaryArmor.Fortification.Improved";
    private const string ImprovedBuffName = "LegendaryArmor.Fortification.Improved.Buff";
    private const string ImprovedAbilityName = "LegendaryArmor.Fortification.Improved.Ability";
    private const string ImprovedBuffShieldName = "LegendaryArmor.Fortification.Improved.Shield.Buff";
    private const string ImprovedAbilityShieldName = "LegendaryArmor.Fortification.Improved.Shield.Ability";

    private const string ImprovedDisplayName = "LegendaryArmor.Fortification.Improved.Name";
    private const int ImprovedEnhancementCost = 3;

    internal static BlueprintFeature ConfigureImproved()
    {
      Logger.Log($"Configuring Fortification Armor (Improved)");

      var fortificationFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.Fortification50Feature, EnhancementType.Armor, ImprovedEnhancementCost);

      var enchant = ArmorEnchantmentRefs.Fortification50Enchant.Reference.Get();
      var ability = EnchantmentTool.CreateEnchantAbility(
        buffName: ImprovedBuffName,
        buffGuid: Guids.ImprovedFortificationBuff,
        displayName: ImprovedDisplayName,
        description: enchant.m_Description.m_Key,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: ImprovedEnhancementCost,
        abilityName: ImprovedAbilityName,
        abilityGuid: Guids.ImprovedFortificationAbility,
        buffComponents: fortificationFeature.GetComponent<AddFortification>());
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        ability,
        buffName: ImprovedBuffShieldName,
        buffGuid: Guids.ImprovedFortificationShieldBuff,
        abilityName: ImprovedAbilityShieldName,
        abilityGuid: Guids.ImprovedFortificationShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName,
        description: enchant.m_Description.m_Key,
        //icon: ??,
        featureName: ImprovedFortificationName,
        Guids.ImprovedFortification,
        featureRanks: 2,
        prerequisiteFeature: Guids.Fortification,
        prerequisiteRanks: 1,
        ability,
        abilityShield);
    }

    private const string GreaterFortificationName = "LegendaryArmor.Fortification.Greater";
    private const string GreaterBuffName = "LegendaryArmor.Fortification.Greater.Buff";
    private const string GreaterAbilityName = "LegendaryArmor.Fortification.Greater.Ability";
    private const string GreaterBuffShieldName = "LegendaryArmor.Fortification.Greater.Shield.Buff";
    private const string GreaterAbilityShieldName = "LegendaryArmor.Fortification.Greater.Shield.Ability";

    private const string GreaterDisplayName = "LegendaryArmor.Fortification.Greater.Name";
    private const string GreaterDescription = "LegendaryArmor.Fortification.Greater.Description";
    private const int GreaterEnhancementCost = 5;

    internal static BlueprintFeature ConfigureGreater()
    {
      Logger.Log($"Configuring Fortification Armor (Greater)");

      var fortificationFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.Fortification75Feature, EnhancementType.Armor, GreaterEnhancementCost);

      var enchant = ArmorEnchantmentRefs.Fortification75Enchant.Reference.Get();
      var ability = EnchantmentTool.CreateEnchantAbility(
        buffName: GreaterBuffName,
        buffGuid: Guids.GreaterFortificationBuff,
        displayName: GreaterDisplayName,
        description: enchant.m_Description.m_Key,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: GreaterEnhancementCost,
        abilityName: GreaterAbilityName,
        abilityGuid: Guids.GreaterFortificationAbility,
        buffComponents: fortificationFeature.GetComponent<AddFortification>());
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        ability,
        buffName: GreaterBuffShieldName,
        buffGuid: Guids.GreaterFortificationShieldBuff,
        abilityName: GreaterAbilityShieldName,
        abilityGuid: Guids.GreaterFortificationShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName,
        description: enchant.m_Description.m_Key,
        //icon: ??,
        featureName: GreaterFortificationName,
        Guids.GreaterFortification,
        featureRanks: 2,
        prerequisiteFeature: Guids.ImprovedFortification,
        prerequisiteRanks: 2,
        ability,
        abilityShield);
    }
  }
}
