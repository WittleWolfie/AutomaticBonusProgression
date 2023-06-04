using AutomaticBonusProgression.Util;
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

    public bool Check(LevelUpState state, UnitDescriptor unit)
    {
      return State.CanAddLegendaryAbility(Attribute);
    }

    public void PostLoad() { }
  }
}
