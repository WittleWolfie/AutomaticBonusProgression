using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Progression.ChupaChupses;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Progression.Feats;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Progression.Main;
using System;
using System.Collections.Generic;

namespace AutomaticBonusProgression.UI.Leveling
{
  internal class UIPatches
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(UIPatches));

    [HarmonyPatch(typeof(FeatProgressionVM))]
    static class FeatProgressionVM_Patch
    {
      [HarmonyAfter("RespecModBarley")]
      [HarmonyPatch(nameof(FeatProgressionVM.BuildFeats)), HarmonyPostfix]
      static void BuildFeats(FeatProgressionVM __instance)
      {
        try
        {
          // TODO: Mental / Physical Prowess, Legendary Gifts
          // Tracks entries to remove from the additional chupa chupa
          List<ProgressionVM.FeatureEntry> entriesToRemove = new();

          // Each of these represents one line on the progression
          List<FeatureProgressionChupaChupsVM> armorAttunement = new();
          List<FeatureProgressionChupaChupsVM> weaponAttunement = new();
          List<FeatureProgressionChupaChupsVM> resistance = new();
          List<FeatureProgressionChupaChupsVM> deflection = new();
          List<FeatureProgressionChupaChupsVM> toughening = new();
          foreach (var levelEntry in __instance.BlueprintProgression.LevelEntries)
          {
            foreach (var feature in levelEntry.Features)
            {
              var featureEntry =
                new ProgressionVM.FeatureEntry()
                {
                  Feature = feature,
                  Level = levelEntry.Level
                };
              if (featureEntry.Feature == Common.ArmorAttunementBase)
                armorAttunement.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.ShieldAttunementBase)
              {
                // Since every toher level corresponds to armor attunement, only show level 8
                if (featureEntry.Level == 8)
                  armorAttunement.Add(__instance.GetChupaChups(featureEntry));
                else
                  entriesToRemove.Add(featureEntry);
              }
              else if (featureEntry.Feature == Common.WeaponAttunementBase)
                weaponAttunement.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.OffHandAttunementBase)
              {
                // Since every toher level corresponds to weapon attunement, only show level 8
                if (featureEntry.Level == 8)
                  weaponAttunement.Add(__instance.GetChupaChups(featureEntry));
                else
                  entriesToRemove.Add(featureEntry);
              }
              else if (featureEntry.Feature == Common.ResistanceBase)
                resistance.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.DeflectionBase)
                deflection.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.TougheningBase)
                toughening.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.LegendaryGifts)
                entriesToRemove.Add(featureEntry); // TODO: Fix this when we're done testing gifts
              else
                continue; // Not an ABP feature
              // TODO: Fix it showing legendary attributes?? Don't even know why
              entriesToRemove.Add(featureEntry);
            }
          }
          // Remove everything we're managing from the extra chupa chups
          __instance.AdditionalChupaChupsList.RemoveAll(
            vm => entriesToRemove.Exists(entry => vm.Feature.Feature == entry.Feature));

          __instance.MainChupaChupsLines.Add(armorAttunement);
          __instance.MainChupaChupsLines.Add(weaponAttunement);
          __instance.MainChupaChupsLines.Add(resistance);
          __instance.MainChupaChupsLines.Add(deflection);
          __instance.MainChupaChupsLines.Add(toughening);
        }
        catch (Exception e)
        {
          Logger.LogException("FeatProgressionVM_Patch.BuildFeats", e);
        }
      }
    }
  }
}
