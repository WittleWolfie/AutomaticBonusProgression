using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

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
        //.SetIcon()
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
        //.SetIcon()
        .SetRanks(3)
        .AddStatBonus(stat: StatType.Intelligence, value: 2, descriptor: ModifierDescriptor.Enhancement)
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
        //.SetIcon()
        .SetRanks(3)
        .AddStatBonus(stat: StatType.Wisdom, value: 2, descriptor: ModifierDescriptor.Enhancement)
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
        //.SetIcon()
        .SetRanks(3)
        .AddStatBonus(stat: StatType.Charisma, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }
  }
}
