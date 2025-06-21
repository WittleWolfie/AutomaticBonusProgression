using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
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
  internal class AddFeatureABP : UnitFactComponentDelegate<AddFeatureABP.AddFeatureABPData>, IPartyHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AddFeatureABP));

    private readonly BlueprintFeatureReference Feature;

    internal AddFeatureABP(Blueprint<BlueprintFeatureReference> feature)
    {
      Feature = feature.Reference;
    }

    public void HandleAddCompanion(UnitEntityData unit)
    {
      if (unit == Owner)
        OnActivate();
    }

    public void HandleCapitalModeChanged() {}

    public void HandleCompanionActivated(UnitEntityData unit)
    {
      if (unit == Owner)
        OnActivate();
    }

    public void HandleCompanionRemoved(UnitEntityData unit, bool stayInGame)
    {
      if (unit == Owner)
        OnDeactivate();
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
        if (Data.AppliedFact == null)
        {
          Data.AppliedFact = Owner.AddFact(Feature);
          while (Owner.GetFeature(Feature).GetRank() < rank)
            Owner.GetFeature(Feature).AddRank();
        }
        else
        {
          if (Owner.GetFeature(Feature).GetRank() < rank)
          {
            while (Owner.GetFeature(Feature).GetRank() < rank)
              Owner.GetFeature(Feature).AddRank();
          }
          else
          {
            while (Owner.GetFeature(Feature).GetRank() > rank)
              Owner.GetFeature(Feature).RemoveRank();
          }
        }
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
          while (Owner.GetFeature(Feature).GetRank() > Math.Max(Fact.GetRank(), 1))
            Owner.GetFeature(Feature).RemoveRank();
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
