using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
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
  internal class EnhancementEquivalence : EntityFactComponentDelegate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnhancementEquivalence));

    internal readonly EnhancementType Type;
    internal readonly int Enhancement;

    internal EnhancementEquivalence(EnchantInfo enchant, EnhancementType? typeOverride = null)
    {
      Type = typeOverride ?? enchant.Type;
      Enhancement = enchant.Cost;
    }

    public override void OnActivate()
    {
      try
      {
        var owner = GetOwner();
        if (owner == null)
        {
          Logger.Warning("No owner");
          return;
        }

        owner.Ensure<UnitParts.UnitPartEnhancement>().AddEnchantment(Type, Enhancement);
      }
      catch (Exception e)
      {
        Logger.LogException("UnitPartEnhancement.OnActivate", e);
      }
    }

    public override void OnDeactivate()
    {
      try
      {
        var owner = GetOwner();
        if (owner == null)
        {
          Logger.Warning("No owner");
          return;
        }

        owner.Get<UnitParts.UnitPartEnhancement>()?.RemoveEnchantment(Type, Enhancement);
      }
      catch (Exception e)
      {
        Logger.LogException("UnitPartEnhancement.OnDeactivate", e);
      }
    }

    private UnitEntityData GetOwner()
    {
      if (Owner is ItemEntity item)
        return item.Wielder;
      else if (Owner is UnitEntityData unit)
        return unit;
      return null;
    }
  }
}
