using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// Handles the default selections applied to companions. Add to the companion's *_FeatureList.
  /// </summary>
  /// 
  /// <remarks>
  /// There's no need to handle legendary gifts since companions never come in with Mythic levels or at level 19+
  /// </remarks>
  [AllowedOn(typeof(BlueprintUnitFact))]
  [TypeId("1dea124f-1eb2-430c-ae9f-c2b453fad3c1")]
  internal class AddABPSelections : UnitFactComponentDelegate<AddABPSelections.AddABPSelectionsData>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BlueprintUnitFact));

    private readonly List<StatType> PhysicalProwess;
    private readonly List<StatType> MentalProwess;

    internal AddABPSelections(List<StatType> physicalProwess, List<StatType> mentalProwess)
    {
      PhysicalProwess = physicalProwess;
      MentalProwess = mentalProwess;
    }

    public override void OnActivate()
    {
      try
      {
        if (Data.Applied)
          return;

        Logger.Log($"Applying ABP Selections to {Owner.CharacterName} {Owner.Progression.CharacterLevel}");
        Data.Applied = true;

        ApplyPhysicalProwess();
        ApplyMentalProwess();
      }
      catch (Exception e)
      {
        Logger.LogException("AddABPSelections.OnActivate", e);
      }
    }

    private void ApplyPhysicalProwess()
    {
      var count = GetPhysicalProwessCount(Owner.Progression.CharacterLevel);
      for (int i = 0; i < count && i < PhysicalProwess.Count; i++)
      {
        var prowess = GetProwess(PhysicalProwess[i]);
        Logger.Verbose(() => $"Applying {prowess.Name} to {Owner.CharacterName}");
        Owner.AddFact(prowess);
      }
    }

    private void ApplyMentalProwess()
    {
      var count = GetMentalProwessCount(Owner.Progression.CharacterLevel);
      for (int i = 0; i < count && i < MentalProwess.Count; i++)
      {
        var prowess = GetProwess(MentalProwess[i]);
        Logger.Verbose(() => $"Applying {prowess.Name} to {Owner.CharacterName}");
        Owner.AddFact(prowess);
      }
    }

    private static BlueprintFeature GetProwess(StatType type)
    {
      return type switch
      {
        StatType.Strength => Common.StrProwess,
        StatType.Dexterity => Common.DexProwess,
        StatType.Constitution => Common.ConProwess,
        StatType.Intelligence => Common.IntProwess,
        StatType.Wisdom => Common.WisProwess,
        StatType.Charisma => Common.ChaProwess,
        _ => throw new NotImplementedException(),
      };
    }

    private static int GetPhysicalProwessCount(int level)
    {
      return level switch
      {
        7 => 1,
        12 => 2,
        13 => 3,
        16 => 4,
        17 => 5,
        18 => 6,
        _ => 0,
      };
    }

    private static int GetMentalProwessCount(int level)
    {
      return level switch
      {
        6 => 1,
        11 => 2,
        13 => 3,
        15 => 4,
        17 => 5,
        18 => 6,
        _ => 0,
      };
    }

    public class AddABPSelectionsData
    {
      [JsonProperty]
      public bool Applied;
    }
  }
}
