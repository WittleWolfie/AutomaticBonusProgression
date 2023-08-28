using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Impact
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Impact));

    private const string EnchantName = "LW.Impact.Enchant";
    private const string EffectName = "LW.Impact";
    private const string BuffName = "LW.Impact.Buff";
    private const string OffHandEffectName = "LW.Impact.OffHand";
    private const string OffHandBuffName = "LW.Impact.OffHand.Buff";

    private const string DisplayName = "LW.Impact.Name";
    private const string Description = "LW.Impact.Description";
    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Impact");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.ImpactEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<ImpactComponent>()
        .Configure();

      var icon = FeatureRefs.TrueJudgmentFeature.Reference.Get().Icon;
      var enchantInfo = 
        new WeaponEnchantInfo(
          DisplayName,
          Description,
          icon,
          EnhancementCost,
          allowedRanges: new() { WeaponRangeType.Melee },
          allowedForms: new(),
          onlyHeavyWeapons: true);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.ImpactEffect, enchant),
        parentBuff: new(BuffName, Guids.ImpactBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.ImpactOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.ImpactOffHandBuff));
    }

    [TypeId("fb708d4d-276c-4a33-9838-fed313528943")]
    private class ImpactComponent :
      ItemEnchantmentComponentDelegate,
      IInitiatorRulebookHandler<RuleCalculateWeaponStats>,
      IInitiatorRulebookHandler<RuleCombatManeuver>
    {
      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          if (evt.Weapon != Owner)
            return;

          var currentSize = evt.WeaponSize;
          var newSize = currentSize.Shift(1);
          evt.WeaponDamageDice.Modify(
            WeaponDamageScaleTable.Scale(
              evt.WeaponDamageDice.ModifiedValue, newSize, currentSize, evt.Weapon.Blueprint),
            Fact);
        }
        catch (Exception e)
        {
          Logger.LogException("ImpactComponent.OnEventAboutToTrigger(RuleCalculateWeaponStats)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCombatManeuver evt)
      {
        try
        {
          if (evt.Type != CombatManeuver.BullRush)
            return;

          var bonus = GameHelper.GetItemEnhancementBonus(Owner);
          evt.AddModifier(bonus, Enchantment, ModifierDescriptor.UniqueItem);
        }
        catch (Exception e)
        {
          Logger.LogException("DuelingComponent.OnEventAboutToTrigger(RuleCombatManeuver)", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }
      public void OnEventDidTrigger(RuleCombatManeuver evt) { }
    }
  }
}
