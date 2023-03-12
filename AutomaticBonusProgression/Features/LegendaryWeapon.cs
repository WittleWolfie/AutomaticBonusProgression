using AutomaticBonusProgression.Enchantments;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryWeapon
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryWeapon));

    private const string LegendaryWeaponName = "LegendaryWeapon";
    private const string LegendaryWeaponDisplayName = "LegendaryWeapon.Name";
    private const string LegendaryWeaponDescription = "LegendaryWeapon.Description";
    private const string LegendaryWeaponAbility = "LegendaryWeapon.Ability";
    internal const string LegendaryWeaponAbilityDescription = "LegendaryWeapon.Ability.Description";

    private const string LegendaryOffHandName = "LegendaryOffHand";
    private const string LegendaryOffHandDisplayName = "LegendaryOffHand.Name";
    internal const string LegendaryOffHandDescription = "LegendaryOffHand.Description";
    private const string LegendaryOffHandAbility = "LegendaryOffHand.Ability";

    internal static BlueprintFeature Configure()
    {
      Logger.Log("Configuring Legendary Weapon");

      // In Game:
      // - Agile
      // - Bane [Done]
      // - Corrosive / Flaming / Frost / Shock / Thundering (Burst)
      // - Cruel
      // - Furious
      // - Ghost Touch
      // - Heartseeker
      // - Keen
      // - Vicious
      // - Anarchic / Axiomatic
      // - Disruption
      // - Furyborn [TODO: Fix base game implementation since it doesn't work w/ changes]
      // - Holy / Unholy
      // - Nullifying
      // - Speed
      // - Brilliant Energy
      // - Vorpal
      //
      // New:
      // - Bewildering
      // - Brawling
      // - Countering
      // - Courageous
      // - Cunning
      // - Dazzling Radiance
      // - Debilitating
      // - Distracting
      // - Dueling
      // - Fortuitous
      // - Grounding
      // - Growing
      // - Ki Focus (This might be too complicated)
      // - Leveraging
      // - Limning
      // - Menacing
      // - Ominous
      // - Quenching
      // - Thawing
      // - Valiant
      // - Sneaky
      // - Culling
      // - Defiant
      // - Impact
      // - Invigorating
      // - Legbreaker
      // - Lifesurge
      // - Phase Locking
      // - Quaking
      // - Wounding
      // - Gory

      var ability = ActivatableAbilityConfigurator.New(LegendaryWeaponAbility, Guids.LegendaryWeaponAbility)
        .SetDisplayName(LegendaryWeaponDisplayName)
        .SetDescription(LegendaryWeaponAbilityDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants: 
            new()
            {
            })
        .AddActivationDisable()
        .Configure();

      var offHandAbility = ActivatableAbilityConfigurator.New(LegendaryOffHandAbility, Guids.LegendaryOffHandAbility)
        .SetDisplayName(LegendaryOffHandDisplayName)
        .SetDescription(LegendaryOffHandDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants: 
            new()
            {
            })
        .AddActivationDisable()
        .Configure();

      return FeatureSelectionConfigurator.New(LegendaryWeaponName, Guids.LegendaryWeapon)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryWeaponDisplayName)
        .SetDescription(LegendaryWeaponDescription)
        //.SetIcon()
        .AddFacts(new() { ability, offHandAbility })
        .AddToAllFeatures(
          Bane.Configure())
        .Configure();
    }
  }
}
