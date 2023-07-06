using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class BrilliantEnergy
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BrilliantEnergy));

    private const string BrilliantEnergyName = "LW.BrilliantEnergy.Name";
    private const string BrilliantEnergyEnchantCopy = "LW.BrilliantEnergy.Enchant";
    private const string BrilliantEnergyBuff = "LW.BrilliantEnergy.Buff";
    private const string BrilliantEnergyEffect = "LW.BrilliantEnergy.Effect";
    private const string BrilliantEnergyOffHandBuff = "LW.BrilliantEnergy.OffHand.Buff";
    private const string BrilliantEnergyOffHandEffect = "LW.BrilliantEnergy.OffHand.Effect";

    private const int EnhancementCost = 3;

    internal static void Configure()
    {
      Logger.Log($"Configuring BrilliantEnergy");

      var brilliantEnergy = WeaponEnchantmentRefs.BrilliantEnergy.Reference.Get();
      var icon = BuffRefs.ArcaneWeaponBrilliantEnergyBuff.Reference.Get().Icon;
      var brilliantEnergyEnchantInfo = new WeaponEnchantInfo(
        BrilliantEnergyName,
        brilliantEnergy.m_Description,
        icon,
        EnhancementCost);
      EnchantTool.CreateEnchant(
        brilliantEnergyEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(BrilliantEnergyEffect, Guids.BrilliantEnergyEffect, Guids.BrilliantEnergyEnchantCopy),
        parentBuff: new(BrilliantEnergyBuff, Guids.BrilliantEnergyBuff));
      EnchantTool.CreateVariantEnchant(
        brilliantEnergyEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          BrilliantEnergyOffHandEffect,
          Guids.BrilliantEnergyOffHandEffect,
          Guids.BrilliantEnergyEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(BrilliantEnergyOffHandBuff, Guids.BrilliantEnergyOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(brilliantEnergy, new(BrilliantEnergyEnchantCopy, Guids.BrilliantEnergyEnchantCopy), brilliantEnergyEnchantInfo);
    }
  }
}