using AutomaticBonusProgression.UnitParts;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("8dda267b-4b51-4daf-a627-9266a9f5b882")]
  internal class EnhancementEquivalentRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnhancementEquivalentRestriction));

    private readonly EnhancementType Type;
    internal readonly int Enhancement;

    internal EnhancementEquivalentRestriction(EnchantInfo enchant)
    {
      Type = enchant.Type;
      Enhancement = enchant.Cost;
    }

    public override bool IsAvailable()
    {
      try
      {
        if (Fact.IsOn)
          return Owner.Ensure<EnhancementEquivalence>().CanKeep(Type);
        return Owner.Ensure<EnhancementEquivalence>().CanAdd(Type, Enhancement);
      }
      catch (Exception e)
      {
        Logger.LogException("EnhancementEquivalentRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}
