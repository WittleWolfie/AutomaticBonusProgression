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
    // If true it means this is granted by Legendary Gifts so the usual check logic doesn't apply
    internal readonly bool IsGift;

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
      if (!IsGift)
      {
        // Make sure this is a valid level
        switch (Attribute)
        {
          case StatType.Strength:
          case StatType.Dexterity:
          case StatType.Constitution:
            if (!PhysicalProwessLevels.Contains(state.NextCharacterLevel))
              return false;
            break;
          case StatType.Intelligence:
          case StatType.Wisdom:
          case StatType.Charisma:
            if (!MentalProwessLevels.Contains(state.NextCharacterLevel))
              return false;
            break;
        }
      }

      Feature feature = null;
      switch (Attribute)
      {
        case StatType.Strength:
          feature = unit.GetFeature(Common.StrProwess);
          break;
        case StatType.Dexterity:
          feature = unit.GetFeature(Common.DexProwess);
          break;
        case StatType.Constitution:
          feature = unit.GetFeature(Common.ConProwess);
          break;
        case StatType.Intelligence:
          feature = unit.GetFeature(Common.IntProwess);
          break;
        case StatType.Wisdom:
          feature = unit.GetFeature(Common.WisProwess);
          break;
        case StatType.Charisma:
          feature = unit.GetFeature(Common.ChaProwess);
          break;
      }

      if (feature is null)
        return true;

      return feature.Rank < feature.Blueprint.Ranks;
    }

    public void PostLoad() { }

    internal static readonly List<int> PhysicalProwessLevels = new() { 7, 12, 13, 16, 17, 18 };
    internal static readonly List<int> MentalProwessLevels = new() { 6, 11, 13, 15, 17, 18 };
  }

  internal class SelectPhysicalProwess : SelectProwess
  {
    public SelectPhysicalProwess(StatType type, bool isGift = false) : base(type) { }
  }
  internal class SelectMentalProwess : SelectProwess
  {
    public SelectMentalProwess(StatType type, bool isGift = false) : base(type) { }
  }
}
