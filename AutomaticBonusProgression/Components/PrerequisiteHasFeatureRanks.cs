using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;

namespace AutomaticBonusProgression.Components
{
  [AllowMultipleComponents]
  [TypeId("264f9021-1828-4ccb-a8cc-c56998162ee0")]
  internal class PrerequisiteHasFeatureRanks : PrerequisiteFeature
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(PrerequisiteHasFeatureRanks));

    private readonly int Ranks;

    internal PrerequisiteHasFeatureRanks(Blueprint<BlueprintFeatureReference> feature, int ranks)
    {
      m_Feature = feature.Reference;
      Ranks = ranks;
    }

    public override bool CheckInternal(FeatureSelectionState selectionState, UnitDescriptor unit, LevelUpState state)
    {
      var feature = unit.GetFeature(Feature);
      return base.CheckInternal(selectionState, unit, state) && feature.GetRank() >= Ranks;
    }

    public override string GetUITextInternal(UnitDescriptor unit)
    {
      return $"{base.GetUITextInternal(unit)} [{Ranks}]";
    }
  }
}
