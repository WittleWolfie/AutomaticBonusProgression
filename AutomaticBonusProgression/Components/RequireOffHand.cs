using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("833a3d47-7eb3-44ec-9138-268ab8f49a5b")]
  internal class RequireOffHand : UnitBuffComponentDelegate, IUnitEquipmentHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(UnitBuffComponentDelegate));

    public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
    {
      try
      {
        if (slot.Owner != Owner)
          return;

        if (Common.HasSecondaryWeapon(Owner))
          return;

        Logger.Verbose(() => $"No off-hand / secondary weapon equipped, removing {Buff.Name}");
        Buff.Remove();
      }
      catch (Exception e)
      {
        Logger.LogException("RequireOffHand.HandleEquipmentSlotUpdated", e);
      }
    }
  }
}
