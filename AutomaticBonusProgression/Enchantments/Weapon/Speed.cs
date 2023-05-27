using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Speed
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Speed));

    private const string SpeedName = "LW.Speed.Name";
    private const string SpeedEnchantCopy = "LW.Speed.Enchant";
    private const string SpeedBuff = "LW.Speed.Buff";
    private const string SpeedEffect = "LW.Speed.Effect";
    private const string SpeedOffHandBuff = "LW.Speed.OffHand.Buff";
    private const string SpeedOffHandEffect = "LW.Speed.OffHand.Effect";

    private const int EnhancementCost = 3;

    internal static void Configure()
    {
      Logger.Log($"Configuring Speed");

      var speed = WeaponEnchantmentRefs.Speed.Reference.Get();
      var speedEnchantInfo = new WeaponEnchantInfo(
        SpeedName,
        speed.m_Description,
        "",
        EnhancementCost);
      EnchantTool.CreateEnchant(
        speedEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(SpeedEffect, Guids.SpeedEffect, Guids.SpeedEnchantCopy),
        parentBuff: new(SpeedBuff, Guids.SpeedBuff));
      EnchantTool.CreateVariantEnchant(
        speedEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          SpeedOffHandEffect,
          Guids.SpeedOffHandEffect,
          Guids.SpeedEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(SpeedOffHandBuff, Guids.SpeedOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(speed, new(SpeedEnchantCopy, Guids.SpeedEnchantCopy), speedEnchantInfo);
    }
  }
}