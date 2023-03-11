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


    // TODO: Look at new enchantments & items!
    // TODO: I should actually add the restriction on armor types cause that might be a benefit for heavy armor? Dunno.

    // Armor In Game
    // - ArcaneArmorBalanced [DONE]
    // - Shadow [DONE]
    // - Fortification [DONE] 
    // - Spell Resistance [DONE] [Change: 13 / 16 / 19 / 22 instead of 13 / 15 / 17 / 19]
    // - Invulnerability [DONE] [Change: Made it 10/magic instead of 5/magic]
    // - Energy Resistance [DONE] [Note: Need to figure out how to interact w/ Trickster Knowledge: Arcana] [Change: Bonus Cost]
    //
    // Maybe Add
    // - Bolstering [Done]
    // - Champion [Done]
    // - Dastard [Done]
    // - Deathless [Done]
    // - Defiant [Done]
    // - Rallying [Done]
    // - Brawling [Done]
    // - Ghost [Done]
    // - Righteous - Unbound - Unrighteous - Vigilant [Done]
    // - Determination [Done]
    // - Martyring [Done]
    // - Expeditious [Done]
    // - Creeping [Done]
    // (Shield)
    // - Bashing [Done]
    // - Blinding [Done]
    // - Wyrmsbreath I need to do something here to make this not suck and not be too strong...
    // - Reflecting

    private const string LegendaryArmorName = "LegendaryArmor";
    private const string LegendaryArmorDisplayName = "LegendaryArmor.Name";
    private const string LegendaryArmorDescription = "LegendaryArmor.Description";
    private const string LegendaryArmorAbility = "LegendaryArmor.Ability";
    internal const string LegendaryArmorAbilityDescription = "LegendaryArmor.Ability.Description";

    private const string LegendaryShieldName = "LegendaryShield";
    private const string LegendaryShieldDisplayName = "LegendaryShield.Name";
    internal const string LegendaryShieldDescription = "LegendaryShield.Description";
    private const string LegendaryShieldAbility = "LegendaryShield.Ability";

    internal static BlueprintFeature ConfigureArmor()
    {
      Logger.Log("Configuring Legendary Armor");

      var ability = ActivatableAbilityConfigurator.New(LegendaryArmorAbility, Guids.LegendaryArmorAbility)
        .SetDisplayName(LegendaryArmorDisplayName)
        .SetDescription(LegendaryArmorAbilityDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants: 
            new()
            {
              Guids.BalancedArmorAbility,

              Guids.BolsteringAbility,

              Guids.BrawlingAbility,

              Guids.ChampionAbility,

              Guids.CreepingAbility,

              Guids.DastardAbility,

              Guids.DeathlessAbility,

              Guids.DeterminationAbility,

              Guids.ExpeditiousAbility,

              Guids.FortificationAbility,
              Guids.ImprovedFortificationAbility,
              Guids.GreaterFortificationAbility,

              Guids.GhostArmorAbility,

              Guids.InvulnerabilityAbility,

              Guids.MartyringAbility,

              Guids.RallyingAbility,

              Guids.RighteousAbility,

              Guids.ShadowArmorAbility,
              Guids.ImprovedShadowArmorAbility,
              Guids.GreaterShadowArmorAbility,

              Guids.SpellResistance13Ability,
              Guids.SpellResistance16Ability,
              Guids.SpellResistance19Ability,
              Guids.SpellResistance22Ability,
            })
        .AddActivationDisable()
        .Configure();

      var shieldAbility = ActivatableAbilityConfigurator.New(LegendaryShieldAbility, Guids.LegendaryShieldAbility)
        .SetDisplayName(LegendaryShieldDisplayName)
        .SetDescription(LegendaryShieldDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants: 
            new()
            {
              Guids.BashingAbility,

              Guids.BlindingAbility,

              Guids.BolsteringShieldAbility,

              Guids.FortificationShieldAbility,
              Guids.ImprovedFortificationShieldAbility,
              Guids.GreaterFortificationShieldAbility,

              Guids.GhostArmorShieldAbility,

              Guids.RallyingShieldAbility,

              Guids.SpellResistance13ShieldAbility,
              Guids.SpellResistance16ShieldAbility,
              Guids.SpellResistance19ShieldAbility,
              Guids.SpellResistance22ShieldAbility,
            })
        .AddActivationDisable()
        .Configure();

      return FeatureSelectionConfigurator.New(LegendaryArmorName, Guids.LegendaryArmor)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryArmorDisplayName)
        .SetDescription(LegendaryArmorDescription)
        //.SetIcon()
        .AddFacts(new() { ability, shieldAbility })
        .AddToAllFeatures(
          BalancedArmor.Configure(),
          Bashing.Configure(),
          Blinding.Configure(),
          Bolstering.Configure(),
          Brawling.Configure(),
          Champion.Configure(),
          Creeping.Configure(),
          Dastard.Configure(),
          Deathless.Configure(),
          Defiant.Configure(),
          Determination.Configure(),
          EnergyResistance.Configure(),
          EnergyResistance.ConfigureImproved(),
          EnergyResistance.ConfigureGreater(),
          Expeditious.Configure(),
          Fortification.Configure(),
          Fortification.ConfigureImproved(),
          Fortification.ConfigureGreater(),
          GhostArmor.Configure(),
          Invulnerability.Configure(),
          Martyring.Configure(),
          Rallying.Configure(),
          Righteous.Configure(),
          ShadowArmor.Configure(),
          ShadowArmor.ConfigureImproved(),
          ShadowArmor.ConfigureGreater(),
          SpellResistance.Configure13(),
          SpellResistance.Configure16(),
          SpellResistance.Configure19(),
          SpellResistance.Configure22())
        .Configure();
    }
  }
}
