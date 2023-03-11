using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
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
    private const string Description = "LegendaryArmor.Balanced.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Balanced Armor");

      var enchantInfo = new ArmorEnchantInfo(
        DisplayName,
        Description,
        "",
        EnhancementCost,
        ranks: 1,
        ArmorProficiencyGroup.Light,
        ArmorProficiencyGroup.Medium);

      var balancedFeature =
        EnchantmentTool.AddEnhancementEquivalence(FeatureRefs.ArcaneArmorBalancedFeature, enchantInfo);

      var buffInfo =
        new BlueprintInfo(BuffName, Guids.BalancedArmorBuff, balancedFeature.GetComponent<CMDBonusAgainstManeuvers>());
      var abilityInfo = new BlueprintInfo(AbilityName, Guids.BalancedArmorAbility);
      var featureInfo = new BlueprintInfo(BalancedArmorName, Guids.BalancedArmor);

      return EnchantmentTool.CreateEnchant(enchantInfo, buffInfo, abilityInfo, featureInfo);
    }
  }
}
