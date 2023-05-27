using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Axiomatic
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Axiomatic));

    private const string AxiomaticName = "LW.Axiomatic.Name";
    private const string AxiomaticEnchantCopy = "LW.Axiomatic.Enchant";
    private const string AxiomaticBuff = "LW.Axiomatic.Buff";
    private const string AxiomaticEffect = "LW.Axiomatic.Effect";
    private const string AxiomaticOffHandBuff = "LW.Axiomatic.OffHand.Buff";
    private const string AxiomaticOffHandEffect = "LW.Axiomatic.OffHand.Effect";

    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Axiomatic");

      var axiomatic = WeaponEnchantmentRefs.Axiomatic.Reference.Get();
      var axiomaticEnchantInfo = new WeaponEnchantInfo(AxiomaticName, axiomatic.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        axiomaticEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(AxiomaticEffect, Guids.AxiomaticEffect, Guids.AxiomaticEnchantCopy),
        parentBuff: new(AxiomaticBuff, Guids.AxiomaticBuff));
      EnchantTool.CreateVariantEnchant(
        axiomaticEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          AxiomaticOffHandEffect,
          Guids.AxiomaticOffHandEffect,
          Guids.AxiomaticEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(AxiomaticOffHandBuff, Guids.AxiomaticOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(axiomatic, new(AxiomaticEnchantCopy, Guids.AxiomaticEnchantCopy), axiomaticEnchantInfo);
    }
  }
}