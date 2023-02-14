using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using System;

namespace AutomaticBonusProgression.Components
{
  [AllowMultipleComponents]
  [TypeId("58acc66d-7dff-4dcd-8ccc-0b46cbad4f8b")]
  internal class PrerequisiteFeatureMaxRanks : Prerequisite
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(PrerequisiteFeatureMaxRanks));

    private readonly BlueprintFeatureReference Feature;
    private readonly int Ranks;

    internal PrerequisiteFeatureMaxRanks(Blueprint<BlueprintFeatureReference> feature, int ranks)
    {
      Feature = feature.Reference;
      Ranks = ranks;
    }

    public override bool CheckInternal(FeatureSelectionState selectionState, UnitDescriptor unit, LevelUpState state)
    {
      try
      {
        var feature = unit.GetFact(Feature);
        return feature is null || feature.GetRank() <= Ranks;
      }
      catch (Exception e)
      {
        Logger.LogException("PrerequisiteFeatureRanks.CheckInternal", e);
      }
      return false;
    }

    public override string GetUITextInternal(UnitDescriptor unit)
    {
      return $"{Feature?.NameSafe()} - Max({Ranks})";
    }
  }
}
