using AutomaticBonusProgression.UnitParts;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Components;
using Newtonsoft.Json;
using System;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// Base class for applying enchantment effects.
  /// </summary>
  /// 
  /// <remarks>
  /// Implementing classes indicate whether the effect buff should be applied, this handles actually applying and removing it.
  /// </remarks>
  [TypeId("9f0122b2-7afe-4289-9104-164a6b6e3671")]
  internal abstract class AttunementEffect :
    UnitBuffComponentDelegate<AttunementEffect.ComponentData>,
    IUnitActiveEquipmentSetHandler,
    IUnitEquipmentHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AttunementEffect));

    internal readonly BlueprintBuffReference EffectBuff;
    internal readonly int Cost;

    protected AttunementEffect(BlueprintBuffReference effectBuff, int cost)
    {
      EffectBuff = effectBuff;
      Cost = cost;
    }

    public override void OnActivate()
    {
      try
      {
        ApplyEffect();
      }
      catch (Exception e)
      {
        Logger.LogException("AttunementEffect.OnActivate", e);
      }
    }

    public override void OnDeactivate()
    {
      try
      {
        if (Data.AppliedBuff is not null)
        {
          Logger.Verbose(() => $"Deactivating {Data.AppliedBuff.Name}");
          Disable();
        }
      }
      catch (Exception e)
      {
        Logger.LogException("AttunementEffect.OnDeactivate", e);
      }
    }

    public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem)
    {
      try
      {
        if (slot.Owner != Owner)
          return;

        ApplyEffect();
      }
      catch (Exception e)
      {
        Logger.LogException("AttunementEffect.HandleEquipmentSlotUpdated", e);
      }
    }

    public void HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit)
    {
      try
      {
        if (unit != Owner)
          return;

        ApplyEffect();
      }
      catch (Exception e)
      {
        Logger.LogException("AttunementEffect.HandleUnitChangeActiveEquipmentSet", e);
      }
    }

    private void ApplyEffect()
    {
      var buffApplied = Data.AppliedBuff is not null;
      var shouldApply = IsAvailable(Owner) && CanAfford(buffApplied);

      if (buffApplied && !shouldApply)
      {
        Logger.Verbose(() => $"Unsupported enchantment, removing {Data.AppliedBuff.Name} [{Type}, {Cost}]");
        Disable();
      }
      else if (!buffApplied && shouldApply)
      {
        var buff = EffectBuff.Get();
        Logger.Verbose(() => $"Applying enchantment {buff.Name} [{Type}, {Cost}]");
        Data.AppliedBuff = Owner.AddBuff(buff, Context);
        Owner.Ensure<UnitPartEnhancement>().AddEnchantment(Type, Cost);
      }
    }

    private void Disable()
    {
      Data.AppliedBuff.Remove();
      Data.AppliedBuff = null;
      Owner.Get<UnitPartEnhancement>()?.RemoveEnchantment(Type, Cost);
    }

    private bool CanAfford(bool applied)
    {
      if (applied)
        return Owner.Ensure<UnitPartEnhancement>().CanKeep(Type);
      return Owner.Ensure<UnitPartEnhancement>().CanAdd(Type, Cost);
    }

    public abstract EnhancementType Type { get; }  
    public abstract bool IsAvailable(UnitDescriptor unit);

    /// <summary>
    /// Returns a comma separated list of requirement strings, or an empty string if there are none
    /// </summary>
    public virtual string GetRequirements()
    {
      return string.Empty;
    }

    internal class ComponentData
    {
      [JsonProperty]
      internal Buff AppliedBuff;
    }
  }
}
