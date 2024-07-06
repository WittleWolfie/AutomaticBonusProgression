using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Newtonsoft.Json;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Grants a unit a legendary feature
  /// </summary>
  internal class SelectLegendaryFeature : ILevelUpAction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(SelectLegendaryFeature));

    internal readonly BlueprintFeature Feature;
    private readonly LegendaryGiftState State;

    [JsonConstructor]
    public SelectLegendaryFeature() { }

    internal SelectLegendaryFeature(BlueprintFeature feature, LegendaryGiftState state)
    {
      Feature = feature;
      State = state;
    }

    public LevelUpActionPriority Priority => LevelUpActionPriority.Features;

    public bool NeedUpdateUnitView => false;

    public void Apply(LevelUpState state, UnitDescriptor unit)
    {
      var feature = unit.AddFact(Feature) as Feature;
      feature.SetSource(Common.MythicClass, state.NextMythicLevel);
    }

    public bool Check(LevelUpState state, UnitDescriptor unit)
    {
      if (!Feature.MeetsPrerequisites(selectionState: null, unit: unit, state: state, fromProgression: false))
        return false;

      return !unit.HasFact(Feature);
    }

    public void PostLoad() { }
  }
}
