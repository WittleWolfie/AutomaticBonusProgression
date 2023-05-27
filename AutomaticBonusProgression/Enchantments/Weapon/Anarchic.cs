using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Anarchic
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Anarchic));

    private const string AnarchicName = "LW.Anarchic.Name";
    private const string AnarchicEnchantCopy = "LW.Anarchic.Enchant";
    private const string AnarchicBuff = "LW.Anarchic.Buff";
    private const string AnarchicEffect = "LW.Anarchic.Effect";
    private const string AnarchicOffHandBuff = "LW.Anarchic.OffHand.Buff";
    private const string AnarchicOffHandEffect = "LW.Anarchic.OffHand.Effect";

    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Anarchic");

      var anarchic = WeaponEnchantmentRefs.Anarchic.Reference.Get();
      var anarchicEnchantInfo = new WeaponEnchantInfo(AnarchicName, anarchic.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        anarchicEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(AnarchicEffect, Guids.AnarchicEffect, Guids.AnarchicEnchantCopy),
        parentBuff: new(AnarchicBuff, Guids.AnarchicBuff));
      EnchantTool.CreateVariantEnchant(
        anarchicEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          AnarchicOffHandEffect,
          Guids.AnarchicOffHandEffect,
          Guids.AnarchicEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(AnarchicOffHandBuff, Guids.AnarchicOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(anarchic, new(AnarchicEnchantCopy, Guids.AnarchicEnchantCopy), anarchicEnchantInfo);
    }
  }
}