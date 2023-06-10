using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Progression.ChupaChupses;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Progression.Feats;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Progression.Main;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

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
          // Remove everything we're managing
          __instance.MainChupaChupsLines.FirstOrDefault().RemoveAll(IsABPFeature);
          __instance.AdditionalChupaChupsList.RemoveAll(IsABPFeature);

          // Each of these represents one line on the progression
          List<FeatureProgressionChupaChupsVM> armorAttunement = new();
          List<FeatureProgressionChupaChupsVM> weaponAttunement = new();
          List<FeatureProgressionChupaChupsVM> resistance = new();
          List<FeatureProgressionChupaChupsVM> deflection = new();
          List<FeatureProgressionChupaChupsVM> toughening = new();
          List<FeatureProgressionChupaChupsVM> physicalProwess = new();
          List<FeatureProgressionChupaChupsVM> mentalProwess = new();

          var mainLine = __instance.MainChupaChupsLines.FirstOrDefault();
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
                // Since every other level corresponds to armor attunement, only show level 8
                if (featureEntry.Level == 8)
                  armorAttunement.Add(__instance.GetChupaChups(featureEntry));
              }
              else if (featureEntry.Feature == Common.WeaponAttunementBase)
                weaponAttunement.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.OffHandAttunementBase)
              {
                // Since every other level corresponds to weapon attunement, only show level 8
                if (featureEntry.Level == 8)
                  weaponAttunement.Add(__instance.GetChupaChups(featureEntry));
              }
              else if (featureEntry.Feature == Common.ResistanceBase)
                resistance.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.DeflectionBase)
                deflection.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.TougheningBase)
                toughening.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.PhysicalProwess)
                physicalProwess.Add(__instance.GetChupaChups(featureEntry));
              else if (featureEntry.Feature == Common.MentalProwess)
                mentalProwess.Add(__instance.GetChupaChups(featureEntry));
            }
          }

          __instance.MainChupaChupsLines.Add(armorAttunement);
          __instance.MainChupaChupsLines.Add(weaponAttunement);
          __instance.MainChupaChupsLines.Add(resistance);
          __instance.MainChupaChupsLines.Add(deflection);
          __instance.MainChupaChupsLines.Add(toughening);
          __instance.MainChupaChupsLines.Add(physicalProwess);
          __instance.MainChupaChupsLines.Add(mentalProwess);
        }
        catch (Exception e)
        {
          Logger.LogException("FeatProgressionVM_Patch.BuildFeats", e);
        }
      }

      private static bool IsABPFeature(FeatureProgressionChupaChupsVM vm)
      {
        var guid = vm.Feature.Feature.AssetGuid;
        var sourceGuid = vm.Feature?.Source?.AssetGuid;
        if (ABPFeatures.Contains(guid) || (sourceGuid is not null && ABPFeatures.Contains(sourceGuid.Value)))
          return true;
        return false;
      }

      private static readonly List<BlueprintGuid> ABPFeatures =
        new()
        {
          new(new Guid(Guids.ArmorAttunementBase)),
          new(new Guid(Guids.ShieldAttunementBase)),
          new(new Guid(Guids.WeaponAttunementBase)),
          new(new Guid(Guids.OffHandAttunementBase)),
          new(new Guid(Guids.ResistanceBase)),
          new(new Guid(Guids.TougheningBase)),
          new(new Guid(Guids.DeflectionBase)),
          new(new Guid(Guids.PhysicalProwess)),
          new(new Guid(Guids.MentalProwess)),
        };
    }
  }
}
