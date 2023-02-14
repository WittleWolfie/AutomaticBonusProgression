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
    // I am skeptical ^. On counting there are only like 6 that are level gated to 17, 1 to 11, 1 to 12, 1 to 15 or so
    // etc.
    // What I should do for Legendary Body / Mind:
    // - you can upgrade your Primary or Secondary from 4 > 6
    // - you can upgrade your tertiary from 2 > 4 or 4 > 6 <-- Actually this causes a problem. Would need to prevent
    // this.

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
