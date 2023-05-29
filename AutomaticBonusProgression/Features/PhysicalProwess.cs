using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Features
{
  internal class PhysicalProwess
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(PhysicalProwess));

    private const string ProwessName = "PhysicalProwess";
    private const string ProwessDisplayName = "PhysicalProwess.Name";
    private const string ProwessDescription = "PhysicalProwess.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring PhysicalProwess");

      ConfigureStrProwess();
      ConfigureDexProwess();
      ConfigureConProwess();

      // Feature is for the UI only
      return FeatureConfigurator.New(ProwessName, Guids.PhysicalProwess)
        .SetIsClassFeature()
        .SetDisplayName(ProwessDisplayName)
        .SetDescription(ProwessDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .Configure();
    }

    private const string StrProwessName = "PhysicalProwess.Str";
    private const string StrProwessDisplayName = "PhysicalProwess.Str.Name";
    private const string StrProwessDescription = "PhysicalProwess.Str.Description";

    private static void ConfigureStrProwess()
    {
      FeatureConfigurator.New(StrProwessName, Guids.StrProwess)
        .SetIsClassFeature()
        .SetDisplayName(StrProwessDisplayName)
        .SetDescription(StrProwessDescription)
        //.SetIcon()
        .SetRanks(3)
        .AddStatBonus(stat: StatType.Strength, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }

    private const string DexProwessName = "PhysicalProwess.Dex";
    private const string DexProwessDisplayName = "PhysicalProwess.Dex.Name";
    private const string DexProwessDescription = "PhysicalProwess.Dex.Description";

    private static void ConfigureDexProwess()
    {
      FeatureConfigurator.New(DexProwessName, Guids.DexProwess)
        .SetIsClassFeature()
        .SetDisplayName(DexProwessDisplayName)
        .SetDescription(DexProwessDescription)
        //.SetIcon()
        .SetRanks(3)
        .AddStatBonus(stat: StatType.Dexterity, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }

    private const string ConProwessName = "PhysicalProwess.Con";
    private const string ConProwessDisplayName = "PhysicalProwess.Con.Name";
    private const string ConProwessDescription = "PhysicalProwess.Con.Description";

    private static void ConfigureConProwess()
    {
      FeatureConfigurator.New(ConProwessName, Guids.ConProwess)
        .SetIsClassFeature()
        .SetDisplayName(ConProwessDisplayName)
        .SetDescription(ConProwessDescription)
        //.SetIcon()
        .SetRanks(3)
        .AddStatBonus(stat: StatType.Constitution, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }
  }
}
