using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.Utility;
using System;
using System.ComponentModel;
using System.Linq;
using UniRx;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Carries the leveling state for legendary gifts.
  /// </summary>
  internal class LegendaryGiftState
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGiftState));

    internal readonly IntReactiveProperty AvailableGifts = new();
    internal readonly LevelUpController Controller;

    internal LegendaryGiftState(LevelUpController controller, int gifts)
    {
      AvailableGifts.Value = gifts;
      Controller = controller;
    }

    #region Legendary Ability
    internal void TryAddLegendaryAbility(StatType type)
    {
      if (!CanAddLegendaryAbility(type))
        return;

      Controller.AddAction(new SelectLegendaryAbility(type, this));
      AvailableGifts.Value--;
    }

    internal void TryRemoveLegendaryAbility(StatType type)
    {
      if (!CanRemoveLegendaryAbility(type))
        return;

      Controller.RemoveAction<SelectLegendaryAbility>(a => a.Attribute == type);
      AvailableGifts.Value++;
      Controller.UpdatePreview();
    }

    internal bool CanAddLegendaryAbility(StatType type, bool checkGifts = true)
    {
      // CheckGifts is used so that `SelectLegendaryAbility` returns correctly. Without this, selecting a later option
      // will clear any legendary ability selections since it will be checked after gifts are spent.
      if (checkGifts && AvailableGifts.Value == 0)
        return false;

      return type switch
      {
        StatType.Strength => !IsMaxRank(Common.LegendaryStr),
        StatType.Dexterity => !IsMaxRank(Common.LegendaryDex),
        StatType.Constitution => !IsMaxRank(Common.LegendaryCon),
        StatType.Intelligence => !IsMaxRank(Common.LegendaryInt),
        StatType.Wisdom => !IsMaxRank(Common.LegendaryWis),
        StatType.Charisma => !IsMaxRank(Common.LegendaryCha),
        _ => throw new NotImplementedException(),
      };
    }

    internal bool CanRemoveLegendaryAbility(StatType type)
    {
      return Controller.LevelUpActions.OfType<SelectLegendaryAbility>().Any(a => a.Attribute == type);
    }
    #endregion

    #region Legendary Prowess
    internal void TrySelectProwess(StatType type)
    {
      if (!CanSelectProwess(type))
        return;

      Logger.Log($"Applying Prowess {type}");
      switch (type)
      {
        case StatType.Strength:
        case StatType.Dexterity:
        case StatType.Constitution:
          Controller.AddAction(new SelectPhysicalProwess(type, isGift: true));
          break;
        case StatType.Intelligence:
        case StatType.Wisdom:
        case StatType.Charisma:
          Controller.AddAction(new SelectMentalProwess(type, isGift: true));
          break;
        default:
          throw new InvalidEnumArgumentException($"Prowess isn't supported for {type}");
      }
      AvailableGifts.Value--;
    }

    internal void TryUnselectProwess(StatType type)
    {
      var removedAction = false;
      switch (type)
      {
        case StatType.Strength:
        case StatType.Dexterity:
        case StatType.Constitution:
          removedAction = Controller.RemoveAction<SelectPhysicalProwess>(a => a.Attribute == type);
          break;
        case StatType.Intelligence:
        case StatType.Wisdom:
        case StatType.Charisma:
          removedAction = Controller.RemoveAction<SelectMentalProwess>(a => a.Attribute == type);
          break;
        default:
          throw new InvalidEnumArgumentException($"Prowess isn't supported for {type}");
      }

      if (removedAction)
      {
        Logger.Log($"Removed Prowess {type}");
        AvailableGifts.Value++;
        Controller.UpdatePreview();
      }
    }

    internal bool CanSelectProwess(StatType type, bool checkGifts = true)
    {
      if (checkGifts && AvailableGifts.Value == 0)
        return false;

      return type switch
      {
        StatType.Strength => !IsMaxRank(Common.StrProwess),
        StatType.Dexterity => !IsMaxRank(Common.DexProwess),
        StatType.Constitution => !IsMaxRank(Common.ConProwess),
        StatType.Intelligence => !IsMaxRank(Common.IntProwess),
        StatType.Wisdom => !IsMaxRank(Common.WisProwess),
        StatType.Charisma => !IsMaxRank(Common.ChaProwess),
        _ => throw new NotImplementedException(),
      };
    }
    #endregion

    private bool IsMaxRank(BlueprintFeature blueprint)
    {
      return GetRank(blueprint) >= blueprint.Ranks;
    }

    private int GetRank(BlueprintFeature blueprint)
    {
      var feature = Controller.Preview.GetFeature(blueprint);
      if (feature is null)
        return 0;
      return feature.Rank;
    }
  }
}
