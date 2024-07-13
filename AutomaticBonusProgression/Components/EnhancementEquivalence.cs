using AutomaticBonusProgression.UnitParts;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Newtonsoft.Json;
using System;

namespace AutomaticBonusProgression.Components
{
  internal enum EnhancementType
  {
    Armor,
    Shield,
    MainHand,
    OffHand
  }

  /// <summary>
  /// Tracks the equivalent enhancement bonus of a fact.
  /// </summary>
  [AllowedOn(typeof(BlueprintUnitFact))]
  [AllowedOn(typeof(BlueprintItemEnchantment))]
  [TypeId("4c9f19e3-0b2c-45b6-87c4-d22140b55f64")]
  internal class EnhancementEquivalence : EntityFactComponentDelegate<EnhancementEquivalence.ComponentData>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnhancementEquivalence));

    internal readonly EnhancementType Type;
    internal readonly int Enhancement;

    internal EnhancementEquivalence(EnhancementType type, int enhancementCost)
    {
      Type = type;
      Enhancement = enhancementCost;
    }

    public override void OnActivate()
    {
      try
      {
        var owner = GetOwner(Owner);
        if (owner == null)
        {
          Logger.Warning("No owner");
          return;
        }

        Data.AppliedType = GetEnhancementType();
        Data.AppliedEnhancement = true;
        Logger.Verbose(() => $"Adding {Enhancement} to {Data.AppliedType}, from {OwnerBlueprint.NameSafe()}");
        owner.Ensure<UnitPartEnhancement>().AddEnchantment(Data.AppliedType, Enhancement);
      }
      catch (Exception e)
      {
        Logger.LogException("EnhancementEquivalence.OnActivate", e);
      }
    }

    public override void OnDeactivate()
    {
      try
      {
        var owner = GetOwner(Owner);
        if (owner == null)
        {
          Logger.Warning("No owner");
          return;
        }

        Logger.Verbose(() => $"Removing {Enhancement} from {Data.AppliedType}, from {OwnerBlueprint.NameSafe()}");
        owner.Get<UnitPartEnhancement>()?.RemoveEnchantment(Data.AppliedType, Enhancement);
      }
      catch (Exception e)
      {
        Logger.LogException("EnhancementEquivalence.OnDeactivate", e);
      }
    }

    public void Refresh(EntityFactComponent component)
    {
      var owner = GetOwner(component.Owner);
      if (owner == null)
      {
        Logger.Warning("No owner");
        return;
      }

      var data = component.GetData<ComponentData>();
      if (data is null || !data.AppliedEnhancement)
      {
        Logger.Warning($"Not refreshing {OwnerBlueprint.NameSafe()}, never applied.");
        return;
      }

      Logger.Verbose(() => $"Adding {Enhancement} to {data.AppliedType}, from {OwnerBlueprint.NameSafe()}");
      var unitPart = owner.Ensure<UnitPartEnhancement>();
      unitPart.AddEnchantment(data.AppliedType, Enhancement);
    }

    private EnhancementType GetEnhancementType()
    {
      if (Owner is ItemEntityShield)
        return EnhancementType.Shield;
      if (Owner is ItemEntityArmor)
        return EnhancementType.Armor;
      if (Owner is ItemEntityWeapon weapon)
        return Common.IsPrimaryWeapon(weapon) ? EnhancementType.MainHand : EnhancementType.OffHand;
      return Type;
    }

    private UnitEntityData GetOwner(EntityDataBase owner)
    {
      if (owner is ItemEntity item)
        return item.Wielder;
      else if (owner is UnitEntityData unit)
        return unit;
      return null;
    }

    internal class ComponentData
    {
      // Track this dynamically since some enchantments don't use explicit variants
      [JsonProperty]
      internal EnhancementType AppliedType;

      [JsonProperty]
      internal bool AppliedEnhancement = false;
    }
  }
}
