using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("8dda267b-4b51-4daf-a627-9266a9f5b882")]
  internal class EnhancementRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnhancementRestriction));

    private readonly EnhancementType Type;
    internal readonly int Enhancement;

    internal EnhancementRestriction(EnchantInfo enchant, EnhancementType? typeOverride = null)
    {
      Type = typeOverride ?? enchant.Type;
      Enhancement = enchant.Cost;
    }

    public override bool IsAvailable()
    {
      try
      {
        if (Fact.IsOn)
          return Owner.Ensure<UnitParts.EnhancementEquivalence>().CanKeep(Type);
        return Owner.Ensure<UnitParts.EnhancementEquivalence>().CanAdd(Type, Enhancement);
      }
      catch (Exception e)
      {
        Logger.LogException("EnhancementRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}
