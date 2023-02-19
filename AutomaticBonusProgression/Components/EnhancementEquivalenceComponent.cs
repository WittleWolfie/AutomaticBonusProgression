using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
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
  /// Listen to this to disable buffs when needed.
  /// </summary>
  internal interface IEnhancementEquivalence : IUnitSubscriber
  {
    void OnChanged(EnhancementType type, Buff buff);
  }

  /// <summary>
  /// Tracks the equivalent enhancement bonus of a fact.
  /// </summary>
  [AllowedOn(typeof(BlueprintUnitFact))]
  [AllowedOn(typeof(BlueprintItemEnchantment))]
  [TypeId("4c9f19e3-0b2c-45b6-87c4-d22140b55f64")]
  internal class EnhancementEquivalenceComponent : EntityFactComponentDelegate, IEnhancementEquivalence
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnhancementEquivalenceComponent));

    private readonly EnhancementType Type;
    private readonly int Enhancement;
    private readonly bool CheckOnChange;

    internal EnhancementEquivalenceComponent(EnhancementType type, int enhancement, bool checkOnChange = false)
    {
      Type = type;
      Enhancement = enhancement;
      CheckOnChange = checkOnChange;
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

        var buff = Type switch
        {
          EnhancementType.Armor => Common.ArmorEquivalence,
          EnhancementType.Shield => throw new NotImplementedException(),
          EnhancementType.MainHand => throw new NotImplementedException(),
          EnhancementType.OffHand => throw new NotImplementedException(),
          _ => throw new NotImplementedException(),
        };

        var currentRank = 0;
        if (owner.GetFact(buff) is not Buff enhancementBuff)
          enhancementBuff = owner.AddBuff(buff, Context);
        else
          currentRank = enhancementBuff.GetRank();

        if (enhancementBuff == null)
        {
          Logger.Warning("Not applied");
          return;
        }

        var rank = currentRank + Enhancement;
        Logger.Verbose(() => $"Setting {Type} rank to {rank} for {owner}");
        enhancementBuff.SetRank(rank);
        EventBus.RaiseEvent<IEnhancementEquivalence>(owner, c => c.OnChanged(Type, enhancementBuff));
      }
      catch (Exception e)
      {
        Logger.LogException("EnhancementEquivalenceComponent.OnActivate", e);
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

        var buff = Type switch
        {
          EnhancementType.Armor => Common.ArmorEquivalence,
          EnhancementType.Shield => throw new NotImplementedException(),
          EnhancementType.MainHand => throw new NotImplementedException(),
          EnhancementType.OffHand => throw new NotImplementedException(),
          _ => throw new NotImplementedException(),
        };

        if (owner.GetFact(buff) is not Buff appliedBuff)
        {
          Logger.Warning("Enhancement equivalence missing");
          return;
        }


        Logger.Verbose(() => $"Removing {Enhancement} {Type} from {owner}");
        for (int i = 0; i < Enhancement; i++)
        {
          using (ContextData<BuffCollection.RemoveByRank>.Request())
            appliedBuff.Remove();
        }
      }
      catch (Exception e)
      {
        Logger.LogException("EnhancementEquivalenceComponent.OnDeactivate", e);
      }
    }

    public void OnChanged(EnhancementType type, Buff buff)
    {
      try
      {
        if (!CheckOnChange)
          return;

        if (type != Type)
          return;

        if (buff.GetRank() > 5)
        {
          if (Fact is not Buff enchantment)
          {
            Logger.Warning($"Should remove {Fact} but it is not a buff");
            return;
          }
          Logger.Verbose(() => $"Removing {enchantment}, enhancement bonus is over 5");
          enchantment.Remove();
        }
      }
      catch (Exception e)
      {
        Logger.LogException("EnhancementEquivalenceComponent.OnIncrease", e);
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
