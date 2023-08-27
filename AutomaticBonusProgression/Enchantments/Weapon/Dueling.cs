using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Dueling
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Dueling));

    private const string EnchantName = "LW.Dueling.Enchant";
    private const string EffectName = "LW.Dueling";
    private const string BuffName = "LW.Dueling.Buff";
    private const string OffHandEffectName = "LW.Dueling.OffHand";
    private const string OffHandBuffName = "LW.Dueling.OffHand.Buff";

    private const string DisplayName = "LW.Dueling.Name";
    private const string Description = "LW.Dueling.Description";
    private const int EnhancementCost = 4;

    internal static void Configure()
    {
      Logger.Log($"Configuring Dueling");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.DuelingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddStatBonusEquipment(stat: StatType.Initiative, value: 4, descriptor: ModifierDescriptor.Enhancement)
        .AddComponent<DuelingComponent>()
        .Configure();

      var criticalFocus = FeatureRefs.CriticalFocus.Reference.Get();
      var enchantInfo = 
        new WeaponEnchantInfo(
          DisplayName,
          Description,
          criticalFocus.Icon,
          EnhancementCost,
          allowedRanges: new() { WeaponRangeType.Melee },
          allowedForms: new(),
          onlyLightWeapons: true);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.DuelingEffect, enchant),
        parentBuff: new(BuffName, Guids.DuelingBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.DuelingOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.DuelingOffHandBuff));
    }

    [TypeId("4e65f655-0da1-4e02-9f9a-cd20c134ff58")]
    private class DuelingComponent :
      ItemEnchantmentComponentDelegate,
      IInitiatorRulebookHandler<RuleCombatManeuver>,
      ITargetRulebookHandler<RuleCalculateCMD>
    {
      public void OnEventAboutToTrigger(RuleCombatManeuver evt)
      {
        try
        {
          if (evt.Type != CombatManeuver.Disarm)
            return;

          evt.AddModifier(2, Fact, ModifierDescriptor.UntypedStackable);
        }
        catch (Exception e)
        {
          Logger.LogException("DuelingComponent.OnEventAboutToTrigger(RuleCombatManeuver)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateCMD evt)
      {
        try
        {
          if (evt.Type != CombatManeuver.Disarm)
            return;

          evt.AddModifier(2, Fact, ModifierDescriptor.UntypedStackable);
        }
        catch (Exception e)
        {
          Logger.LogException("DuelingComponent.OnEventAboutToTrigger(RuleCalculateCMD)", e);
        }
      }

      public void OnEventDidTrigger(RuleCombatManeuver evt) { }

      public void OnEventDidTrigger(RuleCalculateCMD evt) { }
    }
  }
}
