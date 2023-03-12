using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("56ea1d40-d270-4317-9395-8f84228aca95")]
  internal class OffHandRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(OffHandRestriction));

    private readonly ArmorProficiencyGroup[] AllowedTypes;

    internal OffHandRestriction(params ArmorProficiencyGroup[] allowedTypes)
    {
      AllowedTypes = allowedTypes;
    }

    public override bool IsAvailable()
    {
      try
      {
        return Common.HasSecondaryWeapon(Owner);
      }
      catch (Exception e)
      {
        Logger.LogException("OffHandRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}
