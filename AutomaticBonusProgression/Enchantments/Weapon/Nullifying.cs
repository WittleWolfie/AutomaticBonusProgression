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

      var nullifying = WeaponEnchantmentRefs.NullifyingEnchantment.Reference.Get();
      var icon = AbilityRefs.RemoveFear.Reference.Get().Icon;
      var nullifyingEnchantInfo = new WeaponEnchantInfo(
        NullifyingName,
        nullifying.m_Description,
        icon,
        EnhancementCost,
        WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        nullifyingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(NullifyingEffect, Guids.NullifyingEffect, Guids.NullifyingEnchantCopy),
        parentBuff: new(NullifyingBuff, Guids.NullifyingBuff));
      EnchantTool.CreateVariantEnchant(
        nullifyingEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          NullifyingOffHandEffect,
          Guids.NullifyingOffHandEffect,
          Guids.NullifyingEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(NullifyingOffHandBuff, Guids.NullifyingOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(nullifying, new(NullifyingEnchantCopy, Guids.NullifyingEnchantCopy), nullifyingEnchantInfo);
    }
  }
}