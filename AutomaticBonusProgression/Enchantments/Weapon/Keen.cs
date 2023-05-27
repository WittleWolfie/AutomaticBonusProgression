using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Enums.Damage;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Keen
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Keen));

    private const string KeenName = "LW.Keen.Name";
    private const string KeenEnchantCopy = "LW.Keen.Enchant";
    private const string KeenBuff = "LW.Keen.Buff";
    private const string KeenEffect = "LW.Keen.Effect";
    private const string KeenOffHandBuff = "LW.Keen.OffHand.Buff";
    private const string KeenOffHandEffect = "LW.Keen.OffHand.Effect";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Keen");

      var Keen = WeaponEnchantmentRefs.Keen.Reference.Get();
      var KeenEnchantInfo = new WeaponEnchantInfo(
        KeenName, Keen.m_Description, "", EnhancementCost, PhysicalDamageForm.Piercing, PhysicalDamageForm.Slashing);
      EnchantTool.CreateEnchant(
        KeenEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(KeenEffect, Guids.KeenEffect, Guids.KeenEnchantCopy),
        parentBuff: new(KeenBuff, Guids.KeenBuff));
      EnchantTool.CreateVariantEnchant(
        KeenEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          KeenOffHandEffect,
          Guids.KeenOffHandEffect,
          Guids.KeenEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(KeenOffHandBuff, Guids.KeenOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(Keen, new(KeenEnchantCopy, Guids.KeenEnchantCopy), KeenEnchantInfo);
    }
  }
}