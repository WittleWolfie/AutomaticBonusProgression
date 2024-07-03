using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using System;

namespace AutomaticBonusProgression.Features
{
  internal class MentalProwess
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(MentalProwess));

    private const string ProwessName = "MentalProwess";
    private const string ProwessDisplayName = "MentalProwess.Name";
    private const string ProwessDescription = "MentalProwess.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring MentalProwess");

      ConfigureIntProwess();
      ConfigureWisProwess();
      ConfigureChaProwess();

      // Feature is for the UI only
      return FeatureConfigurator.New(ProwessName, Guids.MentalProwess)
        .SetIsClassFeature()
        .SetDisplayName(ProwessDisplayName)
        .SetDescription(ProwessDescription)
        .SetHideInCharacterSheetAndLevelUp()
        .SetIcon(BuffRefs.ThoughtsenseBuff.Reference.Get().Icon)
        .Configure();
    }

    private const string IntProwess = "MentalProwess.Int";
    private const string IntProwessDisplayName = "MentalProwess.Int.Name";
    private const string IntProwessDescription = "MentalProwess.Int.Description";

    private static void ConfigureIntProwess()
    {
      FeatureConfigurator.New(IntProwess, Guids.IntProwess)
        .SetIsClassFeature()
        .SetDisplayName(IntProwessDisplayName)
        .SetDescription(IntProwessDescription)
        .SetIcon(BuffRefs.FoxsCunningBuff.Reference.Get().Icon)
        .SetRanks(3)
        .AddComponent(new AddStatBonusABP(StatType.Intelligence, 2))
        .Configure();
    }

    private const string WisProwessName = "MentalProwess.Wis";
    private const string WisProwessDisplayName = "MentalProwess.Wis.Name";
    private const string WisProwessDescription = "MentalProwess.Wis.Description";

    private static void ConfigureWisProwess()
    {
      FeatureConfigurator.New(WisProwessName, Guids.WisProwess)
        .SetIsClassFeature()
        .SetDisplayName(WisProwessDisplayName)
        .SetDescription(WisProwessDescription)
        .SetIcon(BuffRefs.OwlsWisdomBuff.Reference.Get().Icon)
        .SetRanks(3)
        .AddComponent(new AddStatBonusABP(StatType.Wisdom, 2))
        .Configure();
    }

    private const string ChaProwessName = "MentalProwess.Cha";
    private const string ChaProwessDisplayName = "MentalProwess.Cha.Name";
    private const string ChaProwessDescription = "MentalProwess.Cha.Description";

    private static void ConfigureChaProwess()
    {
      FeatureConfigurator.New(ChaProwessName, Guids.ChaProwess)
        .SetIsClassFeature()
        .SetDisplayName(ChaProwessDisplayName)
        .SetDescription(ChaProwessDescription)
        .SetIcon(BuffRefs.EaglesSplendorBuff.Reference.Get().Icon)
        .SetRanks(3)
        .AddComponent(new AddStatBonusABP(StatType.Charisma, 2))
        .Configure();
    }
  }
}
