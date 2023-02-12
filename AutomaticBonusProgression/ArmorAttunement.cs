using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using System;

namespace AutomaticBonusProgression
{
  internal class ArmorAttunement
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AutomaticBonusProgression.ArmorAttunement));

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
        .AddComponent<ArmorAttunementComponent>()
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
    private class ArmorAttunementComponent : UnitFactComponentDelegate, IUnitActiveEquipmentSetHandler
    {
      public override void OnTurnOn()
      {
        try
        {
          Owner.Body.Armor.MaybeArmor?.RecalculateStats();
          UpdateUnarmoredBonus();
        } catch (Exception e)
        {
          Logger.LogException("ArmorAttunementComponent.OnTurnOn", e);
        }
      }

      public override void OnTurnOff()
      {
        try
        {
          Owner.Body.Armor.MaybeArmor?.RecalculateStats();
          Owner.Stats.AC.RemoveModifiersFrom(Runtime);
        }
        catch (Exception e)
        {
          Logger.LogException("ArmorAttunementComponent.OnTurnOff", e);
        }
      }

      public void HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit)
      {
        try
        {
          UpdateUnarmoredBonus();
        }
        catch (Exception e)
        {
          Logger.LogException("ArmorAttunementComponent.HandleUnitChangeActiveEquipmentSet", e);
        }
      }

      private void UpdateUnarmoredBonus()
      {
        if (Owner.Body.SecondaryHand.HasShield || Owner.Body.Armor.HasArmor)
        {
          Owner.Stats.AC.RemoveModifiersFrom(Runtime);
          return;
        }

        Logger.Verbose(() => $"Granting {Owner} attunement while unarmored.");
        Owner.Stats.AC.AddModifier(Fact.GetRank(), source: Runtime, desc: ModifierDescriptor.ArmorEnhancement);
      }
    }
  }
}
