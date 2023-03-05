using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;
using System;

namespace AutomaticBonusProgression.Components
{
  [AllowMultipleComponents]
  [TypeId("ed989cd3-63e0-4f27-a424-c754d51576ec")]
  internal class BonusAgainstTarget :
    UnitBuffComponentDelegate,
    IInitiatorRulebookHandler<RuleSavingThrow>,
    ITargetRulebookHandler<RuleCalculateAC>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BonusAgainstTarget));

    private enum BonusType
    {
      SavingThrows,
      AC
    }

    private readonly BlueprintBuffReference TargetBuff;
    private readonly BonusType Type;
    private readonly ContextValue Bonus;
    private readonly ModifierDescriptor Descriptor;

    private BonusAgainstTarget(
      BlueprintBuffReference targetBuff,
      BonusType type,
      ContextValue bonus,
      ModifierDescriptor descriptor)
    {
      TargetBuff = targetBuff;
      Type = type;
      Bonus = bonus;
      Descriptor = descriptor;
    }

    internal static BonusAgainstTarget Saves(
      BlueprintBuffReference targetBuff, ContextValue bonus, ModifierDescriptor descriptor = ModifierDescriptor.UntypedStackable)
    {
      return new(targetBuff, BonusType.SavingThrows, bonus, descriptor);
    }

    internal static BonusAgainstTarget AC(
      BlueprintBuffReference targetBuff, ContextValue bonus, ModifierDescriptor descriptor = ModifierDescriptor.UntypedStackable)
    {
      return new(targetBuff, BonusType.AC, bonus, descriptor);
    }

    public void OnEventAboutToTrigger(RuleSavingThrow evt)
    {
      try
      {
        if (Type != BonusType.SavingThrows)
          return;

        var buff = evt.Reason.Caster?.GetFact(TargetBuff);
        if (buff is not null && buff.MaybeContext.MaybeCaster == Owner)
        {
          var bonus = Bonus.Calculate(Context);
          Logger.Verbose(() => $"Granting +{bonus} {Descriptor} save bonus against {evt.Reason.Caster}");
          evt.AddModifier(bonus, Fact, Descriptor);
        }
      }
      catch (Exception e)
      {
        Logger.LogException("BonusAgainstTarget.OnEventAboutToTrigger(RuleSavingThrow)", e);
      }
    }

    public void OnEventAboutToTrigger(RuleCalculateAC evt)
    {
      try
      {
        if (Type != BonusType.AC)
          return;

        var buff = evt.Initiator.GetFact(TargetBuff);
        if (buff is not null && buff.MaybeContext.MaybeCaster == Owner)
        {
          var bonus = Bonus.Calculate(Context);
          Logger.Verbose(() => $"Granting +{bonus} {Descriptor} AC bonus against {evt.Reason.Caster}");
          evt.AddModifier(bonus, Fact, Descriptor);
        }
      }
      catch (Exception e)
      {
        Logger.LogException("BonusAgainstTarget.OnEventAboutToTrigger(RuleCalculateAC)", e);
      }
    }

    public void OnEventDidTrigger(RuleSavingThrow evt) { }

    public void OnEventDidTrigger(RuleCalculateAC evt) { }
  }
}
