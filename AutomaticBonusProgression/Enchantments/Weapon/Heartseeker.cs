using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Heartseeker
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Heartseeker));

    private const string HeartseekerName = "LW.Heartseeker.Name";
    private const string HeartseekerEnchantCopy = "LW.Heartseeker.Enchant";
    private const string HeartseekerBuff = "LW.Heartseeker.Buff";
    private const string HeartseekerEffect = "LW.Heartseeker.Effect";
    private const string HeartseekerOffHandBuff = "LW.Heartseeker.OffHand.Buff";
    private const string HeartseekerOffHandEffect = "LW.Heartseeker.OffHand.Effect";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Heartseeker");

      var heartseeker = WeaponEnchantmentRefs.Heartseeker.Reference.Get();
      var heartseekerEnchantInfo = new WeaponEnchantInfo(
        HeartseekerName, heartseeker.m_Description, "", EnhancementCost, WeaponRangeType.Ranged);
      EnchantTool.CreateEnchant(
        heartseekerEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(HeartseekerEffect, Guids.HeartseekerEffect, Guids.HeartseekerEnchantCopy),
        parentBuff: new(HeartseekerBuff, Guids.HeartseekerBuff));
      EnchantTool.CreateVariantEnchant(
        heartseekerEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          HeartseekerOffHandEffect,
          Guids.HeartseekerOffHandEffect,
          Guids.HeartseekerEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(HeartseekerOffHandBuff, Guids.HeartseekerOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(heartseeker, new(HeartseekerEnchantCopy, Guids.HeartseekerEnchantCopy), heartseekerEnchantInfo);
    }
  }
}