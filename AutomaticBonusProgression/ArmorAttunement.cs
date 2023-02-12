using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using System;

namespace AutomaticBonusProgression
{
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
        .SetDisplayName(ArmorDisplayName)
        .SetDescription(ArmorDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent<RecalculateArmor>()
        .Configure();
    }

    private const string ShieldName = "ShieldAttunement";
    private const string ShieldDisplayName = "ShieldAttunement.Name";
    private const string ShieldDescription = "ShieldAttunement.Description";

    internal static void ConfigureShield()
    {
      Logger.Log($"Configuring Shield Attunement");
    }

    [TypeId("4c92c283-1d5c-43af-9277-f69332f419ae")]
    private class RecalculateArmor : UnitFactComponentDelegate
    {
      public override void OnTurnOn()
      {
        try
        {
          Owner.Body.Armor.MaybeArmor?.RecalculateStats();
        } catch (Exception e)
        {
          Logger.LogException("RecalculateArmor.OnTurnOn", e);
        }
      }

      public override void OnTurnOff()
      {
        try
        {
          Owner.Body.Armor.MaybeArmor?.RecalculateStats();
        }
        catch (Exception e)
        {
          Logger.LogException("RecalculateArmor.OnTurnOff", e);
        }
      }
    }
  }
}
