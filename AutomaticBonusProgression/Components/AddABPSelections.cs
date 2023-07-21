using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AddABPSelections));

    private readonly List<StatType> PhysicalProwess;
    private readonly List<StatType> MentalProwess;
    private readonly List<BlueprintFeatureReference> LegendaryGifts;

    internal AddABPSelections(
      List<StatType> physicalProwess,
      List<StatType> mentalProwess,
      List<BlueprintFeatureReference> legendaryGifts)
    {
      PhysicalProwess = physicalProwess;
      MentalProwess = mentalProwess;
      LegendaryGifts = legendaryGifts;
    }

    public override void OnActivate()
    {
      try
      {
        if (Data.Applied)
          return;

        var level = GetLevels();
        Logger.Log($"Applying ABP Selections to {Owner.CharacterName} {level}");
        Data.Applied = true;

        ApplyPhysicalProwess(level);
        ApplyMentalProwess(level);
      }
      catch (Exception e)
      {
        Logger.LogException("AddABPSelections.OnActivate", e);
      }
    }

    private int GetLevels()
    {
      var summon = Rulebook.CurrentContext.LastEvent<RuleSummonUnit>();
      if (summon is null)
      {
        Logger.Warning("NO SUMMON BITCH");
      }
      return Rulebook.CurrentContext.LastEvent<RuleSummonUnit>()?.Level ?? 20;
    }

    private void ApplyPhysicalProwess(int level)
    {
      var count = GetPhysicalProwessCount(level);
      for (int i = 0; i < count && i < PhysicalProwess.Count; i++)
      {
        var prowess = GetProwess(PhysicalProwess[i]);
        Logger.Verbose(() => $"Applying {prowess.Name} to {Owner.CharacterName}");
        Owner.AddFact(prowess);
      }
    }

    private void ApplyMentalProwess(int level)
    {
      var count = GetMentalProwessCount(level);
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
      if (level < 7)
        return 0;
      if (level < 12)
        return 1;
      if (level < 13)
        return 2;
      if (level < 16)
        return 3;
      if (level < 17)
        return 4;
      if (level < 18)
        return 5;
      return 6;
    }

    private static int GetMentalProwessCount(int level)
    {
      if (level < 6)
        return 0;
      if (level < 11)
        return 1;
      if (level < 13)
        return 2;
      if (level < 15)
        return 3;
      if (level < 17)
        return 4;
      if (level < 18)
        return 5;
      return 6;
    }

    public class AddABPSelectionsData
    {
      [JsonProperty]
      public bool Applied;
    }
  }
}
