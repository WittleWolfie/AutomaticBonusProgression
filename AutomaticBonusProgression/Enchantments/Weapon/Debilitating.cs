using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.ContextData;
using System;
using TabletopTweaks.Core.NewActions;
using static Kingmaker.RuleSystem.RulebookEvent;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Debilitating
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Debilitating));

    private const string EnchantName = "LW.Debilitating.Enchant";
    private const string EffectName = "LW.Debilitating";
    private const string BuffName = "LW.Debilitating.Buff";
    private const string OffHandEffectName = "LW.Debilitating.OffHand";
    private const string OffHandBuffName = "LW.Debilitating.OffHand.Buff";

    private const string DisplayName = "LW.Debilitating.Name";
    private const string Description = "LW.Debilitating.Description";
    private const int EnhancementCost = 1;

    private const string ToggleName = "LW.Debilitating.Toggle";
    private const string ToggleBuffName = "LW.Debilitating.Toggle.Buff";

    private const string ToggleDisplayName = "LW.Debilitating.Toggle.Name";
    private const string ToggleDescription = "LW.Debilitating.Toggle.Description";

    private const string AttackBuff = "LW.Debilitating.Attack";
    private const string ACBuff = "LW.Debilitating.AC";

    private const string AttackBuffDisplayName = "LW.Debilitating.Attack.Name";
    private const string AttackBuffDescription = "LW.Debilitating.Attack.Description";
    private const string ACBuffDisplayName = "LW.Debilitating.AC.Name";
    private const string ACBuffDescription = "LW.Debilitating.AC.Description";

    internal static void Configure()
    {
      Logger.Log($"Configuring Debilitating");

      var disoriented = BuffRefs.DebilitatingInjuryDisorientedEffectBuff.Reference.Get();
      var bewildered = BuffRefs.DebilitatingInjuryBewilderedEffectBuff.Reference.Get();
      var debilitatingInjury = FeatureRefs.DebilitatingInjury.Reference.Get();

      var attackBuff = BuffConfigurator.New(AttackBuff, Guids.DebilitatingAttackBuff)
        .SetDisplayName(AttackBuffDisplayName)
        .SetDescription(AttackBuffDescription)
        .SetIcon(disoriented.Icon)
        .SetRanks(2)
        .AddStatBonus(stat: StatType.AdditionalAttackBonus, value: -1)
        .AddNotDispelable()
        .Configure();

      var acBuff = BuffConfigurator.New(ACBuff, Guids.DebilitatingACBuff)
        .SetDisplayName(ACBuffDisplayName)
        .SetDescription(ACBuffDescription)
        .SetIcon(bewildered.Icon)
        .SetRanks(2)
        .AddStatBonus(stat: StatType.AC, value: -1)
        .AddNotDispelable()
        .Configure();

      var toggleBuff = BuffConfigurator.New(ToggleBuffName, Guids.DebilitatingToggleBuff)
        .SetFlags(BlueprintBuff.Flags.HiddenInUi)
        .Configure();

      var toggle = ActivatableAbilityConfigurator.New(ToggleName, Guids.DebilitatingToggle)
        .SetDisplayName(ToggleDisplayName)
        .SetDescription(ToggleDescription)
        .SetIcon(debilitatingInjury.Icon)
        .SetActivationType(AbilityActivationType.Immediately)
        .SetDeactivateImmediately()
        .SetBuff(toggleBuff)
        .Configure();

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.DebilitatingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent(
          new DebilitatingComponent(
            ActionsBuilder.New()
              .Conditional(
                ConditionsBuilder.New().CasterHasFact(toggleBuff),
                ifTrue: ActionsBuilder.New()
                  .Conditional(
                    ConditionsBuilder.New().CasterHasFact(debilitatingInjury),
                    ifTrue: GetApplyBuff(attackBuff, rank: 2),
                    ifFalse: GetApplyBuff(attackBuff)),
                ifFalse: ActionsBuilder.New()
                  .Conditional(
                    ConditionsBuilder.New().CasterHasFact(debilitatingInjury),
                    ifTrue: GetApplyBuff(acBuff, rank: 2),
                    ifFalse: GetApplyBuff(acBuff)))))
        .Configure();

      var addFacts = new AddFacts() { m_Facts = new[] { toggle.ToReference<BlueprintUnitFactReference>() } };
      var enchantInfo = new WeaponEnchantInfo(
        DisplayName,
        Description,
        debilitatingInjury.Icon,
        EnhancementCost);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.DebilitatingEffect, enchant),
        parentBuff: new(BuffName, Guids.DebilitatingBuff, addFacts));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.DebilitatingOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.DebilitatingOffHandBuff, addFacts));
    }

    private static ActionsBuilder GetApplyBuff(BlueprintBuff buff, int rank = 1)
    {
      return ActionsBuilder.New()
        .Add<ContextActionApplyBuffRanks>(
          a =>
          {
            a.m_Buff = buff.ToReference<BlueprintBuffReference>();
            a.Rank = rank;
            a.DurationValue = ContextDuration.Fixed(10);
          });
    }

    [TypeId("8a21abc4-23bf-4924-bf04-275d474f3005")]
    private class DebilitatingComponent
      : ItemEnchantmentComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>
    {
      private static readonly CustomDataKey IsFlatFooted = new("Debilitating.IsFlatFooted");

      private readonly ActionList Actions;

      internal DebilitatingComponent(ActionsBuilder actions)
      {
        Actions = actions.Build();
      }

      public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
      {
        try
        {
          if (evt.Weapon != Owner)
            return;

          var isFlatFooted = Rulebook.Trigger<RuleCheckTargetFlatFooted>(new(evt.Initiator, evt.Target)).IsFlatFooted;
          evt.SetCustomData(IsFlatFooted, isFlatFooted);
        }
        catch (Exception e)
        {
          Logger.LogException("DebilitatingComponent.OnEventDidTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleAttackWithWeapon evt)
      {
        try
        {
          if (evt.Weapon != Owner)
            return;

          if (!evt.AttackRoll.IsHit)
            return;

          if (!evt.TryGetCustomData(IsFlatFooted, out bool isFlatFooted) || !isFlatFooted)
          {
            Logger.Verbose(() => $"Target is not flat footed.");
            return;
          }

          using (ContextData<ContextAttackData>.Request().Setup(evt.AttackRoll))
          {
            using (Context.GetDataScope(evt.Target))
              Actions.Run();
          }
        }
        catch (Exception e)
        {
          Logger.LogException("DebilitatingComponent.OnEventDidTrigger", e);
        }
      }
    }
  }
}
