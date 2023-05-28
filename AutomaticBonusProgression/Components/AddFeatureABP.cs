using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;
using System;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// This is used to make sure only PCs receive ABP features. It requires each to be wrapped in an adder blueprint but
  /// is probably better than the alternative which means it applies to literally every unit. 
  /// </summary>
  internal class AddFeatureABP : UnitFactComponentDelegate<AddFeatureABP.AddFeatureABPData>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AddFeatureABP));

    private readonly BlueprintFeatureReference Feature;

    internal AddFeatureABP(Blueprint<BlueprintFeatureReference> feature)
    {
      Feature = feature.Reference;
    }

    public override void OnActivate()
    {
      try
      {
        if (!Common.IsAffectedByABP(Owner))
        {
          Logger.Verbose(() => $"Not applying {Feature.Get().name} to {Owner}");
          return;
        }

        Logger.Verbose(() => $"Applying {Feature.Get().name} to {Owner}");
        Data.AppliedFact = Owner.AddFact(Feature);
      }
      catch (Exception e)
      {
        Logger.LogException("AddFeatureABP.OnActivate", e);
      }
    }

    public override void OnDeactivate()
    {
      try
      {
        if (Data.AppliedFact is not null)
        {
          Logger.Verbose(() => $"Removing {Feature.Get().name} from {Owner}");
          Owner.RemoveFact(Data.AppliedFact);
          Data.AppliedFact = null;
        }
      }
      catch (Exception e)
      {
        Logger.LogException("AddFeatureABP.OnDeactivate", e);
      }
    }

    internal class AddFeatureABPData
    {
      [JsonProperty]
      public EntityFact AppliedFact;
    }
  }
}
