using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("960ff8a4-baff-4ebb-aa2a-3c92a9072912")]
  internal class ActivatableAlignmentRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ActivatableAlignmentRestriction));

    private readonly AlignmentComponent Alignment;

    internal ActivatableAlignmentRestriction(AlignmentComponent alignment)
    {
      Alignment = alignment;
    }

    public override bool IsAvailable()
    {
      try
      {
        return Owner.Alignment.ValueRaw.HasComponent(Alignment);
      }
      catch (Exception e)
      {
        Logger.LogException("ActivatableAlignmentRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}