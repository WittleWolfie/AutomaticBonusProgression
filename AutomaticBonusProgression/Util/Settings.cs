using AutomaticBonusProgression.Mechanics;
using AutomaticBonusProgression.UI.Leveling.Legendary;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Localization;
using ModMenu.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityModManagerNet;
using Menu = ModMenu.ModMenu;

namespace AutomaticBonusProgression.Util
{
  internal static class Settings
  {
    private static readonly string RootKey = "abp.settings";
    private static readonly string RootStringKey = "ABP.Settings";
    private const string VerboseLoggingKey = "enable-verbose-logs";
    private const string SellValueKey = "sell-value";
    private const string LegendaryGiftsKey = "enable-legendary-gifts";

    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Settings));

    internal static bool IsEnabled(string key)
    {
      return Menu.GetSettingValue<bool>(GetKey(key));
    }

    internal static float GetSellValue()
    {
      return Menu.GetSettingValue<float>(GetKey(SellValueKey));
    }

    internal static void Init()
    {
      Logger.Log("Initializing settings.");
      var settings =
        SettingsBuilder.New(RootKey, GetString("Title"))
          .AddToggle(
            Toggle.New(GetKey(VerboseLoggingKey), defaultValue: false, GetString("VerboseLogging"))
              .WithLongDescription(GetString("VerboseLogging.Description"))
              .OnValueChanged(Logging.EnableVerboseLogging))
          .AddDefaultButton(OnDefaultsApplied);

      settings.AddSliderFloat(
        SliderFloat.New(GetKey(SellValueKey), 0.3f, GetString("SellValue"), 0f, 1.0f)
          .WithLongDescription(GetString("SellValue.Description"))
          .WithDecimalPlaces(2)
          .WithStep(0.05f)
          .OnValueChanged(OnSellValueChanged));

      settings.AddToggle(
            Toggle.New(GetKey(LegendaryGiftsKey), defaultValue: true, GetString("LegendaryGifts"))
              .WithLongDescription(GetString("LegendaryGifts.Description"))
              .OnValueChanged(LegendaryGiftsPhaseView.EnableLegendaryGifts));

      Menu.AddSettings(settings);
      Logging.EnableVerboseLogging(IsEnabled(VerboseLoggingKey));
    }

    private static void OnDefaultsApplied()
    {
      Logger.Log($"Default settings restored.");
    }

    private static void OnSellValueChanged(float modifier)
    {
      ItemChanges.AdjustSellModifier(modifier);
    }

    private static LocalizedString GetString(string key, bool usePrefix = true)
    {
      var fullKey = usePrefix ? $"{RootStringKey}.{key}" : key;
      return LocalizationTool.GetString(fullKey);
    }

    private static string GetKey(string partialKey)
    {
      return $"{RootKey}.{partialKey}";
    }
  }
}