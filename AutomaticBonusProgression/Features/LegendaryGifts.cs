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

      // TODO: KILL THE SELECTION WE DO NOT NEED YOUR SHIIIIT
      return FeatureSelectionConfigurator.New(LegendaryGiftsName, Guids.LegendaryGifts)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryGiftsDisplayName)
        .SetDescription(LegendaryGiftsDescription)
        .AddHideFeatureInInspect()
        //.SetIcon()
        .AddToAllFeatures(
          // Since the first selection is the only one used when applying to spawned units, only this needs to deal
          // with AddFeatureABP nonsense. The exception is if something had so many legendary gifts granted that they
          // maxed out Legendary Ability (> 30, not expected). Everything else can ignore it since it will never be
          // selected automatically.
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
