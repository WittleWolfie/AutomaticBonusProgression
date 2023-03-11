using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using System;

namespace AutomaticBonusProgression.Conditions
{
  [TypeId("67d0e483-5e3c-42c9-8a1e-513377941f55")]
  internal class HasResource : ContextCondition
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(HasResource));

    internal BlueprintAbilityResourceReference Resource;
    internal int Amount = 1;

    public override bool CheckCondition()
    {
      try
      {
        if (Context.MaybeCaster is null)
        {
          Logger.Warning("No caster!");
          return false;
        }

        return Context.MaybeCaster.Resources.HasEnoughResource(Resource, Amount);
      }
      catch (Exception e)
      {
        Logger.LogException("HasResource.CheckCondition", e);
      }
      return false;
    }

    public override string GetConditionCaption()
    {
      return "Checks if there are enough o' them there ability resources";
    }
  }
}
