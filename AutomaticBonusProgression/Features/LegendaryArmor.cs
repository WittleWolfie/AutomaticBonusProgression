using AutomaticBonusProgression.Enchantments;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryArmor));

    // Armor In Game
    // - ArcaneArmorBalanced [DONE] [Untested]
    // - Shadow [DONE] [Untested]
    // - Fortification [DONE] [Untested]
    // - Spell Resistance
    // - Invulnerability
    // - Energy Resistance
    //
    // Maybe Add
    // - Bolstering
    // - Champion
    // - Dastard
    // - Deathless
    // - Defiant
    // - Expeditious
    // - Creeping
    // - Rallying?
    // - Brawling
    // - Putrid?
    // - Ghost
    // - Martyring
    // - Righteous
    // - Unbound?
    // - Unrighteous?
    // - Vigilant?
    // - Determination
    // - Etherealness
    // (Shield)
    // - Bashing
    // - Blinding
    // - Wyrmsbreath
    // - Reflecting

    private const string LegendaryArmorName = "LegendaryArmor";
    private const string LegendaryArmorDisplayName = "LegendaryArmor.Name";
    private const string LegendaryArmorDescription = "LegendaryArmor.Description";

    private const string LegendaryArmorAbility = "LegendaryArmor.Ability";

    internal static BlueprintFeature ConfigureArmor()
    {
      Logger.Log("Configuring Legendary Armor");

      var ability = ActivatableAbilityConfigurator.New(LegendaryArmorAbility, Guids.LegendaryArmorAbility)
        .SetDisplayName(LegendaryArmorDisplayName)
        .SetDescription(LegendaryArmorDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants: 
            new()
            {
              Guids.BalancedArmorAbility,
              Guids.ShadowArmorAbility,
              Guids.ImprovedShadowArmorAbility,
              Guids.GreaterShadowArmorAbility,
              Guids.FortificationAbility,
              Guids.ImprovedFortificationAbility,
              Guids.GreaterFortificationAbility,
            })
        .AddActivationDisable()
        .Configure();

      return FeatureSelectionConfigurator.New(LegendaryArmorName, Guids.LegendaryArmor)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryArmorDisplayName)
        .SetDescription(LegendaryArmorDescription)
        //.SetIcon()
        .AddFacts(new() { ability })
        .AddToAllFeatures(
          BalancedArmor.Configure(),
          ShadowArmor.Configure(),
          ShadowArmor.ConfigureImproved(),
          ShadowArmor.ConfigureGreater(),
          Fortification.Configure(),
          Fortification.ConfigureImproved(),
          Fortification.ConfigureGreater())
        .Configure();
    }

    private const string LegendaryShieldName = "LegendaryShield";
    private const string LegendaryShieldDisplayName = "LegendaryShield.Name";
    private const string LegendaryShieldDescription = "LegendaryShield.Description";

    internal static BlueprintFeature ConfigureShield()
    {
      Logger.Log("Configuring Legendary Shield");

      return FeatureSelectionConfigurator.New(LegendaryShieldName, Guids.LegendaryShield)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryShieldDisplayName)
        .SetDescription(LegendaryShieldDescription)
        //.SetIcon()
        .Configure();
    }
  }
}
