using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("ed989cd3-63e0-4f27-a424-c754d51576ec")]
  internal class SavingThrowBonusAgainstTarget : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleSavingThrow>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(SavingThrowBonusAgainstTarget));

    private readonly BlueprintBuff TargetBuff;

    internal SavingThrowBonusAgainstTarget(BlueprintBuff targetBuff)
    {
      TargetBuff = targetBuff;
    }

    public void OnEventAboutToTrigger(RuleSavingThrow evt)
    {
      try
      {
        var buff = evt.Reason.Caster?.GetFact(TargetBuff);
        if (buff is not null && buff.MaybeContext.MaybeCaster == Owner)
        {
          Logger.Verbose(() => $"Granting +2 competence bonus against {evt.Reason.Caster}");
          evt.AddModifier(2, Fact, ModifierDescriptor.Competence);
        }
      }
      catch (Exception e)
      {
        Logger.LogException("SavingThrowBonusAgainstTarget.OnEventAboutToTrigger", e);
      }
    }

    public void OnEventDidTrigger(RuleSavingThrow evt) { }
  }
}
