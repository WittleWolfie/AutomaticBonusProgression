using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("fb99dd4d-44fb-421d-85c3-d339faa7099d")]
  internal class EnhancementEquivalenceRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnhancementEquivalenceRestriction));

    private readonly EnhancementType Type;
    private readonly int Enhancement;

    internal EnhancementEquivalenceRestriction(EnhancementType type, int enhancement)
    {
      Type = type;
      Enhancement = enhancement;
    }

    public override bool IsAvailable()
    {
      try
      {
        var currentBonus = Type switch
        {
          EnhancementType.Armor => GetBonus(Common.ArmorEquivalence),
          EnhancementType.Shield => throw new NotImplementedException(),
          EnhancementType.MainHand => throw new NotImplementedException(),
          EnhancementType.OffHand => throw new NotImplementedException(),
          _ => throw new NotImplementedException(),
        };

        Logger.Verbose(() => $"Current bonus: {currentBonus}; Need: {Enhancement}");
        return currentBonus + Enhancement <= 5;
      }
      catch (Exception e)
      {
        Logger.LogException("EnhancementEquivalenceRestriction.IsAvailable", e);
      }
      return false;
    }

    private int GetBonus(BlueprintBuff buff)
    {
      if (Owner.GetFact(buff) is not Buff appliedBuff)
        return 0;

      return appliedBuff.GetRank();
    }
  }
}
