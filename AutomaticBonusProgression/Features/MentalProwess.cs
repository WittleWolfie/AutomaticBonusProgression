using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Features
{
  internal class MentalProwess
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(MentalProwess));

    private const string PrimaryName = "MentalProwess.Primary";
    private const string PrimaryDisplayName = "MentalProwess.Primary.Name";
    private const string PrimaryDescription = "MentalProwess.Primary.Description";

    internal static BlueprintFeature ConfigurePrimary()
    {
      Logger.Log($"Configuring MentalProwess (Primary)");

      ConfigureIntPlus2();
      ConfigureIntPlus4();
      ConfigureIntPlus6();

      ConfigureWisPlus2();
      ConfigureWisPlus4();
      ConfigureWisPlus6();

      ConfigureChaPlus2();
      ConfigureChaPlus4();
      ConfigureChaPlus6();

      return FeatureSelectionConfigurator.New(PrimaryName, Guids.MentalProwessPrimarySelection)
        .SetIsClassFeature()
        .SetDisplayName(PrimaryDisplayName)
        .SetDescription(PrimaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(ConfigureIntPrimary(), ConfigureWisPrimary(), ConfigureChaPrimary())
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
        .SetDisplayName(IntPlus2DisplayName)
        .SetDescription(IntPrimaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .SetLevelEntry(level: 6, Guids.IntPlus2)
        .SetLevelEntry(level: 11, Guids.IntPlus4)
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
        .Configure();
    }
    #endregion

    private const string SecondaryName = "MentalProwess.Secondary";
    private const string SecondaryDisplayName = "MentalProwess.Secondary.Name";
    private const string SecondaryDescription = "MentalProwess.Secondary.Description";

    internal static BlueprintFeature ConfigureSecondary()
    {
      Logger.Log($"Configuring MentalProwess (Secondary)");

      return FeatureSelectionConfigurator.New(SecondaryName, Guids.MentalProwessSecondarySelection)
        .SetIsClassFeature()
        .SetDisplayName(SecondaryDisplayName)
        .SetDescription(SecondaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(ConfigureIntSecondary(), ConfigureWisSecondary(), ConfigureChaSecondary())
        .Configure();
    }

    #region Secondary
    private const string IntSecondaryName = "MentalProwess.Int.Secondary";
    private static BlueprintFeature ConfigureIntSecondary()
    {
      return FeatureConfigurator.New(IntSecondaryName, Guids.IntSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(IntPlus2DisplayName)
        .SetDescription(IntPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.IntPrimaryProgression)
        .AddFacts(new() { Guids.IntPlus2 })
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
        .Configure();
    }
    #endregion

    private const string TertiaryName = "MentalProwess.Tertiary";
    private const string TertiaryDisplayName = "MentalProwess.Tertiary.Name";
    private const string TertiaryDescription = "MentalProwess.Tertiary.Description";

    internal static BlueprintFeature ConfigureTertiary()
    {
      Logger.Log($"Configuring MentalProwess (Tertiary)");

      return FeatureSelectionConfigurator.New(TertiaryName, Guids.MentalProwessTertiarySelection)
        .SetIsClassFeature()
        .SetDisplayName(TertiaryDisplayName)
        .SetDescription(TertiaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(ConfigureIntTertiary(), ConfigureWisTertiary(), ConfigureChaTertiary())
        .Configure();
    }

    #region Tertiary
    private const string IntTertiaryName = "MentalProwess.Int.Tertiary";
    private static BlueprintFeature ConfigureIntTertiary()
    {
      return FeatureConfigurator.New(IntTertiaryName, Guids.IntTertiaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(IntPlus2DisplayName)
        .SetDescription(IntPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.IntPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.IntSecondaryProgression)
        .AddFacts(new() { Guids.IntPlus2 })
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
        .Configure();
    }
    #endregion

    private const string AnyName = "MentalProwess.Any";
    private const string AnyDisplayName = "MentalProwess.Any.Name";
    private const string AnyDescription = "MentalProwess.Any.Description";

    internal static BlueprintFeature ConfigureAny()
    {
      Logger.Log($"Configuring MentalProwess (Select Any)");

      return FeatureSelectionConfigurator.New(AnyName, Guids.MentalProwessAnySelection)
        .SetIsClassFeature()
        .SetDisplayName(AnyDisplayName)
        .SetDescription(AnyDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(Guids.IntPlus4, Guids.IntPlus6, Guids.WisPlus4, Guids.WisPlus6, Guids.ChaPlus4, Guids.ChaPlus6)
        .Configure();
    }

    #region Int Bonuses
    private const string IntPlus2Name = "MentalProwess.Int.Plus2";
    private const string IntPlus2DisplayName = "MentalProwess.Int.Plus2.Name";
    private const string IntPlus2Description = "MentalProwess.Int.Plus2.Description";

    private static void ConfigureIntPlus2()
    {
      FeatureConfigurator.New(IntPlus2Name, Guids.IntPlus2)
        .SetIsClassFeature()
        .SetDisplayName(IntPlus2DisplayName)
        .SetDescription(IntPlus2Description)
        //.SetIcon()
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
        .AddStatBonus(stat: StatType.Intelligence, value: 4, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.IntPlus2)
        .AddRemoveFeatureOnApply(Guids.IntPlus2)
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
        .AddStatBonus(stat: StatType.Intelligence, value: 6, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.IntPlus4)
        .AddRemoveFeatureOnApply(Guids.IntPlus4)
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
        .AddStatBonus(stat: StatType.Wisdom, value: 2, descriptor: ModifierDescriptor.Enhancement)
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
        .AddStatBonus(stat: StatType.Wisdom, value: 4, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.WisPlus2)
        .AddRemoveFeatureOnApply(Guids.WisPlus2)
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
        .AddStatBonus(stat: StatType.Wisdom, value: 6, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.WisPlus4)
        .AddRemoveFeatureOnApply(Guids.WisPlus4)
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
        .AddStatBonus(stat: StatType.Charisma, value: 2, descriptor: ModifierDescriptor.Enhancement)
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
        .AddStatBonus(stat: StatType.Charisma, value: 4, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.ChaPlus2)
        .AddRemoveFeatureOnApply(Guids.ChaPlus2)
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
        .AddStatBonus(stat: StatType.Charisma, value: 6, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.ChaPlus4)
        .AddRemoveFeatureOnApply(Guids.ChaPlus4)
        .Configure();
    }
    #endregion
  }
}
