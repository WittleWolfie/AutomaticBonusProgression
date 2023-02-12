using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class WeaponAttunement
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(WeaponAttunement));

    private const string WeaponName = "WeaponAttunement";
    private const string WeaponDisplayName = "WeaponAttunement.Name";
    private const string WeaponDescription = "WeaponAttunement.Description";

    internal static BlueprintFeature ConfigureWeapon()
    {
      Logger.Log($"Configuring Weapon Attunement");

      return FeatureConfigurator.New(WeaponName, Guids.WeaponAttunement)
        .SetIsClassFeature()
        .SetDisplayName(WeaponDisplayName)
        .SetDescription(WeaponDescription)
        //.SetIcon()
        .SetRanks(5)
        .Configure();
    }

    private const string OffHandName = "OffHandAttunement";
    private const string OffHandDisplayName = "OffHandAttunement.Name";
    private const string OffHandDescription = "OffHandAttunement.Description";

    internal static BlueprintFeature ConfigureOffHand()
    {
      Logger.Log($"Configuring OffHand Attunement");

      return FeatureConfigurator.New(OffHandName, Guids.OffHandAttunement)
        .SetIsClassFeature()
        .SetDisplayName(OffHandDisplayName)
        .SetDescription(OffHandDescription)
        //.SetIcon()
        .SetRanks(4)
        .Configure();
    }
  }
}
