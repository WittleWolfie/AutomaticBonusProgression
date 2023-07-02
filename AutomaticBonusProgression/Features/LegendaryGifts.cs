using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryGifts
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGifts));

    internal static void Configure()
    {
      Logger.Log("Configuring Legendary Gifts");

      LegendaryAbility.Configure();
      LegendaryShieldmaster.Configure();
      LegendaryTwinWeapons.Configure();
      LegendaryArmor.Configure();
      LegendaryArmor.ConfigureShield();
      LegendaryWeapon.Configure();
      LegendaryWeapon.ConfigureOffHand();

      ConfigurePhysicalProwess();
      ConfigureMentalProwess();
    }

    private const string PhysicalProwess = "LegendaryGift.Prowess.Physical";
    private static void ConfigurePhysicalProwess()
    {
      FeatureConfigurator.New(PhysicalProwess, Guids.LegendaryPhysicalProwess)
        .SetIsClassFeature()
        .SetHideInUI()
        .SetRanks(3)
        .Configure();
    }

    private const string MentalProwess = "LegendaryGift.Prowess.Mental";
    private static void ConfigureMentalProwess()
    {
      FeatureConfigurator.New(MentalProwess, Guids.LegendaryMentalProwess)
        .SetIsClassFeature()
        .SetHideInUI()
        .SetRanks(3)
        .Configure();
    }
  }
}
