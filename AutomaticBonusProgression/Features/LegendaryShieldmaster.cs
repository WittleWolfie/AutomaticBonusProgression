using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryShieldmaster
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryShieldmaster));

    private const string LegendaryShieldmasterName = "LegendaryShieldmaster";
    private const string LegendaryShieldmasterDisplayName = "LegendaryShieldmaster.Name";
    private const string LegendaryShieldmasterDescription = "LegendaryShieldmaster.Description";

    // TODO What if instead this just treated the shield case as having just one so you could get at any level? Same for Twin Weapons.
    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Legendary Shieldmaster");

      return FeatureConfigurator.New(LegendaryShieldmasterName, Guids.LegendaryShieldmaster)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryShieldmasterDisplayName)
        .SetDescription(LegendaryShieldmasterDescription)
        //.SetIcon()
        .AddPrerequisiteCharacterLevel(level: 17)
        .Configure();
    }
  }
}
