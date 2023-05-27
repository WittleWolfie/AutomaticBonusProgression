using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Vicious
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Vicious));

    private const string ViciousName = "LW.Vicious.Name";
    private const string ViciousEnchantCopy = "LW.Vicious.Enchant";
    private const string ViciousBuff = "LW.Vicious.Buff";
    private const string ViciousEffect = "LW.Vicious.Effect";
    private const string ViciousOffHandBuff = "LW.Vicious.OffHand.Buff";
    private const string ViciousOffHandEffect = "LW.Vicious.OffHand.Effect";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Vicious");

      var vicious = WeaponEnchantmentRefs.ViciousEnchantment.Reference.Get();
      var viciousEnchantInfo = new WeaponEnchantInfo(
        ViciousName, vicious.m_Description, "", EnhancementCost, WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        viciousEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(ViciousEffect, Guids.ViciousEffect, Guids.ViciousEnchantCopy),
        parentBuff: new(ViciousBuff, Guids.ViciousBuff));
      EnchantTool.CreateVariantEnchant(
        viciousEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          ViciousOffHandEffect,
          Guids.ViciousOffHandEffect,
          Guids.ViciousEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(ViciousOffHandBuff, Guids.ViciousOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(vicious, new(ViciousEnchantCopy, Guids.ViciousEnchantCopy), viciousEnchantInfo);
    }
  }
}