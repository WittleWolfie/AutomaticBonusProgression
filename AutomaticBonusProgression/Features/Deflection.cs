using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Features
{
  internal class Deflection
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Deflection));

    private const string DeflectionName = "Deflection";
    private const string DeflectionBaseName = "Deflection.Base";
    private const string DeflectionDisplayName = "Deflection.Name";
    private const string DeflectionDescription = "Deflection.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Deflection");

      var effect = FeatureConfigurator.New(DeflectionName, Guids.Deflection)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .AddStatBonus(stat: StatType.AC, value: 1, descriptor: ModifierDescriptor.Deflection)
        .Configure();
      return FeatureConfigurator.New(DeflectionBaseName, Guids.DeflectionBase)
        .SetIsClassFeature()
        .SetDisplayName(DeflectionDisplayName)
        .SetDescription(DeflectionDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }
  }
}
