using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace AutomaticBonusProgression.Enchantments
{
  internal class BalancedArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BalancedArmor));

    private const string Name = "BalancedArmor";

    internal static BlueprintBuff Configure()
    {
      Logger.Log($"Configuring Balanced Armor");

      var balancedFeature = FeatureConfigurator.For(FeatureRefs.ArcaneArmorBalancedFeature)
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, 1))
        .Configure();

      var enchant = ArmorEnchantmentRefs.ArcaneArmorBalancedEnchant.Reference.Get();
      return BuffConfigurator.New(Name, Guids.BalancedArmorBuff)
        .SetDisplayName(enchant.m_EnchantName)
        .SetDescription(enchant.m_Description)
        //.SetIcon()
        .AddComponent(balancedFeature.GetComponent<CMDBonusAgainstManeuvers>())
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, 1, checkOnChange: true))
        .Configure();
    }
  }
}
