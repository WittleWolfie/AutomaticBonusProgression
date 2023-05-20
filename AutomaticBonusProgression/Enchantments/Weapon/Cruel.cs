using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.ElementsSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.ContextData;
using System;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Cruel
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Cruel));

    private const string CruelName = "LW.Cruel.Name";
    private const string CruelBuff = "LW.Cruel.Buff";
    private const string CruelEffect = "LW.Cruel.Effect";
    private const string CruelOffHandBuff = "LW.Cruel.OffHand.Buff";
    private const string CruelOffHandEffect = "LW.Cruel.OffHand.Effect";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Cruel");

      var cruel = WeaponEnchantmentRefs.CruelEnchantment.Reference.Get();
      var cruelEnchantInfo = new WeaponEnchantInfo(CruelName, cruel.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        cruelEnchantInfo,
        effectBuff: GetBuffInfo(CruelEffect, Guids.CruelEffect),
        parentBuff: new(CruelBuff, Guids.CruelBuff));
      EnchantTool.CreateVariantEnchant(
        cruelEnchantInfo,
        effectBuff: GetBuffInfo(
          CruelOffHandEffect,
          Guids.CruelOffHandEffect,
          toPrimaryWeapon: false),
        variantBuff: new(CruelOffHandBuff, Guids.CruelOffHandBuff));

      EnchantTool.AddEnhancementEquivalenceWeapon(cruel, cruelEnchantInfo);
    }

    private static BlueprintInfo GetBuffInfo(
      string name,
      string guid,
      bool toPrimaryWeapon = true)
    {
      return new(name, guid, new CruelComponent(toPrimaryWeapon));
    }

    [TypeId("e7bb0ba6-e6f4-40b0-aed0-3c17042a763e")]
    private class CruelComponent :
      UnitBuffComponentDelegate,
      IInitiatorRulebookHandler<RuleAttackWithWeapon>
    {
      private readonly bool ToPrimaryWeapon;
      private readonly ActionList Sicken;
      private readonly ActionList TempHp;

      internal CruelComponent(bool toPrimaryWeapon)
      {
        ToPrimaryWeapon = toPrimaryWeapon;
        Sicken = ActionsBuilder.New()
          .Conditional(
            ConditionsBuilder.New()
              .HasFact(fact: BuffRefs.Frightened.ToString())
              .HasFact(fact: BuffRefs.Shaken.ToString()),
            ifTrue: ActionsBuilder.New().ApplyBuff(BuffRefs.Sickened.ToString(), ContextDuration.Fixed(1)))
          .Build();
        TempHp = ActionsBuilder.New()
          .ApplyBuff(BuffRefs.CruelBuff.ToString(), ContextDuration.Fixed(10, rate: DurationRate.Minutes))
          .Build();
      }

      public void OnEventAboutToTrigger(RuleAttackWithWeapon evt) { }

      public void OnEventDidTrigger(RuleAttackWithWeapon evt)
      {
        try
        {
          if (!evt.AttackRoll.IsHit)
            return;

          var isPrimaryWeapon = Common.IsPrimaryWeapon(evt.Weapon);
          if (ToPrimaryWeapon && !isPrimaryWeapon || !ToPrimaryWeapon && isPrimaryWeapon)
          {
            Logger.Verbose(() => $"Wrong weapon: {ToPrimaryWeapon} - {isPrimaryWeapon} - {evt.Weapon.Name}");
            return;
          }

          using (ContextData<ContextAttackData>.Request().Setup(evt.AttackRoll))
          {
            Logger.Verbose(() => $"Attempting to sicken {evt.Target}");
            using (Context.GetDataScope(evt.Target))
              Sicken.Run();
          }

          foreach (var resolve in evt.ResolveRules)
          {
            // Logic copied from AddInitiatorAttackWithWeaponTrigger
            var damage = resolve.Damage;
            if (damage is not null
              && !damage.IsFake
              && damage.Target.HPLeft <= 0
              && damage.Target.HPLeft + damage.Result > 0)
            {
              using (ContextData<ContextAttackData>.Request().Setup(evt.AttackRoll))
              {
                Logger.Verbose(() => $"Granting temp HP to {evt.Initiator}");
                using (Context.GetDataScope(evt.Initiator))
                  TempHp.Run();
              }
            }
          }
        }
        catch (Exception e)
        {
          Logger.LogException("CruelComponent.OnEventDidTrigger(RuleAttackWithWeapon)", e);
        }
      }
    }
  }
}