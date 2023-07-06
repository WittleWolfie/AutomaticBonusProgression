using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Disruption
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Disruption));

    private const string DisruptionName = "LW.Disruption.Name";
    private const string DisruptionEnchantCopy = "LW.Disruption.Enchant";
    private const string DisruptionBuff = "LW.Disruption.Buff";
    private const string DisruptionEffect = "LW.Disruption.Effect";
    private const string DisruptionOffHandBuff = "LW.Disruption.OffHand.Buff";
    private const string DisruptionOffHandEffect = "LW.Disruption.OffHand.Effect";

    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Disruption");

      var disruption = WeaponEnchantmentRefs.Disruption.Reference.Get();
      var icon = BuffRefs.WeaponBondDisruptionBuff.Reference.Get().Icon;
      var disruptionEnchantInfo = new WeaponEnchantInfo(
        DisruptionName,
        disruption.m_Description,
        icon,
        EnhancementCost,
        allowedRanges: new() { WeaponRangeType.Melee },
        allowedForms: new() { PhysicalDamageForm.Bludgeoning });
      EnchantTool.CreateEnchant(
        disruptionEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(DisruptionEffect, Guids.DisruptionEffect, Guids.DisruptionEnchantCopy),
        parentBuff: new(DisruptionBuff, Guids.DisruptionBuff));
      EnchantTool.CreateVariantEnchant(
        disruptionEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          DisruptionOffHandEffect,
          Guids.DisruptionOffHandEffect,
          Guids.DisruptionEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(DisruptionOffHandBuff, Guids.DisruptionOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(disruption, new(DisruptionEnchantCopy, Guids.DisruptionEnchantCopy), disruptionEnchantInfo);
    }
  }
}