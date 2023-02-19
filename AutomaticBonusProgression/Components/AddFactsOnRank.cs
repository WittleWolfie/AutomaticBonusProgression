using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [AllowMultipleComponents]
  [TypeId("46ab4629-0ce3-4eba-8072-cdc156230076")]
  internal class AddFactsOnRank : UnitFactComponentDelegate<AddFactsOnRank.ComponentData>, IOwnerGainLevelHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AddFactsOnRank));

    private readonly int Rank;
    private readonly List<BlueprintUnitFactReference> Facts;

    internal AddFactsOnRank(int rank, params Blueprint<BlueprintUnitFactReference>[] facts)
    {
      Rank = rank;
      Facts = facts.Select(f => f.Reference).ToList();
    }

    public void HandleUnitGainLevel()
    {
      Apply();
    }

    public override void OnActivate()
    {
      Apply();
    }

    public override void OnDeactivate()
    {
      Remove();
    }

    private void Apply()
    {
      try
      {
        var ranks = Fact.GetRank();
        foreach (var fact in Facts)
        if (ranks >= Rank && !Owner.HasFact(fact))
        {
          Data.AppliedFacts.Add(Owner.AddFact(fact));
          Logger.Verbose(() => $"Applied {fact.Get().Name} to {Owner.CharacterName} for {ranks}");
        }
      }
      catch (Exception e)
      {
        Logger.LogException("AddFactsOnRank.Apply", e);
      }
    }

    private void Remove()
    {
      try
      {
        foreach (var fact in Data.AppliedFacts)
        {
          if (fact is null)
            continue;
          Logger.Verbose(() => $"Removing {fact.Name} from {Owner.CharacterName}");
          Owner.RemoveFact(fact);
        }
        Data.AppliedFacts.Clear();
      }
      catch (Exception e)
      {
        Logger.LogException("AddFactsOnRank.Remove", e);
      }
    }

    public class ComponentData
    {
      [JsonProperty]
      public List<EntityFact> AppliedFacts = new();
    }
  }
}
