using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Features
{
  // TODO: Rather than do some of the AddFeatureABP nonsense here, let's rework this entirely w/ custom UI to simplify
  // things.
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
      ConfigureIntPlus4();
      ConfigureIntPlus6();

      ConfigureWisPlus2();
      ConfigureWisPlus4();
      ConfigureWisPlus6();

      ConfigureChaPlus2();
      ConfigureChaPlus4();
      ConfigureChaPlus6();

      // TODO: Make a feature.. but right now it breaks all muh saves :(
      return FeatureSelectionConfigurator.New(ProwessName, Guids.MentalProwess)
        .SetIsClassFeature()
        .SetDisplayName(ProwessDisplayName)
        .SetDescription(ProwessDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .Configure();
    }

    #region Primary
    private const string IntPrimaryName = "MentalProwess.Int.Primary";
    private const string IntPrimaryDisplayName = "MentalProwess.Int.Primary.Name";
    private const string IntPrimaryDescription = "MentalProwess.Int.Primary.Description";

    private static BlueprintFeature ConfigureIntPrimary()
    {
      return ProgressionConfigurator.New(IntPrimaryName, Guids.IntPrimaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(IntProwessDisplayName)
        .SetDescription(IntPrimaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .SetLevelEntry(level: 6, Guids.IntProwess)
        .SetLevelEntry(level: 11, Guids.IntPlus4)
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string WisPrimaryName = "MentalProwess.Wis.Primary";
    private const string WisPrimaryDisplayName = "MentalProwess.Wis.Primary.Name";
    private const string WisPrimaryDescription = "MentalProwess.Wis.Primary.Description";

    private static BlueprintFeature ConfigureWisPrimary()
    {
      return ProgressionConfigurator.New(WisPrimaryName, Guids.WisPrimaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(WisPlus2DisplayName)
        .SetDescription(WisPrimaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .SetLevelEntry(level: 6, Guids.WisPlus2)
        .SetLevelEntry(level: 11, Guids.WisPlus4)
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string ChaPrimaryName = "MentalProwess.Cha.Primary";
    private const string ChaPrimaryDisplayName = "MentalProwess.Cha.Primary.Name";
    private const string ChaPrimaryDescription = "MentalProwess.Cha.Primary.Description";

    private static BlueprintFeature ConfigureChaPrimary()
    {
      return ProgressionConfigurator.New(ChaPrimaryName, Guids.ChaPrimaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(ChaPlus2DisplayName)
        .SetDescription(ChaPrimaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .SetLevelEntry(level: 6, Guids.ChaPlus2)
        .SetLevelEntry(level: 11, Guids.ChaPlus4)
        .AddHideFeatureInInspect()
        .Configure();
    }
    #endregion

    private const string SecondaryName = "MentalProwess.Secondary";
    private const string SecondaryDescription = "MentalProwess.Secondary.Description";

    internal static BlueprintFeature ConfigureSecondary()
    {
      Logger.Log($"Configuring MentalProwess (Secondary)");

      return FeatureSelectionConfigurator.New(SecondaryName, Guids.MentalProwessSecondarySelection)
        .SetIsClassFeature()
        .SetDisplayName(ProwessDisplayName)
        .SetDescription(SecondaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(ConfigureIntSecondary(), ConfigureWisSecondary(), ConfigureChaSecondary())
        .AddHideFeatureInInspect()
        .AddHideFeatureInInspect()
        .Configure();
    }

    #region Secondary
    private const string IntSecondaryName = "MentalProwess.Int.Secondary";
    private static BlueprintFeature ConfigureIntSecondary()
    {
      return FeatureConfigurator.New(IntSecondaryName, Guids.IntSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(IntProwessDisplayName)
        .SetDescription(IntProwessDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.IntPrimaryProgression)
        .AddFacts(new() { Guids.IntProwess })
        .Configure();
    }

    private const string WisSecondaryName = "MentalProwess.Wis.Secondary";
    private static BlueprintFeature ConfigureWisSecondary()
    {
      return FeatureConfigurator.New(WisSecondaryName, Guids.WisSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(WisPlus2DisplayName)
        .SetDescription(WisPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.WisPrimaryProgression)
        .AddFacts(new() { Guids.WisPlus2 })
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string ChaSecondaryName = "MentalProwess.Cha.Secondary";
    private static BlueprintFeature ConfigureChaSecondary()
    {
      return FeatureConfigurator.New(ChaSecondaryName, Guids.ChaSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(ChaPlus2DisplayName)
        .SetDescription(ChaPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.ChaPrimaryProgression)
        .AddFacts(new() { Guids.ChaPlus2 })
        .AddHideFeatureInInspect()
        .Configure();
    }
    #endregion

    private const string TertiaryName = "MentalProwess.Tertiary";
    private const string TertiaryDescription = "MentalProwess.Tertiary.Description";

    internal static BlueprintFeature ConfigureTertiary()
    {
      Logger.Log($"Configuring MentalProwess (Tertiary)");

      return FeatureSelectionConfigurator.New(TertiaryName, Guids.MentalProwessTertiarySelection)
        .SetIsClassFeature()
        .SetDisplayName(ProwessDisplayName)
        .SetDescription(TertiaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(ConfigureIntTertiary(), ConfigureWisTertiary(), ConfigureChaTertiary())
        .AddHideFeatureInInspect()
        .Configure();
    }

    #region Tertiary
    private const string IntTertiaryName = "MentalProwess.Int.Tertiary";
    private static BlueprintFeature ConfigureIntTertiary()
    {
      return FeatureConfigurator.New(IntTertiaryName, Guids.IntTertiaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(IntProwessDisplayName)
        .SetDescription(IntProwessDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.IntPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.IntSecondaryProgression)
        .AddFacts(new() { Guids.IntProwess })
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string WisTertiaryName = "MentalProwess.Wis.Tertiary";
    private static BlueprintFeature ConfigureWisTertiary()
    {
      return FeatureConfigurator.New(WisTertiaryName, Guids.WisTertiaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(WisPlus2DisplayName)
        .SetDescription(WisPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.WisPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.WisSecondaryProgression)
        .AddFacts(new() { Guids.WisPlus2 })
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string ChaTertiaryName = "MentalProwess.Cha.Tertiary";
    private static BlueprintFeature ConfigureChaTertiary()
    {
      return FeatureConfigurator.New(ChaTertiaryName, Guids.ChaTertiaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(ChaPlus2DisplayName)
        .SetDescription(ChaPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.ChaPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.ChaSecondaryProgression)
        .AddFacts(new() { Guids.ChaPlus2 })
        .AddHideFeatureInInspect()
        .Configure();
    }
    #endregion

    private const string AnyName = "MentalProwess.Any";
    private const string AnyDescription = "MentalProwess.Any.Description";

    internal static BlueprintFeature ConfigureAny()
    {
      Logger.Log($"Configuring MentalProwess (Select Any)");

      return FeatureSelectionConfigurator.New(AnyName, Guids.MentalProwessAnySelection)
        .SetIsClassFeature()
        .SetDisplayName(ProwessDisplayName)
        .SetDescription(AnyDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(Guids.IntPlus4, Guids.IntPlus6, Guids.WisPlus4, Guids.WisPlus6, Guids.ChaPlus4, Guids.ChaPlus6)
        .AddHideFeatureInInspect()
        .Configure();
    }

    #region Int Bonuses
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
    private const string IntPlus4Name = "MentalProwess.Int.Plus4";
    private const string IntPlus4DisplayName = "MentalProwess.Int.Plus4.Name";
    private const string IntPlus4Description = "MentalProwess.Int.Plus4.Description";
    private static void ConfigureIntPlus4()
    {
      FeatureConfigurator.New(IntPlus4Name, Guids.IntPlus4)
        .SetIsClassFeature()
        .SetDisplayName(IntPlus4DisplayName)
        .SetDescription(IntPlus4Description)
        //.SetIcon()
        .AddComponent(new AddStatBonusABP(StatType.Intelligence, value: 4, ModifierDescriptor.Enhancement))
        //.AddPrerequisiteFeature(Guids.IntPlus2)
        //.AddRemoveFeatureOnApply(Guids.IntPlus2)
        .AddHideFeatureInInspect()
        .Configure();
    }
    private const string IntPlus6Name = "MentalProwess.Int.Plus6";
    private const string IntPlus6DisplayName = "MentalProwess.Int.Plus6.Name";
    private const string IntPlus6Description = "MentalProwess.Int.Plus6.Description";
    private static void ConfigureIntPlus6()
    {
      FeatureConfigurator.New(IntPlus6Name, Guids.IntPlus6)
        .SetIsClassFeature()
        .SetDisplayName(IntPlus6DisplayName)
        .SetDescription(IntPlus6Description)
        //.SetIcon()
        .AddComponent(new AddStatBonusABP(StatType.Intelligence, value: 6, ModifierDescriptor.Enhancement))
        .AddPrerequisiteFeature(Guids.IntPlus4)
        .AddRemoveFeatureOnApply(Guids.IntPlus4)
        .AddHideFeatureInInspect()
        .Configure();
    }
    #endregion

    #region Wis Bonuses
    private const string WisPlus2Name = "MentalProwess.Wis.Plus2";
    private const string WisPlus2DisplayName = "MentalProwess.Wis.Plus2.Name";
    private const string WisPlus2Description = "MentalProwess.Wis.Plus2.Description";

    private static void ConfigureWisPlus2()
    {
      FeatureConfigurator.New(WisPlus2Name, Guids.WisPlus2)
        .SetIsClassFeature()
        .SetDisplayName(WisPlus2DisplayName)
        .SetDescription(WisPlus2Description)
        //.SetIcon()
        .AddComponent(new AddStatBonusABP(StatType.Wisdom, value: 2, ModifierDescriptor.Enhancement))
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string WisPlus4Name = "MentalProwess.Wis.Plus4";
    private const string WisPlus4DisplayName = "MentalProwess.Wis.Plus4.Name";
    private const string WisPlus4Description = "MentalProwess.Wis.Plus4.Description";

    private static void ConfigureWisPlus4()
    {
      FeatureConfigurator.New(WisPlus4Name, Guids.WisPlus4)
        .SetIsClassFeature()
        .SetDisplayName(WisPlus4DisplayName)
        .SetDescription(WisPlus4Description)
        //.SetIcon()
        .AddComponent(new AddStatBonusABP(StatType.Wisdom, value: 4, ModifierDescriptor.Enhancement))
        .AddPrerequisiteFeature(Guids.WisPlus2)
        .AddRemoveFeatureOnApply(Guids.WisPlus2)
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string WisPlus6Name = "MentalProwess.Wis.Plus6";
    private const string WisPlus6DisplayName = "MentalProwess.Wis.Plus6.Name";
    private const string WisPlus6Description = "MentalProwess.Wis.Plus6.Description";

    private static void ConfigureWisPlus6()
    {
      FeatureConfigurator.New(WisPlus6Name, Guids.WisPlus6)
        .SetIsClassFeature()
        .SetDisplayName(WisPlus6DisplayName)
        .SetDescription(WisPlus6Description)
        //.SetIcon()
        .AddComponent(new AddStatBonusABP(StatType.Wisdom, value: 6, ModifierDescriptor.Enhancement))
        .AddPrerequisiteFeature(Guids.WisPlus4)
        .AddRemoveFeatureOnApply(Guids.WisPlus4)
        .AddHideFeatureInInspect()
        .Configure();
    }
    #endregion

    #region Cha Bonuses
    private const string ChaPlus2Name = "MentalProwess.Cha.Plus2";
    private const string ChaPlus2DisplayName = "MentalProwess.Cha.Plus2.Name";
    private const string ChaPlus2Description = "MentalProwess.Cha.Plus2.Description";

    private static void ConfigureChaPlus2()
    {
      FeatureConfigurator.New(ChaPlus2Name, Guids.ChaPlus2)
        .SetIsClassFeature()
        .SetDisplayName(ChaPlus2DisplayName)
        .SetDescription(ChaPlus2Description)
        //.SetIcon()
        .AddComponent(new AddStatBonusABP(StatType.Charisma, value: 2, ModifierDescriptor.Enhancement))
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string ChaPlus4Name = "MentalProwess.Cha.Plus4";
    private const string ChaPlus4DisplayName = "MentalProwess.Cha.Plus4.Name";
    private const string ChaPlus4Description = "MentalProwess.Cha.Plus4.Description";

    private static void ConfigureChaPlus4()
    {
      FeatureConfigurator.New(ChaPlus4Name, Guids.ChaPlus4)
        .SetIsClassFeature()
        .SetDisplayName(ChaPlus4DisplayName)
        .SetDescription(ChaPlus4Description)
        //.SetIcon()
        .AddComponent(new AddStatBonusABP(StatType.Charisma, value: 4, ModifierDescriptor.Enhancement))
        .AddPrerequisiteFeature(Guids.ChaPlus2)
        .AddRemoveFeatureOnApply(Guids.ChaPlus2)
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string ChaPlus6Name = "MentalProwess.Cha.Plus6";
    private const string ChaPlus6DisplayName = "MentalProwess.Cha.Plus6.Name";
    private const string ChaPlus6Description = "MentalProwess.Cha.Plus6.Description";

    private static void ConfigureChaPlus6()
    {
      FeatureConfigurator.New(ChaPlus6Name, Guids.ChaPlus6)
        .SetIsClassFeature()
        .SetDisplayName(ChaPlus6DisplayName)
        .SetDescription(ChaPlus6Description)
        //.SetIcon()
        .AddComponent(new AddStatBonusABP(StatType.Charisma, value: 6, ModifierDescriptor.Enhancement))
        .AddPrerequisiteFeature(Guids.ChaPlus4)
        .AddRemoveFeatureOnApply(Guids.ChaPlus4)
        .AddHideFeatureInInspect()
        .Configure();
    }
    #endregion
  }
}
