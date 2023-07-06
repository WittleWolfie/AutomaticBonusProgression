using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Cruel
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Cruel));

    private const string CruelName = "LW.Cruel.Name";
    private const string CruelEnchantCopy = "LW.Cruel.Enchant";
    private const string CruelBuff = "LW.Cruel.Buff";
    private const string CruelEffect = "LW.Cruel.Effect";
    private const string CruelOffHandBuff = "LW.Cruel.OffHand.Buff";
    private const string CruelOffHandEffect = "LW.Cruel.OffHand.Effect";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Cruel");

      var cruel = WeaponEnchantmentRefs.CruelEnchantment.Reference.Get();
      var icon = BuffRefs.CruelBuff.Reference.Get().Icon;
      var cruelEnchantInfo = new WeaponEnchantInfo(CruelName, cruel.m_Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        cruelEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(CruelEffect, Guids.CruelEffect, Guids.CruelEnchantCopy),
        parentBuff: new(CruelBuff, Guids.CruelBuff));
      EnchantTool.CreateVariantEnchant(
        cruelEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          CruelOffHandEffect,
          Guids.CruelOffHandEffect,
          Guids.CruelEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(CruelOffHandBuff, Guids.CruelOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(cruel, new(CruelEnchantCopy, Guids.CruelEnchantCopy), cruelEnchantInfo);
    }
  }
}