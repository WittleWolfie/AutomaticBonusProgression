using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("3db11e4a-3b0b-431f-8fb6-cedb5bd205ec")]
  internal class RequireWeapon : UnitBuffComponentDelegate, IUnitEquipmentHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(UnitBuffComponentDelegate));

    public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
    {
      try
      {
        if (slot.Owner != Owner)
          return;

        if (Owner.Body.PrimaryHand.HasWeapon)
          return;

        Logger.Verbose(() => $"No weapon equipped, removing {Buff.Name}");
        Buff.Remove();
      }
      catch (Exception e)
      {
        Logger.LogException("RequireWeapon.HandleEquipmentSlotUpdated", e);
      }
    }
  }
}
