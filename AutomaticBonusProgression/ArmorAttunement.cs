using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace AutomaticBonusProgression
{
  /// <summary>
  /// 
  /// </summary>
  internal class ArmorAttunement
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ArmorAttunement));

    private const string ArmorSelection = "ArmorSelection";
    private const string ArmorSelectionDisplayName = "ArmorSelection.Name";
    private const string ArmorSelectionDescription = "ArmorSelection.Description";

    internal static BlueprintFeatureSelection Configure()
    {
      Logger.Log("Configuring armor selection");

      return FeatureSelectionConfigurator.New(ArmorSelection, Guids.ArmorSelection)
        .SetIsClassFeature()
        .SetDisplayName(ArmorSelectionDisplayName)
        .SetDescription(ArmorSelectionDescription)
        //.SetIcon()
        .SetAllFeatures(ConfigureArmor())
        .Configure();
    }

    private const string ArmorName = "ArmorAttunement";
    private const string ArmorDisplayName = "ArmorAttunement.Name";
    private const string ArmorDescription = "ArmorAttunement.Description";

    internal static BlueprintFeature ConfigureArmor()
    {
      Logger.Log($"Configuring Armor Attunement");

      return FeatureConfigurator.New(ArmorName, Guids.ArmorAttunement)
        .SetIsClassFeature()
        .SetDisplayName(ArmorName)
        .SetDescription(ArmorDescription)
        //.SetIcon()
        .SetRanks(5)
        .Configure();
    }

    private const string ShieldName = "ShieldAttunement";
    private const string ShieldDisplayName = "ShieldAttunement.Name";
    private const string ShieldDescription = "ShieldAttunement.Description";

    internal static void ConfigureShield()
    {
      Logger.Log($"Configuring Shield Attunement");
    }
  }
}
