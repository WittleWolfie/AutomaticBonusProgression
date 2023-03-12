using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Utility;
using System;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [TypeId("3c941940-984f-477b-87cb-48b258f93d38")]
  internal class RequireShield : UnitBuffComponentDelegate, IUnitEquipmentHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(UnitBuffComponentDelegate));

    private readonly ArmorProficiencyGroup[] AllowedTypes;

    internal RequireShield(params ArmorProficiencyGroup[] allowedTypes)
    {
      AllowedTypes = allowedTypes;
    }

    public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
    {
      try
      {
        if (slot.Owner != Owner)
          return;

        if (!Owner.Body.SecondaryHand.HasShield)
        {
          Logger.Verbose(() => $"No shield equipped, removing {Buff.Name}");
          Buff.Remove();
          return;
        }

        var shield = Owner.Body.SecondaryHand.Shield.ArmorComponent;
        if (AllowedTypes.Contains(shield.ArmorType()))
          return;

        Logger.Verbose(() => $"Shield type is {shield.ArmorType()}, removing {Buff.Name}");
        Buff.Remove();
      }
      catch (Exception e)
      {
        Logger.LogException("RequireShield.HandleEquipmentSlotUpdated", e);
      }
    }
  }
}
