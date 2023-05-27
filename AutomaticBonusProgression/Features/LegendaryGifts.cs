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

    internal static BlueprintFeature Configure()
    {
      Logger.Log("Configuring Legendary Gifts");

      return FeatureSelectionConfigurator.New(LegendaryGiftsName, Guids.LegendaryGifts)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryGiftsDisplayName)
        .SetDescription(LegendaryGiftsDescription)
        .AddHideFeatureInInspect()
        //.SetIcon()
        .AddToAllFeatures(
          LegendaryAbility.Configure(),
          LegendaryShieldmaster.Configure(),
          LegendaryTwinWeapons.Configure(),
          LegendaryProwess.Configure(),
          LegendaryArmor.Configure(),
          LegendaryArmor.ConfigureShield(),
          LegendaryWeapon.Configure(),
          LegendaryWeapon.ConfigureOffHand())
        .Configure();
    }
  }
}
