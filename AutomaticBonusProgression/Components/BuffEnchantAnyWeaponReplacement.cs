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
      Clear();

      foreach (var weapon in
        Owner.Body.AllSlots.OfType<WeaponSlot>()
          .Select(slot => slot.MaybeWeapon)
          .NotNull()
          .Distinct())
      {
        var isPrimary = Common.IsPrimaryWeapon(weapon);
        Logger.Warning($"Weapon: {weapon.Name}, {ToPrimary}, {isPrimary}");
        if (isPrimary && ToPrimary)
        {
          Logger.Verbose(() => $"Applying {Enchant.NameSafe()} to {weapon.Name} (primary)");
          Data.Enchantments.Add(weapon.AddEnchantment(Enchant, Context));
        }
        else if (!isPrimary && !ToPrimary)
        {
          Logger.Verbose(() => $"Applying {Enchant.NameSafe()} to {weapon.Name} (secondary)");
          Data.Enchantments.Add(weapon.AddEnchantment(Enchant, Context));
        }
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
      Logger.Warning($"CLEAR===========");
    }

    public class BuffEnchantAnyWeaponReplacementData
    {
      [JsonProperty]
      public List<ItemEnchantment> Enchantments = new();
    }
  }
}
