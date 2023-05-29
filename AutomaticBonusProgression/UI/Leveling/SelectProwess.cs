using AutomaticBonusProgression.Util;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutomaticBonusProgression.UI.Leveling
{
  /// <summary>
  /// Implements the actual unit effect to grant prowess. There's a subclass for Mental / Physical to support changing
  /// the selection, since that requires calling <c>LevelUpController.RemoveAction()</c> for a specific type.
  /// </summary>
  internal abstract class SelectProwess : ILevelUpAction
  {
    internal readonly StatType Attribute;

    [JsonConstructor]
    public SelectProwess() { }

    internal SelectProwess(StatType type)
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
          unit.AddFact(Common.StrProwess);
          break;
        case StatType.Dexterity:
          unit.AddFact(Common.DexProwess);
          break;
        case StatType.Constitution:
          unit.AddFact(Common.ConProwess);
          break;
        case StatType.Intelligence:
          unit.AddFact(Common.IntProwess);
          break;
        case StatType.Wisdom:
          unit.AddFact(Common.WisProwess);
          break;
        case StatType.Charisma:
          unit.AddFact(Common.ChaProwess);
          break;
      }
    }

    public bool Check(LevelUpState state, UnitDescriptor unit)
    {
      switch (Attribute)
      {
        case StatType.Strength:
        case StatType.Dexterity:
        case StatType.Constitution:
          return PhysicalProwessLevels.Contains(state.NextCharacterLevel);
        case StatType.Intelligence:
        case StatType.Wisdom:
        case StatType.Charisma:
          return MentalProwessLevels.Contains(state.NextCharacterLevel);
      }

      return false;
    }

    public void PostLoad() { }

    internal static readonly List<int> PhysicalProwessLevels = new() { 7, 12, 13, 16, 17, 18 };
    internal static readonly List<int> MentalProwessLevels = new() { 6, 11, 13, 15, 17, 18 };
  }

  internal class SelectPhysicalProwess : SelectProwess
  {
    public SelectPhysicalProwess(StatType type) : base(type) { }
  }
  internal class SelectMentalProwess : SelectProwess
  {
    public SelectMentalProwess(StatType type) : base(type) { }
  }
}
