using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// Handles the default selections applied to companions. Add to the companion's *_FeatureList.
  /// </summary>
  [AllowedOn(typeof(BlueprintUnitFact))]
  [TypeId("1dea124f-1eb2-430c-ae9f-c2b453fad3c1")]
  internal class AddABPSelections : UnitFactComponentDelegate
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

    private void Apply(UnitDescriptor unit)
    {
      try
      {
        if (unit.Progression.CharacterLevel < 6)
          return;

        Logger.Verbose(() => $"Applying ABP Selections to {unit.CharacterName} {unit.Progression.CharacterLevel}");
        ApplyPhysicalProwess(unit);
        ApplyMentalProwess(unit);
      }
      catch (Exception e)
      {
        Logger.LogException("AddABPSelections.Apply", e);
      }
    }

    private void ApplyMythic(UnitDescriptor unit)
    {
      try
      {
        // We only need to care about no mythic or all mythic--the DLC will start you at 20 but otherwise characters
        // won't be pre-configured w/ mythic
        if (unit.Progression.MythicLevel == 0)
          return;

        Logger.Verbose(() => $"Applying Mythic ABP Selections to {unit.CharacterName} {unit.Progression.MythicLevel}");
        ApplyLegendaryGifts(unit);
      }
      catch (Exception e)
      {
        Logger.LogException("AddABPSelections.ApplyMythic", e);
      }
    }

    private void ApplyPhysicalProwess(UnitDescriptor unit)
    {
      var count = GetPhysicalProwessCount(unit.Progression.CharacterLevel);
      for (int i = 0; i < count && i < PhysicalProwess.Count; i++)
      {
        var prowess = GetProwess(PhysicalProwess[i]);
        Logger.Verbose(() => $"Applying {prowess.Name} to {unit.CharacterName}");
        unit.AddFact(prowess);
      }
    }

    private void ApplyMentalProwess(UnitDescriptor unit)
    {
      var count = GetMentalProwessCount(unit.Progression.CharacterLevel);
      for (int i = 0; i < count && i < MentalProwess.Count; i++)
      {
        var prowess = GetProwess(MentalProwess[i]);
        Logger.Verbose(() => $"Applying {prowess.Name} to {unit.CharacterName}");
        unit.AddFact(prowess);
      }
    }

    private void ApplyLegendaryGifts(UnitDescriptor unit)
    {
      foreach (var gift in LegendaryGifts.Select(bp => bp.Get()))
      {
        Logger.Verbose(() => $"Applying {gift.Name} to {unit.CharacterName}");
        unit.AddFact(gift);
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

    [HarmonyPatch(typeof(AddClassLevels))]
    static class AddClassLevels_Patch
    {
      [HarmonyPatch(
          nameof(AddClassLevels.LevelUp),
          typeof(AddClassLevels),
          typeof(UnitDescriptor),
          typeof(int),
          typeof(UnitFact)),
        HarmonyPostfix]
      static void LevelUp(AddClassLevels c, UnitDescriptor unit)
      {
        try
        {
          var abpSelection = c.Fact.GetComponent<AddABPSelections>();
          if (abpSelection is null)
            return;

          if (c.CharacterClass.IsMythic)
            abpSelection.ApplyMythic(unit);
          else
            abpSelection.Apply(unit);
        }
        catch (Exception e)
        {
          Logger.LogException("AddClassLevels_Patch.LevelUp", e);
        }
      }
    }
  }
}
