using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using System;
using static Kingmaker.RuleSystem.RulebookEvent;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Leveraging
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Leveraging));

    private const string EnchantName = "LW.Leveraging.Enchant";
    private const string EffectName = "LW.Leveraging";
    private const string BuffName = "LW.Leveraging.Buff";
    private const string OffHandEffectName = "LW.Leveraging.OffHand";
    private const string OffHandBuffName = "LW.Leveraging.OffHand.Buff";

    private const string DisplayName = "LW.Leveraging.Name";
    private const string Description = "LW.Leveraging.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Leveraging");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.LeveragingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<LeveragingComponent>()
        .Configure();

      var icon = FeatureRefs.IntimidatingProwess.Reference.Get().Icon;
      var enchantInfo = 
        new WeaponEnchantInfo(
          DisplayName,
          Description,
          icon,
          EnhancementCost,
          WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.LeveragingEffect, enchant),
        parentBuff: new(BuffName, Guids.LeveragingBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.LeveragingOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.LeveragingOffHandBuff));
    }

    [TypeId("604d46a6-c62f-48a9-b0d4-9def19c8410c")]
    private class LeveragingComponent :
      ItemEnchantmentComponentDelegate,
      IInitiatorRulebookHandler<RuleCombatManeuver>,
      ITargetRulebookHandler<RuleCalculateCMD>
    {
      private static readonly CustomDataKey DuplicateKey = new("Leveraging.Duplicate");

      public void OnEventAboutToTrigger(RuleCombatManeuver evt)
      {
        try
        {
          if (evt.Type != CombatManeuver.BullRush
              && evt.Type != CombatManeuver.Pull
              && evt.Type != CombatManeuver.Trip)
            return;

          // Prevent multiple enchantments from applying the bonus
          if (evt.TryGetCustomData(DuplicateKey, out bool _))
          {
            Logger.Verbose(() => "Ignoring duplicate trigger");
            return;
          }
          evt.SetCustomData(DuplicateKey, true);

          var bonus = GameHelper.GetItemEnhancementBonus(Owner);
          evt.AddModifier(bonus, Enchantment, ModifierDescriptor.UntypedStackable);
        }
        catch (Exception e)
        {
          Logger.LogException("LeveragingComponent.OnEventAboutToTrigger(RuleCombatManeuver)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateCMD evt)
      {
        try
        {
          if (evt.Type != CombatManeuver.BullRush
              && evt.Type != CombatManeuver.Pull
              && evt.Type != CombatManeuver.Trip)
            return;

          // Prevent multiple enchantments from applying the bonus
          if (evt.TryGetCustomData(DuplicateKey, out bool _))
          {
            Logger.Verbose(() => "Ignoring duplicate trigger");
            return;
          }
          evt.SetCustomData(DuplicateKey, true);

          var bonus = GameHelper.GetItemEnhancementBonus(Owner);
          evt.AddModifier(bonus, Enchantment, ModifierDescriptor.UntypedStackable);
        }
        catch (Exception e)
        {
          Logger.LogException("LeveragingComponent.OnEventAboutToTrigger(RuleCalculateCMD)", e);
        }
      }

      public void OnEventDidTrigger(RuleCombatManeuver evt) { }
      public void OnEventDidTrigger(RuleCalculateCMD evt) { }
    }
  }
}
