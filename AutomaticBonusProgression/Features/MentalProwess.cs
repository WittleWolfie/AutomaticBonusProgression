using AutomaticBonusProgression.Components;
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

      ConfigureIntBonus();
      ConfigureWisBonus();
      ConfigureChaBonus();

      return FeatureSelectionConfigurator.New(PrimaryName, Guids.MentalProwessPrimarySelection)
        .SetIsClassFeature()
        .SetDisplayName(PrimaryDisplayName)
        .SetDescription(PrimaryDescription)
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
        .SetDisplayName(IntPrimaryDisplayName)
        .SetDescription(IntPrimaryDescription)
        //.SetIcon()
        .SetLevelEntry(level: 4, Guids.IntBonus)
        .SetLevelEntry(level: 11, Guids.IntBonus)
        .Configure();
    }

    private const string WisPrimaryName = "MentalProwess.Wis.Primary";
    private const string WisPrimaryDisplayName = "MentalProwess.Wis.Primary.Name";
    private const string WisPrimaryDescription = "MentalProwess.Wis.Primary.Description";

    private static BlueprintFeature ConfigureWisPrimary()
    {
      return ProgressionConfigurator.New(WisPrimaryName, Guids.WisPrimaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(WisPrimaryDisplayName)
        .SetDescription(WisPrimaryDescription)
        //.SetIcon()
        .SetLevelEntry(level: 4, Guids.WisBonus)
        .SetLevelEntry(level: 11, Guids.WisBonus)
        .Configure();
    }

    private const string ChaPrimaryName = "MentalProwess.Cha.Primary";
    private const string ChaPrimaryDisplayName = "MentalProwess.Cha.Primary.Name";
    private const string ChaPrimaryDescription = "MentalProwess.Cha.Primary.Description";

    private static BlueprintFeature ConfigureChaPrimary()
    {
      return ProgressionConfigurator.New(ChaPrimaryName, Guids.ChaPrimaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(ChaPrimaryDisplayName)
        .SetDescription(ChaPrimaryDescription)
        //.SetIcon()
        .SetLevelEntry(level: 4, Guids.ChaBonus)
        .SetLevelEntry(level: 11, Guids.ChaBonus)
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
        //.SetIcon()
        .AddToAllFeatures(ConfigureIntSecondary(), ConfigureWisSecondary(), ConfigureChaSecondary())
        .Configure();
    }

    #region Secondary
    private const string IntSecondaryName = "MentalProwess.Int.Secondary";
    private const string IntSecondaryDisplayName = "MentalProwess.Int.Secondary.Name";

    private static BlueprintFeature ConfigureIntSecondary()
    {
      return FeatureConfigurator.New(IntSecondaryName, Guids.IntSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(IntSecondaryDisplayName)
        .SetDescription(IntBonusDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.IntPrimaryProgression)
        .AddFacts(new() { Guids.IntBonus })
        .Configure();
    }

    private const string WisSecondaryName = "MentalProwess.Wis.Secondary";
    private const string WisSecondaryDisplayName = "MentalProwess.Wis.Secondary.Name";

    private static BlueprintFeature ConfigureWisSecondary()
    {
      return FeatureConfigurator.New(WisSecondaryName, Guids.WisSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(WisSecondaryDisplayName)
        .SetDescription(WisBonusDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.WisPrimaryProgression)
        .AddFacts(new() { Guids.WisBonus })
        .Configure();
    }

    private const string ChaSecondaryName = "MentalProwess.Cha.Secondary";
    private const string ChaSecondaryDisplayName = "MentalProwess.Cha.Secondary.Name";

    private static BlueprintFeature ConfigureChaSecondary()
    {
      return FeatureConfigurator.New(ChaSecondaryName, Guids.ChaSecondaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(ChaSecondaryDisplayName)
        .SetDescription(ChaBonusDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.ChaPrimaryProgression)
        .AddFacts(new() { Guids.ChaBonus })
        .Configure();
    }
    #endregion

    private const string TertiaryName = "MentalProwess.Tertiary";
    private const string TertiaryDisplayName = "MentalProwess.Tertiary.Name";
    private const string TertiaryDescription = "MentalProwess.Tertiary.Description";

    internal static BlueprintFeature ConfigureTertiary()
    {
      Logger.Log($"Configuring MentalProwess");

      return FeatureSelectionConfigurator.New(TertiaryName, Guids.MentalProwessTertiarySelection)
        .SetIsClassFeature()
        .SetDisplayName(TertiaryDisplayName)
        .SetDescription(TertiaryDescription)
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
        .SetDisplayName(IntBonusDisplayName)
        .SetDescription(IntBonusDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.IntPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.IntSecondaryProgression)
        .AddFacts(new() { Guids.IntBonus })
        .Configure();
    }

    private const string WisTertiaryName = "MentalProwess.Wis.Tertiary";

    private static BlueprintFeature ConfigureWisTertiary()
    {
      return FeatureConfigurator.New(WisTertiaryName, Guids.WisTertiaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(WisBonusDisplayName)
        .SetDescription(WisBonusDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.WisPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.WisSecondaryProgression)
        .AddFacts(new() { Guids.WisBonus })
        .Configure();
    }

    private const string ChaTertiaryName = "MentalProwess.Cha.Tertiary";

    private static BlueprintFeature ConfigureChaTertiary()
    {
      return FeatureConfigurator.New(ChaTertiaryName, Guids.ChaTertiaryProgression)
        .SetIsClassFeature()
        .SetDisplayName(ChaBonusDisplayName)
        .SetDescription(ChaBonusDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteNoFeature(Guids.ChaPrimaryProgression)
        .AddPrerequisiteNoFeature(Guids.ChaSecondaryProgression)
        .AddFacts(new() { Guids.ChaBonus })
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
        .AddToAllFeatures(ConfigureIntAny(), ConfigureWisAny(), ConfigureChaAny())
        .Configure();
    }

    #region Any
    private const string IntAnyName = "MentalProwess.Int.Any";

    private static BlueprintFeature ConfigureIntAny()
    {
      return FeatureConfigurator.New(IntAnyName, Guids.IntAny)
        .SetIsClassFeature()
        .SetDisplayName(IntBonusDisplayName)
        .SetDescription(IntBonusDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteFeature(Guids.IntBonus)
        .AddComponent(new PrerequisiteFeatureMaxRanks(Guids.IntBonus, 2))
        .AddFacts(new() { Guids.IntBonus })
        .Configure();
    }

    private const string WisAnyName = "MentalProwess.Wis.Any";

    private static BlueprintFeature ConfigureWisAny()
    {
      return FeatureConfigurator.New(WisAnyName, Guids.WisAny)
        .SetIsClassFeature()
        .SetDisplayName(WisBonusDisplayName)
        .SetDescription(WisBonusDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteFeature(Guids.WisBonus)
        .AddComponent(new PrerequisiteFeatureMaxRanks(Guids.WisBonus, 2))
        .AddFacts(new() { Guids.WisBonus })
        .Configure();
    }

    private const string ChaAnyName = "MentalProwess.Cha.Any";
    private const string ChaAnyDisplayName = "MentalProwess.Cha.Any.Name";
    private const string ChaAnyDescription = "MentalProwess.Cha.Any.Description";

    private static BlueprintFeature ConfigureChaAny()
    {
      return FeatureConfigurator.New(ChaAnyName, Guids.ChaAny)
        .SetIsClassFeature()
        .SetDisplayName(ChaBonusDisplayName)
        .SetDescription(ChaBonusDescription)
        .SetHideInCharacterSheetAndLevelUp()
        //.SetIcon()
        .AddPrerequisiteFeature(Guids.ChaBonus)
        .AddComponent(new PrerequisiteFeatureMaxRanks(Guids.ChaBonus, 2))
        .AddFacts(new() { Guids.ChaBonus })
        .Configure();
    }
    #endregion

    #region Base Bonuses
    private const string IntBonusName = "MentalProwess.Int.Bonus";
    private const string IntBonusDisplayName = "MentalProwess.Int.Bonus.Name";
    private const string IntBonusDescription = "MentalProwess.Int.Bonus.Description";

    private static void ConfigureIntBonus()
    {
      FeatureConfigurator.New(IntBonusName, Guids.IntBonus)
        .SetIsClassFeature()
        .SetDisplayName(IntBonusDisplayName)
        .SetDescription(IntBonusDescription)
        //.SetIcon()
        .SetRanks(3)
        .AddStatBonus(stat: StatType.Intelligence, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }

    private const string WisBonusName = "MentalProwess.Wis.Bonus";
    private const string WisBonusDisplayName = "MentalProwess.Wis.Bonus.Name";
    private const string WisBonusDescription = "MentalProwess.Wis.Bonus.Description";

    private static void ConfigureWisBonus()
    {
      FeatureConfigurator.New(WisBonusName, Guids.WisBonus)
        .SetIsClassFeature()
        .SetDisplayName(WisBonusDisplayName)
        .SetDescription(WisBonusDescription)
        //.SetIcon()
        .SetRanks(3)
        .AddStatBonus(stat: StatType.Wisdom, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }

    private const string ChaBonusName = "MentalProwess.Cha.Bonus";
    private const string ChaBonusDisplayName = "MentalProwess.Cha.Bonus.Name";
    private const string ChaBonusDescription = "MentalProwess.Cha.Bonus.Description";

    private static void ConfigureChaBonus()
    {
      FeatureConfigurator.New(ChaBonusName, Guids.ChaBonus)
        .SetIsClassFeature()
        .SetDisplayName(ChaBonusDisplayName)
        .SetDescription(ChaBonusDescription)
        //.SetIcon()
        .SetRanks(3)
        .AddStatBonus(stat: StatType.Charisma, value: 2, descriptor: ModifierDescriptor.Enhancement)
        .Configure();
    }
    #endregion
  }
}
