using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using Kingmaker.Utility;
using System;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [TypeId("7c0e0f0a-6ca2-43dc-8d6f-f70841a0995c")]
  internal class ShieldRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ShieldRestriction));

    private readonly ArmorProficiencyGroup[] AllowedTypes;

    internal ShieldRestriction(params ArmorProficiencyGroup[] allowedTypes)
    {
      AllowedTypes = allowedTypes;
    }

    public override bool IsAvailable()
    {
      try
      {
        if (!Owner.Body.SecondaryHand.HasShield)
          return false;

        if (!AllowedTypes.Any())
          return true;

        return AllowedTypes.Contains(Owner.Body.SecondaryHand.Shield.ArmorComponent.ArmorType());
      }
      catch (Exception e)
      {
        Logger.LogException("ShieldRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}
