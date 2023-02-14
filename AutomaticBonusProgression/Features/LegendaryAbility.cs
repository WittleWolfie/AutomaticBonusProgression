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
    private const string LegendaryStrengthDisplayName = "LegendaryAbility.Strength.Name";
    private const string LegendaryStrengthDescription = "LegendaryAbility.Strength.Description";

    private static BlueprintFeature ConfigureStrength()
    {
      Logger.Log($"Configuring Legendary Strength");

      return FeatureConfigurator.New(LegendaryStrengthName, Guids.LegendaryStrength)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryStrengthDisplayName)
        .SetDescription(LegendaryStrengthDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddStatBonus(stat: StatType.Strength, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
    }

    private const string LegendaryDexterityName = "LegendaryAbility.Dexterity";
    private const string LegendaryDexterityDisplayName = "LegendaryAbility.Dexterity.Name";
    private const string LegendaryDexterityDescription = "LegendaryAbility.Dexterity.Description";

    private static BlueprintFeature ConfigureDexterity()
    {
      Logger.Log($"Configuring Legendary Dexterity");

      return FeatureConfigurator.New(LegendaryDexterityName, Guids.LegendaryDexterity)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryDexterityDisplayName)
        .SetDescription(LegendaryDexterityDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddStatBonus(stat: StatType.Dexterity, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
    }

    private const string LegendaryConstitutionName = "LegendaryAbility.Constitution";
    private const string LegendaryConstitutionDisplayName = "LegendaryAbility.Constitution.Name";
    private const string LegendaryConstitutionDescription = "LegendaryAbility.Constitution.Description";

    private static BlueprintFeature ConfigureConstitution()
    {
      Logger.Log($"Configuring Legendary Constitution");

      return FeatureConfigurator.New(LegendaryConstitutionName, Guids.LegendaryConstitution)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryConstitutionDisplayName)
        .SetDescription(LegendaryConstitutionDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddStatBonus(stat: StatType.Constitution, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
    }

    private const string LegendaryIntelligenceName = "LegendaryAbility.Intelligence";
    private const string LegendaryIntelligenceDisplayName = "LegendaryAbility.Intelligence.Name";
    private const string LegendaryIntelligenceDescription = "LegendaryAbility.Intelligence.Description";

    private static BlueprintFeature ConfigureIntelligence()
    {
      Logger.Log($"Configuring Legendary Intelligence");

      return FeatureConfigurator.New(LegendaryIntelligenceName, Guids.LegendaryIntelligence)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryIntelligenceDisplayName)
        .SetDescription(LegendaryIntelligenceDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddStatBonus(stat: StatType.Intelligence, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
    }

    private const string LegendaryWisdomName = "LegendaryAbility.Wisdom";
    private const string LegendaryWisdomDisplayName = "LegendaryAbility.Wisdom.Name";
    private const string LegendaryWisdomDescription = "LegendaryAbility.Wisdom.Description";

    private static BlueprintFeature ConfigureWisdom()
    {
      Logger.Log($"Configuring Legendary Wisdom");

      return FeatureConfigurator.New(LegendaryWisdomName, Guids.LegendaryWisdom)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryWisdomDisplayName)
        .SetDescription(LegendaryWisdomDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddStatBonus(stat: StatType.Wisdom, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
    }

    private const string LegendaryCharismaName = "LegendaryAbility.Charisma";
    private const string LegendaryCharismaDisplayName = "LegendaryAbility.Charisma.Name";
    private const string LegendaryCharismaDescription = "LegendaryAbility.Charisma.Description";

    private static BlueprintFeature ConfigureCharisma()
    {
      Logger.Log($"Configuring Legendary Charisma");

      return FeatureConfigurator.New(LegendaryCharismaName, Guids.LegendaryCharisma)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryCharismaDisplayName)
        .SetDescription(LegendaryCharismaDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddStatBonus(stat: StatType.Charisma, value: 1, descriptor: ModifierDescriptor.Inherent)
        .Configure();
    }
  }
}
