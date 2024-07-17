using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Fortuitous
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Fortuitous));

    private const string EnchantName = "LW.Fortuitous.Enchant";
    private const string EffectName = "LW.Fortuitous";
    private const string BuffName = "LW.Fortuitous.Buff";
    private const string OffHandEffectName = "LW.Fortuitous.OffHand";
    private const string OffHandBuffName = "LW.Fortuitous.OffHand.Buff";
    private const string Cooldown = "LW.Fortuitous.Cooldown";

    private const string DisplayName = "LW.Fortuitous.Name";
    private const string Description = "LW.Fortuitous.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Fortuitous");

      var cooldown = BuffConfigurator.New(Cooldown, Guids.FortuitousCooldown)
        .SetFlags(BlueprintBuff.Flags.HiddenInUi, BlueprintBuff.Flags.StayOnDeath)
        .AddNotDispelable()
        .AddFactContextActions(newRound: ActionsBuilder.New().RemoveSelf())
        .Configure();

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.FortuitousEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<FortuitousComponent>()
        .Configure();

      var combatReflexes = FeatureRefs.CombatReflexes.Reference.Get();
      var enchantInfo = 
        new WeaponEnchantInfo(
          DisplayName,
          Description,
          combatReflexes.Icon,
          EnhancementCost,
          WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.FortuitousEffect, enchant),
        parentBuff: new(BuffName, Guids.FortuitousBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.FortuitousOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.FortuitousOffHandBuff));
    }

    [TypeId("c524467d-a200-439b-93be-e28a9ac31da9")]
    private class FortuitousComponent :
      ItemEnchantmentComponentDelegate,
      IInitiatorRulebookHandler<RuleAttackWithWeapon>
    {
      private static BlueprintBuff _cooldown;
      private static BlueprintBuff Cooldown
      {
        get
        {
          _cooldown ??= BlueprintTool.Get<BlueprintBuff>(Guids.FortuitousCooldown);
          return _cooldown;
        }
      }

      public void OnEventAboutToTrigger(RuleAttackWithWeapon evt) { }

      static bool go = false;
      public void OnEventDidTrigger(RuleAttackWithWeapon evt)
      {
        try
        {
          if (go)
            return;

          if (!evt.AttackRoll.IsHit
              || !evt.IsAttackOfOpportunity
              || evt.Target.State.IsDead)
            return;

          var wielder = Owner.Wielder;
          if (wielder is null)
          {
            Logger.Warning("No wielder!");
            return;
          }

          if (wielder.HasFact(Cooldown))
          {
            Logger.Verbose(() => $"Not the first opportunity attack.");
            return;
          }

          // For some reason modifiers don't seem to work applied directly to the rule, so just apply to the wielder
          var attackBonus = wielder.Stats.GetStat(StatType.AdditionalAttackBonus);
          var penalty = attackBonus.AddModifier(-5, Enchantment);

          var attackAnimation = wielder.Unit.View.AnimationManager.CreateHandle(UnitAnimationType.SpecialAttack);
          Rulebook.Trigger<RuleAttackWithWeapon>(
            new(wielder, evt.Target, Owner as ItemEntityWeapon, attackBonusPenalty: 0));
          wielder.Unit.View.AnimationManager.Execute(attackAnimation);

          attackBonus.RemoveModifier(penalty);
          wielder.Unit.AddBuff(Cooldown, wielder);
        }
        catch (Exception e)
        {
          Logger.LogException("FortuitousComponent.OnEventDidTrigger", e);
        }
      }
    }
  }
}
