using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using static AutomaticBonusProgression.Components.BuffEnchantAnyWeaponReplacement;

namespace AutomaticBonusProgression.Components
{
  [TypeId("cafb691d-de98-412c-a0bc-4725cf1f9f14")]
  internal class BuffEnchantAnyWeaponReplacement :
    UnitBuffComponentDelegate<BuffEnchantAnyWeaponReplacementData>,
    IUnitActiveEquipmentSetHandler,
    IUnitEquipmentHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BuffEnchantAnyWeaponReplacement));

    private readonly BlueprintItemEnchantmentReference Enchant;
    private readonly bool ToPrimary;

    internal BuffEnchantAnyWeaponReplacement(BlueprintItemEnchantmentReference enchant, bool toPrimary = true)
    {
      Enchant = enchant;
      ToPrimary = toPrimary;
    }

    public override void OnActivate()
    {
      try
      {
        Apply();
      }
      catch (Exception e)
      {
        Logger.LogException("BuffEnchantAnyWeaponReplacement.OnActivate", e);
      }
    }

    public override void OnDeactivate()
    {
      try
      {
        Clear();
      }
      catch (Exception e)
      {
        Logger.LogException("BuffEnchantAnyWeaponReplacement.OnDeactivate", e);
      }
    }

    public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
    {
      try
      {
        if (slot.Owner != Owner)
          return;

        Clear();
        Apply();
      }
      catch (Exception e)
      {
        Logger.LogException("BuffEnchantAnyWeaponReplacement.HandleEquipmentSlotUpdated", e);
      }
    }

    public void HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit)
    {
      try
      {
        if (unit != Owner)
          return;

        Clear();
        Apply();
      }
      catch (Exception e)
      {
        Logger.LogException("BuffEnchantAnyWeaponReplacement.HandleUnitChangeActiveEquipmentSet", e);
      }
    }

    private void Apply()
    {
      if (ToPrimary)
      {
        var primary = Owner.Body.PrimaryHand.MaybeWeapon;
        if (primary is not null)
        {
          Logger.Verbose(() => $"Applying {Enchant.NameSafe()} to {primary.Name} (primary)");
          Data.Enchantments.Add(primary.AddEnchantment(Enchant, Context));
        }

        var emptyHand = Owner.Body.EmptyHandWeapon;
        if (emptyHand is not null && emptyHand != primary)
        {
          Logger.Verbose(() => $"Applying {Enchant.NameSafe()} to {emptyHand.Name} (empty hand)");
          Data.Enchantments.Add(emptyHand.AddEnchantment(Enchant, Context));
        }
        return;
      }

      var secondary = Owner.Body.SecondaryHand.MaybeWeapon;
      if (secondary is not null && !Common.IsPrimaryWeapon(secondary))
      {
        Logger.Verbose(() => $"Applying {Enchant.NameSafe()} to {secondary.Name} (secondary)");
        Data.Enchantments.Add(secondary.AddEnchantment(Enchant, Context));
      }

      foreach (var limb in
        Owner.Body.AdditionalLimbs
          .Select(l => l.MaybeWeapon)
          .NotNull()
          .Where(l => !Common.IsPrimaryWeapon(l)))
      {
        Logger.Verbose(() => $"Applying {Enchant.NameSafe()} to {limb.Name} (limb)");
        Data.Enchantments.Add(limb.AddEnchantment(Enchant, Context));
      }
    }

    private void Clear()
    {
      foreach (var enchant in Data.Enchantments)
      {
        Logger.Verbose(() => $"Removing {enchant.Blueprint.Name} from {enchant.Owner?.Name}");
        enchant.Owner?.RemoveEnchantment(enchant);
      }
      Data.Enchantments.Clear();
    }

    public class BuffEnchantAnyWeaponReplacementData
    {
      [JsonProperty]
      public List<ItemEnchantment> Enchantments = new();
    }
  }
}
