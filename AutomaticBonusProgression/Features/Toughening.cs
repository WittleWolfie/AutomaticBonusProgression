using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Features
{
  internal class Toughening
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Toughening));

    private const string TougheningName = "Toughening";
    private const string TougheningDisplayName = "Toughening.Name";
    private const string TougheningDescription = "Toughening.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Toughening");

      return FeatureConfigurator.New(TougheningName, Guids.Toughening)
        .SetIsClassFeature()
        .SetDisplayName(TougheningDisplayName)
        .SetDescription(TougheningDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddStatBonus(stat: StatType.AC, value: 1, descriptor: ModifierDescriptor.NaturalArmorEnhancement)
        .Configure();
    }
  }
}
