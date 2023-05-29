using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryProwess
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryProwess));

    private const string LegendaryProwessName = "LegendaryProwess";
    private const string LegendaryProwessDisplayName = "LegendaryProwess.Name";
    private const string LegendaryProwessDescription = "LegendaryProwess.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log("Configuring Legendary Prowess");

      return FeatureSelectionConfigurator.New(LegendaryProwessName, Guids.LegendaryProwess)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryProwessDisplayName)
        .SetDescription(LegendaryProwessDescription)
        //.SetIcon()
        .AddPrerequisiteCharacterLevel(18)
        .AddToAllFeatures(
          Guids.StrProwess,
          Guids.DexProwess,
          Guids.ConProwess,
          Guids.IntProwess,
          Guids.WisProwess,
          Guids.ChaProwess)
        .Configure();
    }
  }
}
