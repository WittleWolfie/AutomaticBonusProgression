using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Designers.Mechanics.Facts;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Balanced
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Balanced));

    private const string BalancedArmorName = "LA.Balanced";
    private const string BuffName = "LA.Balanced.Buff";
    private const string ShieldBuffName = "LA.Balanced.Shield.Buff";

    private const string DisplayName = "LA.Balanced.Name";
    private const string Description = "LA.Balanced.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Balanced Armor");

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName, Description, "", EnhancementCost, ArmorProficiencyGroup.Light, ArmorProficiencyGroup.Medium);
      var balancedFeature = EnchantTool.AddEnhancementEquivalence(FeatureRefs.ArcaneArmorBalancedFeature, enchantInfo);

      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(BuffName, Guids.BalancedEffect, balancedFeature.GetComponent<CMDBonusAgainstManeuvers>()),
        parentBuff: new(BalancedArmorName, Guids.BalancedBuff));
    }
  }
}
