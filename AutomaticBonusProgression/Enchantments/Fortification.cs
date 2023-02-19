using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  // TODO: Functional testing to make sure these enchants I made _work_, especially w/ refactor
  internal class Fortification
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Fortification));

    private const string FortificationName = "LegendaryArmor.Fortification";
    private const string BuffName = "LegendaryArmor.Fortification.Buff";
    private const string AbilityName = "LegendaryArmor.Fortification.Ability";

    private const string DisplayName = "LegendaryArmor.Fortification.Light.Name";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Fortification");

      var fortificationFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.Fortification25Feature, EnhancementType.Armor, EnhancementCost);

      var enchant = ArmorEnchantmentRefs.Fortification25Enchant.Reference.Get();
      return EnchantmentTool.CreateEnchant(
        buffName: BuffName,
        buffGuid: Guids.FortificationBuff,
        displayName: DisplayName,
        description: enchant.m_Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: AbilityName,
        abilityGuid: Guids.FortificationAbility,
        featureName: FortificationName,
        Guids.Fortification,
        buffComponents: fortificationFeature.GetComponent<AddFortification>());
    }

    private const string ImprovedFortificationName = "LegendaryArmor.Fortification.Improved";
    private const string ImprovedBuffName = "LegendaryArmor.Fortification.Improved.Buff";
    private const string ImprovedAbilityName = "LegendaryArmor.Fortification.Improved.Ability";

    private const string ImprovedDisplayName = "LegendaryArmor.Fortification.Improved.Name";
    private const int ImprovedEnhancementCost = 3;

    internal static BlueprintFeature ConfigureImproved()
    {
      Logger.Log($"Configuring Fortification Armor (Improved)");

      var fortificationFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.Fortification50Feature, EnhancementType.Armor, ImprovedEnhancementCost);

      var enchant = ArmorEnchantmentRefs.Fortification50Enchant.Reference.Get();
      return EnchantmentTool.CreateEnchant(
        buffName: ImprovedBuffName,
        buffGuid: Guids.ImprovedFortificationBuff,
        displayName: ImprovedDisplayName,
        description: enchant.m_Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: ImprovedEnhancementCost,
        abilityName: ImprovedAbilityName,
        abilityGuid: Guids.ImprovedFortificationAbility,
        featureName: ImprovedFortificationName,
        Guids.ImprovedFortification,
        ranks: 2,
        prerequisiteFeature: Guids.Fortification,
        buffComponents: fortificationFeature.GetComponent<AddFortification>());
    }

    private const string GreaterFortificationName = "LegendaryArmor.Fortification.Greater";
    private const string GreaterBuffName = "LegendaryArmor.Fortification.Greater.Buff";
    private const string GreaterAbilityName = "LegendaryArmor.Fortification.Greater.Ability";

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
      return EnchantmentTool.CreateEnchant(
        buffName: GreaterBuffName,
        buffGuid: Guids.GreaterFortificationBuff,
        displayName: GreaterDisplayName,
        description: enchant.m_Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: GreaterEnhancementCost,
        abilityName: GreaterAbilityName,
        abilityGuid: Guids.GreaterFortificationAbility,
        featureName: GreaterFortificationName,
        Guids.GreaterFortification,
        ranks: 2,
        prerequisiteFeature: Guids.GreaterFortification,
        buffComponents: fortificationFeature.GetComponent<AddFortification>());
    }
  }
}
