using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Features
{
  internal class Resistance
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Resistance));

    private const string ResistanceName = "Resistance";
    private const string ResistanceDisplayName = "Resistance.Name";
    private const string ResistanceDescription = "Resistance.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Resistance");

      return FeatureConfigurator.New(ResistanceName, Guids.Resistance)
        .SetIsClassFeature()
        .SetDisplayName(ResistanceDisplayName)
        .SetDescription(ResistanceDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddBuffAllSavesBonus(value: 1, descriptor: ModifierDescriptor.Resistance)
        .Configure();
    }
  }
}
