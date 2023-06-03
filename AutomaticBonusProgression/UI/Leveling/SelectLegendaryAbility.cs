using AutomaticBonusProgression.Util;
using JetBrains.Annotations;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Newtonsoft.Json;

namespace AutomaticBonusProgression.UI.Leveling
{
  /// <summary>
  /// Implements the actual unit effect to grant legendary ability.
  /// </summary>
  internal class SelectLegendaryAbility : ILevelUpAction
  {
    internal readonly StatType Attribute;

    [JsonConstructor]
    public SelectLegendaryAbility() { }

    internal SelectLegendaryAbility(StatType type)
    {
      Attribute = type;
    }

    public LevelUpActionPriority Priority => LevelUpActionPriority.AddAttribute;

    public bool NeedUpdateUnitView => false;

    public void Apply(LevelUpState state, UnitDescriptor unit)
    {
      switch (Attribute)
      {
        case StatType.Strength:
          unit.AddFact(Common.LegendaryStr);
          break;
        case StatType.Dexterity:
          unit.AddFact(Common.LegendaryDex);
          break;
        case StatType.Constitution:
          unit.AddFact(Common.LegendaryCon);
          break;
        case StatType.Intelligence:
          unit.AddFact(Common.LegendaryInt);
          break;
        case StatType.Wisdom:
          unit.AddFact(Common.LegendaryWis);
          break;
        case StatType.Charisma:
          unit.AddFact(Common.LegendaryCha);
          break;
      }
    }

    public bool Check(LevelUpState state, [NotNull] UnitDescriptor unit)
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
