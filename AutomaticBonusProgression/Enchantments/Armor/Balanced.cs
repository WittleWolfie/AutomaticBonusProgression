using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Designers.Mechanics.Facts;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Balanced
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Balanced));

    private const string BalancedArmorName = "LA.Balanced";
    private const string BuffName = "LA.Balanced.Buff";
    private const string AbilityName = "LA.Balanced.Ability";

    private const string DisplayName = "LA.Balanced.Name";
    private const string Description = "LA.Balanced.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Balanced Armor");

      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, "",
        EnhancementCost,
        ranks: 1,
        ArmorProficiencyGroup.Light,
        ArmorProficiencyGroup.Medium);

      var balancedFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.ArcaneArmorBalancedFeature, enchantInfo);

      var buffInfo =
        new BlueprintInfo(BuffName, Guids.BalancedArmorBuff, balancedFeature.GetComponent<CMDBonusAgainstManeuvers>());
      var abilityInfo = new BlueprintInfo(AbilityName, Guids.BalancedArmorAbility);
      var featureInfo = new BlueprintInfo(BalancedArmorName, Guids.BalancedArmor);

      return EnchantTool.CreateEnchant(enchantInfo, buffInfo, abilityInfo, featureInfo);
    }
  }
}
