using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryShieldmaster
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryShieldmaster));

    private const string LegendaryShieldmasterName = "LegendaryShieldmaster";
    private const string LegendaryShieldmasterDisplayName = "LegendaryShieldmaster.Name";
    private const string LegendaryShieldmasterDescription = "LegendaryShieldmaster.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Legendary Shieldmaster");

      return FeatureConfigurator.New(LegendaryShieldmasterName, Guids.LegendaryShieldmaster)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryShieldmasterDisplayName)
        .SetDescription(LegendaryShieldmasterDescription)
        .SetIcon(BuffRefs.ShieldOfDawnBuff.Reference.Get().Icon)
        .Configure();
    }
  }
}
