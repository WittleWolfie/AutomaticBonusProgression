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
    // Armor In Game
    // - ArcaneArmorBalanced [DONE]
    // - Shadow [DONE]
    // - Fortification [DONE] 
    // - Spell Resistance [DONE] [Change: 13 / 16 / 19 / 22 instead of 13 / 15 / 17 / 19]
    // - Invulnerability [DONE] [Change: Made it 10/magic instead of 5/magic]
    // - Energy Resistance [DONE] [Note: Need to figure out how to interact w/ Trickster Knowledge: Arcana]
    //
    // Maybe Add
    // - Bolstering [Done]
    // - Champion [Done]
    // - Dastard [Done]
    // - Deathless [Done]
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
    private const string LegendaryArmorAbilityDescription = "LegendaryArmor.Ability.Description";

    private const string LegendaryShieldName = "LegendaryShield";
    private const string LegendaryShieldDisplayName = "LegendaryShield.Name";
    private const string LegendaryShieldDescription = "LegendaryShield.Description";
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

              Guids.ChampionAbility,

              Guids.DastardAbility,

              Guids.DeathlessAbility,

              Guids.AcidResist10Ability,
              Guids.ColdResist10Ability,
              Guids.ElectricityResist10Ability,
              Guids.FireResist10Ability,
              Guids.SonicResist10Ability,

              Guids.AcidResist20Ability,
              Guids.ColdResist20Ability,
              Guids.ElectricityResist20Ability,
              Guids.FireResist20Ability,
              Guids.SonicResist20Ability,

              Guids.AcidResist30Ability,
              Guids.ColdResist30Ability,
              Guids.ElectricityResist30Ability,
              Guids.FireResist30Ability,
              Guids.SonicResist30Ability,

              Guids.FortificationAbility,
              Guids.ImprovedFortificationAbility,
              Guids.GreaterFortificationAbility,

              Guids.InvulnerabilityAbility,

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
              Guids.BolsteringShieldAbility,

              Guids.AcidResist10ShieldAbility,
              Guids.ColdResist10ShieldAbility,
              Guids.ElectricityResist10ShieldAbility,
              Guids.FireResist10ShieldAbility,
              Guids.SonicResist10ShieldAbility,

              Guids.AcidResist20ShieldAbility,
              Guids.ColdResist20ShieldAbility,
              Guids.ElectricityResist20ShieldAbility,
              Guids.FireResist20ShieldAbility,
              Guids.SonicResist20ShieldAbility,

              Guids.AcidResist30ShieldAbility,
              Guids.ColdResist30ShieldAbility,
              Guids.ElectricityResist30ShieldAbility,
              Guids.FireResist30ShieldAbility,
              Guids.SonicResist30ShieldAbility,

              Guids.FortificationShieldAbility,
              Guids.ImprovedFortificationShieldAbility,
              Guids.GreaterFortificationShieldAbility,

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
          Bolstering.Configure(),
          Champion.Configure(),
          Dastard.Configure(),
          Deathless.Configure(),
          EnergyResistance.Configure(),
          EnergyResistance.ConfigureImproved(),
          EnergyResistance.ConfigureGreater(),
          Fortification.Configure(),
          Fortification.ConfigureImproved(),
          Fortification.ConfigureGreater(),
          Invulnerability.Configure(),
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
