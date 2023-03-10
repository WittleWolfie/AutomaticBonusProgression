using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Buffs.Components;
using System;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [TypeId("4b835824-d53d-470b-8e7b-a8acb450005a")]
  internal class RequireArmor : UnitBuffComponentDelegate, IUnitEquipmentHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(UnitBuffComponentDelegate));

    private readonly ArmorProficiencyGroup[] AllowedTypes;

    internal RequireArmor(params ArmorProficiencyGroup[] allowedTypes)
    {
      AllowedTypes = allowedTypes;
    }

    public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
    {
      try
      {
        if (slot.Owner != Owner)
          return;

        if (slot is not ArmorSlot armor)
          return;

        if (!armor.HasArmor && !AllowedTypes.Contains(ArmorProficiencyGroup.Light))
        {
          Logger.Verbose(() => $"No armor equipped, removing {Buff.Name}");
          Buff.Remove();
          return;
        }

        if (AllowedTypes.Contains(armor.Armor.ArmorType()))
          return;

        Logger.Verbose(() => $"Armor type is {armor.Armor.ArmorType()}, removing {Buff.Name}");
        Buff.Remove();
      }
      catch (Exception e)
      {
        Logger.LogException("RequireArmor.HandleEquipmentSlotUpdated", e);
      }
    }
  }
}
