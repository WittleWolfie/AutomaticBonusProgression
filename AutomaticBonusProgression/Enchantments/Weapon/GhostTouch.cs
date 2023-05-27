using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class GhostTouch
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(GhostTouch));

    private const string GhostTouchName = "LW.GhostTouch.Name";
    private const string GhostTouchEnchantCopy = "LW.GhostTouch.Enchant";
    private const string GhostTouchBuff = "LW.GhostTouch.Buff";
    private const string GhostTouchEffect = "LW.GhostTouch.Effect";
    private const string GhostTouchOffHandBuff = "LW.GhostTouch.OffHand.Buff";
    private const string GhostTouchOffHandEffect = "LW.GhostTouch.OffHand.Effect";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Ghost Touch");

      var ghostTouch = WeaponEnchantmentRefs.GhostTouch.Reference.Get();
      var ghostTouchEnchantInfo = new WeaponEnchantInfo(GhostTouchName, ghostTouch.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        ghostTouchEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(GhostTouchEffect, Guids.GhostTouchEffect, Guids.GhostTouchEnchantCopy),
        parentBuff: new(GhostTouchBuff, Guids.GhostTouchBuff));
      EnchantTool.CreateVariantEnchant(
        ghostTouchEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          GhostTouchOffHandEffect,
          Guids.GhostTouchOffHandEffect,
          Guids.GhostTouchEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(GhostTouchOffHandBuff, Guids.GhostTouchOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(ghostTouch, new(GhostTouchEnchantCopy, Guids.GhostTouchEnchantCopy), ghostTouchEnchantInfo);
    }
  }
}