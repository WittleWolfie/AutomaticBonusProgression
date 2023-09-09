using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Lifesurge
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Lifesurge));

    private const string EnchantName = "LW.Lifesurge.Enchant";
    private const string EffectName = "LW.Lifesurge";
    private const string BuffName = "LW.Lifesurge.Buff";
    private const string OffHandEffectName = "LW.Lifesurge.OffHand";
    private const string OffHandBuffName = "LW.Lifesurge.OffHand.Buff";

    private const string DisplayName = "LW.Lifesurge.Name";
    private const string Description = "LW.Lifesurge.Description";
    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Lifesurge");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.LifesurgeEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<LifesurgeComponent>()
        .Configure();

      var deathward = AbilityRefs.DeathWard.Reference.Get();
      var enchantInfo =
        new WeaponEnchantInfo(DisplayName, Description, deathward.Icon, EnhancementCost, WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.LifesurgeEffect, enchant),
        parentBuff: new(BuffName, Guids.LifesurgeBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.LifesurgeOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.LifesurgeOffHandBuff));
    }

    [TypeId("a8167c33-e6f7-4a69-913d-8563c9d8a24f")]
    private class LifesurgeComponent
      : ItemEnchantmentComponentDelegate,
      IInitiatorRulebookHandler<RuleSavingThrow>,
      IInitiatorRulebookHandler<RuleCalculateWeaponStats>,
      IInitiatorRulebookHandler<RuleAttackWithWeapon>
    {
      private static BlueprintFeature _undeadType;
      private static BlueprintFeature UndeadType
      {
        get
        {
          _undeadType ??= FeatureRefs.UndeadType.Reference.Get();
          return _undeadType;
        }
      }

      public void OnEventAboutToTrigger(RuleSavingThrow evt)
      {
        try
        {
          var spell = evt.Reason.Context?.SourceAbility;
          if (spell is null || spell.School != SpellSchool.Necromancy)
            return;

          var stats = evt.Initiator.Stats;
          var bonus = GameHelper.GetItemEnhancementBonus(Owner);
          Logger.Verbose(() => $"Adding +{bonus} against {spell.name}");
          evt.AddTemporaryModifier(stats.SaveWill.AddModifier(bonus, Runtime, ModifierDescriptor.UniqueItem));
          evt.AddTemporaryModifier(stats.SaveFortitude.AddModifier(bonus, Runtime, ModifierDescriptor.UniqueItem));
          evt.AddTemporaryModifier(stats.SaveReflex.AddModifier(bonus, Runtime, ModifierDescriptor.UniqueItem));
        }
        catch (Exception e)
        {
          Logger.LogException("LifesurgeComponent.OnEventAboutToTrigger(RuleSavingThrow)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          if (evt.Weapon != Owner)
            return;

          var target = evt.AttackWithWeapon.Target;
          if (target is null)
            return;

          if (!target.HasFact(UndeadType))
            return;

          Logger.Verbose(() => $"Increasing critical edge against {target.CharacterName}");
          evt.DoubleCriticalEdge = true;
        }
        catch (Exception e)
        {
          Logger.LogException("LifesurgeComponent.OnEventAboutToTrigger(RuleCalculateWeaponStats)", e);
        }
      }

      public void OnEventDidTrigger(RuleAttackWithWeapon evt)
      {
        try
        {
          if (evt.Weapon != Owner)
            return;

          var target = evt.Target;
          if (target is null)
            return;

          var attackRoll = evt.AttackRoll;
          if (!attackRoll.IsHit || !attackRoll.IsCriticalConfirmed)
            return;

          if (!target.HasFact(UndeadType))
            return;

          var healDice = evt.WeaponStats.CriticalMultiplier - 1;
          Logger.Verbose(() => $"Healing {Owner.Wielder.CharacterName} for {healDice}d4");
          Rulebook.Trigger<RuleHealDamage>(
            new(Owner.Wielder, Owner.Wielder, new DiceFormula(healDice, DiceType.D4), bonus: 0));
        }
        catch (Exception e)
        {
          Logger.LogException("LifesurgeComponent.OnEventDidTrigger(RuleAttackWithWeapon)", e);
        }
      }

      public void OnEventDidTrigger(RuleSavingThrow evt) { }
      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }
      public void OnEventAboutToTrigger(RuleAttackWithWeapon evt) { }
    }
  }
}
