using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
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
  [TypeId("8d011154-8225-4a40-bdc4-d3c9735884b1")]
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
          Logger.Verbose(() => $"Not applying {Feature.Get().name} to {Owner.CharacterName}");
          return;
        }

        var rank = Fact.GetRank();
        Logger.Verbose(() => $"Applying {Feature.Get().name} [{rank}] to {Owner.CharacterName}");
        for (int i = 0; i < rank; i++)
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
          Logger.Verbose(() => $"Removing {Feature.Get().name} from {Owner.CharacterName}");
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
