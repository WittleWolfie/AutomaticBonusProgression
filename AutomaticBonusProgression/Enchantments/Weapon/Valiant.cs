using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Valiant
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Valiant));

    private const string EnchantName = "LW.Valiant.Enchant";
    private const string EffectName = "LW.Valiant";
    private const string BuffName = "LW.Valiant.Buff";
    private const string OffHandEffectName = "LW.Valiant.OffHand";
    private const string OffHandBuffName = "LW.Valiant.OffHand.Buff";

    private const string DisplayName = "LW.Valiant.Name";
    private const string Description = "LW.Valiant.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Valiant");

      var icon = FeatureRefs.CavalierMightyCharge.Reference.Get().Icon;
      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.ValiantEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<ValiantComponent>()
        .Configure();

      var enchantInfo = new WeaponEnchantInfo(DisplayName, Description, icon, EnhancementCost, WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.ValiantEffect, enchant),
        parentBuff: new(BuffName, Guids.ValiantBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.ValiantOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.ValiantOffHandBuff));
    }


    [TypeId("849914ba-108f-4906-9380-de58b8e32c71")]
    private class ValiantComponent
      : ItemEnchantmentComponentDelegate,
      IInitiatorRulebookHandler<RuleCalculateCMB>,
      ITargetRulebookHandler<RuleCalculateCMD>,
      IInitiatorRulebookHandler<RulePrepareDamage>
    {
      private static BlueprintBuff _challenge;
      private static BlueprintBuff Challenge
      {
        get
        {
          _challenge ??= BuffRefs.CavalierChallengeBuffTarget.Reference.Get();
          return _challenge;
        }
      }
      private static BlueprintBuff _knightsChallenge;
      private static BlueprintBuff KnightsChallenge
      {
        get
        {
          _knightsChallenge ??= BuffRefs.CavalierKnightsChallengeBuffTarget.Reference.Get();
          return _knightsChallenge;
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateCMB evt)
      {
        try
        {
          if (evt.Type != CombatManeuver.SunderArmor && evt.Type != CombatManeuver.Disarm)
            return;

          var target = evt.Target;
          if (target is null)
            return;

          var challenge = target.GetFact(Challenge);
          var knightsChallenge = target.GetFact(KnightsChallenge);

          if (challenge is null && knightsChallenge is null)
            return;

          if (challenge?.MaybeContext?.MaybeCaster != Owner.Wielder
            && knightsChallenge?.MaybeContext?.MaybeCaster != Owner.Wielder)
            return;

          Logger.Verbose(() => $"Adding +2 to {evt.Type} targeting {target.CharacterName}");
          evt.AddModifier(2, Fact, ModifierDescriptor.UniqueItem);
        }
        catch (Exception e)
        {
          Logger.LogException("ValiantComponent.OnEventAboutToTrigger(RuleCalculateCMB)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateCMD evt)
      {
        try
        {
          if (evt.Type != CombatManeuver.SunderArmor && evt.Type != CombatManeuver.Disarm)
            return;

          var target = evt.Initiator;
          if (target is null)
            return;

          var challenge = target.GetFact(Challenge);
          var knightsChallenge = target.GetFact(KnightsChallenge);

          if (challenge is null && knightsChallenge is null)
            return;

          if (challenge?.MaybeContext?.MaybeCaster != Owner.Wielder
            && knightsChallenge?.MaybeContext?.MaybeCaster != Owner.Wielder)
            return;

          Logger.Verbose(() => $"Adding +4 to {evt.Type} against {target.CharacterName}");
          evt.AddModifier(4, Fact, ModifierDescriptor.UniqueItem);
        }
        catch (Exception e)
        {
          Logger.LogException("ValiantComponent.OnEventAboutToTrigger(RuleCalculateCMD)", e);
        }
      }

      public void OnEventAboutToTrigger(RulePrepareDamage evt)
      {
        try
        {
          if (Owner != evt.DamageBundle.Weapon)
            return;

          var challenge = evt.Target.GetFact(Challenge);
          var knightsChallenge = evt.Target.GetFact(KnightsChallenge);

          if (challenge is null && knightsChallenge is null)
            return;

          if (challenge?.MaybeContext?.MaybeCaster != Owner.Wielder
            && knightsChallenge?.MaybeContext?.MaybeCaster != Owner.Wielder)
            return;

          var damage = DamageTypes.Untyped().CreateDamage(
              new DiceFormula(rollsCount: 1, diceType: DiceType.D6),
              bonus: 0);
          damage.SourceFact = Fact;
          Logger.Verbose(() => $"Adding 1d6 damage to attack against {evt.Target.CharacterName}");
          evt.Add(damage);
        }
        catch (Exception e)
        {
          Logger.LogException("ValiantComponent.OnEventAboutToTrigger(RulePrepareDamage)", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateCMB evt) { }
      public void OnEventDidTrigger(RuleCalculateCMD evt) { }
      public void OnEventDidTrigger(RulePrepareDamage evt) { }

    }
  }
}
