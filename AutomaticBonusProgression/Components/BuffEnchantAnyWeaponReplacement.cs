using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
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
    IUnitEquipmentHandler,
    IPolymorphActivatedHandler,
    IPolymorphDeactivatedHandler,
    IUnitEmptyHandWeaponHandler
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

        Apply();
      }
      catch (Exception e)
      {
        Logger.LogException("BuffEnchantAnyWeaponReplacement.HandleUnitChangeActiveEquipmentSet", e);
      }
    }

    public void OnPolymorphActivated(UnitEntityData unit, Polymorph polymorph)
    {
      try
      {
        if (unit != Owner)
          return;

        Apply();
      }
      catch (Exception e)
      {
        Logger.LogException("BuffEnchantAnyWeaponReplacement.OnPolymorphActivated", e);
      }
    }

    public void OnPolymorphDeactivated(UnitEntityData unit, Polymorph polymorph)
    {
      try
      {
        if (unit != Owner)
          return;

        Apply();
      }
      catch (Exception e)
      {
        Logger.LogException("BuffEnchantAnyWeaponReplacement.OnPolymorphDeactivated", e);
      }
    }

    public void HandleUnitEmptyHandWeaponUpdated()
    {
      try
      {
        Apply();
      }
      catch (Exception e)
      {
        Logger.LogException("BuffEnchantAnyWeaponReplacement.HandleUnitEmptyHandWeaponUpdated", e);
      }
    }

    private void Apply()
    {
      // Track the weapons that should be enchanted
      List<ItemEntityWeapon> enchantedWeapons = new();
      foreach (var weapon in
        Owner.Body.AllSlots.OfType<WeaponSlot>()
          .Select(slot => slot.MaybeWeapon)
          .NotNull()
          .Distinct())
      {
        var isPrimary = Common.IsPrimaryWeapon(weapon);
        if (isPrimary && ToPrimary)
        {
          enchantedWeapons.Add(weapon);
          if (Data.Enchantments.ContainsKey(weapon))
            continue;

          Logger.Verbose(() => $"Applying {Enchant.NameSafe()} to {weapon.Name} (primary)");
          Data.Enchantments[weapon] = weapon.AddEnchantment(Enchant, Context);
        }
        else if (!isPrimary && !ToPrimary)
        {
          enchantedWeapons.Add(weapon);
          if (Data.Enchantments.ContainsKey(weapon))
            continue;

          Logger.Verbose(() => $"Applying {Enchant.NameSafe()} to {weapon.Name} (secondary)");
          Data.Enchantments[weapon] = weapon.AddEnchantment(Enchant, Context);
        }
      }

      // Now identify already enchanted weapons that should be cleared
      List<ItemEntityWeapon> unenchantedWeapons = new();
      foreach (var weapon in Data.Enchantments.Keys)
      {
        if (!enchantedWeapons.Contains(weapon))
          unenchantedWeapons.Add(weapon);
      }

      foreach (var weapon in unenchantedWeapons)
      {
        var enchant = Data.Enchantments[weapon];
        Logger.Verbose(() => $"Removing {enchant.Blueprint.Name} from {weapon.Name}");
        weapon.RemoveEnchantment(enchant);
        Data.Enchantments.Remove(weapon);
      }
    }

    private void Clear()
    {
      foreach (var enchant in Data.Enchantments)
      {
        var weapon = enchant.Key;
        var enchantment = enchant.Value;
        Logger.Verbose(() => $"Removing {enchantment.Blueprint.Name} from {weapon.Name}");
        weapon.RemoveEnchantment(enchantment);
      }
      Data.Enchantments.Clear();
    }

    public class BuffEnchantAnyWeaponReplacementData
    {
      [JsonProperty]
      public Dictionary<ItemEntityWeapon, ItemEnchantment> Enchantments = new();
    }
  }
}
