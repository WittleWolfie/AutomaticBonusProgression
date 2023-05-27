using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Unholy
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Unholy));

    private const string UnholyName = "LW.Unholy.Name";
    private const string UnholyEnchantCopy = "LW.Unholy.Enchant";
    private const string UnholyBuff = "LW.Unholy.Buff";
    private const string UnholyEffect = "LW.Unholy.Effect";
    private const string UnholyOffHandBuff = "LW.Unholy.OffHand.Buff";
    private const string UnholyOffHandEffect = "LW.Unholy.OffHand.Effect";

    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Unholy");

      var unholy = WeaponEnchantmentRefs.Unholy.Reference.Get();
      var unholyEnchantInfo = new WeaponEnchantInfo(UnholyName, unholy.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        unholyEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(UnholyEffect, Guids.UnholyEffect, Guids.UnholyEnchantCopy),
        parentBuff: new(UnholyBuff, Guids.UnholyBuff));
      EnchantTool.CreateVariantEnchant(
        unholyEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          UnholyOffHandEffect,
          Guids.UnholyOffHandEffect,
          Guids.UnholyEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(UnholyOffHandBuff, Guids.UnholyOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(unholy, new(UnholyEnchantCopy, Guids.UnholyEnchantCopy), unholyEnchantInfo);
    }
  }
}