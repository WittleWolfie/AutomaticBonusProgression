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

      var icon = BuffRefs.DefensiveSpinBuff.Reference.Get().Icon;
      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName, Description, icon, EnhancementCost, ArmorProficiencyGroup.Light, ArmorProficiencyGroup.Medium);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.ArcaneArmorBalancedEnchant, enchantInfo);

      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff:
          new(
            BuffName,
            Guids.BalancedEffect,
            FeatureRefs.ArcaneArmorBalancedFeature.Reference.Get().GetComponent<CMDBonusAgainstManeuvers>()),
        parentBuff: new(BalancedArmorName, Guids.BalancedBuff));
    }
  }
}
