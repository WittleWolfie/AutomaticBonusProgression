using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("488a56f4-0d32-4d88-ba74-ce658169b322")]
  internal class AddStatBonusABP : AddStatBonus
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AddStatBonusABP));

    internal AddStatBonusABP(StatType stat, int value, ModifierDescriptor descriptor)
    {
      Stat = stat;
      Value = value;
      Descriptor = descriptor;
    }

    public override void OnActivate()
    {
      try
      {
        if (!Common.IsAffectedByABP(Owner))
        {
          Logger.Verbose(() => $"Skipping {Stat} bonus for unaffected unit: {Owner.CharacterName}");
          return;
        }
        base.OnActivate();
      }
      catch (Exception e)
      {
        Logger.LogException("AddStatBonusABP.OnActivate", e);
      }
    }

    public override void OnTurnOn()
    {
      try
      {
        if (!Common.IsAffectedByABP(Owner))
        {
          Logger.Verbose(() => $"Skipping {Stat} for unaffected unit: {Owner?.CharacterName}");
          return;
        }
        base.OnTurnOn();
      }
      catch (Exception e)
      {
        Logger.LogException("AddStatBonusABP.OnActivate", e);
      }
    }
  }
}
