using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Furyborn
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Furyborn));

    private const string FurybornName = "LW.Furyborn.Name";
    private const string FurybornEnchantCopy = "LW.Furyborn.Enchant";
    private const string FurybornBuff = "LW.Furyborn.Buff";
    private const string FurybornEffect = "LW.Furyborn.Effect";
    private const string FurybornOffHandBuff = "LW.Furyborn.OffHand.Buff";
    private const string FurybornOffHandEffect = "LW.Furyborn.OffHand.Effect";

    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Furyborn");

      var Furyborn = WeaponEnchantmentRefs.Furyborn.Reference.Get();
      var FurybornEnchantInfo = new WeaponEnchantInfo(
        FurybornName, Furyborn.m_Description, "", EnhancementCost, WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        FurybornEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(FurybornEffect, Guids.FurybornEffect, Guids.FurybornEnchantCopy),
        parentBuff: new(FurybornBuff, Guids.FurybornBuff));
      EnchantTool.CreateVariantEnchant(
        FurybornEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          FurybornOffHandEffect,
          Guids.FurybornOffHandEffect,
          Guids.FurybornEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(FurybornOffHandBuff, Guids.FurybornOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(Furyborn, new(FurybornEnchantCopy, Guids.FurybornEnchantCopy), FurybornEnchantInfo);
    }
  }
}