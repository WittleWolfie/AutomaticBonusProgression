using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class WeaponAttunement
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(WeaponAttunement));

    private const string WeaponName = "WeaponAttunement";
    private const string WeaponBaseName = "WeaponAttunement.Base";
    private const string WeaponDisplayName = "WeaponAttunement.Name";
    private const string WeaponDescription = "WeaponAttunement.Description";

    internal static BlueprintFeature ConfigureWeapon()
    {
      Logger.Log($"Configuring Weapon Attunement");

      var effect = FeatureConfigurator.New(WeaponName, Guids.WeaponAttunement)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .Configure();
      return FeatureConfigurator.New(WeaponBaseName, Guids.WeaponAttunementBase)
        .SetIsClassFeature()
        .SetDisplayName(WeaponDisplayName)
        .SetDescription(WeaponDescription)
        .SetIcon(AbilityRefs.HolySword.Reference.Get().Icon)
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string OffHandName = "OffHandAttunement";
    private const string OffHandBaseName = "OffHandAttunement.Base";
    private const string OffHandDisplayName = "OffHandAttunement.Name";
    private const string OffHandDescription = "OffHandAttunement.Description";

    internal static BlueprintFeature ConfigureOffHand()
    {
      Logger.Log($"Configuring OffHand Attunement");

      var effect = FeatureConfigurator.New(OffHandName, Guids.OffHandAttunement)
        .SetIsClassFeature()
        .SetRanks(4)
        .SetHideInUI()
        .AddHideFeatureInInspect()
        .Configure();
      return FeatureConfigurator.New(OffHandBaseName, Guids.OffHandAttunementBase)
        .SetIsClassFeature()
        .SetDisplayName(OffHandDisplayName)
        .SetDescription(OffHandDescription)
        .SetIcon(BuffRefs.ArcaneAccuracyBuff.Reference.Get().Icon)
        .SetRanks(4)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }
  }
}
