using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Courageous
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Courageous));

    private const string EnchantName = "LW.Courageous.Enchant";
    private const string EffectName = "LW.Courageous";
    private const string BuffName = "LW.Courageous.Buff";
    private const string OffHandEffectName = "LW.Courageous.OffHand";
    private const string OffHandBuffName = "LW.Courageous.OffHand.Buff";

    private const string DisplayName = "LW.Courageous.Name";
    private const string Description = "LW.Courageous.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Courageous");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.CourageousEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent(
          new AddItemEnhancementSaveBonus(
            SavingThrowType.Will, spellDescriptors: SpellDescriptor.Fear, descriptor: ModifierDescriptor.Morale))
        .Configure();

      var removeFear = BuffRefs.RemoveFearBuff.Reference.Get();
      var enchantInfo = new WeaponEnchantInfo(
        DisplayName,
        Description,
        removeFear.Icon,
        EnhancementCost,
        WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.CourageousEffect, enchant),
        parentBuff: new(BuffName, Guids.CourageousBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.CourageousOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.CourageousOffHandBuff));
    }
  }
}
