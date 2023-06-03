using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.Utility;
using System.Linq;
using UniRx;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Carries the leveling state for legendary gifts.
  /// </summary>
  internal class LegendaryGiftState
  {
    internal readonly IntReactiveProperty AvailableGifts = new();
    internal readonly LevelUpController Controller;

    internal LegendaryGiftState(LevelUpController controller, int gifts)
    {
      AvailableGifts.Value = gifts;
      Controller = controller;
    }

    internal bool CanAddLegendaryAbility(StatType type)
    {
      if (AvailableGifts.Value == 0)
        return false;

      return type switch
      {
        StatType.Strength => IsMaxRank(Common.LegendaryStr),
        StatType.Dexterity => IsMaxRank(Common.LegendaryDex),
        StatType.Constitution => IsMaxRank(Common.LegendaryCon),
        StatType.Intelligence => IsMaxRank(Common.LegendaryInt),
        StatType.Wisdom => IsMaxRank(Common.LegendaryWis),
        StatType.Charisma => IsMaxRank(Common.LegendaryCha),
        _ => throw new System.NotImplementedException(),
      };
    }

    internal bool CanRemoveLegendaryAbility(StatType type)
    {
      return Controller.LevelUpActions.OfType<SelectLegendaryAbility>().Any(a => a.Attribute == type);
    }

    private bool IsMaxRank(BlueprintFeature blueprint)
    {
      var feature = Controller.Preview.GetFeature(blueprint);
      if (feature == null)
        return false;
      return feature.Rank >= feature.Blueprint.Ranks;
    }
  }
}
