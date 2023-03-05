using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("7c0e0f0a-6ca2-43dc-8d6f-f70841a0995c")]
  internal class ShieldEquippedRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ShieldEquippedRestriction));

    public override bool IsAvailable()
    {
      try
      {
        return Owner.Body.SecondaryHand.HasShield;
      }
      catch (Exception e)
      {
        Logger.LogException("ShieldEquippedRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}
