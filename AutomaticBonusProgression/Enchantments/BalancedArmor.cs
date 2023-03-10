using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
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

    private const string DisplayName = "LegendaryArmor.Balanced.Name";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Balanced Armor");

      var balancedFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ArcaneArmorBalancedFeature, EnhancementType.Armor, EnhancementCost);

      var enchant = ArmorEnchantmentRefs.ArcaneArmorBalancedEnchant.Reference.Get();
      var balanced = EnchantmentTool.CreateArmorEnchant(
        buffName: BuffName,
        buffGuid: Guids.BalancedArmorBuff,
        displayName: DisplayName,
        description: enchant.m_Description.m_Key,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: AbilityName,
        abilityGuid: Guids.BalancedArmorAbility,
        featureName: BalancedArmorName,
        Guids.BalancedArmor,
        buffComponents: balancedFeature.GetComponent<CMDBonusAgainstManeuvers>());


      return balanced;
    }
  }
}
