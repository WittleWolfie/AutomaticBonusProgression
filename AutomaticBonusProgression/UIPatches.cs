using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Progression.ChupaChupses;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Progression.Feats;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Progression.Main;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomaticBonusProgression
{
  internal class UIPatches
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(UIPatches));

    private static BlueprintFeatureSelection _armorAttunement;
    private static BlueprintFeatureSelection ArmorAttunement
    {
      get
      {
        _armorAttunement ??= BlueprintTool.Get<BlueprintFeatureSelection>(Guids.ArmorSelection);
        return _armorAttunement;
      }
    }

    [HarmonyPatch(typeof(FeatProgressionVM))]
    static class FeatProgressionVM_Patch
    {
      [HarmonyAfter("RespecModBarley")]
      [HarmonyPatch(nameof(FeatProgressionVM.BuildFeats)), HarmonyPostfix]
      static void BuildFeats(FeatProgressionVM __instance)
      {
        try
        {
          List<ProgressionVM.FeatureEntry> features = new();
          List<FeatureProgressionChupaChupsVM> line = new();
          foreach (var level in __instance.BlueprintProgression.LevelEntries)
          {
            foreach (var feature in level.Features)
            {
              if (!IsABPFeature(feature))
                continue;

              ProgressionVM.FeatureEntry featureEntry =
                new()
                {
                  Feature = feature,
                  Level = level.Level,
                  Index = features.Count(f => f.Feature == feature && f.Level == level.Level)
                };
              features.Add(featureEntry);
              line.Add(__instance.GetChupaChups(featureEntry));
            }
          }
          __instance.MainChupaChupsLines.Add(line);
        }
        catch (Exception e)
        {
          Logger.LogException("FeatProgressionVM_Patch.BuildFeats", e);
        }
      }

      [HarmonyPatch(nameof(FeatProgressionVM.ShouldBeDisplayed)), HarmonyPostfix]
      static void ShouldBeDisplayed(BlueprintFeatureBase feature, ref bool __result)
      {
        try
        {
          __result = __result && !IsABPFeature(feature);
        }
        catch (Exception e)
        {
          Logger.LogException("FeatProgressionVM_Patch.ShouldBeDisplayed", e);
        }
      }

      private static bool IsABPFeature(BlueprintFeatureBase feature)
      {
        if (feature == ArmorAttunement)
          return true;
        return false;
      }
    }
  }
}
