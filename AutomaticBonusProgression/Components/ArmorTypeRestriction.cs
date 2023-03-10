using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [TypeId("5dee52fa-b83d-46aa-9c77-7adcdd1b4a8b")]
  internal class ArmorTypeRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ArmorTypeRestriction));

    private readonly ArmorProficiencyGroup[] ArmorTypes;

    internal ArmorTypeRestriction(params ArmorProficiencyGroup[] allowedTypes)
    {
      ArmorTypes = allowedTypes;
    }

    public override bool IsAvailable()
    {
      try
      {
        if (!Owner.Body.Armor.HasArmor)
        {
          return ArmorTypes.Contains(ArmorProficiencyGroup.Light);
        }

        var armor = Owner.Body.Armor.Armor;
        return ArmorTypes.Contains(armor.ArmorType());
      }
      catch (Exception e)
      {
        Logger.LogException("ArmorTypeRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}
