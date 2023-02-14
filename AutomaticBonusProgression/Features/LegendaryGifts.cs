using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryGifts
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGifts));

    private const string LegendaryGiftsName = "LegendaryGifts";
    private const string LegendaryGiftsDisplayName = "LegendaryGifts.Name";
    private const string LegendaryGiftsDescription = "LegendaryGifts.Description";

    // TODO: What about a delay gift to cover the case where they don't want to spend their gift on inherent bonuses?
    // This could be important for mythic <-> character level mismatches

    internal static BlueprintFeature Configure()
    {
      Logger.Log("Configuring Legendary Gifts");

      return FeatureSelectionConfigurator.New(LegendaryGiftsName, Guids.LegendaryGifts)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryGiftsDisplayName)
        .SetDescription(LegendaryGiftsDescription)
        //.SetIcon()
        .AddToAllFeatures(
          LegendaryAbility.Configure(),
          LegendaryShieldmaster.Configure(),
          LegendaryTwinWeapons.Configure())
        .Configure();
    }
  }
}
