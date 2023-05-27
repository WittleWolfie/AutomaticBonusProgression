using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Enums;
using System;

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
        .AddComponent<ResistanceComponent>()
        .AddHideFeatureInInspect()
        .Configure();
    }

    [TypeId("c7c76424-7278-4f32-b679-ed1d6fcffbb4")]
    private class ResistanceComponent : BuffAllSavesBonus
    {
      public ResistanceComponent()
      {
        Value = 1;
        Descriptor = ModifierDescriptor.Resistance;
      }

      public override void OnTurnOn()
      {
        try
        {
          if (!Common.IsAffectedByABP(Owner))
          {
            Logger.Verbose(() => $"Skipping resistance for unaffected unit: {Owner?.CharacterName}");
            return;
          }
          base.OnTurnOn();
        }
        catch (Exception e)
        {
          Logger.LogException("ResistanceComponent.OnTurnOn", e);
        }
      }
    }
  }
}
