using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Furious
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Furious));

    private const string FuriousName = "LW.Furious.Name";
    private const string FuriousEnchantCopy = "LW.Furious.Enchant";
    private const string FuriousBuff = "LW.Furious.Buff";
    private const string FuriousEffect = "LW.Furious.Effect";
    private const string FuriousOffHandBuff = "LW.Furious.OffHand.Buff";
    private const string FuriousOffHandEffect = "LW.Furious.OffHand.Effect";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Furious");

      var furious = WeaponEnchantmentRefs.Furious.Reference.Get();
      var icon = BuffRefs.RageSpellBuff.Reference.Get().Icon;
      var furiousEnchantInfo = new WeaponEnchantInfo(FuriousName, furious.m_Description, icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        furiousEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(FuriousEffect, Guids.FuriousEffect, Guids.FuriousEnchantCopy),
        parentBuff: new(FuriousBuff, Guids.FuriousBuff));
      EnchantTool.CreateVariantEnchant(
        furiousEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          FuriousOffHandEffect,
          Guids.FuriousOffHandEffect,
          Guids.FuriousEnchantCopy,
          toPrimaryWeapon: false),
        variantBuff: new(FuriousOffHandBuff, Guids.FuriousOffHandBuff));

      EnchantTool.SetUpWeaponEnchant(furious, new(FuriousEnchantCopy, Guids.FuriousEnchantCopy), furiousEnchantInfo);
    }
  }
}