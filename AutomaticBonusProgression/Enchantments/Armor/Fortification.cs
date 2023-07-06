using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Fortification
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Fortification));

    internal static void Configure()
    {
      Logger.Log($"Configuring Fortification");

      ConfigureBasic();
      ConfigureImproved();
      ConfigureGreater();
    }

    private const string EffectName = "LA.Fortification.Effect";
    private const string BuffName = "LA.Fortification.Buff";
    private const string BuffShieldName = "LA.Fortification.Shield.Buff";

    private const string DisplayName = "LA.Fortification.Name";
    private const int BasicCost = 1;

    private static void ConfigureBasic()
    {
      var enchant = ArmorEnchantmentRefs.Fortification25Enchant.Reference.Get();
      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          enchant.m_Description.m_Key,
          BuffRefs.SacredArmorEnchantFortification25Buff.Reference.Get().Icon,
          BasicCost);

      var fortification = EnchantTool.AddEnhancementEquivalence(FeatureRefs.Fortification25Feature, enchantInfo);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(EffectName, Guids.FortificationEffect, fortification.GetComponent<AddFortification>()),
        parentBuff: new(BuffName, Guids.FortificationBuff),
        variantBuff: new(BuffShieldName, Guids.FortificationShieldBuff));
    }

    private const string ImprovedEffectName = "LA.Fortification.Improved.Effect";
    private const string ImprovedBuffName = "LA.Fortification.Improved.Buff";
    private const string ImprovedBuffShieldName = "LA.Fortification.Improved.Shield.Buff";

    private const string ImprovedDisplayName = "LA.Fortification.Improved.Name";
    private const int ImprovedCost = 3;

    internal static void ConfigureImproved()
    {
      var enchant = ArmorEnchantmentRefs.Fortification50Enchant.Reference.Get();
      var enchantInfo =
        new ArmorEnchantInfo(
          ImprovedDisplayName,
          enchant.m_Description.m_Key,
          BuffRefs.SacredArmorEnchantFortification50Buff.Reference.Get().Icon,
          ImprovedCost);

      var fortification = EnchantTool.AddEnhancementEquivalence(FeatureRefs.Fortification50Feature, enchantInfo);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(
          ImprovedEffectName, Guids.ImprovedFortificationEffect, fortification.GetComponent<AddFortification>()),
        parentBuff: new(ImprovedBuffName, Guids.ImprovedFortificationBuff),
        variantBuff: new(ImprovedBuffShieldName, Guids.ImprovedFortificationShieldBuff));
    }

    private const string GreaterEffectName = "LA.Fortification.Greater.Effect";
    private const string GreaterBuffName = "LA.Fortification.Greater.Buff";
    private const string GreaterBuffShieldName = "LA.Fortification.Greater.Shield.Buff";

    private const string GreaterDisplayName = "LA.Fortification.Greater.Name";
    private const int GreaterCost = 5;

    internal static void ConfigureGreater()
    {
      Logger.Log($"Configuring Fortification Armor (Greater)");

      var enchant = ArmorEnchantmentRefs.Fortification75Enchant.Reference.Get();
      var enchantInfo =
        new ArmorEnchantInfo(
          GreaterDisplayName,
          enchant.m_Description.m_Key,
          BuffRefs.SacredArmorEnchantFortification75Buff.Reference.Get().Icon,
          GreaterCost);

      var fortification = EnchantTool.AddEnhancementEquivalence(FeatureRefs.Fortification75Feature, enchantInfo);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(
          GreaterEffectName, Guids.GreaterFortificationEffect, fortification.GetComponent<AddFortification>()),
        parentBuff: new(GreaterBuffName, Guids.GreaterFortificationBuff),
        variantBuff: new(GreaterBuffShieldName, Guids.GreaterFortificationShieldBuff));
    }
  }
}
