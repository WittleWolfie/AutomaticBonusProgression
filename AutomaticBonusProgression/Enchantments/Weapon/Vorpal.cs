using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Enums.Damage;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Vorpal
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Vorpal));

    private const string VorpalName = "LW.Vorpal.Name";
    private const string VorpalEnchantCopy = "LW.Vorpal.Enchant";
    private const string VorpalBuff = "LW.Vorpal.Buff";
    private const string VorpalEffect = "LW.Vorpal.Effect";
    private const string VorpalOffHandBuff = "LW.Vorpal.OffHand.Buff";
    private const string VorpalOffHandEffect = "LW.Vorpal.OffHand.Effect";

    private const int EnhancementCost = 5;

    internal static void Configure()
    {
      Logger.Log($"Configuring Vorpal");

      var vorpal = WeaponEnchantmentRefs.Vorpal.Reference.Get();
      var vorpalEnchantInfo = new WeaponEnchantInfo(
        VorpalName, vorpal.m_Description, "", EnhancementCost, PhysicalDamageForm.Slashing);
      EnchantTool.CreateEnchant(
        vorpalEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(VorpalEffect, Guids.VorpalEffect, Guids.VorpalEnchantCopy),
        parentBuff: new(VorpalBuff, Guids.VorpalBuff));
      EnchantTool.CreateVariantEnchant(
        vorpalEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          VorpalOffHandEffect,
          Guids.VorpalOffHandEffect,
          Guids.VorpalEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(VorpalOffHandBuff, Guids.VorpalOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(vorpal, new(VorpalEnchantCopy, Guids.VorpalEnchantCopy), vorpalEnchantInfo);
    }
  }
}