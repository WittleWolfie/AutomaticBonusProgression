using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using System;

namespace AutomaticBonusProgression.Components
{
  [TypeId("960ff8a4-baff-4ebb-aa2a-3c92a9072912")]
  internal class AlignmentActivatableRestriction : ActivatableAbilityRestriction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AlignmentActivatableRestriction));

    private readonly AlignmentComponent Alignment;

    internal AlignmentActivatableRestriction(AlignmentComponent alignment)
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
        Logger.LogException("AlignmentActivatableRestriction.IsAvailable", e);
      }
      return false;
    }
  }
}