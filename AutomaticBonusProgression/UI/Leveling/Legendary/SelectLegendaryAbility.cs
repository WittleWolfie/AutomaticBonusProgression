﻿using AutomaticBonusProgression.Util;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Newtonsoft.Json;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Implements the actual unit effect to grant legendary ability.
  /// </summary>
  internal class SelectLegendaryAbility : ILevelUpAction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(SelectLegendaryAbility));

    internal readonly StatType Attribute;
    private readonly LegendaryGiftState State;

    [JsonConstructor]
    public SelectLegendaryAbility() { }

    internal SelectLegendaryAbility(StatType type, LegendaryGiftState state)
    {
      Attribute = type;
      State = state;
    }

    public LevelUpActionPriority Priority => LevelUpActionPriority.AddAttribute;

    public bool NeedUpdateUnitView => false;

    public void Apply(LevelUpState state, UnitDescriptor unit)
    {
      Feature feature = null;
      switch (Attribute)
      {
        case StatType.Strength:
          feature = unit.AddFact(Common.LegendaryStr) as Feature;
          break;
        case StatType.Dexterity:
          feature = unit.AddFact(Common.LegendaryDex) as Feature;
          break;
        case StatType.Constitution:
          feature = unit.AddFact(Common.LegendaryCon) as Feature;
          break;
        case StatType.Intelligence:
          feature = unit.AddFact(Common.LegendaryInt) as Feature;
          break;
        case StatType.Wisdom:
          feature = unit.AddFact(Common.LegendaryWis) as Feature;
          break;
        case StatType.Charisma:
          feature = unit.AddFact(Common.LegendaryCha) as Feature;
          break;
      }
      feature!.SetSource(Common.MythicClass, state.NextMythicLevel);
    }

    public bool Check(LevelUpState state, UnitDescriptor unit)
    {
      Feature feature = null;
      switch (Attribute)
      {
        case StatType.Strength:
          feature = unit.GetFeature(Common.LegendaryStr);
          break;
        case StatType.Dexterity:
          feature = unit.GetFeature(Common.LegendaryDex);
          break;
        case StatType.Constitution:
          feature = unit.GetFeature(Common.LegendaryCon);
          break;
        case StatType.Intelligence:
          feature = unit.GetFeature(Common.LegendaryInt);
          break;
        case StatType.Wisdom:
          feature = unit.GetFeature(Common.LegendaryWis);
          break;
        case StatType.Charisma:
          feature = unit.GetFeature(Common.LegendaryCha);
          break;
      }

      if (feature is null)
        return true;

      return feature.Rank < feature.Blueprint.Ranks;
    }

    public void PostLoad() { }
  }
}
