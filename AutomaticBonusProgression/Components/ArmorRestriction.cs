using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [TypeId("5dee52fa-b83d-46aa-9c77-7adcdd1b4a8b")]
  internal class ArmorRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ArmorRestriction));

    private readonly ArmorProficiencyGroup[] AllowedTypes;

    internal ArmorRestriction(params ArmorProficiencyGroup[] allowedTypes)
    {
      AllowedTypes = allowedTypes;
    }

    public override bool IsAvailable()
    {
      try
      {
        if (!Owner.Body.Armor.HasArmor)
        {
          return AllowedTypes.Contains(ArmorProficiencyGroup.Light);
        }

        var armor = Owner.Body.Armor.Armor;
        return AllowedTypes.Contains(armor.ArmorType());
      }
      catch (Exception e)
      {
        Logger.LogException("ArmorRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}
