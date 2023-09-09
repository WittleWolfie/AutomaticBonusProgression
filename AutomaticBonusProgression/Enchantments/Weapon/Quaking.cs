using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Utility;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;
using static Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Quaking
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Quaking));

    private const string EnchantName = "LW.Quaking.Enchant";
    private const string EffectName = "LW.Quaking";
    private const string BuffName = "LW.Quaking.Buff";
    private const string OffHandEffectName = "LW.Quaking.OffHand";
    private const string OffHandBuffName = "LW.Quaking.OffHand.Buff";

    private const string Trip = "LW.Quaking.Trip";

    private const string TripAdjacent = "LW.Quaking.Trip.Adjacent";
    private const string TripAdjacentDisplayName = "LW.Quaking.Trip.Adjacent.Name";

    private const string TripCone = "LW.Quaking.Trip.Cone";
    private const string TripConeDisplayName = "LW.Quaking.Trip.Cone.Name";

    private const string TripLine = "LW.Quaking.Trip.Line";
    private const string TripLineDisplayName = "LW.Quaking.Trip.Line.Name";

    private const string DisplayName = "LW.Quaking.Name";
    private const string Description = "LW.Quaking.Description";
    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Quaking");

      var icon = FeatureRefs.ArmyAdditionalActionAfterKill.Reference.Get().Icon;

      var tripAdjacent = AbilityConfigurator.New(TripAdjacent, Guids.QuakingTripAdjacent)
        .SetDisplayName(TripAdjacentDisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .SetType(AbilityType.Extraordinary)
        .SetRange(AbilityRange.Personal)
        .SetActionType(CommandType.Standard)
        .SetAnimation(CastAnimationStyle.Omni)
        .SetParent(Guids.QuakingTrip)
        .AddAbilityTargetsAround(radius: 5.Feet(), targetType: TargetType.Enemy)
        .AddAbilityEffectRunAction(
          ActionsBuilder.New().CombatManeuver(onSuccess: ActionsBuilder.New(), type: CombatManeuver.Trip))
        .Configure();

      var tripCone = AbilityConfigurator.New(TripCone, Guids.QuakingTripCone)
        .SetDisplayName(TripConeDisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .SetType(AbilityType.Extraordinary)
        .SetRange(AbilityRange.Projectile)
        .SetActionType(CommandType.Standard)
        .SetAnimation(CastAnimationStyle.Directional)
        .SetParent(Guids.QuakingTrip)
        .AllowTargeting(point: true, enemies: true)
        .AddAbilityDeliverProjectile(
          type: AbilityProjectileType.Cone,
          length: 10.Feet(),
          projectiles: new() { ProjectileRefs.AirCone15Feet00.ToString() })
        .AddAbilityEffectRunAction(
          ActionsBuilder.New()
            .Conditional(
              ConditionsBuilder.New().IsEnemy(),
              ifTrue: ActionsBuilder.New()
                .CombatManeuver(onSuccess: ActionsBuilder.New(), type: CombatManeuver.Trip)))
        .Configure();

      var tripLine = AbilityConfigurator.New(TripLine, Guids.QuakingTripLine)
        .SetDisplayName(TripLineDisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .SetType(AbilityType.Extraordinary)
        .SetRange(AbilityRange.Projectile)
        .SetActionType(CommandType.Standard)
        .SetAnimation(CastAnimationStyle.Directional)
        .SetParent(Guids.QuakingTrip)
        .AllowTargeting(point: true, enemies: true)
        .AddAbilityDeliverProjectile(
          type: AbilityProjectileType.Line,
          length: 20.Feet(),
          projectiles: new() { ProjectileRefs.Kinetic_AirBlastLine00.ToString() })
        .AddAbilityEffectRunAction(
          ActionsBuilder.New()
            .Conditional(
              ConditionsBuilder.New().IsEnemy(),
              ifTrue: ActionsBuilder.New()
                .CombatManeuver(onSuccess: ActionsBuilder.New(), type: CombatManeuver.Trip)))
        .Configure();

      var tripParent = AbilityConfigurator.New(Trip, Guids.QuakingTrip)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .SetRange(AbilityRange.Personal)
        .SetAnimation(CastAnimationStyle.Omni)
        .SetActionType(CommandType.Standard)
        .AddAbilityVariants(new() { tripAdjacent, tripCone, tripLine })
        .Configure();

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.QuakingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddUnitFactEquipment(tripParent)
        .Configure();

      var enchantInfo =
        new WeaponEnchantInfo(
          DisplayName,
          Description,
          icon,
          EnhancementCost,
          allowedRanges: new() { WeaponRangeType.Melee },
          allowedForms: new() { PhysicalDamageForm.Bludgeoning });
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.QuakingEffect, enchant),
        parentBuff: new(BuffName, Guids.QuakingBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.QuakingOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.QuakingOffHandBuff));
    }
  }
}
