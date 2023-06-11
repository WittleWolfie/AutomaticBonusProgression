using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryTwinWeapons
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryTwinWeapons));

    private const string LegendaryTwinWeaponsName = "LegendaryTwinWeapons";
    private const string LegendaryTwinWeaponsDisplayName = "LegendaryTwinWeapons.Name";
    private const string LegendaryTwinWeaponsDescription = "LegendaryTwinWeapons.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Legendary Twin Weapons");

      return FeatureConfigurator.New(LegendaryTwinWeaponsName, Guids.LegendaryTwinWeapons)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryTwinWeaponsDisplayName)
        .SetDescription(LegendaryTwinWeaponsDescription)
        .SetIcon(BuffRefs.DivineFavorBuff.Reference.Get().Icon)
        .AddPrerequisiteCharacterLevel(9)
        .Configure();
    }
  }
}
