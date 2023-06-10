using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Features
{
  internal class Resistance
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Resistance));

    private const string ResistanceName = "Resistance";
    private const string ResistanceBaseName = "Resistance.Base";
    private const string ResistanceDisplayName = "Resistance.Name";
    private const string ResistanceDescription = "Resistance.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Resistance");

      var effect = FeatureConfigurator.New(ResistanceName, Guids.Resistance)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .AddBuffAllSavesBonus(value: 1, descriptor: ModifierDescriptor.Resistance)
        .Configure();
      return FeatureConfigurator.New(ResistanceBaseName, Guids.ResistanceBase)
        .SetIsClassFeature()
        .SetDisplayName(ResistanceDisplayName)
        .SetDescription(ResistanceDescription)
        .SetIcon(BuffRefs.ResistanceBuff.Reference.Get().Icon)
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }
  }
}
