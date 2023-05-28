using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryAbility
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryAbility));

    private const string LegendaryAbilityName = "LegendaryAbility";
    private const string LegendaryAbilityDisplayName = "LegendaryAbility.Name";
    private const string LegendaryAbilityDescription = "LegendaryAbility.Description";

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Legendary Ability");

      return FeatureSelectionConfigurator.New(LegendaryAbilityName, Guids.LegendaryAbility)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryAbilityDisplayName)
        .SetDescription(LegendaryAbilityDescription)
        //.SetIcon()
        .SetHideInCharacterSheetAndLevelUp()
        .AddHideFeatureInInspect()
        .AddToAllFeatures(
          ConfigureStrength(),
          ConfigureDexterity(),
          ConfigureConstitution(),
          ConfigureIntelligence(),
          ConfigureWisdom(),
          ConfigureCharisma())
        .Configure();
    }

    private const string LegendaryStrengthName = "LegendaryAbility.Strength";
    private const string LegendaryStrengthBaseName = "LegendaryAbility.Strength.Base";
    private const string LegendaryStrengthDisplayName = "LegendaryAbility.Strength.Name";
    private const string LegendaryStrengthDescription = "LegendaryAbility.Strength.Description";

    private static BlueprintFeature ConfigureStrength()
    {
      Logger.Log($"Configuring Legendary Strength");

      var effect = FeatureConfigurator.New(LegendaryStrengthName, Guids.LegendaryStrength)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .AddStatBonus(stat: StatType.Strength, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
      return FeatureConfigurator.New(LegendaryStrengthBaseName, Guids.LegendaryStrengthBase)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryStrengthDisplayName)
        .SetDescription(LegendaryStrengthDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string LegendaryDexterityName = "LegendaryAbility.Dexterity";
    private const string LegendaryDexterityBaseName = "LegendaryAbility.Dexterity.Base";
    private const string LegendaryDexterityDisplayName = "LegendaryAbility.Dexterity.Name";
    private const string LegendaryDexterityDescription = "LegendaryAbility.Dexterity.Description";

    private static BlueprintFeature ConfigureDexterity()
    {
      Logger.Log($"Configuring Legendary Dexterity");

      var effect = FeatureConfigurator.New(LegendaryDexterityName, Guids.LegendaryDexterity)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .AddStatBonus(stat: StatType.Dexterity, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
      return FeatureConfigurator.New(LegendaryDexterityBaseName, Guids.LegendaryDexterityBase)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryDexterityDisplayName)
        .SetDescription(LegendaryDexterityDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string LegendaryConstitutionName = "LegendaryAbility.Constitution";
    private const string LegendaryConstitutionBaseName = "LegendaryAbility.Constitution.Base";
    private const string LegendaryConstitutionDisplayName = "LegendaryAbility.Constitution.Name";
    private const string LegendaryConstitutionDescription = "LegendaryAbility.Constitution.Description";

    private static BlueprintFeature ConfigureConstitution()
    {
      Logger.Log($"Configuring Legendary Constitution");

      var effect = FeatureConfigurator.New(LegendaryConstitutionName, Guids.LegendaryConstitution)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .AddStatBonus(stat: StatType.Constitution, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
      return FeatureConfigurator.New(LegendaryConstitutionBaseName, Guids.LegendaryConstitutionBase)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryConstitutionDisplayName)
        .SetDescription(LegendaryConstitutionDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string LegendaryIntelligenceName = "LegendaryAbility.Intelligence";
    private const string LegendaryIntelligenceBaseName = "LegendaryAbility.Intelligence.Base";
    private const string LegendaryIntelligenceDisplayName = "LegendaryAbility.Intelligence.Name";
    private const string LegendaryIntelligenceDescription = "LegendaryAbility.Intelligence.Description";

    private static BlueprintFeature ConfigureIntelligence()
    {
      Logger.Log($"Configuring Legendary Intelligence");

      var effect = FeatureConfigurator.New(LegendaryIntelligenceName, Guids.LegendaryIntelligence)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .AddStatBonus(stat: StatType.Intelligence, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
      return FeatureConfigurator.New(LegendaryIntelligenceBaseName, Guids.LegendaryIntelligenceBase)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryIntelligenceDisplayName)
        .SetDescription(LegendaryIntelligenceDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string LegendaryWisdomName = "LegendaryAbility.Wisdom";
    private const string LegendaryWisdomBaseName = "LegendaryAbility.Wisdom.Base";
    private const string LegendaryWisdomDisplayName = "LegendaryAbility.Wisdom.Name";
    private const string LegendaryWisdomDescription = "LegendaryAbility.Wisdom.Description";

    private static BlueprintFeature ConfigureWisdom()
    {
      Logger.Log($"Configuring Legendary Wisdom");

      var effect = FeatureConfigurator.New(LegendaryWisdomName, Guids.LegendaryWisdom)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .AddStatBonus(stat: StatType.Wisdom, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
      return FeatureConfigurator.New(LegendaryWisdomBaseName, Guids.LegendaryWisdomBase)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryWisdomDisplayName)
        .SetDescription(LegendaryWisdomDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string LegendaryCharismaName = "LegendaryAbility.Charisma";
    private const string LegendaryCharismaBaseName = "LegendaryAbility.Charisma.Base";
    private const string LegendaryCharismaDisplayName = "LegendaryAbility.Charisma.Name";
    private const string LegendaryCharismaDescription = "LegendaryAbility.Charisma.Description";

    private static BlueprintFeature ConfigureCharisma()
    {
      Logger.Log($"Configuring Legendary Charisma");

      var effect = FeatureConfigurator.New(LegendaryCharismaName, Guids.LegendaryCharisma)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .AddStatBonus(stat: StatType.Charisma, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
      return FeatureConfigurator.New(LegendaryCharismaBaseName, Guids.LegendaryCharismaBase)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryCharismaDisplayName)
        .SetDescription(LegendaryCharismaDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }
  }
}
