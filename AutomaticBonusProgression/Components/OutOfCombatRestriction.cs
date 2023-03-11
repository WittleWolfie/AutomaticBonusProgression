using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("df2be02b-1d0b-4fb3-bd41-be6c7bb5e928")]
  internal class OutOfCombatRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(OutOfCombatRestriction));

    public override bool IsAvailable()
    {
      try
      {
        return Fact.IsOn || !Owner.IsInCombat;
      }
      catch (Exception e)
      {
        Logger.LogException("OutOfCombatRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}
