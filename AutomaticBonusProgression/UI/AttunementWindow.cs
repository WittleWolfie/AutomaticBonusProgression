using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using Kingmaker.Localization;
using ModMenu.Window;

namespace AutomaticBonusProgression.UI
{
  internal class AttunementWindow
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AttunementWindow));

    internal static void Configure()
    {
      Logger.Log($"Configuring AttunementWindow");

      ModMenu.ModMenu.AddWindow(
        WindowBuilder.New(GetKey("Attunement"), title: GetString("Attunement")));
    }

    private static string GetKey(string key)
    {
      return $"ABP.{key}";
    }

    private static LocalizedString GetString(string key)
    {
      return LocalizationTool.GetString($"ABP.UI.{key}");
    }
  }
}
