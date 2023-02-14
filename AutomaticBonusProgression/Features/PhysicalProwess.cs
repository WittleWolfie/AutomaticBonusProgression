using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Features
{
  internal class PhysicalProwess
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(PhysicalProwess));

    private const string PrimaryName = "PhysicalProwess.Primary";
    private const string PrimaryDisplayName = "PhysicalProwess.Primary.Name";
    private const string PrimaryDescription = "PhysicalProwess.Primary.Description";

    internal static BlueprintFeature ConfigurePrimary()
    {
      Logger.Log($"Configuring PhysicalProwess (Primary)");

      ConfigureStrPlus2();
      ConfigureStrPlus4();
      ConfigureStrPlus6();

      ConfigureDexPlus2();
      ConfigureDexPlus4();
      ConfigureDexPlus6();

      ConfigureConPlus2();
      ConfigureConPlus4();
      ConfigureConPlus6();

      return FeatureSelectionConfigurator.New(PrimaryName, Guids.PhysicalProwessPrimarySelection)
        .SetIsClassFeature()
        .SetDisplayName(PrimaryDisplayName)
        .SetDescription(PrimaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(ConfigureStrPrimary(), ConfigureDexPrimary(), ConfigureConPrimary())
        .Configure();
    }

    #region Primary
    private const string StrPrimaryName = "PhysicalProwess.Str.Primary";
    private const string StrPrimaryDisplayName = "PhysicalProwess.Str.Primary.Name";
    private const string StrPrimaryDescription = "PhysicalProwess.Str.Primary.Description";

    private static BlueprintFeature ConfigureStrPrimary()
    {
      return ProgressionConfigurator.New(StrPrimaryName, Guids.StrPrimaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(StrPlus2DisplayName)
        .SetDescription(StrPrimaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .SetLevelEntry(level: 7, Guids.StrPlus2)
        .SetLevelEntry(level: 12, Guids.StrPlus4)
        .Configure();
    }

    private const string DexPrimaryName = "PhysicalProwess.Dex.Primary";
    private const string DexPrimaryDisplayName = "PhysicalProwess.Dex.Primary.Name";
    private const string DexPrimaryDescription = "PhysicalProwess.Dex.Primary.Description";

    private static BlueprintFeature ConfigureDexPrimary()
    {
      return ProgressionConfigurator.New(DexPrimaryName, Guids.DexPrimaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(DexPlus2DisplayName)
        .SetDescription(DexPrimaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .SetLevelEntry(level: 7, Guids.DexPlus2)
        .SetLevelEntry(level: 12, Guids.DexPlus4)
        .Configure();
    }

    private const string ConPrimaryName = "PhysicalProwess.Con.Primary";
    private const string ConPrimaryDisplayName = "PhysicalProwess.Con.Primary.Name";
    private const string ConPrimaryDescription = "PhysicalProwess.Con.Primary.Description";

    private static BlueprintFeature ConfigureConPrimary()
    {
      return ProgressionConfigurator.New(ConPrimaryName, Guids.ConPrimaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(ConPlus2DisplayName)
        .SetDescription(ConPrimaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .SetLevelEntry(level: 7, Guids.ConPlus2)
        .SetLevelEntry(level: 12, Guids.ConPlus4)
        .Configure();
    }
    #endregion

    private const string SecondaryName = "PhysicalProwess.Secondary";
    private const string SecondaryDescription = "PhysicalProwess.Secondary.Description";

    internal static BlueprintFeature ConfigureSecondary()
    {
      Logger.Log($"Configuring PhysicalProwess (Secondary)");

      return FeatureSelectionConfigurator.New(SecondaryName, Guids.PhysicalProwessSecondarySelection)
        .SetIsClassFeature()
        .SetDisplayName(PrimaryDisplayName)
        .SetDescription(SecondaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(ConfigureStrSecondary(), ConfigureDexSecondary(), ConfigureConSecondary())
        .Configure();
    }

    #region Secondary
    private const string StrSecondaryName = "PhysicalProwess.Str.Secondary";
    private static BlueprintFeature ConfigureStrSecondary()
    {
      return FeatureConfigurator.New(StrSecondaryName, Guids.StrSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(StrPlus2DisplayName)
        .SetDescription(StrPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.StrPrimaryProgression)
        .AddFacts(new() { Guids.StrPlus2 })
        .Configure();
    }

    private const string DexSecondaryName = "PhysicalProwess.Dex.Secondary";
    private static BlueprintFeature ConfigureDexSecondary()
    {
      return FeatureConfigurator.New(DexSecondaryName, Guids.DexSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(DexPlus2DisplayName)
        .SetDescription(DexPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.DexPrimaryProgression)
        .AddFacts(new() { Guids.DexPlus2 })
        .Configure();
    }

    private const string ConSecondaryName = "PhysicalProwess.Con.Secondary";
    private static BlueprintFeature ConfigureConSecondary()
    {
      return FeatureConfigurator.New(ConSecondaryName, Guids.ConSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(ConPlus2DisplayName)
        .SetDescription(ConPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.ConPrimaryProgression)
        .AddFacts(new() { Guids.ConPlus2 })
        .Configure();
    }
    #endregion

    private const string TertiaryName = "PhysicalProwess.Tertiary";
    private const string TertiaryDescription = "PhysicalProwess.Tertiary.Description";

    internal static BlueprintFeature ConfigureTertiary()
    {
      Logger.Log($"Configuring PhysicalProwess (Tertiary)");

      return FeatureSelectionConfigurator.New(TertiaryName, Guids.PhysicalProwessTertiarySelection)
        .SetIsClassFeature()
        .SetDisplayName(PrimaryDisplayName)
        .SetDescription(TertiaryDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(ConfigureStrTertiary(), ConfigureDexTertiary(), ConfigureConTertiary())
        .Configure();
    }

    #region Tertiary
    private const string StrTertiaryName = "PhysicalProwess.Str.Tertiary";
    private static BlueprintFeature ConfigureStrTertiary()
    {
      return FeatureConfigurator.New(StrTertiaryName, Guids.StrTertiaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(StrPlus2DisplayName)
        .SetDescription(StrPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.StrPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.StrSecondaryProgression)
        .AddFacts(new() { Guids.StrPlus2 })
        .Configure();
    }

    private const string DexTertiaryName = "PhysicalProwess.Dex.Tertiary";
    private static BlueprintFeature ConfigureDexTertiary()
    {
      return FeatureConfigurator.New(DexTertiaryName, Guids.DexTertiaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(DexPlus2DisplayName)
        .SetDescription(DexPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.DexPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.DexSecondaryProgression)
        .AddFacts(new() { Guids.DexPlus2 })
        .Configure();
    }

    private const string ConTertiaryName = "PhysicalProwess.Con.Tertiary";
    private static BlueprintFeature ConfigureConTertiary()
    {
      return FeatureConfigurator.New(ConTertiaryName, Guids.ConTertiaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(ConPlus2DisplayName)
        .SetDescription(ConPlus2Description)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.ConPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.ConSecondaryProgression)
        .AddFacts(new() { Guids.ConPlus2 })
        .Configure();
    }
    #endregion

    private const string AnyName = "PhysicalProwess.Any";
    private const string AnyDescription = "PhysicalProwess.Any.Description";

    internal static BlueprintFeature ConfigureAny()
    {
      Logger.Log($"Configuring PhysicalProwess (Select Any)");

      return FeatureSelectionConfigurator.New(AnyName, Guids.PhysicalProwessAnySelection)
        .SetIsClassFeature()
        .SetDisplayName(PrimaryDisplayName)
        .SetDescription(AnyDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddToAllFeatures(Guids.StrPlus4, Guids.StrPlus6, Guids.DexPlus4, Guids.DexPlus6, Guids.ConPlus4, Guids.ConPlus6)
        .Configure();
    }

    #region Str Bonuses
    private const string StrPlus2Name = "PhysicalProwess.Str.Plus2";
    private const string StrPlus2DisplayName = "PhysicalProwess.Str.Plus2.Name";
    private const string StrPlus2Description = "PhysicalProwess.Str.Plus2.Description";

    private static void ConfigureStrPlus2()
    {
      FeatureConfigurator.New(StrPlus2Name, Guids.StrPlus2)
        .SetIsClassFeature()
        .SetDisplayName(StrPlus2DisplayName)
        .SetDescription(StrPlus2Description)
        //.SetIcon()
        .AddStatBonus(stat: StatType.Strength, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }

    private const string StrPlus4Name = "PhysicalProwess.Str.Plus4";
    private const string StrPlus4DisplayName = "PhysicalProwess.Str.Plus4.Name";
    private const string StrPlus4Description = "PhysicalProwess.Str.Plus4.Description";

    private static void ConfigureStrPlus4()
    {
      FeatureConfigurator.New(StrPlus4Name, Guids.StrPlus4)
        .SetIsClassFeature()
        .SetDisplayName(StrPlus4DisplayName)
        .SetDescription(StrPlus4Description)
        //.SetIcon()
        .AddStatBonus(stat: StatType.Strength, value: 4, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.StrPlus2)
        .AddRemoveFeatureOnApply(Guids.StrPlus2)
        .Configure();
    }

    private const string StrPlus6Name = "PhysicalProwess.Str.Plus6";
    private const string StrPlus6DisplayName = "PhysicalProwess.Str.Plus6.Name";
    private const string StrPlus6Description = "PhysicalProwess.Str.Plus6.Description";

    private static void ConfigureStrPlus6()
    {
      FeatureConfigurator.New(StrPlus6Name, Guids.StrPlus6)
        .SetIsClassFeature()
        .SetDisplayName(StrPlus6DisplayName)
        .SetDescription(StrPlus6Description)
        //.SetIcon()
        .AddStatBonus(stat: StatType.Strength, value: 6, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.StrPlus4)
        .AddRemoveFeatureOnApply(Guids.StrPlus4)
        .Configure();
    }
    #endregion

    #region Dex Bonuses
    private const string DexPlus2Name = "PhysicalProwess.Dex.Plus2";
    private const string DexPlus2DisplayName = "PhysicalProwess.Dex.Plus2.Name";
    private const string DexPlus2Description = "PhysicalProwess.Dex.Plus2.Description";

    private static void ConfigureDexPlus2()
    {
      FeatureConfigurator.New(DexPlus2Name, Guids.DexPlus2)
        .SetIsClassFeature()
        .SetDisplayName(DexPlus2DisplayName)
        .SetDescription(DexPlus2Description)
        //.SetIcon()
        .AddStatBonus(stat: StatType.Dexterity, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }

    private const string DexPlus4Name = "PhysicalProwess.Dex.Plus4";
    private const string DexPlus4DisplayName = "PhysicalProwess.Dex.Plus4.Name";
    private const string DexPlus4Description = "PhysicalProwess.Dex.Plus4.Description";

    private static void ConfigureDexPlus4()
    {
      FeatureConfigurator.New(DexPlus4Name, Guids.DexPlus4)
        .SetIsClassFeature()
        .SetDisplayName(DexPlus4DisplayName)
        .SetDescription(DexPlus4Description)
        //.SetIcon()
        .AddStatBonus(stat: StatType.Dexterity, value: 4, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.DexPlus2)
        .AddRemoveFeatureOnApply(Guids.DexPlus2)
        .Configure();
    }

    private const string DexPlus6Name = "PhysicalProwess.Dex.Plus6";
    private const string DexPlus6DisplayName = "PhysicalProwess.Dex.Plus6.Name";
    private const string DexPlus6Description = "PhysicalProwess.Dex.Plus6.Description";

    private static void ConfigureDexPlus6()
    {
      FeatureConfigurator.New(DexPlus6Name, Guids.DexPlus6)
        .SetIsClassFeature()
        .SetDisplayName(DexPlus6DisplayName)
        .SetDescription(DexPlus6Description)
        //.SetIcon()
        .AddStatBonus(stat: StatType.Dexterity, value: 6, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.DexPlus4)
        .AddRemoveFeatureOnApply(Guids.DexPlus4)
        .Configure();
    }
    #endregion

    #region Con Bonuses
    private const string ConPlus2Name = "PhysicalProwess.Con.Plus2";
    private const string ConPlus2DisplayName = "PhysicalProwess.Con.Plus2.Name";
    private const string ConPlus2Description = "PhysicalProwess.Con.Plus2.Description";

    private static void ConfigureConPlus2()
    {
      FeatureConfigurator.New(ConPlus2Name, Guids.ConPlus2)
        .SetIsClassFeature()
        .SetDisplayName(ConPlus2DisplayName)
        .SetDescription(ConPlus2Description)
        //.SetIcon()
        .AddStatBonus(stat: StatType.Constitution, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }

    private const string ConPlus4Name = "PhysicalProwess.Con.Plus4";
    private const string ConPlus4DisplayName = "PhysicalProwess.Con.Plus4.Name";
    private const string ConPlus4Description = "PhysicalProwess.Con.Plus4.Description";

    private static void ConfigureConPlus4()
    {
      FeatureConfigurator.New(ConPlus4Name, Guids.ConPlus4)
        .SetIsClassFeature()
        .SetDisplayName(ConPlus4DisplayName)
        .SetDescription(ConPlus4Description)
        //.SetIcon()
        .AddStatBonus(stat: StatType.Constitution, value: 4, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.ConPlus2)
        .AddRemoveFeatureOnApply(Guids.ConPlus2)
        .Configure();
    }

    private const string ConPlus6Name = "PhysicalProwess.Con.Plus6";
    private const string ConPlus6DisplayName = "PhysicalProwess.Con.Plus6.Name";
    private const string ConPlus6Description = "PhysicalProwess.Con.Plus6.Description";

    private static void ConfigureConPlus6()
    {
      FeatureConfigurator.New(ConPlus6Name, Guids.ConPlus6)
        .SetIsClassFeature()
        .SetDisplayName(ConPlus6DisplayName)
        .SetDescription(ConPlus6Description)
        //.SetIcon()
        .AddStatBonus(stat: StatType.Constitution, value: 6, descriptor: ModifierDescriptor.Enhancement)
        .AddPrerequisiteFeature(Guids.ConPlus4)
        .AddRemoveFeatureOnApply(Guids.ConPlus4)
        .Configure();
    }
    #endregion
  }
}
