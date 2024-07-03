using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("b3e8cb34-b8f7-4ef2-9ed9-6284d0e406a7")]
  internal class AddStatBonusABP : UnitFactComponentDelegate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AddStatBonusABP));

    private readonly StatType Stat;
    private readonly int Bonus;

    internal AddStatBonusABP(StatType Stat, int Bonus)
    {
      this.Stat = Stat;
      this.Bonus = Bonus;
    }

    public override void OnTurnOn()
    {
      try
      {
        var bonus = Bonus * Fact.GetRank();
        if (Owner.State.Features.AdvancedFakeEnhancement)
        {
          Logger.Verbose(() => "Applying AdvancedFakeEnhancement bonus (+4)");
          bonus += 4;
        }
        else if (Owner.State.Features.FakeEnhancement)
        {
          Logger.Verbose(() => "Applying FakeEnhancement bonus (+2)");
          bonus += 2;
        }

        Logger.Verbose(() => $"Granting {Owner.CharacterName} +{bonus} to {Stat}");
        ModifiableValue stat = Owner.Stats.GetStat(Stat);
        stat.AddModifierUnique(bonus, Runtime, ModifierDescriptor.Enhancement);
      }
      catch (Exception e)
      {
        Logger.LogException("AddStatBonusABP.OnTurnOn()", e);
      }
    }

    public override void OnTurnOff()
    {
      try
      {
        Owner.Stats.GetStat(Stat).RemoveModifiersFrom(Runtime);
      }
      catch (Exception e)
      {
        Logger.LogException("AddStatBonusABP.OnTurnOff()", e);
      }
    }
  }
}
