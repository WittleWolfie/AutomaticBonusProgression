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
    private const string DeflectionDisplayName = "Deflection.Name";
    private const string DeflectionDescription = "Deflection.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Deflection");

      return FeatureConfigurator.New(DeflectionName, Guids.Deflection)
        .SetIsClassFeature()
        .SetDisplayName(DeflectionDisplayName)
        .SetDescription(DeflectionDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddStatBonus(stat: StatType.AC, value: 1, descriptor: ModifierDescriptor.Deflection)
        .Configure();
    }
  }
}
