using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Nullifying
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Nullifying));

    private const string NullifyingName = "LW.Nullifying.Name";
    private const string NullifyingEnchantCopy = "LW.Nullifying.Enchant";
    private const string NullifyingBuff = "LW.Nullifying.Buff";
    private const string NullifyingEffect = "LW.Nullifying.Effect";
    private const string NullifyingOffHandBuff = "LW.Nullifying.OffHand.Buff";
    private const string NullifyingOffHandEffect = "LW.Nullifying.OffHand.Effect";

    private const int EnhancementCost = 3;

    internal static void Configure()
    {
      Logger.Log($"Configuring Nullifying");

      var Nullifying = WeaponEnchantmentRefs.NullifyingEnchantment.Reference.Get();
      var NullifyingEnchantInfo = new WeaponEnchantInfo(
        NullifyingName,
        Nullifying.m_Description,
        "",
        EnhancementCost,
        WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        NullifyingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(NullifyingEffect, Guids.NullifyingEffect, Guids.NullifyingEnchantCopy),
        parentBuff: new(NullifyingBuff, Guids.NullifyingBuff));
      EnchantTool.CreateVariantEnchant(
        NullifyingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          NullifyingOffHandEffect,
          Guids.NullifyingOffHandEffect,
          Guids.NullifyingEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(NullifyingOffHandBuff, Guids.NullifyingOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(Nullifying, new(NullifyingEnchantCopy, Guids.NullifyingEnchantCopy), NullifyingEnchantInfo);
    }
  }
}