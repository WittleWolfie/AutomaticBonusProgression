using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("5b6e6b9f-7767-4620-8b18-bc4063639c93")]
  internal class WeaponRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(WeaponRestriction));

    public override bool IsAvailable()
    {
      try
      {
        return Owner.Body.PrimaryHand.HasWeapon;
      }
      catch (Exception e)
      {
        Logger.LogException("WeaponRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}
