using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Holy
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Holy));

    private const string HolyName = "LW.Holy.Name";
    private const string HolyEnchantCopy = "LW.Holy.Enchant";
    private const string HolyBuff = "LW.Holy.Buff";
    private const string HolyEffect = "LW.Holy.Effect";
    private const string HolyOffHandBuff = "LW.Holy.OffHand.Buff";
    private const string HolyOffHandEffect = "LW.Holy.OffHand.Effect";

    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Holy");

      var holy = WeaponEnchantmentRefs.Holy.Reference.Get();
      var icon = BuffRefs.ArcaneWeaponHolyBuff.Reference.Get().Icon;
      var holyEnchantInfo = new WeaponEnchantInfo(HolyName, holy.m_Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        holyEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(HolyEffect, Guids.HolyEffect, Guids.HolyEnchantCopy),
        parentBuff: new(HolyBuff, Guids.HolyBuff));
      EnchantTool.CreateVariantEnchant(
        holyEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          HolyOffHandEffect,
          Guids.HolyOffHandEffect,
          Guids.HolyEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(HolyOffHandBuff, Guids.HolyOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(holy, new(HolyEnchantCopy, Guids.HolyEnchantCopy), holyEnchantInfo);
    }
  }
}