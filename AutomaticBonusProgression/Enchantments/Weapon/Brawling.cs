using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Brawling
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Brawling));

    private const string EnchantName = "LW.Brawling.Enchant";
    private const string EffectName = "LW.Brawling";
    private const string BuffName = "LW.Brawling.Buff";
    private const string OffHandEffectName = "LW.Brawling.OffHand";
    private const string OffHandBuffName = "LW.Brawling.OffHand.Buff";

    private const string DisplayName = "LW.Brawling.Name";
    private const string Description = "LW.Brawling.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Brawling");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.BrawlingWeaponEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddStatBonusEquipment(stat: StatType.AdditionalCMB, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();

      var flurry = FeatureRefs.FlurryOfBlows.Reference.Get();
      var enchantInfo = new WeaponEnchantInfo(
        DisplayName,
        Description,
        flurry.Icon,
        EnhancementCost,
        allowedRanges: new(),
        allowedForms: new() { PhysicalDamageForm.Bludgeoning },
        onlyLightWeapons: true);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.BrawlingWeaponEffect, enchant),
        parentBuff: new(BuffName, Guids.BrawlingWeaponBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.BrawlingWeaponOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.BrawlingWeaponOffHandBuff));
    }
  }
}
