using AutomaticBonusProgression.Enchantments;
using AutomaticBonusProgression.Features;
using AutomaticBonusProgression.Mechanics;
using AutomaticBonusProgression.UI;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Root;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;
using UnityModManagerNet;

namespace AutomaticBonusProgression
{
  public static class Main
  {
    public static bool Enabled;
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Main));

    public static bool Load(UnityModManager.ModEntry modEntry)
    {
      try
      {
        modEntry.OnToggle = OnToggle;
        var harmony = new Harmony(modEntry.Info.Id);
        harmony.PatchAll();
        Logger.Log("Finished patching.");
      }
      catch (Exception e)
      {
        Logger.LogException("Failed to patch", e);
      }
      return true;
    }

    public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
    {
      Enabled = value;
      return true;
    }

    [HarmonyPatch(typeof(BlueprintsCache))]
    static class BlueprintsCaches_Patch
    {
      private static bool Initialized = false;

      [HarmonyPriority(Priority.First)]
      [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
      static void Init()
      {
        try
        {
          if (Initialized)
          {
            Logger.Warning("Already configured blueprints.");
            return;
          }
          Initialized = true;

          // First strings
          LocalizationTool.LoadEmbeddedLocalizationPacks(
            "AutomaticBonusProgression.Strings.Abilities.json",
            "AutomaticBonusProgression.Strings.Attunement.json",
            "AutomaticBonusProgression.Strings.Items.json",
            "AutomaticBonusProgression.Strings.LegendaryArmor.json",
            "AutomaticBonusProgression.Strings.LegendaryGifts.json",
            "AutomaticBonusProgression.Strings.LegendaryWeapon.json",
            "AutomaticBonusProgression.Strings.Settings.json",
            "AutomaticBonusProgression.Strings.UI.json");

          Settings.Init();

          Enchantments.Enchantments.Configure();
          AttunementProgression.Configure();
          LegendaryGifts.Configure();
          CompanionSelections.Configure();
          ItemChanges.Configure();
          TricksterArcana.Configure();
        }
        catch (Exception e)
        {
          Logger.LogException("Failed to configure blueprints.", e);
        }
      }
    }

    [HarmonyPatch(typeof(StartGameLoader))]
    static class StartGameLoader_Patch
    {
      private static bool Initialized = false;

      [HarmonyPatch(nameof(StartGameLoader.LoadPackTOC)), HarmonyPostfix]
      static void LoadPackTOC()
      {
        try
        {
          if (Initialized)
          {
            Logger.Warning("Already configured delayed blueprints.");
            return;
          }
          Initialized = true;

          RootConfigurator.ConfigureDelayedBlueprints();
        }
        catch (Exception e)
        {
          Logger.LogException("Failed to configure delayed blueprints.", e);
        }
      }
    }
  }
}

